using System;
using System.Collections.Generic;
using System.Text;

namespace bngconv
{
    class OS_Math
    {
        static double Pi180 = Math.PI / 180.0;

        static void Main(string[] args)
        {
            osgb os = new osgb();
            string result = os.test();
            Console.WriteLine(result);
            Console.Read();
        }

        public static double Helmert_X(double X, double Y, double Z, double DX, double Y_Rot, double Z_Rot, double s)
        {
            // Computed Helmert transformed X coordinate.
            // Input: -  cartesian XYZ coords ( X, Y,Z), X translation (DX) all in meters ;  Y and Z rotations in seconds of arc (Y_Rot, Z_Rot) and scale in ppm (s).

            // Convert rotations to radians and ppm scale to a factor
            double sfactor = s * 0.000001;
            double RadY_Rot = (Y_Rot / 3600) * Pi180;
            double RadZ_Rot = (Z_Rot / 3600) * Pi180;

            // Compute transformed X coord
            double Helmert_X_result = X + (X * sfactor) - (Y * RadZ_Rot) + (Z * RadY_Rot) + DX;

            return Helmert_X_result;

        }

        public static double Helmert_Y(double X, double Y, double Z, double DY, double X_Rot, double Z_Rot, double s)
        {
            // Computed Helmert transformed Y coordinate.
            // Input: -  cartesian XYZ coords (X,Y,Z), Y translation (DY) all in meters ;  X and Z rotations in seconds of arc (X_Rot, Z_Rot) and scale in ppm (s).

            // Convert rotations to radians and ppm scale to a factor
            double sfactor = s * 0.000001;
            double RadX_Rot = (X_Rot / 3600) * Pi180;
            double RadZ_Rot = (Z_Rot / 3600) * Pi180;

            // Compute transformed Y coord
            double Helmert_Y_result = (X * RadZ_Rot) + Y + (Y * sfactor) - (Z * RadX_Rot) + DY;

            return Helmert_Y_result;

        }


        public static double Helmert_Z(double X, double Y, double Z, double DZ, double X_Rot, double Y_Rot, double s)
        {
            // Computed Helmert transformed Z coordinate.
            // Input: -  cartesian XYZ coords (X,Y,Z), Z translation (DZ) all in meters ;  X and Y rotations in seconds of arc (X_Rot, Y_Rot) and scale in ppm (s).

            // Convert rotations to radians and ppm scale to a factor
            double sfactor = s * 0.000001;
            double RadX_Rot = (X_Rot / 3600) * Pi180;
            double RadY_Rot = (Y_Rot / 3600) * Pi180;

            // Compute transformed Z coord
            double Helmert_Z_result = (-1 * X * RadY_Rot) + (Y * RadX_Rot) + Z + (Z * sfactor) + DZ;

            return Helmert_Z_result;
        }


        public static double XYZ_to_Lat(double X, double Y, double Z, double a, double b)
        {
            // Convert XYZ to Latitude (PHI) in Dec Degrees.
            // Input: -  XYZ cartesian coords (X,Y,Z) and ellipsoid axis dimensions (a & b), all in meters.

            // THIS FUNCTION REQUIRES THE "Iterate_XYZ_to_Lat" FUNCTION
            // THIS FUNCTION IS CALLED BY THE "XYZ_to_H" FUNCTION

            double RootXYSqr = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            double e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2);
            double PHI1 = Math.Atan(Z / (RootXYSqr * (1 - e2)));

            double PHI = Iterate_XYZ_to_Lat(a, e2, PHI1, Z, RootXYSqr);

            return PHI * (180 / Math.PI);
        }


        static double Iterate_XYZ_to_Lat(double a, double e2, double PHI1, double Z, double RootXYSqrt)
        {
            // Iteratively computes Latitude (PHI).
            // Input: ellipsoid semi major axis (a) in meters;  eta squared (e2);  estimated value for latitude (PHI1) in radians;  cartesian Z coordinate (Z) in meters;  RootXYMath.Sqrt computed from X & Y in meters.

            // THIS FUNCTION IS CALLED BY THE "XYZ_to_PHI" FUNCTION
            // THIS FUNCTION IS ALSO USED ON IT// S OWN IN THE  "Projection and Transformation Calculations.xls" SPREADSHEET


            double V = a / (Math.Sqrt(1 - (e2 * (Math.Pow(Math.Sin(PHI1), 2)))));
            double PHI2 = Math.Atan((Z + (e2 * V * (Math.Sin(PHI1)))) / RootXYSqrt);

            while (Math.Abs(PHI1 - PHI2) > 0.000000001)
            {
                PHI1 = PHI2;
                V = a / (Math.Sqrt(1 - (e2 * (Math.Pow(Math.Sin(PHI1), 2)))));
                PHI2 = Math.Atan((Z + (e2 * V * (Math.Sin(PHI1)))) / RootXYSqrt);
            }

            return PHI2;
        }

        public static double XYZ_to_Long(double X, double Y)
        {
            // Convert XYZ to Longitude (LAM) in Dec Degrees.
            // Input: -  X and Y cartesian coords in meters.

            return Math.Atan(Y / X) * (180 / Math.PI);
        }

        static double XYZ_to_H(double X, double Y, double Z, double a, double b)
        {
            // Convert XYZ to Ellipsoidal Height.
            // Input: -  XYZ cartesian coords (X,Y,Z) and ellipsoid axis dimensions (a & b), all in meters.

            // REQUIRES THE "XYZ_to_Lat" FUNCTION

            // Compute PHI (Dec Degrees) first
            double PHI = XYZ_to_Lat(X, Y, Z, a, b);

            // Convert PHI radians
            double RadPHI = PHI * Pi180;

            // Compute H
            double RootXYSqrt = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            double e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2);
            double V = a / (Math.Sqrt(1 - (e2 * (Math.Pow(Math.Sin(RadPHI), 2)))));
            double H = (RootXYSqrt / Math.Cos(RadPHI)) - V;

            return H;

        }

        public static double Lat_Long_H_to_X(double PHI, double LAM, double H, double a, double b)
        {
            // Convert geodetic coords lat (PHI), long (LAM) and height (H) to cartesian X coordinate.
            // Input: -  Latitude (PHI)& Longitude (LAM) both in decimal degrees;  Ellipsoidal height (H) and ellipsoid axis dimensions (a & b) all in meters.

            // Convert angle measures to radians
            double RadPHI = PHI * Pi180;
            double RadLAM = LAM * Pi180;

            // Compute eccentricity squared and nu
            double e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2);
            double V = a / (Math.Sqrt(1 - (e2 * (Math.Pow(Math.Sin(RadPHI), 2)))));

            // Compute X
            double Lat_Long_H_to_X_result = (V + H) * (Math.Cos(RadPHI)) * (Math.Cos(RadLAM));
            return Lat_Long_H_to_X_result;
        }


        public static double Lat_Long_H_to_Y(double PHI, double LAM, double H, double a, double b)
        {
            // Convert geodetic coords lat (PHI), long (LAM) and height (H) to cartesian Y coordinate.
            // Input: -  Latitude (PHI)& Longitude (LAM) both in decimal degrees;  Ellipsoidal height (H) and ellipsoid axis dimensions (a & b) all in meters.

            // Convert angle measures to radians
            double RadPHI = PHI * Pi180;
            double RadLAM = LAM * Pi180;

            // Compute eccentricity squared and nu
            double e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2);
            double V = a / (Math.Sqrt(1 - (e2 * Math.Pow((Math.Sin(RadPHI)), 2))));

            // Compute Y
            double Lat_Long_H_to_Y_Result = (V + H) * (Math.Cos(RadPHI)) * (Math.Sin(RadLAM));
            return Lat_Long_H_to_Y_Result;
        }


        public static double Lat_H_to_Z(double PHI, double H, double a, double b)
        {
            // Convert geodetic coord components latitude (PHI) and height (H) to cartesian Z coordinate.
            // Input: -  Latitude (PHI) decimal degrees;  Ellipsoidal height (H) and ellipsoid axis dimensions (a & b) all in meters.

            // Convert angle measures to radians
            double RadPHI = PHI * Pi180;

            // Compute eccentricity squared and nu
            double e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2);
            double V = a / (Math.Sqrt(1 - (e2 * (Math.Pow(Math.Sin(RadPHI), 2)))));

            // Compute X
            double Lat_H_to_Z_result = ((V * (1 - e2)) + H) * (Math.Sin(RadPHI));
            return Lat_H_to_Z_result;
        }


        public static double Lat_Long_to_East(double PHI, double LAM, double a, double b, double e0, double f0, double PHI0, double LAM0)
        {
            // Project Latitude and longitude to Transverse Mercator eastings.
            // Input: -  Latitude (PHI) and Longitude (LAM) in decimal degrees;  ellipsoid axis dimensions (a & b) in meters;  eastings of false origin (e0) in meters;  central meridian scale factor (f0);  latitude (PHI0) and longitude (LAM0) of false origin in decimal degrees.

            // Convert angle measures to radians
            double RadPHI = PHI * Pi180;
            double RadLAM = LAM * Pi180;
            double RadPHI0 = PHI0 * Pi180;
            double RadLAM0 = LAM0 * Pi180;

            double af0 = a * f0;
            double bf0 = b * f0;
            double e2 = (Math.Pow(af0, 2) - Math.Pow(bf0, 2)) / Math.Pow(af0, 2);
            double n = (af0 - bf0) / (af0 + bf0);
            double nu = af0 / (Math.Sqrt(1 - (e2 * Math.Pow((Math.Sin(RadPHI)), 2))));
            double rho = (nu * (1 - e2)) / (1 - (e2 * Math.Pow(Math.Sin(RadPHI), 2)));
            double eta2 = (nu / rho) - 1;
            double p = RadLAM - RadLAM0;

            double IV = nu * (Math.Cos(RadPHI));
            double V = (nu / 6) * (Math.Pow((Math.Cos(RadPHI)), 3) * ((nu / rho) - (Math.Pow(Math.Tan(RadPHI), 2))));
            double VI = (nu / 120) *
                Math.Pow((Math.Cos(RadPHI)), 5) *
                (5 - (18 * Math.Pow(Math.Tan(RadPHI), 2) +
                Math.Pow((Math.Tan(RadPHI)), 4) +
                (14 * eta2) -
                (58 * Math.Pow((Math.Tan(RadPHI)), 2) * eta2)));

            double Lat_Long_to_East_result = e0 + (p * IV) + (Math.Pow(p, 3) * V) + (Math.Pow(p, 5) * VI);
            return Lat_Long_to_East_result;

        }


        static double Lat_Long_to_North(double PHI, double LAM, double a, double b, double e0, double n0, double f0, double PHI0, double LAM0)
        {
            // Project Latitude and longitude to Transverse Mercator northings
            // Input: -  Latitude (PHI) and Longitude (LAM) in decimal degrees;  ellipsoid axis dimensions (a & b) in meters;  eastings (e0) and northings (n0) of false origin in meters;  central meridian scale factor (f0);  latitude (PHI0) and longitude (LAM0) of false origin in decimal degrees.

            // REQUIRES THE "Marc" FUNCTION

            // Convert angle measures to radians
            double RadPHI = PHI * Pi180;
            double RadLAM = LAM * Pi180;
            double RadPHI0 = PHI0 * Pi180;
            double RadLAM0 = LAM0 * Pi180;

            double af0 = a * f0;
            double bf0 = b * f0;
            double e2 = (Math.Pow(af0, 2) - Math.Pow(bf0, 2)) / Math.Pow(af0, 2);
            double n = (af0 - bf0) / (af0 + bf0);
            double nu = af0 / (Math.Sqrt(1 - (e2 * Math.Pow((Math.Sin(RadPHI)), 2))));
            double rho = (nu * (1 - e2)) / (1 - (e2 * Math.Pow(Math.Sin(RadPHI), 2)));
            double eta2 = (nu / rho) - 1;
            double p = RadLAM - RadLAM0;
            double M = Marc(bf0, n, RadPHI0, RadPHI);

            double I = M + n0;
            double II = (nu / 2) * (Math.Sin(RadPHI)) * (Math.Cos(RadPHI));
            double III = ((nu / 24) * (Math.Sin(RadPHI)) * Math.Pow((Math.Cos(RadPHI)), 3)) * (5 - Math.Pow((Math.Tan(RadPHI)), 2) + (9 * eta2));
            double IIIA = ((nu / 720) * (Math.Sin(RadPHI)) * Math.Pow((Math.Cos(RadPHI)), 5)) * (61 - (58 * Math.Pow((Math.Tan(RadPHI)), 2)) + Math.Pow((Math.Tan(RadPHI)), 4));

            double Lat_Long_to_North_result = I + (Math.Pow(p, 2) * II) + (Math.Pow(p, 4) * III) + (Math.Pow(p, 6) * IIIA);
            return Lat_Long_to_North_result;
        }



        static double InitialLat(double North, double n0, double afo, double PHI0, double n, double bfo)
        {
            // Compute initial value for Latitude (PHI) IN RADIANS.
            // Input: -  northing of point (North) and northing of false origin (n0) in meters;  semi major axis multiplied by central meridian scale factor (af0) in meters;  latitude of false origin (PHI0) IN RADIANS;  n (computed from a, b and f0) and  ellipsoid semi major axis multiplied by central meridian scale factor (bf0) in meters.

            // REQUIRES THE "Marc" FUNCTION
            // THIS FUNCTION IS CALLED BY THE "E_N_to_Lat", "E_N_to_Long" and "E_N_to_C" FUNCTIONS
            // THIS FUNCTION IS ALSO USED ON IT// S OWN IN THE  "Projection and Transformation Calculations.xls" SPREADSHEET

            // First PHI value (PHI1)
            double PHI1 = ((North - n0) / afo) + PHI0;

            // Calculate M
            double M = Marc(bfo, n, PHI0, PHI1);

            // Calculate new PHI value (PHI2)
            double PHI2 = ((North - n0 - M) / afo) + PHI1;

            // Iterate to get final value for InitialLat
            while (Math.Abs(North - n0 - M) > 0.00001)
            {
                PHI2 = ((North - n0 - M) / afo) + PHI1;
                M = Marc(bfo, n, PHI0, PHI2);
                PHI1 = PHI2;
            }

            return PHI2;

        }

        static double Marc(double bf0, double n, double PHI0, double PHI)
        {
            // Compute meridional arc.
            // Input: -  ellipsoid semi major axis multiplied by central meridian scale factor (bf0) in meters;  n (computed from a, b and f0);  lat of false origin (PHI0) and initial or final latitude of point (PHI) IN RADIANS.

            // THIS FUNCTION IS CALLED BY THE -  "Lat_Long_to_North" and "InitialLat" FUNCTIONS
            // THIS FUNCTION IS ALSO USED ON IT// S OWN IN THE "Projection and Transformation Calculations.xls" SPREADSHEET

            double Marc_result = bf0 * (((1 + n + ((5 / 4) * Math.Pow(n, 2)) + ((5 / 4) * Math.Pow(n, 3))) * (PHI - PHI0)) - (((3 * n) + (3 * Math.Pow(n, 2)) + ((21 / 8) * Math.Pow(n, 3))) * (Math.Sin(PHI - PHI0)) * (Math.Cos(PHI + PHI0))) + ((((15 / 8) * Math.Pow(n, 2)) + ((15 / 8) * Math.Pow(n, 3))) * (Math.Sin(2 * (PHI - PHI0))) * (Math.Cos(2 * (PHI + PHI0)))) - (((35 / 24) * Math.Pow(n, 3)) * (Math.Sin(3 * (PHI - PHI0))) * (Math.Cos(3 * (PHI + PHI0)))));
            return Marc_result;

        }


        public static double E_N_to_Lat(double East, double North, double a, double b, double e0, double n0, double f0, double PHI0, double LAM0)
        {
            // Un-project Transverse Mercator eastings and northings back to latitude.
            // Input: -  eastings (East) and northings (North) in meters;  ellipsoid axis dimensions (a & b) in meters;  eastings (e0) and northings (n0) of false origin in meters;  central meridian scale factor (f0) and  latitude (PHI0) and longitude (LAM0) of false origin in decimal degrees.

            // REQUIRES THE "Marc" AND "InitialLat" FUNCTIONS

            // Convert angle measures to radians
            double RadPHI0 = PHI0 * Pi180;
            double RadLAM0 = LAM0 * Pi180;

            // Compute af0, bf0, e squared (e2), n and Et
            double af0 = a * f0;
            double bf0 = b * f0;
            double e2 = (Math.Pow(af0, 2) - Math.Pow(bf0, 2)) / Math.Pow(af0, 2);
            double n = (af0 - bf0) / (af0 + bf0);
            double Et = East - e0;

            // Compute initial value for latitude (PHI) in radians
            double PHId = InitialLat(North, n0, af0, RadPHI0, n, bf0);

            // Compute nu, rho and eta2 using value for PHId
            double nu = af0 / (Math.Sqrt(1 - (e2 * Math.Pow((Math.Sin(PHId)), 2))));
            double rho = (nu * (1 - e2)) / (1 - (e2 * Math.Pow(Math.Sin(PHId), 2)));
            double eta2 = (nu / rho) - 1;

            // Compute Latitude
            double VII = (Math.Tan(PHId)) / (2 * rho * nu);
            double VIII = ((Math.Tan(PHId)) / (24 * rho * Math.Pow(nu, 3))) * (5 + (3 * Math.Pow((Math.Tan(PHId)), 2)) + eta2 - (9 * eta2 * Math.Pow((Math.Tan(PHId)), 2)));
            double IX = ((Math.Tan(PHId)) / (720 * rho * Math.Pow(nu, 5))) * (61 + (90 * Math.Pow((Math.Tan(PHId)), 2)) + (45 * Math.Pow((Math.Tan(PHId)), 4)));

            double E_N_to_Lat_result = (180 / Math.PI) * (PHId - (Math.Pow(Et, 2) * VII) + (Math.Pow(Et, 4) * VIII) - (Math.Pow(Et, 6) * IX));
            return E_N_to_Lat_result;
        }

        public static double E_N_to_Long(double East, double North, double a, double b, double e0, double n0, double f0, double PHI0, double LAM0)
        {
            // Un-project Transverse Mercator eastings and northings back to longitude.
            // Input: -  eastings (East) and northings (North) in meters;  ellipsoid axis dimensions (a & b) in meters;  eastings (e0) and northings (n0) of false origin in meters;  central meridian scale factor (f0) and  latitude (PHI0) and longitude (LAM0) of false origin in decimal degrees.

            // REQUIRES THE "Marc" AND "InitialLat" FUNCTIONS

            // Convert angle measures to radians
            double RadPHI0 = PHI0 * Pi180;
            double RadLAM0 = LAM0 * Pi180;

            // Compute af0, bf0, e squared (e2), n and Et
            double af0 = a * f0;
            double bf0 = b * f0;
            double e2 = (Math.Pow(af0, 2) - Math.Pow(bf0, 2)) / Math.Pow(af0, 2);
            double n = (af0 - bf0) / (af0 + bf0);
            double Et = East - e0;

            // Compute initial value for latitude (PHI) in radians
            double PHId = InitialLat(North, n0, af0, RadPHI0, n, bf0);

            // Compute nu, rho and eta2 using value for PHId
            double nu = af0 / (Math.Sqrt(1 - (e2 * Math.Pow((Math.Sin(PHId)), 2))));
            double rho = (nu * (1 - e2)) / (1 - (e2 * Math.Pow(Math.Sin(PHId), 2)));
            double eta2 = (nu / rho) - 1;

            // Compute Longitude
            double X = Math.Pow((Math.Cos(PHId)), -1) / nu;
            double XI = (Math.Pow((Math.Cos(PHId)), -1) / (6 * Math.Pow(nu, 3))) * ((nu / rho) + (2 * Math.Pow((Math.Tan(PHId)), 2)));
            double XII = (Math.Pow((Math.Cos(PHId)), -1) / (120 * Math.Pow(nu, 5))) * (5 + (28 * Math.Pow((Math.Tan(PHId)), 2)) + (24 * Math.Pow((Math.Tan(PHId)), 4)));
            double XIIA = (Math.Pow((Math.Cos(PHId)), -1) / (5040 * Math.Pow(nu, 7))) * (61 + (662 * Math.Pow((Math.Tan(PHId)), 2)) + (1320 * Math.Pow((Math.Tan(PHId)), 4)) + (720 * Math.Pow((Math.Tan(PHId)), 6)));

            double E_N_to_Long_result = (180 / Math.PI) * (RadLAM0 + (Et * X) - (Math.Pow(Et, 3) * XI) + (Math.Pow(Et, 5) * XII) - (Math.Pow(Et, 7) * XIIA));
            return E_N_to_Long_result;
        }


        static double Lat_Long_to_C(double PHI, double LAM, double LAM0, double a, double b, double f0)
        {
            // Compute convergence (in decimal degrees) from latitude and longitude
            // Input: -  latitude (PHI), longitude (LAM) and longitude (LAM0) of false origin in decimal degrees;  ellipsoid axis dimensions (a & b) in meters;  central meridian scale factor (f0).

            // Convert angle measures to radians
            double RadPHI = PHI * Pi180;
            double RadLAM = LAM * Pi180;
            double RadLAM0 = LAM0 * Pi180;

            // Compute af0, bf0 and e squared (e2)
            double af0 = a * f0;
            double bf0 = b * f0;
            double e2 = (Math.Pow(af0, 2) - Math.Pow(bf0, 2)) / Math.Pow(af0, 2);

            // Compute nu, rho, eta2 and p
            double nu = af0 / (Math.Sqrt(1 - (e2 * Math.Pow((Math.Sin(RadPHI)), 2))));
            double rho = (nu * (1 - e2)) / (1 - (e2 * Math.Pow(Math.Sin(RadPHI), 2)));
            double eta2 = (nu / rho) - 1;
            double p = RadLAM - RadLAM0;

            // Compute Convergence
            double XIII = Math.Sin(RadPHI);
            double XIV = ((Math.Sin(RadPHI) * Math.Pow((Math.Cos(RadPHI)), 2)) / 3) * (1 + (3 * eta2) + (2 * Math.Pow(eta2, 2)));
            double XV = ((Math.Sin(RadPHI) * Math.Pow((Math.Cos(RadPHI)), 4)) / 15) * (2 - Math.Pow((Math.Tan(RadPHI)), 2));

            double Lat_Long_to_C_result = Pi180 * ((p * XIII) + (Math.Pow(p, 3) * XIV) + (Math.Pow(p, 5) * XV));
            return Lat_Long_to_C_result;

        }

        static double E_N_to_C(double East, double North, double a, double b, double e0, double n0, double f0, double PHI0)
        {
            // Compute convergence (in decimal degrees) from easting and northing
            // Input: -  Eastings (East) and Northings (North) in meters;  ellipsoid axis dimensions (a & b) in meters;  easting (e0) and northing (n0) of true origin in meters;  central meridian scale factor (f0);  latitude of central meridian (PHI0) in decimal degrees.

            // REQUIRES THE "Marc" AND "InitialLat" FUNCTIONS

            // Convert angle measures to radians
            double RadPHI0 = PHI0 * Pi180;

            // Compute af0, bf0, e squared (e2), n and Et
            double af0 = a * f0;
            double bf0 = b * f0;
            double e2 = (Math.Pow(af0, 2) - Math.Pow(bf0, 2)) / Math.Pow(af0, 2);
            double n = (af0 - bf0) / (af0 + bf0);
            double Et = East - e0;

            // Compute initial value for latitude (PHI) in radians
            double PHId = InitialLat(North, n0, af0, RadPHI0, n, bf0);

            // Compute nu, rho and eta2 using value for PHId
            double nu = af0 / (Math.Sqrt(1 - (e2 * Math.Pow((Math.Sin(PHId)), 2))));
            double rho = (nu * (1 - e2)) / (1 - (e2 * Math.Pow(Math.Sin(PHId), 2)));
            double eta2 = (nu / rho) - 1;

            // Compute Convergence
            double XVI = (Math.Tan(PHId)) / nu;
            double XVII = ((Math.Tan(PHId)) / (3 * Math.Pow(nu, 3))) * (1 + Math.Pow((Math.Tan(PHId)), 2) - eta2 - (2 * Math.Pow(eta2, 2)));
            double XVIII = ((Math.Tan(PHId)) / (15 * Math.Pow(nu, 5))) * (2 + (5 * Math.Pow((Math.Tan(PHId)), 2)) + (3 * Math.Pow((Math.Tan(PHId)), 4)));

            double E_N_to_C_result = Pi180 * ((Et * XVI) - (Math.Pow(Et, 3) * XVII) + (Math.Pow(Et, 5) * XVIII));
            return E_N_to_C_result;
        }

        static double Lat_Long_to_LSF(double PHI, double LAM, double LAM0, double a, double b, double f0)
        {
            // Compute local scale factor from latitude and longitude
            // Input: -  latitude (PHI), longitude (LAM) and longitude (LAM0) of false origin in decimal degrees;  ellipsoid axis dimensions (a & b) in meters;  central meridian scale factor (f0).

            // Convert angle measures to radians
            double RadPHI = PHI * Pi180;
            double RadLAM = LAM * Pi180;
            double RadLAM0 = LAM0 * Pi180;

            // Compute af0, bf0 and e squared (e2)
            double af0 = a * f0;
            double bf0 = b * f0;
            double e2 = (Math.Pow(af0, 2) - Math.Pow(bf0, 2)) / Math.Pow(af0, 2);

            // Compute nu, rho, eta2 and p
            double nu = af0 / (Math.Sqrt(1 - (e2 * Math.Pow((Math.Sin(RadPHI)), 2))));
            double rho = (nu * (1 - e2)) / (1 - (e2 * Math.Pow(Math.Sin(RadPHI), 2)));
            double eta2 = (nu / rho) - 1;
            double p = RadLAM - RadLAM0;

            // Compute local scale factor
            double XIX = (Math.Pow(Math.Cos(RadPHI), 2) / 2) * (1 + eta2);
            double XX = (Math.Pow(Math.Cos(RadPHI), 4) / 24) * (5 - (4 * Math.Pow((Math.Tan(RadPHI)), 2)) + (14 * eta2) - (28 * Math.Pow((Math.Tan(RadPHI * eta2)), 2)));

            double Lat_Long_to_LSF_result = f0 * (1 + (Math.Pow(p, 2) * XIX) + (Math.Pow(p, 4) * XX));
            return Lat_Long_to_LSF_result;

        }

        static double E_N_to_LSF(double East, double North, double a, double b, double e0, double n0, double f0, double PHI0)
        {
            // Compute local scale factor from from easting and northing
            // Input: -  Eastings (East) and Northings (North) in meters;  ellipsoid axis dimensions (a & b) in meters;  easting (e0) and northing (n0) of true origin in meters;  central meridian scale factor (f0);  latitude of central meridian (PHI0) in decimal degrees.

            // REQUIRES THE "Marc" AND "InitialLat" FUNCTIONS

            // Convert angle measures to radians
            double RadPHI0 = PHI0 * Pi180;

            // Compute af0, bf0, e squared (e2), n and Et
            double af0 = a * f0;
            double bf0 = b * f0;
            double e2 = (Math.Pow(af0, 2) - Math.Pow(bf0, 2)) / Math.Pow(af0, 2);
            double n = (af0 - bf0) / (af0 + bf0);
            double Et = East - e0;

            // Compute initial value for latitude (PHI) in radians
            double PHId = InitialLat(North, n0, af0, RadPHI0, n, bf0);

            // Compute nu, rho and eta2 using value for PHId
            double nu = af0 / (Math.Sqrt(1 - (e2 * Math.Pow((Math.Sin(PHId)), 2))));
            double rho = (nu * (1 - e2)) / (1 - (e2 * Math.Pow(Math.Sin(PHId), 2)));
            double eta2 = (nu / rho) - 1;

            // Compute local scale factor
            double XXI = 1 / (2 * rho * nu);
            double XXII = (1 + (4 * eta2)) / (24 * Math.Pow(rho, 2) * Math.Pow(nu, 2));

            double E_N_to_LSF_return = f0 * (1 + (Math.Pow(Et, 2) * XXI) + (Math.Pow(Et, 4) * XXII));
            return E_N_to_LSF_return;

        }

        static double E_N_to_t_minus_T(double AtEast, double AtNorth, double ToEast, double ToNorth, double a, double b, double e0, double n0, double f0, double PHI0)
        {
            // Compute (t-T) correction in decimal degrees at point (AtEast, AtNorth) to point (ToEast,ToNorth)
            // Input: -  Eastings (AtEast) and Northings (AtNorth) in meters, of point where (t-T) is being computed;  Eastings (ToEast) and Northings (ToNorth) in meters, of point at other end of line to which (t-T) is being computed;  ellipsoid axis dimensions (a & b) and easting & northing (e0 & n0) of true origin in meters;  central meridian scale factor (f0);  latitude of central meridian (PHI0) in decimal degrees.

            // REQUIRES THE "Marc" AND "InitialLat" FUNCTIONS

            // Convert angle measures to radians
            double RadPHI0 = PHI0 * Pi180;

            // Compute af0, bf0, e squared (e2), n and Nm (Northing of mid point)
            double af0 = a * f0;
            double bf0 = b * f0;
            double e2 = (Math.Pow(af0, 2) - Math.Pow(bf0, 2)) / Math.Pow(af0, 2);
            double n = (af0 - bf0) / (af0 + bf0);
            double Nm = (AtNorth + ToNorth) / 2;

            // Compute initial value for latitude (PHI) in radians
            double PHId = InitialLat(Nm, n0, af0, RadPHI0, n, bf0);

            // Compute nu, rho and eta2 using value for PHId
            double nu = af0 / (Math.Sqrt(1 - (e2 * Math.Pow((Math.Sin(PHId)), 2))));
            double rho = (nu * (1 - e2)) / (1 - (e2 * Math.Pow(Math.Sin(PHId), 2)));

            // Compute (t-T)
            double XXIII = 1 / (6 * nu * rho);

            double E_N_to_t_minus_T_result = Pi180 * ((2 * (AtEast - e0)) + (ToEast - e0)) * (AtNorth - ToNorth) * XXIII;
            return E_N_to_t_minus_T_result;
        }
    }
}