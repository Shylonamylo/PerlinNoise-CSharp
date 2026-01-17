using Pastel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerlinNoise
{
    public class Game
    {
        public bool Press = false;
        public int X = 0;
        public int Y = 0;
        public List<double> Noise = new();
        public bool Stop = false;

        public Game() 
        {

            Thread ControlsThread = new(Controls);
            ControlsThread.Start();
            Thread GameThread = new(GameFunc);
            GameThread.Start();
            Console.WriteLine("threads alive");
        }
        private void GameFunc()
        {
            while (true&&!Stop)
            {
                if (Press)
                {
                    GenerateNoise();
                    Draw();
                    Press = false;
                }
            }
        }
        private double Interpolate(double A0, double A1, double W)
        {
            return (A1 - A0) * (3.0 - W * 2.0) * W * W + A0;
        }
        private void GenerateNoise()
        {
            var PerlinNoise = new PerlinNoise(23, X, Y, 32, 32, 1, 2, 2, 10);
            Noise = PerlinNoise.Noise;
        }
        private void Draw()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < (int)Math.Sqrt(Noise.Count); i++)
            {
                for (int j = 0; j < (int)Math.Sqrt(Noise.Count); j++)
                {
                    int Comp = (int)Interpolate(0, 255, (Noise[(i * (int)Math.Sqrt(Noise.Count)) + j] + 1) * 0.5);
                    string Col = $"{Comp:X2}{Comp:X2}{Comp:X2}";
                    Console.Write("■".Pastel(Col) + " ");
                }
                Console.WriteLine();
            }
        }
        private void Controls()
        {
            while (true && !Stop)
            {
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
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    Stop = true;
                }
            }
        }
    }
}
