namespace TEN.Forms
{
	partial class FrmMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
			this.menu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.restartToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.contentsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusBar = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripSimulator = new System.Windows.Forms.ToolStrip();
			this.btnStart = new System.Windows.Forms.ToolStripButton();
			this.btnPause = new System.Windows.Forms.ToolStripButton();
			this.btnStop = new System.Windows.Forms.ToolStripButton();
			this.btnRestart = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnPointer = new System.Windows.Forms.ToolStripButton();
			this.btnNewRoad = new System.Windows.Forms.ToolStripButton();
			this.btnNewTrafficLight = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.btnDelete = new System.Windows.Forms.ToolStripButton();
			this.btnConfigure = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
			this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
			this.mapDrawer = new TEN.Forms.MapDrawer();
			this.menu.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.toolStripSimulator.SuspendLayout();
			this.SuspendLayout();
			// 
			// menu
			// 
			this.menu.Dock = System.Windows.Forms.DockStyle.None;
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.editToolStripMenuItem1,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem1});
			this.menu.Location = new System.Drawing.Point(0, 0);
			this.menu.Name = "menu";
			this.menu.Size = new System.Drawing.Size(484, 24);
			this.menu.TabIndex = 1;
			this.menu.Text = "menu";
			// 
			// fileToolStripMenuItem1
			// 
			this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator9,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
			this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem1.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
			this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Enabled = false;
			this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
			this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.openToolStripMenuItem.Text = "&Open";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Enabled = false;
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Enabled = false;
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveAsToolStripMenuItem.Text = "Save &As";
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(149, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem1
			// 
			this.editToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem1,
            this.redoToolStripMenuItem1});
			this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
			this.editToolStripMenuItem1.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem1.Text = "&Edit";
			// 
			// undoToolStripMenuItem1
			// 
			this.undoToolStripMenuItem1.Enabled = false;
			this.undoToolStripMenuItem1.Name = "undoToolStripMenuItem1";
			this.undoToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.undoToolStripMenuItem1.Text = "&Undo";
			// 
			// redoToolStripMenuItem1
			// 
			this.redoToolStripMenuItem1.Enabled = false;
			this.redoToolStripMenuItem1.Name = "redoToolStripMenuItem1";
			this.redoToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.redoToolStripMenuItem1.Text = "&Redo";
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.stopToolStripMenuItem1,
            this.restartToolStripMenuItem1,
            this.toolStripSeparator3,
            this.reportToolStripMenuItem,
            this.toolStripSeparator13,
            this.optionsToolStripMenuItem1});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
			this.toolsToolStripMenuItem.Text = "&Simulation";
			// 
			// playToolStripMenuItem
			// 
			this.playToolStripMenuItem.Name = "playToolStripMenuItem";
			this.playToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.playToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.playToolStripMenuItem.Text = "&Play";
			this.playToolStripMenuItem.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// pauseToolStripMenuItem
			// 
			this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
			this.pauseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.pauseToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.pauseToolStripMenuItem.Text = "Paus&e";
			this.pauseToolStripMenuItem.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// stopToolStripMenuItem1
			// 
			this.stopToolStripMenuItem1.Name = "stopToolStripMenuItem1";
			this.stopToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
			this.stopToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
			this.stopToolStripMenuItem1.Text = "&Stop";
			this.stopToolStripMenuItem1.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// restartToolStripMenuItem1
			// 
			this.restartToolStripMenuItem1.Name = "restartToolStripMenuItem1";
			this.restartToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
			this.restartToolStripMenuItem1.Text = "&Restart";
			this.restartToolStripMenuItem1.Click += new System.EventHandler(this.btnRestart_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(152, 6);
			// 
			// reportToolStripMenuItem
			// 
			this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
			this.reportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
			this.reportToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.reportToolStripMenuItem.Text = "Rep&ort";
			this.reportToolStripMenuItem.Click += new System.EventHandler(this.reportToolStripMenuItem_Click);
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(152, 6);
			// 
			// optionsToolStripMenuItem1
			// 
			this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
			this.optionsToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
			this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
			this.optionsToolStripMenuItem1.Text = "&Options";
			this.optionsToolStripMenuItem1.Click += new System.EventHandler(this.optionsToolStripMenuItem1_Click);
			// 
			// helpToolStripMenuItem1
			// 
			this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem1,
            this.toolStripSeparator12,
            this.aboutToolStripMenuItem1});
			this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
			this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem1.Text = "&Help";
			// 
			// contentsToolStripMenuItem1
			// 
			this.contentsToolStripMenuItem1.Enabled = false;
			this.contentsToolStripMenuItem1.Name = "contentsToolStripMenuItem1";
			this.contentsToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.contentsToolStripMenuItem1.Text = "&Contents";
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(149, 6);
			// 
			// aboutToolStripMenuItem1
			// 
			this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
			this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.aboutToolStripMenuItem1.Text = "&About...";
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.AutoScroll = true;
			this.toolStripContainer1.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.mapDrawer);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(484, 181);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(484, 264);
			this.toolStripContainer1.TabIndex = 2;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menu);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripSimulator);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBar});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(484, 22);
			this.statusStrip1.TabIndex = 0;
			// 
			// statusBar
			// 
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(80, 17);
			this.statusBar.Text = "Traffic Engine";
			// 
			// toolStripSimulator
			// 
			this.toolStripSimulator.BackColor = System.Drawing.SystemColors.Control;
			this.toolStripSimulator.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStripSimulator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripSimulator.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStripSimulator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart,
            this.btnPause,
            this.btnStop,
            this.btnRestart,
            this.toolStripSeparator1,
            this.btnPointer,
            this.btnNewRoad,
            this.btnNewTrafficLight,
            this.toolStripSeparator4,
            this.btnDelete,
            this.btnConfigure,
            this.toolStripSeparator5,
            this.btnZoomIn,
            this.btnZoomOut});
			this.toolStripSimulator.Location = new System.Drawing.Point(0, 24);
			this.toolStripSimulator.Name = "toolStripSimulator";
			this.toolStripSimulator.Padding = new System.Windows.Forms.Padding(0);
			this.toolStripSimulator.Size = new System.Drawing.Size(484, 37);
			this.toolStripSimulator.Stretch = true;
			this.toolStripSimulator.TabIndex = 2;
			// 
			// btnStart
			// 
			this.btnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
			this.btnStart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(34, 34);
			this.btnStart.Text = "Start Simulation";
			this.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnPause
			// 
			this.btnPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
			this.btnPause.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(34, 34);
			this.btnPause.Text = "Pause Simulation";
			this.btnPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// btnStop
			// 
			this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
			this.btnStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(34, 34);
			this.btnStop.Text = "Stop Simulation";
			this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnRestart
			// 
			this.btnRestart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnRestart.Image = ((System.Drawing.Image)(resources.GetObject("btnRestart.Image")));
			this.btnRestart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnRestart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnRestart.Name = "btnRestart";
			this.btnRestart.Size = new System.Drawing.Size(34, 34);
			this.btnRestart.Text = "Restart Simulation";
			this.btnRestart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 37);
			// 
			// btnPointer
			// 
			this.btnPointer.Checked = true;
			this.btnPointer.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btnPointer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnPointer.Image = global::TEN.Properties.Resources.Cursor;
			this.btnPointer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnPointer.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPointer.Name = "btnPointer";
			this.btnPointer.Size = new System.Drawing.Size(34, 34);
			this.btnPointer.Text = "Pointer";
			this.btnPointer.Click += new System.EventHandler(this.btnPointer_Click);
			// 
			// btnNewRoad
			// 
			this.btnNewRoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnNewRoad.Image = global::TEN.Properties.Resources.Button_Add;
			this.btnNewRoad.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnNewRoad.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnNewRoad.Name = "btnNewRoad";
			this.btnNewRoad.Size = new System.Drawing.Size(34, 34);
			this.btnNewRoad.Text = "New Road";
			this.btnNewRoad.Click += new System.EventHandler(this.btnNewRoad_Click);
			// 
			// btnNewTrafficLight
			// 
			this.btnNewTrafficLight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnNewTrafficLight.Image = global::TEN.Properties.Resources.XP_traffic_lights_red;
			this.btnNewTrafficLight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnNewTrafficLight.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnNewTrafficLight.Name = "btnNewTrafficLight";
			this.btnNewTrafficLight.Size = new System.Drawing.Size(34, 34);
			this.btnNewTrafficLight.Text = "New Traffic Light";
			this.btnNewTrafficLight.Click += new System.EventHandler(this.btnNewTrafficLight_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 37);
			// 
			// btnDelete
			// 
			this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnDelete.Image = global::TEN.Properties.Resources.Button_Close;
			this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(34, 34);
			this.btnDelete.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnConfigure
			// 
			this.btnConfigure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnConfigure.Image = global::TEN.Properties.Resources.objects_misc_gears;
			this.btnConfigure.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnConfigure.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnConfigure.Name = "btnConfigure";
			this.btnConfigure.Size = new System.Drawing.Size(34, 34);
			this.btnConfigure.Text = "Configure";
			this.btnConfigure.Click += new System.EventHandler(this.btnConfigure_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 37);
			// 
			// btnZoomIn
			// 
			this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnZoomIn.Image = global::TEN.Properties.Resources.zoom_more;
			this.btnZoomIn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnZoomIn.Name = "btnZoomIn";
			this.btnZoomIn.Size = new System.Drawing.Size(34, 34);
			this.btnZoomIn.Text = "Zoom In";
			this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
			// 
			// btnZoomOut
			// 
			this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnZoomOut.Image = global::TEN.Properties.Resources.zoom_less;
			this.btnZoomOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnZoomOut.Name = "btnZoomOut";
			this.btnZoomOut.Size = new System.Drawing.Size(34, 34);
			this.btnZoomOut.Text = "Zoom Out";
			this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
			// 
			// mapDrawer
			// 
			this.mapDrawer.AutoScroll = true;
			this.mapDrawer.AutoScrollMinSize = new System.Drawing.Size(10000, 10000);
			this.mapDrawer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapDrawer.HoveredEdge = null;
			this.mapDrawer.Location = new System.Drawing.Point(0, 0);
			this.mapDrawer.Name = "mapDrawer";
			this.mapDrawer.SelectedEdge = null;
			this.mapDrawer.Size = new System.Drawing.Size(482, 179);
			this.mapDrawer.SnapOffset = 12;
			this.mapDrawer.TabIndex = 0;
			this.mapDrawer.Text = "mapDrawer";
			this.mapDrawer.Zoom = 1F;
			this.mapDrawer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mapDrawer_KeyDown);
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 264);
			this.Controls.Add(this.toolStripContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menu;
			this.MinimumSize = new System.Drawing.Size(500, 300);
			this.Name = "FrmMain";
			this.Text = "Traffic Engine";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FrmMain_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
			this.menu.ResumeLayout(false);
			this.menu.PerformLayout();
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStripSimulator.ResumeLayout(false);
			this.toolStripSimulator.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MenuStrip menu;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel statusBar;
		private System.Windows.Forms.ToolStrip toolStripSimulator;
		private System.Windows.Forms.ToolStripButton btnStart;
		private MapDrawer mapDrawer;
		private System.Windows.Forms.ToolStripButton btnPause;
		private System.Windows.Forms.ToolStripButton btnStop;
		private System.Windows.Forms.ToolStripButton btnRestart;
		private System.Windows.Forms.ToolStripButton btnPointer;
		private System.Windows.Forms.ToolStripButton btnNewRoad;
		private System.Windows.Forms.ToolStripButton btnNewTrafficLight;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton btnDelete;
		private System.Windows.Forms.ToolStripButton btnConfigure;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton btnZoomIn;
		private System.Windows.Forms.ToolStripButton btnZoomOut;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
	}
}

