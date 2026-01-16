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
            bool Press = false;
            int X = 0;
            int Y = 0;
            while (true) 
            {
                if (Press)
                {
                    Console.SetCursorPosition(0, 0);
                    var PerlinNoise = new PerlinNoise(23, X, Y, 32, 32, 1, 2, 2, 10);
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
                    Press = false;
                    Thread.Sleep(100);
                }
                if (Console.ReadKey().Key == ConsoleKey.DownArrow)
                {
                    Y++;
                    Press = true;
                }
                if (Console.ReadKey().Key == ConsoleKey.UpArrow)
                {
                    Y--;
                    Press = true;
                }
                if (Console.ReadKey().Key == ConsoleKey.RightArrow)
                {
                    X++;
                    Press = true;
                }
                if (Console.ReadKey().Key == ConsoleKey.LeftArrow)
                {
                    X--;
                    Press = true;
                }
                if(Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            Console.ReadLine();
        }
    }
}
