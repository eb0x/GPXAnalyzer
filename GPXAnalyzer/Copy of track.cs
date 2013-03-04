using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Xml;

using gpx;

namespace GPXAnalyzer
{

    public struct Waypoint
    {
        public double elevation;
        public double latitude;
        public double longitude;
        public DateTime instant;

        public Waypoint(double lat, double longi, double ele, DateTime inst)
        {
            this.latitude = lat;
            this.longitude = longi;
            this.elevation = ele;
            this.instant = inst;
        }

        public Waypoint(double lat, double longi)
        {
            this.latitude = lat;
            this.longitude = longi;
            this.elevation = 0;
            this.instant = new DateTime(0L);
        }
    }

    class Track
    {
        // holds the current GPX file information
        private gpxType gpxInfo = null;

        // total distance in meters
        private double totalDist;

        // average speed in meters per second
        private double totalAvgSpeed;

        // total time for the track
        private TimeSpan totalTime;

        List<Waypoint> WaypointList = new List<Waypoint>();    // all Waypoint in track

        //null initializer
        public Track() { }

        public List<Waypoint> Waypoints
        {
            get { return WaypointList; }
            set { WaypointList = value; }
        }


        private void AddWaypoint(Double longitude, Double latitude, Double elevation, DateTime dt)
        {
            Waypoint wpt = new Waypoint(longitude, latitude, elevation, dt);
            try
            {
                WaypointList.Add(wpt);
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
                DialogResult dlg = MessageBox.Show("Nasty error adding waypoint" + ex.Message,
                    "Error adding track/waypoint",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        public void PrintTrack()
        {
            foreach (Waypoint wpt in WaypointList)
            {
                DateTime dt = wpt.instant;
                Console.WriteLine("Date: " + dt.ToLongDateString() +
                                        ", Time: " + dt.ToLongTimeString());
            }

            Console.WriteLine("Radius of Earth (start): " + Earth.RadiusAtGeodeticLatitude(WaypointList[0].latitude));
            Console.WriteLine("Radius of Earth (end): " + Earth.RadiusAtGeodeticLatitude(WaypointList[WaypointList.Count - 1].latitude));
        }

        public double TrackLength()
        {
            Waypoint wp1, wp2;

            wp1 = WaypointList[0];
            double distance = 0D, dflat = 0D;

            for (int i = 1; i < WaypointList.Count; i++)
            {

                wp2 = WaypointList[i];
                distance += Earth.Haversine(wp1, wp2);
                dflat += Earth.GetFlatDistance(wp1, wp2);
                wp1 = wp2;
                Console.WriteLine("Distance " + i + ": " + distance + " ( " + dflat + " ) ");
            }

            return distance;
        }


        public void LoadTrack_1(string file)
        {
            XmlTextReader reader = new XmlTextReader(file);
            Waypoint wpt = new Waypoint(0L, 0L);
            Double longitude = 0L;
            Double latitude = 0L;
            Double lat_degrees = 0D;
            Double long_degrees = 0D;
            Double elevation = 0L;
            DateTime dt = new DateTime();
            String lastNodeval = "";

            int wps = 0;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.

                        switch (reader.Name)
                        {
                            case "trkpt":
                                lat_degrees = double.Parse(reader.GetAttribute("lat"));
                                long_degrees = double.Parse(reader.GetAttribute("lon"));
                                longitude = Earth.ConvertDegreesToRadians(long_degrees);
                                latitude = Earth.ConvertDegreesToRadians(lat_degrees);
                                break;
                            default:
                                break;
                        }
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        Console.WriteLine(reader.Value);
                        lastNodeval = reader.Value;
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        switch (reader.Name)
                        {
                            case "ele": elevation = double.Parse(lastNodeval);
                                break;
                            case "time":
                                Regex r = new Regex(@"(?<YEAR>\d{4})-(?<MONTH>\d{2})-(?<DAY>\d{2})T(?<HOUR>\d{2}):(?<MIN>\d{2}):(?<SEC>\d{2})Z");
                                Match m1 = r.Match(lastNodeval);

                                //Output match information if a match was found
                                if (m1.Success)
                                {

                                    int year = Int32.Parse(m1.Groups["YEAR"].Value);
                                    int month = Int32.Parse(m1.Groups["MONTH"].Value);
                                    int day = Int32.Parse(m1.Groups["DAY"].Value);
                                    int hour = Int32.Parse(m1.Groups["HOUR"].Value);
                                    int min = Int32.Parse(m1.Groups["MIN"].Value);
                                    int sec = Int32.Parse(m1.Groups["SEC"].Value);

                                    dt = new DateTime(year, month, day, hour, min, sec);
                                    Console.WriteLine("Date: " + dt.ToLongDateString() +
                                        ", Time: " + dt.ToLongTimeString());
                                }
                                break;
                            case "trkpt":
                                Console.Write("trkpt:" +
                                    " Elevation: " + elevation +
                                    " Latitude: " + latitude +
                                    " Longitude: " + longitude +
                                    "\n");
                                AddWaypoint(longitude, latitude, elevation, dt);
                                wps++;
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }
        }

        public void LoadTrack(string fileName)
        {
            // Clean up UI
            totalDist = 0;
            totalAvgSpeed = 0;
            totalTime = TimeSpan.Zero;
 //           lbTracks.Items.Clear();
 //         lbPoints.Items.Clear();

            // Load file
            XmlSerializer ser = new XmlSerializer(typeof(gpxType));
            using (FileStream str = new FileStream(fileName, FileMode.Open))
            {
 //               this.Text = "GPS Track Viewer - " + Path.GetFileName(fileName);
                gpxInfo = (gpxType)ser.Deserialize(str);
                int nTracks = 0;
                if (gpxInfo.trk != null)
                {
                    // populate tracks listbox
                    nTracks = gpxInfo.trk.Length;
                    foreach (trkType track in gpxInfo.trk)
                    {
 //                       lbTracks.Items.Add(track.name);
                    }
                }
                if (nTracks > 0)
                {
                    // Select the first track
                    // This raises the SelectedIndexChanged event.
   //                 lbTracks.SelectedIndex = 0;
                }
   //             lblTracks.Text = string.Format("{0} Tracks", nTracks);
            }
        }
    }
}


