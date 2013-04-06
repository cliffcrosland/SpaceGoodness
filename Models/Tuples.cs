using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceGoodness.Models
{
    public class Tuples
    {
        public static Tuple<double, double> Add(Tuple<double, double> a, Tuple<double, double> b)
        {
            return new Tuple<double, double>(a.Item1 + b.Item1, a.Item2 + b.Item2);
        }

        public static Tuple<double, double> Minus(Tuple<double, double> a, Tuple<double, double> b)
        {
            return new Tuple<double, double>(a.Item1 - b.Item1, a.Item2 - b.Item2);
        }

        public static Tuple<double, double> Scale(Tuple<double, double> tuple, double scale)
        {
            return new Tuple<double, double>(tuple.Item1 * scale, tuple.Item2 * scale);
        }

        public static double Magnitude(Tuple<double, double> tuple)
        {
            double x = tuple.Item1;
            double y = tuple.Item2;
            return Math.Sqrt(x * x + y * y);
        }

        public static Tuple<double, double> PolarToCartesian(double r, double theta)
        {
            double x = r * Math.Cos(theta);
            double y = r * Math.Sin(theta);
            return new Tuple<double, double>(x, y);
        }

        public static Tuple<double, double> WorldWrap(Tuple<double, double> tuple, double width, double height)
        {
            return new Tuple<double, double>(((int ) tuple.Item1 + width) % width, ((int) tuple.Item2 + height) % height);
        }

        public static Tuple<double, double> Zero = new Tuple<double, double>(0.0, 0.0);
    }
}