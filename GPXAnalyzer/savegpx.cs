using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

using gpx;

namespace GPXAnalyzer
{

    partial class Track
    {
        public void Save(string aFile, string trackName, string dtype)
        {
            Console.WriteLine("Saving: " + aFile);
            
            string t = ""; //holds the xml

          //  Boolean elevationArrayTested = false;
            string pl = "Author: Jeremy Ellman";

            trkType track = FindTrack(trackName);
            wptType wpt;

            /* This part of the GPX is going to be the same no matter what. */
            t += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
            "<gpx version=\"1.1\"\n" +
            "     creator=\"GPXAnalyzer\"" +
            "     xmlns=\"http://www.topografix.com/GPX/1/1\"\n" +
            "     xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n" +
            "     xsi:schemaLocation=\"http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd\">\n";


            if (dtype == "track")
            {
                t += "   <trk>\n";
                t += "      <name>GPX Analyzer Track</name>\n" +
                "      <cmt>Permalink: <![CDATA[ " + pl + " ]]>\n</cmt>\n";
                t += "      <trkseg>\n";
            }
            else if (dtype == "route")
            {
                t += "   <rte>\n";
                t += "      <name>GPX Analyzer Route</name>\n" +
                "      <cmt>Permalink: <![CDATA[ " + pl + " ]]>\n</cmt>\n";
            }

            foreach (trksegType seg in track.trkseg)
            {
                if (seg == null) continue;

                for (int i = 0; i < seg.trkpt.Length; i++)
                {
                    wpt = seg.trkpt[i];
                    if (wpt.lon == 0 && wpt.lat == 0) continue;

                    if (dtype == "track")
                    {
                        t += "      <trkpt ";
                    }
                    else if (dtype == "route")
                    {
                        t += "      <rtept ";
                    }
                    else if (dtype == "points")
                    {
                        t += "      <wpt ";
                    }
                    t += "lat=\"" + wpt.lat + "\" " +
                    "lon=\"" + wpt.lon + "\">\n";
                   
                   if (wpt.eleSpecified) 
                       t += "         <ele>" + wpt.ele + "</ele>\n";
                   if (wpt.timeSpecified)   // "o" = UTC roundtripable time format
                       // http://msdn.microsoft.com/en-us/library/az4se3k1(VS.80).aspx
                       t += "         <time>" + wpt.time.ToString("o") + "</time>\n";


                    /*            if (elevationArrayTested == true)
                                {
                                    string currentElevation = 0;
                                    currentElevation = self.elevationArray[i];
                                    currentElevation = Math.Round(currentElevation * 0.3048);
                                    
                                } */
                   string name = wpt.name;
                   if (name == string.Empty) name = (i > 0 ? "Turn " + i : "Start");
                    t += "         <name>" + name + "</name>\n";

                    if (dtype == "track")
                    {
                        t += "      </trkpt>\n";
                    }
                    else if (dtype == "route")
                    {
                        t += "      </rtept>\n";
                    }
                    else if (dtype == "points")
                    {
                        t += "      </wpt>\n";
                    }
                }
            }
            if (dtype == "track")
            {
                t += "      </trkseg>\n";
                t += "   </trk>\n";
            }
            else if (dtype == "route")
            {
                t += "   </rte>\n";
            }

            t += "</gpx>\n";
            innerSave(aFile, t);
        }

        private void innerSave(string filename, string contents)
        {
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
                file.WriteLine(contents);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("File: " + filename + " Could not be written: " + ex.Message);
            }
        }
    }
}