using System;
using System.Collections.Generic;
using System.Text;

using gpx;

namespace GPXAnalyzer
{
    static class GPXUtils
    {
        // http://en.wikipedia.org/wiki/Earth_radius
        // http://en.wikipedia.org/wiki/WGS84
        static double equatorialRadius = 6378137.000D;      //Semi-major axis a in WGS84
        static double polarRadius = 6356752.314245D;     // Semi-minor axis b

        
        static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        static double RadiansToDegrees(double radians)
        {
            return radians * 180.0 / Math.PI;
        }

        static double RadiansToNauticalMiles(double radians)
        {
            // There are 60 nautical miles for each degree
            return radians * 60 * 180 / Math.PI;
        }

        static double RadiansToMeters(double radians)
        {
            // there are 1852 meters in a nautical mile
            return 1852 * RadiansToNauticalMiles(radians);
        }


        private static double square(double x)
        {
            return x * x;
        }

        public static Double RadiusAtGeodeticLatitude(Double latitude)
        {
            double numerator, denominator, result;
            double cos_theta = Math.Cos(latitude);
            double sin_theta = Math.Sin(latitude);

            numerator =
                square(square(equatorialRadius) * cos_theta) +
                square(square(polarRadius) * sin_theta);

            denominator =
                square(equatorialRadius * cos_theta) +
                square(polarRadius * sin_theta);

            result = Math.Sqrt(numerator / denominator);

            return result;

        }

        public static string LatLonToString(double lat, double lon)
        {
            string latDir = (lat >= 0) ? "N" : "S";
            string lonDir = (lon >= 0) ? "E" : "W";
            double tLat = Math.Abs(lat);
            double tLon = Math.Abs(lon);
            double fLat = (tLat - Math.Truncate(tLat)) * 60;
            double fLon = (tLon - Math.Truncate(tLon)) * 60;
            string sLat = string.Format("{0}{1} {2}", latDir, Math.Truncate(tLat), fLat.ToString("#.000"));
            string sLon = string.Format("{0}{1} {2}", lonDir, Math.Truncate(tLon), fLon.ToString("#.000"));

            return string.Format("{0} {1}", sLat, sLon);
        }

        // flat earth approximation
        public static void GetCourseAndDistance(wptType pt1, wptType pt2, ref double course, ref double dist)
        {
            // convert latitude and longitude to radians
            double lat1 = DegreesToRadians((double)pt1.lat);
            double lon1 = DegreesToRadians((double)pt1.lon);
            double lat2 = DegreesToRadians((double)pt2.lat);
            double lon2 = DegreesToRadians((double)pt2.lon);

            // compute latitude and longitude differences
            double dlat = lat2 - lat1;
            double dlon = lon2 - lon1;

            double distanceNorth = dlat;
            double distanceEast = dlon * Math.Cos(lat1);

            // compute the distance in radians
            dist = Math.Sqrt(distanceNorth * distanceNorth + distanceEast * distanceEast);

            // and convert the radians to meters
            dist = RadiansToMeters(dist);

            // add the elevation difference to the calculation
            double dele = (double)pt2.ele - (double)pt1.ele;
            dist = Math.Sqrt(dist * dist + dele * dele);

            // compute the course
            course = Math.Atan2(distanceEast, distanceNorth) % (2 * Math.PI);
            course = RadiansToDegrees(course);
            if (course < 0)
                course += 360;
        }

        /// <summary>
        /// Great Circle distance between two points. lat1 and lat2 are latutudes
        /// lon1 lon2 are longitudes, ele1 ele2 are elevations. 
        /// 
        /// ALL ANGLES ARE RADIANS
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <param name="ele1"></param>
        /// <param name="ele2"></param>
        /// <returns></returns>

        public static double Haversine(double lat1, double lon1, double lat2, double lon2, double ele1, double ele2)
        {
            double dLongitude = lon2 - lon1;
            double dLatitude = lat2 - lat1;
            double dist = Double.MinValue;

            // Intermediate result a.

            double a = square(Math.Sin(dLatitude / 2.0)) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       square(Math.Sin(dLongitude / 2.0));

            // Intermediate result c (great circle distance in Radians).

            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

            // Distance.

            // const Double kEarthRadiusMiles = 3956.0;
            //const Double kEarthRadiusKms = 6378137;
            // use mean lat here for simplicity -- je

            Double kEarthRadiusKms = RadiusAtGeodeticLatitude((lat1 - lat2) / 2.0);
            dist = kEarthRadiusKms * c;

            // add the elevation difference to the calculation -- from flat earth
            // pythagoras -- should be geodetic
            double dele = ele2 - ele1;
            dist = Math.Sqrt(dist * dist + dele * dele);

            return dist;

        }

        /// <summary>
        /// Distance between two WayPoints from GPX
        /// </summary>
        /// <param name="wpt1"></param>
        /// <param name="wpt2"></param>
        /// <returns></returns>
        public static double Haversine(wptType wpt1, wptType wpt2)
        {
            // convert latitude and longitude to radians
            double lat1 = DegreesToRadians((double)wpt1.lat);
            double lon1 = DegreesToRadians((double)wpt1.lon);
            double lat2 = DegreesToRadians((double)wpt2.lat);
            double lon2 = DegreesToRadians((double)wpt2.lon);

            return Haversine( lat1, lon1, lat2, lon2, (double)wpt2.ele, (double)wpt1.ele);
            
        }


        static void OutputTrack(trkType track)
        {
            Console.WriteLine("Track ’{0}’", track.name);
            if (track.trkseg.Length > 0)
            {
                Console.WriteLine("{0} segments", track.trkseg.Length);
                foreach (trksegType seg in track.trkseg)
                {
                    Console.WriteLine("{0} Points", seg.trkpt.Length);
                    wptType wptPrev = seg.trkpt[0];
                    double totalDist = 0;
                    double totalHDist = 0;
                    double elevationGain = 0;
                    foreach (wptType wpt in seg.trkpt)
                    {
                        double course = 0;
                        double dist = 0;
                        double hd = Haversine(wptPrev, wpt);
                        totalHDist += hd;
                        GetCourseAndDistance(wptPrev, wpt, ref course, ref dist);
                        totalDist += dist;
                        if (wpt.ele > wptPrev.ele)
                        {
                            elevationGain += (double)(wpt.ele - wptPrev.ele);
                        }
                        Console.WriteLine("{0} {1} {2} {3} {4} {5}", wpt.time, wpt.lat, wpt.lon, wpt.ele, course, dist);
                        wptPrev = wpt;
                    }
                    Console.WriteLine("Total distance = {0} kilometers (HD {1}). Elevation gain = {2} meters.",
                    totalDist / 1000, totalHDist / 1000, elevationGain);
                }
            }
        }
    }

}
