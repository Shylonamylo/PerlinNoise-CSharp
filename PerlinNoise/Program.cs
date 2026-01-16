using Pastel;
using System.Drawing;

namespace PerlinNoise
{
    internal class Program
    {
        private static double Interpolate(double A0, double A1, double W)
        {
            return (A1 - A0) * (3.0 - W * 2.0) * W * W + A0;
        }
        static void Main(string[] args)
        {

            var PerlinNoise = new PerlinNoise(23,0,0,16,16,1,2,2,10);
            Console.WriteLine(PerlinNoise.Noise.Count);
            for (int i = 0; i < (int)Math.Sqrt(PerlinNoise.Noise.Count); i++)
            {
                for (int j = 0; j < (int)Math.Sqrt(PerlinNoise.Noise.Count); j++)
                {
                    double Component = Interpolate(0, 255, (PerlinNoise.Noise[(i * (int)Math.Sqrt(PerlinNoise.Noise.Count)) + j] + 1) * 0.5);
                    Color myColor = Color.FromArgb((int)Component, (int)Component, (int)Component);
                    string hex = myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
                    Console.Write("■".Pastel(hex) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
