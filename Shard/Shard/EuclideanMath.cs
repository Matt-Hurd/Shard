using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shard
{
    class EuclideanMath
    {
        private static Random randomGenerator = new Random();

        public static double DistanceBetween(Vector2 point1, Vector2 point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }

        public static double DistanceBetween(Vector3 point1, Vector3 point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2) + Math.Pow(point2.Z - point1.Z, 2));
        }

        public static int GetSign(double value)
        {
            if (value < 0)
                return -1;
            if (value == 0)
                return 0;
            else
                return 1;
        }

        public static double GetMean(int[] values)
        {
            int total = 0;
            foreach(int num in values)
                total += num;
            return ((double)total / (double)values.Length);
        }

        public static int RandomInteger()
        {
            return RandomInteger(Int32.MinValue, Int32.MaxValue);
        }

        public static int RandomInteger(int min, int max)
        {
            return (min + (int)(randomGenerator.NextDouble() * ((max - min) + 1)));
        }

        public static double RandomDouble()
        {
            return randomGenerator.NextDouble();
        }

        public static int[] GenerateValueArray(int size, int meanLevel)
        {
            int[] values = new int[size];
            for (int i = 0; i < values.Length; i++)
                values[i] = RandomInteger(1, 5);
            if((int)Math.Round(GetMean(values)) == meanLevel)
                return values;
            else
            {
                values = null;
                return GenerateValueArray(size, meanLevel);
            }
        }
    }
}
