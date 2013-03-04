using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace bngconv
{
    public class osgb
    {
        double northings;
        double eastings;
        double longitude;
        double latitude;

        string status;
        string[,] prefixes = new string[13, 7] 	
                        {{"SV","SW","SX","SY","SZ","TV","TW"},
                        {"SQ","SR","SS","ST","SU","TQ","TR"},
                       	{"SL","SM","SN","SO","SP","TL","TM"},
                       	{"SF","SG","SH","SJ","SK","TF","TG"},
                       	{"SA","SB","SC","SD","SE","TA","TB"},
                       	{"NV","NW","NX","NY","NZ","OV","OW"},
                       	{"NQ","NR","NS","NT","NU","OQ","OR"},
                       	{"NL","NM","NN","NO","NP","OL","OM"},
                       	{"NF","NG","NH","NJ","NK","OF","OG"},
                       	{"NA","NB","NC","ND","NE","OA","OB"},
                       	{"HV","HW","HX","HY","HZ","JV","JW"},
                       	{"HQ","HR","HS","HT","HU","JQ","JR"},
                       	{"HL","HM","HN","HO","HP","JL","JM"}};


        public void osbg()
        {
            this.northings = 0;
            this.eastings = 0;
            this.latitude = 0;
            this.longitude = 0;
            this.status = "Undefined";
        }

        public string test()
        {
            setGridCoordinates(409020, 564180);
            string ne = getGridRef(3);
            setGridCoordinates(381990, 404850);
            string m2 = getGridRef(3);
            Boolean m3 = parseGridRef("NZ090642");
            Console.WriteLine("M25: " + m2 + "\n" + " NE: " + ne + " M3(parse):" + eastings + "," + northings);
            Boolean m4 = parseGridRef("ST725888");
            setGridCoordinates(651409, 313177);
            Console.WriteLine(" :" + eastings + "," + northings);
            getWGS84();
            Console.WriteLine("\n Lat Long:" + latitude + "," + longitude);
            return ("done");

        }

        public void setGridCoordinates(double eastings, double northings)
        {
            this.northings = northings;
            this.eastings = eastings;
            this.status = "OK";
        }

        public void setDegrees(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public void setError(string msg)
        {
            this.status = msg;
        }

        private string _zeropad(double num, int len)
        {
            string str = num.ToString();
            while (str.Length < len)
            {
                str = '0' + str;
            }
            return str;
        }

        public string getGridRef(int precision)
        {
            int x = 0;
            int y = 0;

            if (precision < 0)
                precision = 0;
            if (precision > 5)
                precision = 5;

            double e = 0D;

            double n = 0D;
            if (precision > 0)
            {
                y = (int)Math.Floor(northings / 100000);
                x = (int)Math.Floor(eastings / 100000);

                e = Math.Round(eastings % 100000);
                n = Math.Round(northings % 100000);

                double div = (5 - precision);
                e = Math.Round(e / Math.Pow(10, div));
                n = Math.Round(n / Math.Pow(10, div));
            }

            string prefix = prefixes[y, x];

            return prefix + " " + _zeropad(e, precision) + " " + _zeropad(n, precision);
        }

        public Boolean parseGridRef(string landranger)
        {
            Boolean ok = false;

            northings = 0;
            eastings = 0;

            int precision;

            for (precision = 5; precision >= 1; precision--)
            {
                Regex pattern = new Regex("^(?<GridLetters>[A-Z]{2})\\s*(?<EASTING>\\d{" + precision + "})\\s*(?<NORTHING>\\d{" + precision + "})$", RegexOptions.IgnoreCase);
                Match m = pattern.Match(landranger);

                if (m.Success)
                {
                    string gridSheet = m.Groups["GridLetters"].Value;
                    double gridEast = 0;
                    double gridNorth = 0;

                    //5x1 4x10 3x100 2x1000 1x10000 
                    if (precision > 0)
                    {
                        double mult = Math.Pow(10, 5 - precision);
                        gridEast = int.Parse(m.Groups["EASTING"].Value) * mult;
                        gridNorth = int.Parse(m.Groups["NORTHING"].Value) * mult;
                    }

                    int x, y;

                    for (x = 0; x < prefixes.GetLength(1); x++)
                    {
                        for (y = 1; y < prefixes.GetLength(0); y++)
                            if (prefixes[y, x].Equals(gridSheet))
                            {
                                eastings = (x * 100000) + gridEast;
                                northings = (y * 100000) + gridNorth;
                                ok = true;
                                break;
                            }
                        if (ok) break;
                    }
                }
            }
            return ok;
        }

        public void getWGS84()
        {

            double height = 0;

            double lat1 = OS_Math.E_N_to_Lat(this.eastings, this.northings, 6377563.396, 6356256.910, 400000, -100000, 0.999601272, 49.00000, -2.00000);
            double lon1 = OS_Math.E_N_to_Long(this.eastings, this.northings, 6377563.396, 6356256.910, 400000, -100000, 0.999601272, 49.00000, -2.00000);

            double x1 = OS_Math.Lat_Long_H_to_X(lat1, lon1, height, 6377563.396, 6356256.910);
            double y1 = OS_Math.Lat_Long_H_to_Y(lat1, lon1, height, 6377563.396, 6356256.910);
            double z1 = OS_Math.Lat_H_to_Z(lat1, height, 6377563.396, 6356256.910);

            double x2 = OS_Math.Helmert_X(x1, y1, z1, 446.448, 0.2470, 0.8421, -20.4894);
            double y2 = OS_Math.Helmert_Y(x1, y1, z1, -125.157, 0.1502, 0.8421, -20.4894);
            double z2 = OS_Math.Helmert_Z(x1, y1, z1, 542.060, 0.1502, 0.2470, -20.4894);

            double latitude = OS_Math.XYZ_to_Lat(x2, y2, z2, 6378137.000, 6356752.313);
            double longitude = OS_Math.XYZ_to_Long(x2, y2);

            setDegrees(latitude, longitude);
            return;
        }
    }
}

/*
GT_OSGB.prototype.

GT_OSGB.prototype.

GT_OSGB.prototype.

GT_OSGB.prototype.

GT_OSGB.prototype.
GT_OSGB.prototype.
 */

   