using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ev3_Localization.Models;

namespace Ev3_Localization
{
    public class ParticleFilter
    {
        private double[][] _worldMatrix;
        static Random random = new Random();

        private IList<Particle> _particles;
        public IList<Particle> Particles
        {
            get { return _particles; }
            set { _particles = value; }
        }

        public ParticleFilter()
        {
            _particles = new List<Particle>();
        }

        public void GenerateParticles(int numberOfParticles, int size)
        {
            for (int i = 0; i < numberOfParticles; i++)
            {
                var particle = new Particle();
                particle.Weight = 1d / Convert.ToDouble(numberOfParticles);
                particle.Position = new Point(random.Next(0, size), random.Next(0, size));
                particle.OrientationInDegrees = random.Next(0, 360);
                _particles.Add(particle);
            }
        }
    }
}
