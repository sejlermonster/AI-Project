using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media.TextFormatting;
using Ev3_Localization.Models;

namespace Ev3_Localization
{
    public class ParticleFilter
    {
        private static readonly Random random = new Random();
        private readonly double[,] _worldMatrix;
        private List<Landmark> _landMarks;

        public IList<Particle> Particles { get; set; }

        public ParticleFilter(List<Landmark> landMarks, double[,] worldMatrix)
        {
            if (landMarks == null)
                throw new ArgumentNullException(nameof(landMarks));
            if (worldMatrix == null)
                throw new ArgumentNullException(nameof(worldMatrix));

            _landMarks = landMarks;
            _worldMatrix = worldMatrix;
            Particles = new List<Particle>();
        }

        public void GenerateParticles(int numberOfParticles, int sizeX, int sizeY)
        {
            for (int i = 0; i < numberOfParticles; i++)
            {
                var particle = new Particle
                {
                    Weight = 1d / Convert.ToDouble(numberOfParticles),
                    Position = new Point(random.Next(0, sizeX), random.Next(0, sizeY)),
                    //OrientationInRadians = Math.PI * random.Next(0, 360) / 180
                    OrientationInRadians = Math.PI * (random.Next(0, 4)*90) / 180
                };
                Particles.Add(particle);
            }
        }

        public void MoveParticles(double distance)
        {
            foreach (var particle in Particles)
            {
                particle.Position = new Point(particle.Position.X + Math.Cos(particle.OrientationInRadians) * distance, // % _worldMatrix.GetLength(0),
                                              particle.Position.Y + Math.Sin(particle.OrientationInRadians) * distance); // % _worldMatrix.GetLength(1)));
            }
            foreach (var particle in Particles)
            {
                particle.ApplyGaussianNoiseForPosition();
            }
        }

        public void ParticlesTurnRight(double angleInDegrees)
        {
            var angleInRadians = angleInDegrees / 180 * Math.PI;
            foreach (var particle in Particles)
            {
                particle.OrientationInRadians = (particle.OrientationInRadians - angleInRadians) % (2 * Math.PI);
            }
            foreach (var particle in Particles)
            {
                particle.ApplyGaussianNoiseForOrientation();
            }
        }

        public void ParticlesTurnLeft(double angleInDegrees)
        {
            var angleInRadians = angleInDegrees / 180 * Math.PI;
            foreach (var particle in Particles)
            {
                particle.OrientationInRadians = (particle.OrientationInRadians + angleInRadians) % (2 * Math.PI);
            }

            foreach (var particle in Particles)
            {
                particle.ApplyGaussianNoiseForPosition();
            }
        }

        public void Resampling(double measurement)
        {
            if (measurement > _worldMatrix.GetLength(0) && measurement > _worldMatrix.GetLength(1))
                return;

            SetImportanceWeight(measurement);
            var particles2 = new List<Particle>();
            var index = random.Next(0, Particles.Count);
            var beta = 0.0;
            var maxParticle = Particles.Max();
            var max = maxParticle.Weight;
            for (int i = 0; i < Particles.Count; i++)
            {
                beta += random.NextDouble() * max;
                while (beta > Particles[index].Weight)
                {
                    beta -= Particles[index].Weight;
                    index = (index + 1) % Particles.Count;
                }
                particles2.Add(Particles[index].Clone());
            }
            Particles = particles2;
            NormalizeParticleWeights();
        }

        public void SetImportanceWeight(double measurement)
        {
            var highestPossibleDistance = Math.Sqrt(Math.Pow(_worldMatrix.GetLength(0), 2) + Math.Pow(_worldMatrix.GetLength(1), 2));
            foreach (var particle in Particles)
            {
                var distance = CalculateShortestDistance(particle);
                particle.Weight = highestPossibleDistance - Math.Abs(measurement - distance);
            }
            
            var particles = Particles.OrderBy(x => x.Weight).Reverse().ToList();
            int i = particles.Count;
            //Belønning
            foreach (var particle in particles)
            {
                particle.Weight = particle.Weight * i;
                i--;
            }
            Particles = particles;
            NormalizeParticleWeights();
        }

        public void PrintParticles()
        {
            foreach (var particle in Particles)
            {
                Debug.WriteLine("Particle 1: X: " + particle.Position.X + "Y: " + particle.Position.Y + "Direction: " + (particle.OrientationInRadians * 180/Math.PI));
            }
        }

        private double CalculateShortestDistance(Particle particle)
        {
            var tempX = particle.Position.X;
            var tempY = particle.Position.Y;
            while (!(tempX > _worldMatrix.GetLength(0))
                   && !(tempX < 0.1)
                   && !(tempY > _worldMatrix.GetLength(1))
                   && !(tempY < 0.1)
                   && (!WithinLandmark(tempX, tempY, _landMarks[0])
                   && !WithinLandmark(tempX, tempY, _landMarks[1])
                   && !WithinLandmark(tempX, tempY, _landMarks[2])))
            {
                tempX = tempX + Math.Cos(particle.OrientationInRadians);
                tempY = tempY + Math.Sin(particle.OrientationInRadians);
            }
            var shortestDistance = Math.Sqrt(Math.Pow(tempX - particle.Position.X, 2) + Math.Pow(tempY - particle.Position.Y, 2));
            return shortestDistance;
        }

        private static Boolean WithinLandmark(double x, double y, Landmark landmark)
        {
            return ((x > landmark.UpperLeft.X)
                    && (x < landmark.BottomRight.X)
                    && (y < landmark.UpperRight.Y)
                    && (y > landmark.BottomRight.Y));
        }

        private void NormalizeParticleWeights()
        {
            var totalCount = 0.0;
            foreach (var particle in Particles)
            {
                totalCount += particle.Weight;
            }
            foreach (var particle in Particles)
            {
                particle.Weight = particle.Weight / totalCount;
            }
            totalCount = 0.0;
            foreach (var particle in Particles)
            {
                totalCount += particle.Weight;
            }
        }
    }
}
