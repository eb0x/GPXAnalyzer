using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

using gpx;

namespace GPXAnalyzer
{

    partial class Track
    {
        // holds the current GPX file information
        private gpxType gpxInfo = null;

        // total distance in meters
        private double totalDist;

        // distance time calcs
        double[] distances;// place to cache distances
        DateTime[] times; // times

        // average speed in meters per second
        private double totalAvgSpeed;

        // total time for the track
        private TimeSpan totalTime;

        // keep track of bounds for scaling.
        private boundsType trackBounds = new boundsType();

        // elevation bounds
        private double minEle;
        private double maxEle;

        private string _name;

        //null initializer
        public Track() { }

        public Track(decimal[] pts, string[] labels)
        {
            if (pts.Length > 0)
            {
                gpxInfo = new gpxType();
                gpxInfo.trk = new trkType[1];
                gpxInfo.trk[0] = new trkType();
                trkType track = gpxInfo.trk[0];
                track.name = "generated_route";
                track.trkseg = new trksegType[2];
                track.trkseg[0] = new trksegType();
                trksegType seg = track.trkseg[0];
                seg.trkpt = new wptType[pts.Length / 2];
                wptType wpt;

                for (int i = 0; i < seg.trkpt.Length; i++)
                {
                    seg.trkpt[i] = new wptType();
                    wpt = seg.trkpt[i];
                    wpt.name = labels[i];
                    wpt.lat = pts[i * 2];
                    wpt.lon = pts[i * 2 + 1];
                }
            }
        }

        public String Text
        {
            get { return _name; }
            set { _name = value; }
        }

        public Boolean isEmpty()
        {
            return gpxInfo == null;
        }


        public void LoadTrack(string fileName)
        {
            // Clean up UI
            totalDist = 0;
            totalAvgSpeed = 0;
            totalTime = TimeSpan.Zero;

            // Load file
            XmlSerializer ser = new XmlSerializer(typeof(gpxType));
            using (FileStream str = new FileStream(fileName, FileMode.Open))
            {
                this.Text = Path.GetFileName(fileName);
                gpxInfo = (gpxType)ser.Deserialize(str);
            }
        }

        public void DeleteTrack(string name)
        {
            Console.WriteLine("Deleting " + name);
            // gpxInfo.trk
            for (int i = 0; i < gpxInfo.trk.Length; i++)
            {
                trkType track = gpxInfo.trk[i];
                if (!ValidTrack(i)) continue;

                if (name.Equals(track.name))
                {
                    Console.WriteLine("Will delete: " + track.name);
                    gpxInfo.trk[i] = null;
                }
            }
        }

        public void DisplayTrack(GPXAnalyzer frm)
        {
            int nTracks = 0;

            frm.clearTracks();
            frm.clearPoints();

            if (gpxInfo.trk != null)
            {
                // populate tracks listbox
                nTracks = gpxInfo.trk.Length;
                foreach (trkType track in gpxInfo.trk)
                {
                    frm.AddTrackName(track.name);
                }
            }
            /*       if (nTracks > 0)
                   {
                       // Select the first track
                       // This raises the SelectedIndexChanged event.
                       frm.lblTracks.SelectedIndex = 0;
                   } */
            frm.SetTrackText(string.Format("{0} Tracks in {1}", nTracks, Text));
        }

        /// <summary>
        /// For a given track index calculate overall data, include bounds,
        /// distances and times. This is used subsequently in various profiles
        /// 
        /// Two passes are required in
        /// order to calculate scales. 
        /// </summary>
        /// <param name="Trackidx"></param>
        private void SetupTrackData(int Trackidx)
        {
            trkType track = gpxInfo.trk[Trackidx];
            trksegType seg = track.trkseg[0];
            wptType wpt;
            wptType wptPrev = seg.trkpt[0];
            // initialize totals
            totalDist = 0;
            totalAvgSpeed = 0;
            totalTime = TimeSpan.Zero;
            trackBounds.maxlat = decimal.MinValue;
            trackBounds.minlat = decimal.MaxValue;
            trackBounds.maxlon = decimal.MinValue;
            trackBounds.minlat = decimal.MaxValue;
            trackBounds.minlat = decimal.MaxValue;
            // initialize elevation bounds
            minEle = double.MaxValue;
            maxEle = double.MinValue;

            distances = new double[seg.trkpt.Length]; // place to cache distances
            times = new DateTime[seg.trkpt.Length]; // times

            minEle = 0;
            maxEle = 0;
            totalDist = 0;
            totalTime = new TimeSpan();

            for (int iPoint = 0; iPoint < seg.trkpt.Length; iPoint++)
            {
                wpt = seg.trkpt[iPoint];
                double dist = GPXUtils.Haversine(wptPrev, wpt); //expensive trig calc
                // update elevation extremes
                minEle = Math.Min((double)wpt.ele, minEle);
                maxEle = Math.Max((double)wpt.ele, maxEle);
                // update bounds
                trackBounds.minlat = Math.Min(wpt.lat, trackBounds.minlat);
                trackBounds.maxlat = Math.Max(wpt.lat, trackBounds.maxlat);
                trackBounds.minlon = Math.Min(wpt.lon, trackBounds.minlon);
                trackBounds.maxlon = Math.Max(wpt.lon, trackBounds.maxlon);

                distances[iPoint] = dist;
                totalDist += dist;
                times[iPoint] = wpt.time;
                totalTime += wpt.time - wptPrev.time;
                wptPrev = wpt;
            }
            // compute average speed
            totalAvgSpeed = totalDist / totalTime.TotalSeconds;
        }



        // Fills the Points list box with the track points
        // from the selected track and computes totals for the track.
        public void DisplayPoints(GPXAnalyzer frm, int whichTrack)
        {
            if (whichTrack < 0) return;
            trkType track = gpxInfo.trk[whichTrack];
            if (track == null) return;
            trksegType seg = track.trkseg[0];
            wptType wpt;
            wptType wptPrev = null;

            SetupTrackData( whichTrack );
            frm.SetPointsText(string.Format("{0} Points", seg.trkpt.Length));

            for (int iPoint = 0; iPoint < seg.trkpt.Length; iPoint++)
            {
                wpt = seg.trkpt[iPoint];
                string spt;

                if (wpt.name != null && !(wpt.name == string.Empty))
                {
                    spt = string.Format("{0,-5} {1}", iPoint + 1, wpt.name);
                }
                else
                {
                    spt = string.Format("{0,-5} {1}", iPoint + 1,
                      GPXUtils.LatLonToString((double)wpt.lat, (double)wpt.lon));
                }

                frm.AddPointsName(spt);
                wptPrev = wpt;
            }            
        }

        /// <summary>
        /// Find a track by name from the collection of tracks 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private trkType FindTrack(string name)
        {
            trkType track = null;

            foreach (trkType trk in gpxInfo.trk)
            {
                if (trk.name.Equals(name)) track = trk;
            }

            return track;
        }
        private Boolean ValidTrack(int whichTrack)
        {
            if ((whichTrack < 0) || (gpxInfo.trk[whichTrack] == null)) return false;
            else return true;
        }

        public PointF[] GenerateTrackGraphic(GPXAnalyzer frm, int Trackidx, int width, int height, int borderWidth)
        {
            PointF[] Points = new PointF[0];
            if (!ValidTrack(Trackidx)) return Points;

            if (gpxInfo != null)
            {
                // Compute scale factor
                double dlat = (double)(trackBounds.maxlat - trackBounds.minlat);
                double dlon = (double)(trackBounds.maxlon - trackBounds.minlon);
                double xScale = (width - 2 * borderWidth) / dlon;
                double yScale = (height - 2 * borderWidth) / dlat;
                xScale = Math.Min(xScale, yScale);
                yScale = xScale;

                trkType track = gpxInfo.trk[Trackidx];
                trksegType seg = track.trkseg[0];
                Points = new PointF[seg.trkpt.Length];
                wptType wpt;
                for (int iPoint = 0; iPoint < seg.trkpt.Length; iPoint++)
                {
                    wpt = seg.trkpt[iPoint];
                    PointF pt = frm.LatLonToScreen((double)wpt.lat, (double)wpt.lon,
                        (double)trackBounds.minlat, (double)trackBounds.minlon, xScale, yScale);
                    Points[iPoint] = pt;
                }

            }
            return Points;
        }

       
        public PointF[] GenerateElevationGraphic(GPXAnalyzer frm, int Trackidx, int width, int height, int borderWidth)
        {
            trkType track = gpxInfo.trk[Trackidx];
            trksegType seg = track.trkseg[0];
            wptType wpt;
            wptType wptPrev = seg.trkpt[0];
            double runningDist = 0;
            PointF[] Points = new PointF[seg.trkpt.Length];

            SetupTrackData(Trackidx);
            // Compute scale factor
            // Determine elevation difference.
            double dele = maxEle - minEle;
            double xScale = (width - 2 * borderWidth) / totalDist;
            double yScale = (height - 2 * borderWidth) / dele;

            // Calculate graphics code
            for (int iPoint = 0; iPoint < seg.trkpt.Length; iPoint++)
            {
                wpt = seg.trkpt[iPoint];
                runningDist += distances[iPoint];    //stored geo calc
                PointF pt = frm.DistEleToScreen(runningDist, (double)wpt.ele, minEle, xScale, yScale);
                Points[iPoint] = pt;
                wptPrev = wpt;
            }
            // Draw the lines between the points
            return Points;
        }

        public void PopulateDataTable(GPXAnalyzer frm, int Trackidx)
        {
            if (Trackidx >= 0)
            {
                trkType track = gpxInfo.trk[Trackidx];
                trksegType seg = track.trkseg[0];
                wptType wpt, wptPrev = null;


                double legLength;
                decimal ele;
                double speed = 0D;
                string name;
                decimal total_ascent = 0;
                decimal total_descent = 0;
                double total_distance = 0D;
                decimal ele_diff = 0;
                TimeSpan elapsedTime = new TimeSpan(0L);
                TimeSpan legTime = new TimeSpan(0L); 


                for (int iPoint = 0; iPoint < seg.trkpt.Length; iPoint++)
                {
                    wpt = seg.trkpt[iPoint];
                    if (wpt.name == null || wpt.name.Equals("")) name = "WP:" + iPoint; else name = wpt.name;

                    legLength = distances[iPoint];
                    total_distance += legLength;

                    ele = wpt.ele;

                    if (wptPrev != null)
                    {
                        legTime = wpt.time - wptPrev.time;
                        elapsedTime += legTime;
                        speed = legLength / legTime.Seconds; // in metres/sec
                        speed = (legTime.Seconds * 1609.344) / (legLength * 60.0); //in minute miles

                      //  if ((total_distance / 1609.344))

                        ele_diff = wpt.ele - wptPrev.ele;
                        if (ele_diff < 0) total_descent += -ele_diff;
                        else total_ascent += ele_diff;
                    }

                    frm.dgvData_addRow(name,
                        legLength, ele, wpt.time, speed,
                        total_ascent, total_descent, total_distance, elapsedTime);
                    frm.DGVDataEntry_addRow(name, wpt.lat, wpt.lon);

                    wptPrev = wpt;
                }
           //     frm.dgvData_addRow
            }
        }

    }
}


