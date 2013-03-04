using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GPXAnalyzer
{
    public partial class GPXAnalyzer : Form
    {
        Track CurrentTrack;
        osgb OSGB = new osgb();

        //for displaying the graphic panel

        // border size, in pixels.
        private int borderWidth = 5;


        public GPXAnalyzer()
        {
            InitializeComponent();
        }

        public OpenFileDialog aFileDialogue
        {
            get
            {
                return openFileDialog1;
            } // end get
            set
            {
                openFileDialog1 = value;
            } // end set
        } // end aFileDialogue

        public SaveFileDialog aSaveFileDialogue
        {
            get
            {
                return saveFileDialog1;
            } // end get
            set
            {
                saveFileDialog1 = value;
            } // end set
        }


        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GPXAnalyzer -- Analyze GPX Tracks\n\nJeremy Ellman (c) 2008");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            this.aFileDialogue.DefaultExt = "gpx";
            this.aFileDialogue.Filter = "gpx files (*.gpx)|*.gpx";
            this.aFileDialogue.FileName = "tracks";
            if (this.aFileDialogue.ShowDialog() == DialogResult.OK)
            {
                string filename = this.aFileDialogue.FileName;
                clearTracks();
                clearPoints();
                CurrentTrack = new Track();
                CurrentTrack.LoadTrack(filename);
                CurrentTrack.DisplayTrack(this);
                //   tr.PrintTrack();
                //   tr.TrackLength();
                // string path = Path.GetDirectoryName(filename);
                //  LoadXMLFiles(path, true);
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Track tr = new Track();
            // tr.TestLength();
        }

        public void clearTracks()
        {
            lblTracks.Items.Clear();
            SetTrackText("Tracks");
        }

        public void AddTrackName(string name)
        {
            lblTracks.Items.Add(name);
        }

        public void SetTrackText(string msg)
        {
            this.lblTracks.Text = msg;
            this.labelTracks.Text = msg;
        }

        public void SetPointsText(string msg)
        {
            lblPoints.Items.Clear();
            this.lblPoints.Text = msg;
            this.labelPoints.Text = msg;

        }

        public void clearPoints()
        {
            //this.lbl
            lblPoints.Items.Clear();
            SetPointsText("Points");
        }

        private void lblTracks_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*MouseEventArgs me = (System.Windows.Forms.MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
            {
                this.contextMenuStriplblTracks.Show();
            } */
            CurrentTrack.DisplayPoints(this, lblTracks.SelectedIndex);
            // redraw map
            pnlMap.Invalidate();
            //  pnlMapPaint();
        }

        public void AddPointsName(string name)
        {
            lblPoints.Items.Add(name);
        }

        //  private void pnlMapPaint(object sender, PaintEventArgs e)


        /// <summary>
        /// Convert lat and lon in decimal degrees to screen coordinates
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="minlat"></param>
        /// <param name="minlon"></param>
        /// <param name="xScale"></param>
        /// <param name="yScale"></param>
        /// <returns></returns>
        public PointF LatLonToScreen(double lat, double lon, double minlat, double minlon,
                  double xScale, double yScale)
        {
            double x = borderWidth + (lon - minlon) * xScale;

            // have to invert the y coordinate
            double y = pnlMap.Height - borderWidth - ((lat - minlat) * yScale);

            return new PointF((float)x, (float)y);
        }


        public PointF DistEleToScreen(double dist, double ele, double minEle, double xScale, double yScale)
        {
            double x = borderWidth + (dist * xScale);

            // invert the Y coordinate
            double y = pnlProfile.Height - borderWidth - ((ele - minEle) * yScale);
            return new PointF((float)x, (float)y);
        }

        private void pnlMap_Paint(object sender, PaintEventArgs e)
        {
            // redraw the entire window
            using (Graphics g = pnlMap.CreateGraphics())
            {
                g.Clear(pnlMap.BackColor);

                if (lblTracks.SelectedIndex >= 0)
                {
                    PointF[] Points = CurrentTrack.GenerateTrackGraphic(this, lblTracks.SelectedIndex, pnlMap.Width, pnlMap.Height, borderWidth);
                    //Console.WriteLine("there are " + Points.Length + " Points");
                    // Draw the lines between the points
                    Pen p = new Pen(Color.DarkGray, 8.0f);
                    if (Points.Length > 1) g.DrawLines(p, Points);
                    //         g.DrawPolygon(Pens.Bisque, new Point[] { new Point(130, 140), new Point(20, 180) }); // syntax check

                    // Mark Way Points
                    foreach (PointF pt in Points)
                    {
                        g.FillEllipse(Brushes.Black, pt.X, pt.Y, 2.0f, 2.0f);
                    }

                }
            }
        }

        private void pnlMap_Resize(object sender, EventArgs e)
        {
            pnlMap.Invalidate();
        }



        private void GPXAnalyzer_Resize(object sender, EventArgs e)
        {
            Console.WriteLine(String.Format("Resize Screen: Width {0}, Height {1}, Splitter Width {2}, Splitter Height {3}", this.Width, this.Height, this.splitContainer_GPX.Width, this.splitContainer_GPX.Height));
            // where is splitter now
            this.SuspendLayout();

            float proportion = (float)splitContainer_GPX.Width / (float)this.Width;
            // resize splitter container
            this.splitContainer_GPX.Width = this.Width;
            /*  splitContainer1.SplitterDistance /
              this.splitContainer1.SplitterDistance = (int)proportion * this.Width; */
            this.ResumeLayout();
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {

        }

        private void labelTracks_Click(object sender, EventArgs e)
        {

        }

        private void pnlProfile_Paint(object sender, PaintEventArgs e)
        {
            // redraw the entire window
            using (Graphics g = pnlProfile.CreateGraphics())
            {
                g.Clear(pnlProfile.BackColor);

                if (lblTracks.SelectedIndex >= 0)
                {
                    PointF[] Points = CurrentTrack.GenerateElevationGraphic(this, lblTracks.SelectedIndex, pnlProfile.Width, pnlProfile.Height, borderWidth);
                    //Console.WriteLine("there are " + Points.Length + " Points");
                    // Draw the lines between the points
                    Pen p = new Pen(Color.DarkGray, 8.0f);
                    if (Points.Length > 1) 
                        try
                        {
                            g.DrawLines(Pens.Blue, Points); // two points needed
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("There was an error: " + ex.Message);
                        }

                    // Mark Way Points
                    foreach (PointF pt in Points)
                    {
                        g.FillEllipse(Brushes.Black, pt.X, pt.Y, 2.0f, 2.0f);
                    }

                }
            }
        }

        private void pnlProfile_Resize(object sender, EventArgs e)
        {
            pnlProfile.Invalidate();
        }

        //Called whenever a tab is selected
        //
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if ((e.TabPage == this.tabTabular) && CurrentTrack != null)
            {
                this.SuspendLayout();
                SetUpReviewDataGridView();
                CurrentTrack.PopulateDataTable(this, lblTracks.SelectedIndex);
                DGVTrack.Visible = true;
                this.ResumeLayout();
                DGVTrack.Show();
            }
        }


        // taken from 
        // http://msdn2.microsoft.com/en-us/library/system.windows.forms.datagridview(VS.80).aspx
        //
        private void SetUpReviewDataGridView()
        {
            //  this.Controls.Add(dgvTrack);
            DGVTrack.ColumnCount = 10;
            DataGridViewCellStyle style =
                DGVTrack.ColumnHeadersDefaultCellStyle;
            style.BackColor = Color.Navy;
            style.ForeColor = Color.White;
            style.Font = new Font(DGVTrack.Font, FontStyle.Bold);

            DGVTrack.EditMode = DataGridViewEditMode.EditOnEnter;
            DGVTrack.Name = "dgvTrack";
            //  dgvTrack.Location = new Point(8, 8);
            //   dgvTrack.Size = new Size(500, 300);
            DGVTrack.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            DGVTrack.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Raised;
            DGVTrack.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            DGVTrack.GridColor = SystemColors.ActiveBorder;
            DGVTrack.RowHeadersVisible = false;
            DGVTrack.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.InactiveCaptionText;
            // Only one selection is supported at a time
            DGVTrack.MultiSelect = false;
            // Configure the grid to use cell selection
            DGVTrack.SelectionMode = DataGridViewSelectionMode.CellSelect;
            DGVTrack.AutoSizeColumnsMode =
//         DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;  //Discussed here http://msdn2.microsoft.com/en-us/library/system.windows.forms.datagridviewautosizecolumnsmode.aspx
            DataGridViewAutoSizeColumnsMode.AllCells;
            DGVTrack.Columns[1].Name = "Leg Length";
            DGVTrack.Columns[1].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            DGVTrack.Columns[2].Name = "Elevation";
            DGVTrack.Columns[3].Name = "Time";
            DGVTrack.Columns[4].Name = "Speed";
            DGVTrack.Columns[4].ValueType = typeof(decimal);
            DGVTrack.Columns[5].Name = "Total Ascent";
            DGVTrack.Columns[6].Name = "Total Descent";
            DGVTrack.Columns[7].Name = "Total Distance";
            DGVTrack.Columns[8].Name = "Elapsed Time";
            DGVTrack.Columns[9].Name = "Split Time";
            

            /*
             * foreach (DataGridViewColumn col in dgvTrack.Columns)
               {
                   col.ValueType = typeof(double);
               }
             */


            // Make the font italic for row four.
            DGVTrack.Columns[4].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);

            DGVTrack.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVTrack.MultiSelect = false;

            DGVTrack.BackgroundColor = Color.Honeydew;

            DGVTrack.Dock = DockStyle.Fill;
            DGVTrack.Rows.Clear();
            /*
                        dgvTrack.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvTrack_CellFormatting);
                        dgvTrack.CellParsing += new DataGridViewCellParsingEventHandler(dgvTrack_CellParsing);
                        addNewRowButton.Click += new EventHandler(addNewRowButton_Click);
                        deleteRowButton.Click += new EventHandler(deleteRowButton_Click);
                        ledgerStyleButton.Click += new EventHandler(ledgerStyleButton_Click);
                        dgvTrack.CellValidating += new DataGridViewCellValidatingEventHandler(dgvTrack_CellValidating);
                        */
        }

        public void dgvData_addRow(string name, double distance,
            decimal ele, DateTime time, double speed,
            decimal total_ascent, decimal total_descent, double total_distance,
            TimeSpan elapsedTime)
        {
            string[] row = new string[9];
            row[0] = name;
            row[1] = String.Format("{0:0.00}", distance);
            row[2] = String.Format("{0:0.00}", ele);
            row[3] = time.ToLongTimeString();
            row[4] = String.Format("{0:0.00}", speed);
            row[5] = String.Format("{0:0.00}", total_ascent);
            row[6] = String.Format("{0:0.00}", total_descent);
            row[7] = String.Format("{0:0.00}", total_distance);
            row[8] = String.Format("{0:D2}:{1:D2}:{2:D2}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);
            DataGridViewRowCollection rows = this.DGVTrack.Rows;
            rows.Add(row);
        }

        private void dgvTrack_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(String.Format("dgvTrack_CellContentClick"));
        }

        private void NewtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTrack != null && !CurrentTrack.isEmpty())
            {
                DialogResult dlgres = MessageBox.Show(
                        "The current track is not empty.\n\n" +
                "select OK if you to create a new track, or Cancel to keep it",
                "Current Track Active",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dlgres != DialogResult.OK) return;

            }

            clearTracks();
            clearPoints();
            CurrentTrack = new Track();
            DGVDataEntry.Rows.Clear();
            //CurrentTrack.
        }

        public void DGVDataEntry_addRow(string name, Decimal lat, Decimal lon)
        {
            string[] row = new string[9];
            for (int i = 0; i < row.Length; i++)
            {
                row[i] = string.Empty;
            }
            DataGridViewRowCollection rows = this.DGVDataEntry.Rows;
            int n = rows.Count;
            row[0] = name;
            row[5] = String.Format("{0:0.00000}", lat);
            row[6] = String.Format("{0:0.00000}", lon);
            rows.Add(row);
            //DataGridViewRow arow = rows[n];
            //arow.Cells["latitude"] = new DataGridViewCell(String.Format("{0:0.00}", lat));

        }
        

        private void DGVDataEntry_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(String.Format("DGV Cell ClickResize Screen: Width {0}, Height {1}, Splitter Width {2}, Splitter Height {3}",
                this.Width, this.Height, this.splitContainer_GPX.Width, this.splitContainer_GPX.Height));
        }

        private void DGVDataEntry_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int coliDx = e.ColumnIndex;
            int rowiDx = e.RowIndex;
            if (rowiDx > 0)
            {
                DataGridViewRow row = this.DGVDataEntry.Rows[rowiDx];
                Console.WriteLine(String.Format("DGV Cell Click row {0}, coliDx {1} ", row, coliDx));
            }
        }

        private void DGVDataEntry_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            
            int coliDx = e.ColumnIndex;
            int rowiDx = e.RowIndex;
            ValidateDGVDataEntry();
            
        }

        private void DGVDataEntry_RowLeave_1(object sender, DataGridViewCellEventArgs e)
        {
            int coliDx = e.ColumnIndex;
            int rowiDx = e.RowIndex;
            string lat, lon;
            DataGridViewRow row = this.DGVDataEntry.Rows[rowiDx];
            string landranger = "";
            string bng6 = row.Cells["BNG6"].Value.ToString();
            string easting = row.Cells["easting"].Value.ToString();
            string northing = row.Cells["northing"].Value.ToString();
            
            Console.WriteLine(String.Format("DGV Leave row {0}, coliDx {1} ", row, coliDx));
           // row.Cells[1].Value = "Mumble";
            //
            if (bng6.Equals("") &&
                !easting.Equals("") &&
                !northing.Equals("")) 
                row.Cells["BNG6"].Value = easting + northing;

            landranger =
                (string)row.Cells["OSSheet"].Value +
                (string)row.Cells["BNG6"].Value;

            OSGB.getosbg(landranger, out lat, out lon);
            row.Cells["latitude"].Value = lat;
            row.Cells["longitude"].Value = lon;

        }

        /// <summary>
        /// Walk over the whole DataEntry DataGridView  and check all fields are completed
        /// </summary>
        private void ValidateDGVDataEntry()
        {
            String defaultsheet = "NT";    //some default
            string lat, lon;

            foreach (DataGridViewRow row in this.DGVDataEntry.Rows)
            {
                string val = (string)row.Cells["OSSheet"].Value;
                if (val != null && !val.Equals("")) defaultsheet = val;
                else row.Cells["OSSheet"].Value = defaultsheet;

                string landranger = defaultsheet;
                string bng6 = (string)row.Cells["BNG6"].Value;
                string easting = (string)row.Cells["easting"].Value;
                string northing = (string)row.Cells["northing"].Value;             
                //
                if (easting != null && northing != null &&
                    !easting.Equals("") && !northing.Equals(""))
                    row.Cells["BNG6"].Value = easting + northing;

                landranger =
                    (string)row.Cells["OSSheet"].Value +
                    (string)row.Cells["BNG6"].Value;

                OSGB.getosbg(landranger, out lat, out lon);
                row.Cells["latitude"].Value = lat;
                row.Cells["longitude"].Value = lon;

            }
        }

        private void lblTracks_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStriplblTracks.Show();
            }
        }

        /// <summary>
        /// Walk the DGVDataEntry and return the lats and lons as an array
        /// </summary>
        /// <returns></returns>
        private decimal[] getCoordsfromDataEntry()
        {
            decimal[] points = new decimal[DGVDataEntry.RowCount * 2];
            int i = 0;

            foreach (DataGridViewRow row in this.DGVDataEntry.Rows)
            {
                string valLat = row.Cells["latitude"].Value.ToString();
                string valLon = row.Cells["longitude"].Value.ToString();
                if (valLon.Equals("") && valLat.Equals("")) continue;

                points[i] = (valLat.Equals("")) ? 0 : decimal.Parse(valLat);
                points[i + 1] = (valLon.Equals("")) ? 0 : decimal.Parse(valLon);
                i += 2;
            }
            return points;
        }

        /// <summary>
        /// Gets the waypoint labels from the DGV as an array.
        /// (will plonk these into a track object later on)
        /// </summary>
        /// <returns>waypoint labels</returns>
        private string[] getLabelsfromDataEntry()
        {
            string[] labels = new string[DGVDataEntry.RowCount];
            int i = 0;

            foreach (DataGridViewRow row in this.DGVDataEntry.Rows)
            {
                string rc = row.Cells["WPName"].EditedFormattedValue.ToString();
                labels[i++] = rc;
            }
            return labels;
        }

        //see http://www.codeguru.com/csharp/csharp/cs_graphics/mouse/article.php/c6133/
        private void lblTracks_MouseDown(object sender, MouseEventArgs e)
        {
            int nDx = e.Y / lblTracks.ItemHeight; //Get Index of item selected
            int top = lblTracks.TopIndex;       // relative to current top item
            lblTracks.SetSelected(nDx +top, true);

            if (e.Button == MouseButtons.Right)
            { //Check for right button click
                this.contextMenuStriplblTracks.Visible = true;
                this.contextMenuStriplblTracks.Show((Control)sender, e.X, e.Y);
                //  MessageBeep(16);
                // m_mouse_event.Text = "Right Button Clicked\r\n";
            }
        }



        private void RenameTracktoolStripMenuItem2_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Rename: " + lblTracks.SelectedItem.ToString());
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string trackName = lblTracks.SelectedItem.ToString();
            DialogResult dlg =
                MessageBox.Show("Really delete " + trackName + "?",
    "Delete track " + trackName,
    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dlg == DialogResult.Yes)
            {
                CurrentTrack.DeleteTrack(lblTracks.SelectedItem.ToString());
                lblTracks.Items.Remove(lblTracks.SelectedItem);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lblTracks.SelectedItem != null)
            {
                string trackName = lblTracks.SelectedItem.ToString();
                saveTrackRoute(trackName, "track");
            }
            else
            {
                if (CurrentTrack == null)
                {
                    decimal[] pts = getCoordsfromDataEntry();
                    string[] labels = getLabelsfromDataEntry();
                    CurrentTrack = new Track(pts, labels);
                    //  CurrentTrack.
                }
                saveTrackRoute("generated_route", "route");
            }
        }

        /// <summary>
        /// save a track or a route -- form part
        /// </summary>
        /// <param name="trackName"></param>
        private void saveTrackRoute(string trackName, string trackOrRoute)
        {
            aSaveFileDialogue.Title = "Save " + trackName + "?";
            aSaveFileDialogue.DefaultExt = "gpx";
            aSaveFileDialogue.Filter = "gpx files (*.gpx)|*.gpx";
            aSaveFileDialogue.FileName = trackName + ".gpx";

            try
            {
                if (aSaveFileDialogue.ShowDialog() == DialogResult.OK)
                {
                    string filename = aSaveFileDialogue.FileName;
                    string path = Path.GetDirectoryName(filename);
                    if (CurrentTrack != null && trackName != "")
                        CurrentTrack.Save(filename, trackName, trackOrRoute);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File could not be saved. Reason: \n\n\"" + ex.Message + "\"");
            }
        }

        private void SaveFromContextMenu_Click(object sender, EventArgs e)
        {
            string trackName = lblTracks.SelectedItem.ToString();
            saveTrackRoute(trackName, "track");  
        }

        private void DGVDataEntry_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("Drag enter");
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                //If CTRL was pressed to a copy else move
                if ((e.KeyState & 8) == 8)
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void DGVDataEntry_DragDrop(object sender, DragEventArgs e)
        {
            string s = e.Data.GetData(DataFormats.Text).ToString();
            Console.WriteLine("Drop enter");
        }

        private void GPXAnalyzer_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileDrop", false))
            {
                string[] paths = (string[])(e.Data.GetData("FileDrop", false));
                foreach (string path in paths)
                {
                    Console.WriteLine(path);
                }
            }
        }

        private void GPXAnalyzer_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileDrop", false)) e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        /// <summary>
        /// Called when the text in the descrition box is changed. 
        /// This happens on paste as well as DnD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RTB_DataEntry_TextChanged(object sender, EventArgs e)
        {
            ArrayList coords = new ArrayList();
            Console.WriteLine("RTB: " + RTB_DataEntry.Text);
            coords = OSGB.parseGridRef(RTB_DataEntry.Text);
            // extract_EN_from_Text(RTB_DataEntry.Text);
        }

        /// <summary>
        /// return an array of Landranger Strings exctracted from the text
        /// </summary>
        /// <param name="text_containing_coords"></param>
        /// <returns></returns>
        private ArrayList Coordinate_Parser(string text_containing_coords)
        {
            return null;
        }


        /*
        private void extract_EN_from_Text(String text)
        {
          //  string rest = OSGB.parseGridRef(text);
            Console.WriteLine("rest" + rest);
        }
        */


    }
}
