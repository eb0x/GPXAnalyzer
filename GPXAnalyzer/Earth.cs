using System;
using System.Collections.Generic;
using System.Text;


namespace GPXAnalyzer
{
    public static class Earth
    {
        // http://en.wikipedia.org/wiki/Earth_radius
        static double equatorialRadius = 6378.137D;  //'a' in the formula
        static double polarRadius = 6356.7523D;     // 'b' in the formula
     


        public static Double RadiusAtGeodeticLatitude(Double latitude)
        {
            double numerator, denominator, result;
            double cos_theta = Math.Cos(latitude);
            double sin_theta = Math.Sin(latitude);

            numerator =
                square(equatorialRadius * equatorialRadius * cos_theta) +
                square(polarRadius * polarRadius * sin_theta);

            denominator =
                square(equatorialRadius * cos_theta) +
                square(polarRadius * sin_theta);

            result = Math.Sqrt(numerator / denominator);

            return result;

        }

        private static double square(double x)
        {
            return x * x;
        }


        //To convert a value in degrees to radians, multiply it by p/180:
        // Source C# cookbook

        public static double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }

        public static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }
    }
}
