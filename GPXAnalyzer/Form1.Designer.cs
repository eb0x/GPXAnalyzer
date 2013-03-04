namespace GPXAnalyzer
{
    partial class GPXAnalyzer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGPX = new System.Windows.Forms.TabPage();
            this.panelGPX = new System.Windows.Forms.Panel();
            this.splitContainer_GPX = new System.Windows.Forms.SplitContainer();
            this.TrackDatasplitContainer = new System.Windows.Forms.SplitContainer();
            this.labelTracks = new System.Windows.Forms.Label();
            this.lblTracks = new System.Windows.Forms.ListBox();
            this.lblPoints = new System.Windows.Forms.ListBox();
            this.labelPoints = new System.Windows.Forms.Label();
            this.pnlMap = new System.Windows.Forms.Panel();
            this.Profiler = new System.Windows.Forms.TabPage();
            this.pnlProfile = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tabTabular = new System.Windows.Forms.TabPage();
            this.DGVTrack = new System.Windows.Forms.DataGridView();
            this.tabDataEntry = new System.Windows.Forms.TabPage();
            this.splitContainer_DataEntry = new System.Windows.Forms.SplitContainer();
            this.RTB_DataEntry = new System.Windows.Forms.RichTextBox();
            this.DGVDataEntry = new System.Windows.Forms.DataGridView();
            this.WPName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OSSheet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BNG6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Easting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Northing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Latitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Longitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStriplblTracks = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.RenameTracktoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveFromContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tabControl1.SuspendLayout();
            this.tabGPX.SuspendLayout();
            this.panelGPX.SuspendLayout();
            this.splitContainer_GPX.Panel1.SuspendLayout();
            this.splitContainer_GPX.Panel2.SuspendLayout();
            this.splitContainer_GPX.SuspendLayout();
            this.TrackDatasplitContainer.Panel1.SuspendLayout();
            this.TrackDatasplitContainer.Panel2.SuspendLayout();
            this.TrackDatasplitContainer.SuspendLayout();
            this.Profiler.SuspendLayout();
            this.tabTabular.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVTrack)).BeginInit();
            this.tabDataEntry.SuspendLayout();
            this.splitContainer_DataEntry.Panel1.SuspendLayout();
            this.splitContainer_DataEntry.Panel2.SuspendLayout();
            this.splitContainer_DataEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVDataEntry)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStriplblTracks.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGPX);
            this.tabControl1.Controls.Add(this.Profiler);
            this.tabControl1.Controls.Add(this.tabTabular);
            this.tabControl1.Controls.Add(this.tabDataEntry);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 29);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1008, 447);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabGPX
            // 
            this.tabGPX.Controls.Add(this.panelGPX);
            this.tabGPX.Location = new System.Drawing.Point(4, 28);
            this.tabGPX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabGPX.Name = "tabGPX";
            this.tabGPX.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabGPX.Size = new System.Drawing.Size(1000, 415);
            this.tabGPX.TabIndex = 0;
            this.tabGPX.Text = "GPX Data";
            this.tabGPX.UseVisualStyleBackColor = true;
            // 
            // panelGPX
            // 
            this.panelGPX.Controls.Add(this.splitContainer_GPX);
            this.panelGPX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGPX.Location = new System.Drawing.Point(3, 4);
            this.panelGPX.Name = "panelGPX";
            this.panelGPX.Size = new System.Drawing.Size(994, 407);
            this.panelGPX.TabIndex = 0;
            // 
            // splitContainer_GPX
            // 
            this.splitContainer_GPX.BackColor = System.Drawing.Color.LemonChiffon;
            this.splitContainer_GPX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer_GPX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_GPX.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_GPX.Name = "splitContainer_GPX";
            // 
            // splitContainer_GPX.Panel1
            // 
            this.splitContainer_GPX.Panel1.Controls.Add(this.TrackDatasplitContainer);
            this.splitContainer_GPX.Panel1.Padding = new System.Windows.Forms.Padding(6, 0, 6, 6);
            this.splitContainer_GPX.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
            // 
            // splitContainer_GPX.Panel2
            // 
            this.splitContainer_GPX.Panel2.Controls.Add(this.pnlMap);
            this.splitContainer_GPX.Panel2.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
            this.splitContainer_GPX.Size = new System.Drawing.Size(994, 407);
            this.splitContainer_GPX.SplitterDistance = 329;
            this.splitContainer_GPX.SplitterWidth = 2;
            this.splitContainer_GPX.TabIndex = 0;
            // 
            // TrackDatasplitContainer
            // 
            this.TrackDatasplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrackDatasplitContainer.Location = new System.Drawing.Point(6, 0);
            this.TrackDatasplitContainer.Name = "TrackDatasplitContainer";
            this.TrackDatasplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // TrackDatasplitContainer.Panel1
            // 
            this.TrackDatasplitContainer.Panel1.Controls.Add(this.labelTracks);
            this.TrackDatasplitContainer.Panel1.Controls.Add(this.lblTracks);
            // 
            // TrackDatasplitContainer.Panel2
            // 
            this.TrackDatasplitContainer.Panel2.Controls.Add(this.lblPoints);
            this.TrackDatasplitContainer.Panel2.Controls.Add(this.labelPoints);
            this.TrackDatasplitContainer.Size = new System.Drawing.Size(315, 399);
            this.TrackDatasplitContainer.SplitterDistance = 154;
            this.TrackDatasplitContainer.TabIndex = 0;
            // 
            // labelTracks
            // 
            this.labelTracks.AutoSize = true;
            this.labelTracks.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTracks.Location = new System.Drawing.Point(0, 0);
            this.labelTracks.Margin = new System.Windows.Forms.Padding(5);
            this.labelTracks.Name = "labelTracks";
            this.labelTracks.Size = new System.Drawing.Size(59, 21);
            this.labelTracks.TabIndex = 0;
            this.labelTracks.Text = "Tracks";
            this.labelTracks.Click += new System.EventHandler(this.labelTracks_Click);
            // 
            // lblTracks
            // 
            this.lblTracks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTracks.FormattingEnabled = true;
            this.lblTracks.ItemHeight = 19;
            this.lblTracks.Location = new System.Drawing.Point(0, 74);
            this.lblTracks.Margin = new System.Windows.Forms.Padding(3, 3, 3, 30);
            this.lblTracks.Name = "lblTracks";
            this.lblTracks.Size = new System.Drawing.Size(315, 80);
            this.lblTracks.TabIndex = 1;
            this.lblTracks.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblTracks_MouseClick);
            this.lblTracks.SelectedIndexChanged += new System.EventHandler(this.lblTracks_SelectedIndexChanged);
            this.lblTracks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTracks_MouseDown);
            // 
            // lblPoints
            // 
            this.lblPoints.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPoints.FormattingEnabled = true;
            this.lblPoints.ItemHeight = 19;
            this.lblPoints.Location = new System.Drawing.Point(0, 123);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(315, 118);
            this.lblPoints.TabIndex = 3;
            // 
            // labelPoints
            // 
            this.labelPoints.AutoSize = true;
            this.labelPoints.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelPoints.Location = new System.Drawing.Point(0, 0);
            this.labelPoints.Margin = new System.Windows.Forms.Padding(5);
            this.labelPoints.Name = "labelPoints";
            this.labelPoints.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.labelPoints.Size = new System.Drawing.Size(55, 36);
            this.labelPoints.TabIndex = 2;
            this.labelPoints.Text = "Points";
            // 
            // pnlMap
            // 
            this.pnlMap.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMap.Location = new System.Drawing.Point(0, 69);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(661, 336);
            this.pnlMap.TabIndex = 0;
            this.pnlMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMap_Paint);
            this.pnlMap.Resize += new System.EventHandler(this.pnlMap_Resize);
            // 
            // Profiler
            // 
            this.Profiler.Controls.Add(this.pnlProfile);
            this.Profiler.Controls.Add(this.toolStrip1);
            this.Profiler.Location = new System.Drawing.Point(4, 28);
            this.Profiler.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Profiler.Name = "Profiler";
            this.Profiler.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Profiler.Size = new System.Drawing.Size(1000, 415);
            this.Profiler.TabIndex = 1;
            this.Profiler.Text = "Profiler";
            this.Profiler.ToolTipText = "Graphs and Data Tables of the selected track";
            this.Profiler.UseVisualStyleBackColor = true;
            // 
            // pnlProfile
            // 
            this.pnlProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProfile.Location = new System.Drawing.Point(3, 29);
            this.pnlProfile.Name = "pnlProfile";
            this.pnlProfile.Size = new System.Drawing.Size(994, 382);
            this.pnlProfile.TabIndex = 1;
            this.pnlProfile.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlProfile_Paint);
            this.pnlProfile.Resize += new System.EventHandler(this.pnlProfile_Resize);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(3, 4);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(994, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tabTabular
            // 
            this.tabTabular.BackColor = System.Drawing.Color.LemonChiffon;
            this.tabTabular.Controls.Add(this.DGVTrack);
            this.tabTabular.Location = new System.Drawing.Point(4, 28);
            this.tabTabular.Name = "tabTabular";
            this.tabTabular.Size = new System.Drawing.Size(1000, 415);
            this.tabTabular.TabIndex = 2;
            this.tabTabular.Text = "Review Table";
            this.tabTabular.ToolTipText = "See track data in table format ";
            this.tabTabular.UseVisualStyleBackColor = true;
            // 
            // DGVTrack
            // 
            this.DGVTrack.AllowUserToAddRows = false;
            this.DGVTrack.AllowUserToDeleteRows = false;
            this.DGVTrack.AllowUserToOrderColumns = true;
            this.DGVTrack.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.DGVTrack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVTrack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGVTrack.Location = new System.Drawing.Point(0, 0);
            this.DGVTrack.Name = "DGVTrack";
            this.DGVTrack.ReadOnly = true;
            this.DGVTrack.RowTemplate.Height = 24;
            this.DGVTrack.Size = new System.Drawing.Size(1000, 415);
            this.DGVTrack.TabIndex = 0;
            // 
            // tabDataEntry
            // 
            this.tabDataEntry.Controls.Add(this.splitContainer_DataEntry);
            this.tabDataEntry.Location = new System.Drawing.Point(4, 28);
            this.tabDataEntry.Name = "tabDataEntry";
            this.tabDataEntry.Size = new System.Drawing.Size(1000, 415);
            this.tabDataEntry.TabIndex = 3;
            this.tabDataEntry.Text = "Data Entry";
            this.tabDataEntry.UseVisualStyleBackColor = true;
            // 
            // splitContainer_DataEntry
            // 
            this.splitContainer_DataEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_DataEntry.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_DataEntry.Name = "splitContainer_DataEntry";
            // 
            // splitContainer_DataEntry.Panel1
            // 
            this.splitContainer_DataEntry.Panel1.Controls.Add(this.RTB_DataEntry);
            // 
            // splitContainer_DataEntry.Panel2
            // 
            this.splitContainer_DataEntry.Panel2.Controls.Add(this.DGVDataEntry);
            this.splitContainer_DataEntry.Size = new System.Drawing.Size(1000, 415);
            this.splitContainer_DataEntry.SplitterDistance = 249;
            this.splitContainer_DataEntry.TabIndex = 0;
            this.splitContainer_DataEntry.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            // 
            // RTB_DataEntry
            // 
            this.RTB_DataEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTB_DataEntry.EnableAutoDragDrop = true;
            this.RTB_DataEntry.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RTB_DataEntry.Location = new System.Drawing.Point(0, 0);
            this.RTB_DataEntry.Name = "RTB_DataEntry";
            this.RTB_DataEntry.Size = new System.Drawing.Size(249, 415);
            this.RTB_DataEntry.TabIndex = 0;
            this.RTB_DataEntry.Text = "Drag your text description of a route here and the coordinates will be inserted i" +
                "nto the table";
            this.RTB_DataEntry.TextChanged += new System.EventHandler(this.RTB_DataEntry_TextChanged);
            // 
            // DGVDataEntry
            // 
            this.DGVDataEntry.AllowUserToOrderColumns = true;
            this.DGVDataEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVDataEntry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WPName,
            this.OSSheet,
            this.BNG6,
            this.Easting,
            this.Northing,
            this.Latitude,
            this.Longitude});
            this.DGVDataEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGVDataEntry.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.DGVDataEntry.Location = new System.Drawing.Point(0, 0);
            this.DGVDataEntry.Name = "DGVDataEntry";
            this.DGVDataEntry.RowTemplate.Height = 24;
            this.DGVDataEntry.Size = new System.Drawing.Size(747, 415);
            this.DGVDataEntry.TabIndex = 1;
            this.DGVDataEntry.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVDataEntry_RowLeave);
            this.DGVDataEntry.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVDataEntry_CellClick);
            // 
            // WPName
            // 
            this.WPName.HeaderText = "WP Name";
            this.WPName.Name = "WPName";
            // 
            // OSSheet
            // 
            this.OSSheet.HeaderText = "OS Sheet";
            this.OSSheet.Name = "OSSheet";
            // 
            // BNG6
            // 
            this.BNG6.HeaderText = "BNG 6 digit";
            this.BNG6.Name = "BNG6";
            // 
            // Easting
            // 
            this.Easting.HeaderText = "Easting";
            this.Easting.Name = "Easting";
            // 
            // Northing
            // 
            this.Northing.HeaderText = "Northing";
            this.Northing.Name = "Northing";
            // 
            // Latitude
            // 
            this.Latitude.HeaderText = "Latitude";
            this.Latitude.Name = "Latitude";
            // 
            // Longitude
            // 
            this.Longitude.HeaderText = "Longitude";
            this.Longitude.Name = "Longitude";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.windowMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1008, 29);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewtoolStripMenuItem,
            this.openMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(48, 25);
            this.fileMenuItem.Text = "File";
            // 
            // NewtoolStripMenuItem
            // 
            this.NewtoolStripMenuItem.Name = "NewtoolStripMenuItem";
            this.NewtoolStripMenuItem.Size = new System.Drawing.Size(119, 26);
            this.NewtoolStripMenuItem.Text = "New";
            this.NewtoolStripMenuItem.ToolTipText = "Create a New Route ";
            this.NewtoolStripMenuItem.Click += new System.EventHandler(this.NewtoolStripMenuItem_Click);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.Size = new System.Drawing.Size(119, 26);
            this.openMenuItem.Text = "Open";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(119, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(116, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(119, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(51, 25);
            this.editMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(58, 25);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.toolsToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(62, 25);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(113, 26);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // windowMenuItem
            // 
            this.windowMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowMenuItem.Name = "windowMenuItem";
            this.windowMenuItem.Size = new System.Drawing.Size(81, 25);
            this.windowMenuItem.Text = "Window";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 25);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(124, 26);
            this.aboutMenuItem.Text = "About";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Title = "Save a Route or Track";
            // 
            // contextMenuStriplblTracks
            // 
            this.contextMenuStriplblTracks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.toolStripSeparator5,
            this.RenameTracktoolStripMenuItem2,
            this.toolStripSeparator4,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.SaveFromContextMenu,
            this.toolStripSeparator3});
            this.contextMenuStriplblTracks.MinimumSize = new System.Drawing.Size(20, 20);
            this.contextMenuStriplblTracks.Name = "contextMenuStriplblTracks";
            this.contextMenuStriplblTracks.Size = new System.Drawing.Size(181, 124);
            this.contextMenuStriplblTracks.Text = "Track Menu";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(120, 28);
            this.toolStripTextBox1.Text = "Track Ops";
            this.toolStripTextBox1.ToolTipText = "Operations on the visible track";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
            // 
            // RenameTracktoolStripMenuItem2
            // 
            this.RenameTracktoolStripMenuItem2.Name = "RenameTracktoolStripMenuItem2";
            this.RenameTracktoolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.RenameTracktoolStripMenuItem2.Text = "Rename";
            this.RenameTracktoolStripMenuItem2.ToolTipText = "Rename the current track";
            this.RenameTracktoolStripMenuItem2.Click += new System.EventHandler(this.RenameTracktoolStripMenuItem2_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.AutoToolTip = true;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.ToolTipText = "Delete the Current Track";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.Yellow;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // SaveFromContextMenu
            // 
            this.SaveFromContextMenu.Name = "SaveFromContextMenu";
            this.SaveFromContextMenu.Size = new System.Drawing.Size(180, 22);
            this.SaveFromContextMenu.Text = "Save Just this";
            this.SaveFromContextMenu.Click += new System.EventHandler(this.SaveFromContextMenu_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // GPXAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 476);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "GPXAnalyzer";
            this.Text = "GPXAnalyzer";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.GPXAnalyzer_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.GPXAnalyzer_DragEnter);
            this.Resize += new System.EventHandler(this.GPXAnalyzer_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabGPX.ResumeLayout(false);
            this.panelGPX.ResumeLayout(false);
            this.splitContainer_GPX.Panel1.ResumeLayout(false);
            this.splitContainer_GPX.Panel2.ResumeLayout(false);
            this.splitContainer_GPX.ResumeLayout(false);
            this.TrackDatasplitContainer.Panel1.ResumeLayout(false);
            this.TrackDatasplitContainer.Panel1.PerformLayout();
            this.TrackDatasplitContainer.Panel2.ResumeLayout(false);
            this.TrackDatasplitContainer.Panel2.PerformLayout();
            this.TrackDatasplitContainer.ResumeLayout(false);
            this.Profiler.ResumeLayout(false);
            this.Profiler.PerformLayout();
            this.tabTabular.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVTrack)).EndInit();
            this.tabDataEntry.ResumeLayout(false);
            this.splitContainer_DataEntry.Panel1.ResumeLayout(false);
            this.splitContainer_DataEntry.Panel2.ResumeLayout(false);
            this.splitContainer_DataEntry.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVDataEntry)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStriplblTracks.ResumeLayout(false);
            this.contextMenuStriplblTracks.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGPX;
        private System.Windows.Forms.TabPage Profiler;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.Panel panelGPX;
        private System.Windows.Forms.SplitContainer splitContainer_GPX;
        private System.Windows.Forms.ListBox lblPoints;
        private System.Windows.Forms.Label labelPoints;
        private System.Windows.Forms.ListBox lblTracks;
        private System.Windows.Forms.Label labelTracks;
        private System.Windows.Forms.Panel pnlMap;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel pnlProfile;
        private System.Windows.Forms.SplitContainer TrackDatasplitContainer;
        private System.Windows.Forms.TabPage tabTabular;
        private System.Windows.Forms.DataGridView DGVTrack;
        private System.Windows.Forms.TabPage tabDataEntry;
        private System.Windows.Forms.ToolStripMenuItem NewtoolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStriplblTracks;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem SaveFromContextMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem RenameTracktoolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.SplitContainer splitContainer_DataEntry;
        private System.Windows.Forms.RichTextBox RTB_DataEntry;
        private System.Windows.Forms.DataGridView DGVDataEntry;
        private System.Windows.Forms.DataGridViewTextBoxColumn WPName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OSSheet;
        private System.Windows.Forms.DataGridViewTextBoxColumn BNG6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Easting;
        private System.Windows.Forms.DataGridViewTextBoxColumn Northing;
        private System.Windows.Forms.DataGridViewTextBoxColumn Latitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Longitude;
    }
}

