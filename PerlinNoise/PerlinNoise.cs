using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PerlinNoise
{
    public class PerlinNoise
    {
        int Seed { get; set; }

        int Width = 0;
        int Height = 0;

        int X = 0;
        int Y = 0;

        double GenFrequency = 0;
        double GenAmplitude = 0;

        int GenOctaves = 0;
        int GridSize = 0;

        public List<double> Noise = new List<double>();

        public PerlinNoise(int seed, int Width, int Height, double GenFrequency, double GenAmplitude, int GenOctaves, int GridSize)
        {
            Seed = seed;
            this.Width = Width;
            this.Height = Height;
            this.GenFrequency = GenFrequency;
            this.GenAmplitude = GenAmplitude;
            this.GenOctaves = GenOctaves;
            this.GridSize = GridSize;

            CalculateNoiseMap();
        }

        public PerlinNoise(int seed, int X, int Y, int Width, int Height, double GenFrequency, double GenAmplitude, int GenOctaves, int GridSize)
        {
            Seed = seed;
            this.X = X;
            this.Y = Y;
            this.Width = Width + X;
            this.Height = Height + Y;
            this.GenFrequency = GenFrequency;
            this.GenAmplitude = GenAmplitude;
            this.GenOctaves = GenOctaves;
            this.GridSize = GridSize;

            CalculateNoiseMap();
        }
        private Vector2 RandomGradient(int iX, int iY)
        {
            iX += 10;
            iY += 10;

            Random random = new Random(Seed*iX*iY);

            var rnd = random.NextDouble() * 360;

            return new Vector2((float)Math.Sin(rnd), (float)Math.Cos(rnd));
        }
        private double DotGridGradient(int iX, int iY, double X, double Y)
        {
            var gradient = RandomGradient(iX, iY);

            var dX = X - iX;
            var dY = Y - iY;

            return dX * gradient[0] + dY * gradient[1];
        }
        private double Interpolate(double A0, double A1, double W)
        {
            return (A1 - A0) * (3.0 - W * 2.0) * W * W + A0;
        }
        private double Perlin(double X, double Y)
        {
            var X0 = (int)X;
            var Y0 = (int)Y;
            var X1 = X0+1;
            var Y1 = Y0+1;

            var sX = X - X0;
            var sY = Y - Y0;

            var n0 = DotGridGradient(X0, Y0, X, Y);
            var n1 = DotGridGradient(X1, Y0, X, Y);
            var iX0 = Interpolate(n0,n1,sX);

            n0 = DotGridGradient(X0, Y1, X, Y);
            n1 = DotGridGradient(X1, Y1, X, Y);
            var iX1 = Interpolate(n0, n1, sX);

            var value = Interpolate(iX0,iX1,sY);

            return value;
        }
        private void CalculateNoiseMap()
        {
            for (int FY = Y; FY < Height; FY++)
            {
                for (int FX = X; FX < Width; FX++)
                {
                    var index = FY*Width + FX;
                    double val = 0;
                    var freq = GenFrequency;
                    var amp = GenAmplitude;
                    for(int I = 0; I < GenOctaves; I++)
                    {
                        var perlin = Perlin(FX * freq / GridSize, FY * freq / GridSize);
                        val += perlin * amp;
                        freq *= 2;
                        amp /= 2;
                    }
                    val *= 1.2;
                    if(val > 1) val = 1;
                    if(val < -1) val = -1;
                    Noise.Add(Math.Round(val,1));
                }
            }
        } 
    }
}
