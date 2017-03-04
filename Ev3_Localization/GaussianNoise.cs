using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ev3_Localization
{
    public class GaussianNoise
    {
        public static double NextGaussian(double mu = 0, double sigma = 1)
        {
            var random = new Random();
            var u1 = random.NextDouble();
            var u2 = random.NextDouble();

            var rand_std_normal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                Math.Sin(2.0 * Math.PI * u2);

            var rand_normal = mu + sigma * rand_std_normal;

            return rand_normal;
        }
    }
}
