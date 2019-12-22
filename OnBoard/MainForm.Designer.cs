﻿namespace OnBoard
{
    partial class MainForm
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
            this.m_groupBoxTrainSettings = new System.Windows.Forms.GroupBox();
            this.m_listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_groupBoxRearCurrentTrack = new System.Windows.Forms.GroupBox();
            this.m_textBoxRearCurrentVirtualOccupationTrackLocation = new System.Windows.Forms.TextBox();
            this.m_labelRearCurrentTrackVirtualOccupationLocation = new System.Windows.Forms.Label();
            this.m_textBoxRearCurrentTrackVirtualOccupationTrackID = new System.Windows.Forms.TextBox();
            this.m_labelRearCurrentTrackVirtualOccupationTrackID = new System.Windows.Forms.Label();
            this.m_textBoxRearCurrentTrackSpeedChangeVMax = new System.Windows.Forms.TextBox();
            this.m_textBoxRearCurrentTrackID = new System.Windows.Forms.TextBox();
            this.m_labelRearCurrentTrackSpeedChangeVMax = new System.Windows.Forms.Label();
            this.m_labelRearCurrentTrackID = new System.Windows.Forms.Label();
            this.m_groupBoxFrontCurrentTrack = new System.Windows.Forms.GroupBox();
            this.m_textBoxFrontCurrentVirtualOccupationTrackLocation = new System.Windows.Forms.TextBox();
            this.m_labelFrontCurrentTrackVirtualOccupationLocation = new System.Windows.Forms.Label();
            this.m_textBoxFrontCurrentTrackVirtualOccupationTrackID = new System.Windows.Forms.TextBox();
            this.m_labelFrontCurrentTrackVirtualOccupationTrackID = new System.Windows.Forms.Label();
            this.m_textBoxFrontCurrentTrackMaxSpeedKM = new System.Windows.Forms.TextBox();
            this.m_labelFrontCurrentTrackMaxSpeedKM = new System.Windows.Forms.Label();
            this.m_textBoxFrontCurrentTrackSpeedChangeVMax = new System.Windows.Forms.TextBox();
            this.m_textBoxFrontCurrentTrackStation = new System.Windows.Forms.TextBox();
            this.m_textBoxFrontCurrentTrackDwellTime = new System.Windows.Forms.TextBox();
            this.m_textBoxFrontCurrentTrackLength = new System.Windows.Forms.TextBox();
            this.m_textBoxFrontCurrentTrackID = new System.Windows.Forms.TextBox();
            this.m_labelFrontCurrentTrackStation = new System.Windows.Forms.Label();
            this.m_labelFrontCurrentTrackSpeedChangeVMax = new System.Windows.Forms.Label();
            this.m_labelFrontCurrentTrackDwellTime = new System.Windows.Forms.Label();
            this.m_labelFrontCurrentTrackLength = new System.Windows.Forms.Label();
            this.m_labelFrontCurrentTrackID = new System.Windows.Forms.Label();
            this.m_textBoxDoorTimerCounter = new System.Windows.Forms.TextBox();
            this.m_labelDoorTimerCounter = new System.Windows.Forms.Label();
            this.m_comboBoxTrain = new System.Windows.Forms.ComboBox();
            this.m_textBoxCurrentLocation = new System.Windows.Forms.TextBox();
            this.m_textBoxDoorStatus = new System.Windows.Forms.TextBox();
            this.m_textBoxCurrentAcceleration = new System.Windows.Forms.TextBox();
            this.m_textBoxRearCurrentLocation = new System.Windows.Forms.TextBox();
            this.m_textBoxCurrentTrainSpeedKM = new System.Windows.Forms.TextBox();
            this.m_labelDoorStatus = new System.Windows.Forms.Label();
            this.m_labelCurrentLocation = new System.Windows.Forms.Label();
            this.m_labelCurrentAcceleration = new System.Windows.Forms.Label();
            this.m_labelRearCurrentLocation = new System.Windows.Forms.Label();
            this.m_labelCurrentTrainSpeedKM = new System.Windows.Forms.Label();
            this.m_labelTrains = new System.Windows.Forms.Label();
            this.m_buttonSave = new System.Windows.Forms.Button();
            this.m_buttonStart = new System.Windows.Forms.Button();
            this.m_dataGridViewAllTrains = new System.Windows.Forms.DataGridView();
            this.m_mainMenu = new System.Windows.Forms.MenuStrip();
            this.m_settingsPopup = new System.Windows.Forms.ToolStripMenuItem();
            this.m_generalItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_trainItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_communicationItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_trainSimItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_routeItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_listViewVirtualOccupation = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_richTextBox = new System.Windows.Forms.RichTextBox();
            this.m_listViewFootPrintTracks = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.m_groupBoxTrainSettings.SuspendLayout();
            this.m_groupBoxRearCurrentTrack.SuspendLayout();
            this.m_groupBoxFrontCurrentTrack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridViewAllTrains)).BeginInit();
            this.m_mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_groupBoxTrainSettings
            // 
            this.m_groupBoxTrainSettings.Controls.Add(this.m_listView);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_groupBoxRearCurrentTrack);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_groupBoxFrontCurrentTrack);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxDoorTimerCounter);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelDoorTimerCounter);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_comboBoxTrain);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxCurrentLocation);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxDoorStatus);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxCurrentAcceleration);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxRearCurrentLocation);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxCurrentTrainSpeedKM);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelDoorStatus);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelCurrentLocation);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelCurrentAcceleration);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelRearCurrentLocation);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelCurrentTrainSpeedKM);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelTrains);
            this.m_groupBoxTrainSettings.Location = new System.Drawing.Point(335, 241);
            this.m_groupBoxTrainSettings.Margin = new System.Windows.Forms.Padding(2);
            this.m_groupBoxTrainSettings.Name = "m_groupBoxTrainSettings";
            this.m_groupBoxTrainSettings.Padding = new System.Windows.Forms.Padding(2);
            this.m_groupBoxTrainSettings.Size = new System.Drawing.Size(720, 531);
            this.m_groupBoxTrainSettings.TabIndex = 2;
            this.m_groupBoxTrainSettings.TabStop = false;
            this.m_groupBoxTrainSettings.Text = "Train Settings";
            // 
            // m_listView
            // 
            this.m_listView.BackColor = System.Drawing.Color.White;
            this.m_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.m_listView.ForeColor = System.Drawing.Color.Black;
            this.m_listView.FullRowSelect = true;
            this.m_listView.GridLines = true;
            this.m_listView.HideSelection = false;
            this.m_listView.Location = new System.Drawing.Point(391, 133);
            this.m_listView.Margin = new System.Windows.Forms.Padding(2);
            this.m_listView.MultiSelect = false;
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(295, 383);
            this.m_listView.TabIndex = 26;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            this.m_listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Route Tracks";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Station";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 100;
            // 
            // m_groupBoxRearCurrentTrack
            // 
            this.m_groupBoxRearCurrentTrack.Controls.Add(this.m_textBoxRearCurrentVirtualOccupationTrackLocation);
            this.m_groupBoxRearCurrentTrack.Controls.Add(this.m_labelRearCurrentTrackVirtualOccupationLocation);
            this.m_groupBoxRearCurrentTrack.Controls.Add(this.m_textBoxRearCurrentTrackVirtualOccupationTrackID);
            this.m_groupBoxRearCurrentTrack.Controls.Add(this.m_labelRearCurrentTrackVirtualOccupationTrackID);
            this.m_groupBoxRearCurrentTrack.Controls.Add(this.m_textBoxRearCurrentTrackSpeedChangeVMax);
            this.m_groupBoxRearCurrentTrack.Controls.Add(this.m_textBoxRearCurrentTrackID);
            this.m_groupBoxRearCurrentTrack.Controls.Add(this.m_labelRearCurrentTrackSpeedChangeVMax);
            this.m_groupBoxRearCurrentTrack.Controls.Add(this.m_labelRearCurrentTrackID);
            this.m_groupBoxRearCurrentTrack.Location = new System.Drawing.Point(21, 374);
            this.m_groupBoxRearCurrentTrack.Margin = new System.Windows.Forms.Padding(2);
            this.m_groupBoxRearCurrentTrack.Name = "m_groupBoxRearCurrentTrack";
            this.m_groupBoxRearCurrentTrack.Padding = new System.Windows.Forms.Padding(2);
            this.m_groupBoxRearCurrentTrack.Size = new System.Drawing.Size(352, 141);
            this.m_groupBoxRearCurrentTrack.TabIndex = 14;
            this.m_groupBoxRearCurrentTrack.TabStop = false;
            this.m_groupBoxRearCurrentTrack.Text = "Rear Current Track";
            // 
            // m_textBoxRearCurrentVirtualOccupationTrackLocation
            // 
            this.m_textBoxRearCurrentVirtualOccupationTrackLocation.Location = new System.Drawing.Point(195, 102);
            this.m_textBoxRearCurrentVirtualOccupationTrackLocation.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxRearCurrentVirtualOccupationTrackLocation.Name = "m_textBoxRearCurrentVirtualOccupationTrackLocation";
            this.m_textBoxRearCurrentVirtualOccupationTrackLocation.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxRearCurrentVirtualOccupationTrackLocation.TabIndex = 28;
            // 
            // m_labelRearCurrentTrackVirtualOccupationLocation
            // 
            this.m_labelRearCurrentTrackVirtualOccupationLocation.AutoSize = true;
            this.m_labelRearCurrentTrackVirtualOccupationLocation.Location = new System.Drawing.Point(13, 102);
            this.m_labelRearCurrentTrackVirtualOccupationLocation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelRearCurrentTrackVirtualOccupationLocation.Name = "m_labelRearCurrentTrackVirtualOccupationLocation";
            this.m_labelRearCurrentTrackVirtualOccupationLocation.Size = new System.Drawing.Size(159, 13);
            this.m_labelRearCurrentTrackVirtualOccupationLocation.TabIndex = 27;
            this.m_labelRearCurrentTrackVirtualOccupationLocation.Text = "Vir. Occ. Front Track Location : ";
            // 
            // m_textBoxRearCurrentTrackVirtualOccupationTrackID
            // 
            this.m_textBoxRearCurrentTrackVirtualOccupationTrackID.Location = new System.Drawing.Point(195, 80);
            this.m_textBoxRearCurrentTrackVirtualOccupationTrackID.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxRearCurrentTrackVirtualOccupationTrackID.Name = "m_textBoxRearCurrentTrackVirtualOccupationTrackID";
            this.m_textBoxRearCurrentTrackVirtualOccupationTrackID.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxRearCurrentTrackVirtualOccupationTrackID.TabIndex = 26;
            // 
            // m_labelRearCurrentTrackVirtualOccupationTrackID
            // 
            this.m_labelRearCurrentTrackVirtualOccupationTrackID.AutoSize = true;
            this.m_labelRearCurrentTrackVirtualOccupationTrackID.Location = new System.Drawing.Point(44, 80);
            this.m_labelRearCurrentTrackVirtualOccupationTrackID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelRearCurrentTrackVirtualOccupationTrackID.Name = "m_labelRearCurrentTrackVirtualOccupationTrackID";
            this.m_labelRearCurrentTrackVirtualOccupationTrackID.Size = new System.Drawing.Size(129, 13);
            this.m_labelRearCurrentTrackVirtualOccupationTrackID.TabIndex = 25;
            this.m_labelRearCurrentTrackVirtualOccupationTrackID.Text = "Vir. Occ. Front Track ID : ";
            // 
            // m_textBoxRearCurrentTrackSpeedChangeVMax
            // 
            this.m_textBoxRearCurrentTrackSpeedChangeVMax.Location = new System.Drawing.Point(195, 57);
            this.m_textBoxRearCurrentTrackSpeedChangeVMax.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxRearCurrentTrackSpeedChangeVMax.Name = "m_textBoxRearCurrentTrackSpeedChangeVMax";
            this.m_textBoxRearCurrentTrackSpeedChangeVMax.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxRearCurrentTrackSpeedChangeVMax.TabIndex = 22;
            // 
            // m_textBoxRearCurrentTrackID
            // 
            this.m_textBoxRearCurrentTrackID.Location = new System.Drawing.Point(195, 34);
            this.m_textBoxRearCurrentTrackID.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxRearCurrentTrackID.Name = "m_textBoxRearCurrentTrackID";
            this.m_textBoxRearCurrentTrackID.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxRearCurrentTrackID.TabIndex = 18;
            // 
            // m_labelRearCurrentTrackSpeedChangeVMax
            // 
            this.m_labelRearCurrentTrackSpeedChangeVMax.AutoSize = true;
            this.m_labelRearCurrentTrackSpeedChangeVMax.Location = new System.Drawing.Point(46, 59);
            this.m_labelRearCurrentTrackSpeedChangeVMax.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelRearCurrentTrackSpeedChangeVMax.Name = "m_labelRearCurrentTrackSpeedChangeVMax";
            this.m_labelRearCurrentTrackSpeedChangeVMax.Size = new System.Drawing.Size(128, 13);
            this.m_labelRearCurrentTrackSpeedChangeVMax.TabIndex = 16;
            this.m_labelRearCurrentTrackSpeedChangeVMax.Text = "Map Max Speed (km/h) : ";
            // 
            // m_labelRearCurrentTrackID
            // 
            this.m_labelRearCurrentTrackID.AutoSize = true;
            this.m_labelRearCurrentTrackID.Location = new System.Drawing.Point(145, 34);
            this.m_labelRearCurrentTrackID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelRearCurrentTrackID.Name = "m_labelRearCurrentTrackID";
            this.m_labelRearCurrentTrackID.Size = new System.Drawing.Size(27, 13);
            this.m_labelRearCurrentTrackID.TabIndex = 13;
            this.m_labelRearCurrentTrackID.Text = "ID : ";
            // 
            // m_groupBoxFrontCurrentTrack
            // 
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_textBoxFrontCurrentVirtualOccupationTrackLocation);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_labelFrontCurrentTrackVirtualOccupationLocation);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_textBoxFrontCurrentTrackVirtualOccupationTrackID);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_labelFrontCurrentTrackVirtualOccupationTrackID);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_textBoxFrontCurrentTrackMaxSpeedKM);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_labelFrontCurrentTrackMaxSpeedKM);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_textBoxFrontCurrentTrackSpeedChangeVMax);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_textBoxFrontCurrentTrackStation);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_textBoxFrontCurrentTrackDwellTime);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_textBoxFrontCurrentTrackLength);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_textBoxFrontCurrentTrackID);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_labelFrontCurrentTrackStation);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_labelFrontCurrentTrackSpeedChangeVMax);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_labelFrontCurrentTrackDwellTime);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_labelFrontCurrentTrackLength);
            this.m_groupBoxFrontCurrentTrack.Controls.Add(this.m_labelFrontCurrentTrackID);
            this.m_groupBoxFrontCurrentTrack.Location = new System.Drawing.Point(21, 133);
            this.m_groupBoxFrontCurrentTrack.Margin = new System.Windows.Forms.Padding(2);
            this.m_groupBoxFrontCurrentTrack.Name = "m_groupBoxFrontCurrentTrack";
            this.m_groupBoxFrontCurrentTrack.Padding = new System.Windows.Forms.Padding(2);
            this.m_groupBoxFrontCurrentTrack.Size = new System.Drawing.Size(352, 236);
            this.m_groupBoxFrontCurrentTrack.TabIndex = 13;
            this.m_groupBoxFrontCurrentTrack.TabStop = false;
            this.m_groupBoxFrontCurrentTrack.Text = "Front Current Track";
            // 
            // m_textBoxFrontCurrentVirtualOccupationTrackLocation
            // 
            this.m_textBoxFrontCurrentVirtualOccupationTrackLocation.Location = new System.Drawing.Point(195, 193);
            this.m_textBoxFrontCurrentVirtualOccupationTrackLocation.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxFrontCurrentVirtualOccupationTrackLocation.Name = "m_textBoxFrontCurrentVirtualOccupationTrackLocation";
            this.m_textBoxFrontCurrentVirtualOccupationTrackLocation.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxFrontCurrentVirtualOccupationTrackLocation.TabIndex = 28;
            // 
            // m_labelFrontCurrentTrackVirtualOccupationLocation
            // 
            this.m_labelFrontCurrentTrackVirtualOccupationLocation.AutoSize = true;
            this.m_labelFrontCurrentTrackVirtualOccupationLocation.Location = new System.Drawing.Point(13, 193);
            this.m_labelFrontCurrentTrackVirtualOccupationLocation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelFrontCurrentTrackVirtualOccupationLocation.Name = "m_labelFrontCurrentTrackVirtualOccupationLocation";
            this.m_labelFrontCurrentTrackVirtualOccupationLocation.Size = new System.Drawing.Size(159, 13);
            this.m_labelFrontCurrentTrackVirtualOccupationLocation.TabIndex = 27;
            this.m_labelFrontCurrentTrackVirtualOccupationLocation.Text = "Vir. Occ. Front Track Location : ";
            // 
            // m_textBoxFrontCurrentTrackVirtualOccupationTrackID
            // 
            this.m_textBoxFrontCurrentTrackVirtualOccupationTrackID.Location = new System.Drawing.Point(195, 171);
            this.m_textBoxFrontCurrentTrackVirtualOccupationTrackID.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxFrontCurrentTrackVirtualOccupationTrackID.Name = "m_textBoxFrontCurrentTrackVirtualOccupationTrackID";
            this.m_textBoxFrontCurrentTrackVirtualOccupationTrackID.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxFrontCurrentTrackVirtualOccupationTrackID.TabIndex = 26;
            // 
            // m_labelFrontCurrentTrackVirtualOccupationTrackID
            // 
            this.m_labelFrontCurrentTrackVirtualOccupationTrackID.AutoSize = true;
            this.m_labelFrontCurrentTrackVirtualOccupationTrackID.Location = new System.Drawing.Point(44, 171);
            this.m_labelFrontCurrentTrackVirtualOccupationTrackID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelFrontCurrentTrackVirtualOccupationTrackID.Name = "m_labelFrontCurrentTrackVirtualOccupationTrackID";
            this.m_labelFrontCurrentTrackVirtualOccupationTrackID.Size = new System.Drawing.Size(129, 13);
            this.m_labelFrontCurrentTrackVirtualOccupationTrackID.TabIndex = 25;
            this.m_labelFrontCurrentTrackVirtualOccupationTrackID.Text = "Vir. Occ. Front Track ID : ";
            // 
            // m_textBoxFrontCurrentTrackMaxSpeedKM
            // 
            this.m_textBoxFrontCurrentTrackMaxSpeedKM.Location = new System.Drawing.Point(195, 148);
            this.m_textBoxFrontCurrentTrackMaxSpeedKM.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxFrontCurrentTrackMaxSpeedKM.Name = "m_textBoxFrontCurrentTrackMaxSpeedKM";
            this.m_textBoxFrontCurrentTrackMaxSpeedKM.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxFrontCurrentTrackMaxSpeedKM.TabIndex = 24;
            // 
            // m_labelFrontCurrentTrackMaxSpeedKM
            // 
            this.m_labelFrontCurrentTrackMaxSpeedKM.AutoSize = true;
            this.m_labelFrontCurrentTrackMaxSpeedKM.Location = new System.Drawing.Point(61, 148);
            this.m_labelFrontCurrentTrackMaxSpeedKM.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelFrontCurrentTrackMaxSpeedKM.Name = "m_labelFrontCurrentTrackMaxSpeedKM";
            this.m_labelFrontCurrentTrackMaxSpeedKM.Size = new System.Drawing.Size(104, 13);
            this.m_labelFrontCurrentTrackMaxSpeedKM.TabIndex = 23;
            this.m_labelFrontCurrentTrackMaxSpeedKM.Text = "Max Speed (km/h) : ";
            // 
            // m_textBoxFrontCurrentTrackSpeedChangeVMax
            // 
            this.m_textBoxFrontCurrentTrackSpeedChangeVMax.Location = new System.Drawing.Point(195, 102);
            this.m_textBoxFrontCurrentTrackSpeedChangeVMax.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxFrontCurrentTrackSpeedChangeVMax.Name = "m_textBoxFrontCurrentTrackSpeedChangeVMax";
            this.m_textBoxFrontCurrentTrackSpeedChangeVMax.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxFrontCurrentTrackSpeedChangeVMax.TabIndex = 22;
            // 
            // m_textBoxFrontCurrentTrackStation
            // 
            this.m_textBoxFrontCurrentTrackStation.Location = new System.Drawing.Point(195, 125);
            this.m_textBoxFrontCurrentTrackStation.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxFrontCurrentTrackStation.Name = "m_textBoxFrontCurrentTrackStation";
            this.m_textBoxFrontCurrentTrackStation.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxFrontCurrentTrackStation.TabIndex = 21;
            // 
            // m_textBoxFrontCurrentTrackDwellTime
            // 
            this.m_textBoxFrontCurrentTrackDwellTime.Location = new System.Drawing.Point(195, 80);
            this.m_textBoxFrontCurrentTrackDwellTime.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxFrontCurrentTrackDwellTime.Name = "m_textBoxFrontCurrentTrackDwellTime";
            this.m_textBoxFrontCurrentTrackDwellTime.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxFrontCurrentTrackDwellTime.TabIndex = 20;
            // 
            // m_textBoxFrontCurrentTrackLength
            // 
            this.m_textBoxFrontCurrentTrackLength.Location = new System.Drawing.Point(195, 57);
            this.m_textBoxFrontCurrentTrackLength.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxFrontCurrentTrackLength.Name = "m_textBoxFrontCurrentTrackLength";
            this.m_textBoxFrontCurrentTrackLength.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxFrontCurrentTrackLength.TabIndex = 19;
            // 
            // m_textBoxFrontCurrentTrackID
            // 
            this.m_textBoxFrontCurrentTrackID.Location = new System.Drawing.Point(195, 34);
            this.m_textBoxFrontCurrentTrackID.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxFrontCurrentTrackID.Name = "m_textBoxFrontCurrentTrackID";
            this.m_textBoxFrontCurrentTrackID.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxFrontCurrentTrackID.TabIndex = 18;
            // 
            // m_labelFrontCurrentTrackStation
            // 
            this.m_labelFrontCurrentTrackStation.AutoSize = true;
            this.m_labelFrontCurrentTrackStation.Location = new System.Drawing.Point(122, 125);
            this.m_labelFrontCurrentTrackStation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelFrontCurrentTrackStation.Name = "m_labelFrontCurrentTrackStation";
            this.m_labelFrontCurrentTrackStation.Size = new System.Drawing.Size(49, 13);
            this.m_labelFrontCurrentTrackStation.TabIndex = 17;
            this.m_labelFrontCurrentTrackStation.Text = "Station : ";
            // 
            // m_labelFrontCurrentTrackSpeedChangeVMax
            // 
            this.m_labelFrontCurrentTrackSpeedChangeVMax.AutoSize = true;
            this.m_labelFrontCurrentTrackSpeedChangeVMax.Location = new System.Drawing.Point(46, 105);
            this.m_labelFrontCurrentTrackSpeedChangeVMax.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelFrontCurrentTrackSpeedChangeVMax.Name = "m_labelFrontCurrentTrackSpeedChangeVMax";
            this.m_labelFrontCurrentTrackSpeedChangeVMax.Size = new System.Drawing.Size(128, 13);
            this.m_labelFrontCurrentTrackSpeedChangeVMax.TabIndex = 16;
            this.m_labelFrontCurrentTrackSpeedChangeVMax.Text = "Map Max Speed (km/h) : ";
            // 
            // m_labelFrontCurrentTrackDwellTime
            // 
            this.m_labelFrontCurrentTrackDwellTime.AutoSize = true;
            this.m_labelFrontCurrentTrackDwellTime.Location = new System.Drawing.Point(88, 82);
            this.m_labelFrontCurrentTrackDwellTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelFrontCurrentTrackDwellTime.Name = "m_labelFrontCurrentTrackDwellTime";
            this.m_labelFrontCurrentTrackDwellTime.Size = new System.Drawing.Size(82, 13);
            this.m_labelFrontCurrentTrackDwellTime.TabIndex = 15;
            this.m_labelFrontCurrentTrackDwellTime.Text = "Dwell Time (s) : ";
            // 
            // m_labelFrontCurrentTrackLength
            // 
            this.m_labelFrontCurrentTrackLength.AutoSize = true;
            this.m_labelFrontCurrentTrackLength.Location = new System.Drawing.Point(98, 57);
            this.m_labelFrontCurrentTrackLength.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelFrontCurrentTrackLength.Name = "m_labelFrontCurrentTrackLength";
            this.m_labelFrontCurrentTrackLength.Size = new System.Drawing.Size(72, 13);
            this.m_labelFrontCurrentTrackLength.TabIndex = 14;
            this.m_labelFrontCurrentTrackLength.Text = "Length (cm) : ";
            // 
            // m_labelFrontCurrentTrackID
            // 
            this.m_labelFrontCurrentTrackID.AutoSize = true;
            this.m_labelFrontCurrentTrackID.Location = new System.Drawing.Point(145, 34);
            this.m_labelFrontCurrentTrackID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelFrontCurrentTrackID.Name = "m_labelFrontCurrentTrackID";
            this.m_labelFrontCurrentTrackID.Size = new System.Drawing.Size(27, 13);
            this.m_labelFrontCurrentTrackID.TabIndex = 13;
            this.m_labelFrontCurrentTrackID.Text = "ID : ";
            // 
            // m_textBoxDoorTimerCounter
            // 
            this.m_textBoxDoorTimerCounter.Location = new System.Drawing.Point(561, 102);
            this.m_textBoxDoorTimerCounter.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxDoorTimerCounter.Name = "m_textBoxDoorTimerCounter";
            this.m_textBoxDoorTimerCounter.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxDoorTimerCounter.TabIndex = 12;
            // 
            // m_labelDoorTimerCounter
            // 
            this.m_labelDoorTimerCounter.AutoSize = true;
            this.m_labelDoorTimerCounter.Location = new System.Drawing.Point(407, 102);
            this.m_labelDoorTimerCounter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelDoorTimerCounter.Name = "m_labelDoorTimerCounter";
            this.m_labelDoorTimerCounter.Size = new System.Drawing.Size(108, 13);
            this.m_labelDoorTimerCounter.TabIndex = 11;
            this.m_labelDoorTimerCounter.Text = "Door Timer Counter : ";
            // 
            // m_comboBoxTrain
            // 
            this.m_comboBoxTrain.FormattingEnabled = true;
            this.m_comboBoxTrain.Location = new System.Drawing.Point(216, 32);
            this.m_comboBoxTrain.Margin = new System.Windows.Forms.Padding(2);
            this.m_comboBoxTrain.Name = "m_comboBoxTrain";
            this.m_comboBoxTrain.Size = new System.Drawing.Size(470, 21);
            this.m_comboBoxTrain.TabIndex = 3;
            // 
            // m_textBoxCurrentLocation
            // 
            this.m_textBoxCurrentLocation.Location = new System.Drawing.Point(561, 57);
            this.m_textBoxCurrentLocation.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxCurrentLocation.Name = "m_textBoxCurrentLocation";
            this.m_textBoxCurrentLocation.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxCurrentLocation.TabIndex = 10;
            // 
            // m_textBoxDoorStatus
            // 
            this.m_textBoxDoorStatus.Location = new System.Drawing.Point(561, 80);
            this.m_textBoxDoorStatus.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxDoorStatus.Name = "m_textBoxDoorStatus";
            this.m_textBoxDoorStatus.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxDoorStatus.TabIndex = 9;
            // 
            // m_textBoxCurrentAcceleration
            // 
            this.m_textBoxCurrentAcceleration.Location = new System.Drawing.Point(216, 102);
            this.m_textBoxCurrentAcceleration.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxCurrentAcceleration.Name = "m_textBoxCurrentAcceleration";
            this.m_textBoxCurrentAcceleration.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxCurrentAcceleration.TabIndex = 8;
            // 
            // m_textBoxRearCurrentLocation
            // 
            this.m_textBoxRearCurrentLocation.Location = new System.Drawing.Point(216, 80);
            this.m_textBoxRearCurrentLocation.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxRearCurrentLocation.Name = "m_textBoxRearCurrentLocation";
            this.m_textBoxRearCurrentLocation.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxRearCurrentLocation.TabIndex = 7;
            // 
            // m_textBoxCurrentTrainSpeedKM
            // 
            this.m_textBoxCurrentTrainSpeedKM.Location = new System.Drawing.Point(216, 57);
            this.m_textBoxCurrentTrainSpeedKM.Margin = new System.Windows.Forms.Padding(2);
            this.m_textBoxCurrentTrainSpeedKM.Name = "m_textBoxCurrentTrainSpeedKM";
            this.m_textBoxCurrentTrainSpeedKM.Size = new System.Drawing.Size(125, 20);
            this.m_textBoxCurrentTrainSpeedKM.TabIndex = 6;
            // 
            // m_labelDoorStatus
            // 
            this.m_labelDoorStatus.AutoSize = true;
            this.m_labelDoorStatus.Location = new System.Drawing.Point(445, 80);
            this.m_labelDoorStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelDoorStatus.Name = "m_labelDoorStatus";
            this.m_labelDoorStatus.Size = new System.Drawing.Size(72, 13);
            this.m_labelDoorStatus.TabIndex = 5;
            this.m_labelDoorStatus.Text = "Door Status : ";
            // 
            // m_labelCurrentLocation
            // 
            this.m_labelCurrentLocation.AutoSize = true;
            this.m_labelCurrentLocation.Location = new System.Drawing.Point(398, 57);
            this.m_labelCurrentLocation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelCurrentLocation.Name = "m_labelCurrentLocation";
            this.m_labelCurrentLocation.Size = new System.Drawing.Size(117, 13);
            this.m_labelCurrentLocation.TabIndex = 4;
            this.m_labelCurrentLocation.Text = "Current Location (cm) : ";
            // 
            // m_labelCurrentAcceleration
            // 
            this.m_labelCurrentAcceleration.AutoSize = true;
            this.m_labelCurrentAcceleration.Location = new System.Drawing.Point(79, 102);
            this.m_labelCurrentAcceleration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelCurrentAcceleration.Name = "m_labelCurrentAcceleration";
            this.m_labelCurrentAcceleration.Size = new System.Drawing.Size(112, 13);
            this.m_labelCurrentAcceleration.TabIndex = 3;
            this.m_labelCurrentAcceleration.Text = "Current Acceleration : ";
            // 
            // m_labelRearCurrentLocation
            // 
            this.m_labelRearCurrentLocation.AutoSize = true;
            this.m_labelRearCurrentLocation.Location = new System.Drawing.Point(46, 80);
            this.m_labelRearCurrentLocation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelRearCurrentLocation.Name = "m_labelRearCurrentLocation";
            this.m_labelRearCurrentLocation.Size = new System.Drawing.Size(143, 13);
            this.m_labelRearCurrentLocation.TabIndex = 2;
            this.m_labelRearCurrentLocation.Text = "Rear Current Location (cm) : ";
            // 
            // m_labelCurrentTrainSpeedKM
            // 
            this.m_labelCurrentTrainSpeedKM.AutoSize = true;
            this.m_labelCurrentTrainSpeedKM.Location = new System.Drawing.Point(74, 57);
            this.m_labelCurrentTrainSpeedKM.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelCurrentTrainSpeedKM.Name = "m_labelCurrentTrainSpeedKM";
            this.m_labelCurrentTrainSpeedKM.Size = new System.Drawing.Size(118, 13);
            this.m_labelCurrentTrainSpeedKM.TabIndex = 1;
            this.m_labelCurrentTrainSpeedKM.Text = "Current Speed (km/h) : ";
            // 
            // m_labelTrains
            // 
            this.m_labelTrains.AutoSize = true;
            this.m_labelTrains.Location = new System.Drawing.Point(146, 32);
            this.m_labelTrains.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelTrains.Name = "m_labelTrains";
            this.m_labelTrains.Size = new System.Drawing.Size(45, 13);
            this.m_labelTrains.TabIndex = 0;
            this.m_labelTrains.Text = "Trains : ";
            // 
            // m_buttonSave
            // 
            this.m_buttonSave.Location = new System.Drawing.Point(236, 504);
            this.m_buttonSave.Name = "m_buttonSave";
            this.m_buttonSave.Size = new System.Drawing.Size(75, 35);
            this.m_buttonSave.TabIndex = 3;
            this.m_buttonSave.Text = "Save";
            this.m_buttonSave.UseVisualStyleBackColor = true;
            this.m_buttonSave.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // m_buttonStart
            // 
            this.m_buttonStart.Location = new System.Drawing.Point(96, 504);
            this.m_buttonStart.Name = "m_buttonStart";
            this.m_buttonStart.Size = new System.Drawing.Size(75, 35);
            this.m_buttonStart.TabIndex = 4;
            this.m_buttonStart.Text = "Start";
            this.m_buttonStart.UseVisualStyleBackColor = true;
            this.m_buttonStart.Click += new System.EventHandler(this.m_buttonStart_Click);
            // 
            // m_dataGridViewAllTrains
            // 
            this.m_dataGridViewAllTrains.AllowUserToAddRows = false;
            this.m_dataGridViewAllTrains.AllowUserToResizeColumns = false;
            this.m_dataGridViewAllTrains.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.m_dataGridViewAllTrains.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dataGridViewAllTrains.Location = new System.Drawing.Point(9, 28);
            this.m_dataGridViewAllTrains.Margin = new System.Windows.Forms.Padding(2);
            this.m_dataGridViewAllTrains.MultiSelect = false;
            this.m_dataGridViewAllTrains.Name = "m_dataGridViewAllTrains";
            this.m_dataGridViewAllTrains.ReadOnly = true;
            this.m_dataGridViewAllTrains.RowHeadersVisible = false;
            this.m_dataGridViewAllTrains.RowTemplate.Height = 24;
            this.m_dataGridViewAllTrains.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_dataGridViewAllTrains.Size = new System.Drawing.Size(831, 157);
            this.m_dataGridViewAllTrains.TabIndex = 5;
            this.m_dataGridViewAllTrains.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_dataGridViewAllTrains_CellContentClick);
            this.m_dataGridViewAllTrains.SelectionChanged += new System.EventHandler(this.m_dataGridViewAllTrains_SelectionChanged);
            // 
            // m_mainMenu
            // 
            this.m_mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_settingsPopup});
            this.m_mainMenu.Location = new System.Drawing.Point(0, 0);
            this.m_mainMenu.Name = "m_mainMenu";
            this.m_mainMenu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.m_mainMenu.Size = new System.Drawing.Size(1443, 24);
            this.m_mainMenu.TabIndex = 7;
            this.m_mainMenu.Text = "menuStrip1";
            this.m_mainMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.m_mainMenu_ItemClicked);
            // 
            // m_settingsPopup
            // 
            this.m_settingsPopup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_generalItem,
            this.m_trainItem,
            this.m_communicationItem,
            this.m_trainSimItem,
            this.m_routeItem});
            this.m_settingsPopup.Name = "m_settingsPopup";
            this.m_settingsPopup.Size = new System.Drawing.Size(61, 20);
            this.m_settingsPopup.Text = "&Settings";
            // 
            // m_generalItem
            // 
            this.m_generalItem.Name = "m_generalItem";
            this.m_generalItem.Size = new System.Drawing.Size(161, 22);
            this.m_generalItem.Text = "General";
            this.m_generalItem.Click += new System.EventHandler(this.m_generalItem_Click);
            // 
            // m_trainItem
            // 
            this.m_trainItem.Name = "m_trainItem";
            this.m_trainItem.Size = new System.Drawing.Size(161, 22);
            this.m_trainItem.Text = "Train";
            this.m_trainItem.Click += new System.EventHandler(this.m_trainItem_Click);
            // 
            // m_communicationItem
            // 
            this.m_communicationItem.Name = "m_communicationItem";
            this.m_communicationItem.Size = new System.Drawing.Size(161, 22);
            this.m_communicationItem.Text = "Communication";
            this.m_communicationItem.Click += new System.EventHandler(this.m_communicationItem_Click);
            // 
            // m_trainSimItem
            // 
            this.m_trainSimItem.Name = "m_trainSimItem";
            this.m_trainSimItem.Size = new System.Drawing.Size(161, 22);
            this.m_trainSimItem.Text = "Train Sim";
            this.m_trainSimItem.Click += new System.EventHandler(this.m_trainSimItem_Click);
            // 
            // m_routeItem
            // 
            this.m_routeItem.Name = "m_routeItem";
            this.m_routeItem.Size = new System.Drawing.Size(161, 22);
            this.m_routeItem.Text = "Route";
            this.m_routeItem.Click += new System.EventHandler(this.m_routeItem_Click);
            // 
            // m_backgroundWorker
            // 
            this.m_backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.m_backgroundWorker_DoWork);
            // 
            // m_listViewVirtualOccupation
            // 
            this.m_listViewVirtualOccupation.BackColor = System.Drawing.Color.White;
            this.m_listViewVirtualOccupation.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.m_listViewVirtualOccupation.ForeColor = System.Drawing.Color.Gray;
            this.m_listViewVirtualOccupation.FullRowSelect = true;
            this.m_listViewVirtualOccupation.GridLines = true;
            this.m_listViewVirtualOccupation.HideSelection = false;
            this.m_listViewVirtualOccupation.Location = new System.Drawing.Point(1074, 26);
            this.m_listViewVirtualOccupation.Margin = new System.Windows.Forms.Padding(2);
            this.m_listViewVirtualOccupation.MultiSelect = false;
            this.m_listViewVirtualOccupation.Name = "m_listViewVirtualOccupation";
            this.m_listViewVirtualOccupation.Size = new System.Drawing.Size(104, 383);
            this.m_listViewVirtualOccupation.TabIndex = 27;
            this.m_listViewVirtualOccupation.UseCompatibleStateImageBehavior = false;
            this.m_listViewVirtualOccupation.View = System.Windows.Forms.View.Details;
            this.m_listViewVirtualOccupation.SelectedIndexChanged += new System.EventHandler(this.m_listViewVirtualOccupation_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Virtual Occ Tracks";
            this.columnHeader3.Width = 100;
            // 
            // m_richTextBox
            // 
            this.m_richTextBox.Location = new System.Drawing.Point(9, 186);
            this.m_richTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.m_richTextBox.Name = "m_richTextBox";
            this.m_richTextBox.Size = new System.Drawing.Size(719, 281);
            this.m_richTextBox.TabIndex = 8;
            this.m_richTextBox.Text = "";
            this.m_richTextBox.TextChanged += new System.EventHandler(this.m_richTextBox_TextChanged);
            // 
            // m_listViewFootPrintTracks
            // 
            this.m_listViewFootPrintTracks.BackColor = System.Drawing.Color.White;
            this.m_listViewFootPrintTracks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            this.m_listViewFootPrintTracks.ForeColor = System.Drawing.Color.Black;
            this.m_listViewFootPrintTracks.FullRowSelect = true;
            this.m_listViewFootPrintTracks.GridLines = true;
            this.m_listViewFootPrintTracks.HideSelection = false;
            this.m_listViewFootPrintTracks.Location = new System.Drawing.Point(1182, 26);
            this.m_listViewFootPrintTracks.Margin = new System.Windows.Forms.Padding(2);
            this.m_listViewFootPrintTracks.MultiSelect = false;
            this.m_listViewFootPrintTracks.Name = "m_listViewFootPrintTracks";
            this.m_listViewFootPrintTracks.Size = new System.Drawing.Size(104, 383);
            this.m_listViewFootPrintTracks.TabIndex = 28;
            this.m_listViewFootPrintTracks.UseCompatibleStateImageBehavior = false;
            this.m_listViewFootPrintTracks.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "FootPrint Tracks";
            this.columnHeader4.Width = 100;
            // 
            // bindingSource1
            // 
            this.bindingSource1.CurrentChanged += new System.EventHandler(this.bindingSource1_CurrentChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 732);
            this.Controls.Add(this.m_listViewFootPrintTracks);
            this.Controls.Add(this.m_listViewVirtualOccupation);
            this.Controls.Add(this.m_richTextBox);
            this.Controls.Add(this.m_dataGridViewAllTrains);
            this.Controls.Add(this.m_buttonStart);
            this.Controls.Add(this.m_buttonSave);
            this.Controls.Add(this.m_groupBoxTrainSettings);
            this.Controls.Add(this.m_mainMenu);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.m_groupBoxTrainSettings.ResumeLayout(false);
            this.m_groupBoxTrainSettings.PerformLayout();
            this.m_groupBoxRearCurrentTrack.ResumeLayout(false);
            this.m_groupBoxRearCurrentTrack.PerformLayout();
            this.m_groupBoxFrontCurrentTrack.ResumeLayout(false);
            this.m_groupBoxFrontCurrentTrack.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridViewAllTrains)).EndInit();
            this.m_mainMenu.ResumeLayout(false);
            this.m_mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox m_groupBoxTrainSettings;
        private System.Windows.Forms.GroupBox m_groupBoxRearCurrentTrack;
        private System.Windows.Forms.TextBox m_textBoxRearCurrentVirtualOccupationTrackLocation;
        private System.Windows.Forms.Label m_labelRearCurrentTrackVirtualOccupationLocation;
        private System.Windows.Forms.TextBox m_textBoxRearCurrentTrackVirtualOccupationTrackID;
        private System.Windows.Forms.Label m_labelRearCurrentTrackVirtualOccupationTrackID;
        private System.Windows.Forms.TextBox m_textBoxRearCurrentTrackSpeedChangeVMax;
        private System.Windows.Forms.TextBox m_textBoxRearCurrentTrackID;
        private System.Windows.Forms.Label m_labelRearCurrentTrackSpeedChangeVMax;
        private System.Windows.Forms.Label m_labelRearCurrentTrackID;
        private System.Windows.Forms.GroupBox m_groupBoxFrontCurrentTrack;
        private System.Windows.Forms.TextBox m_textBoxFrontCurrentVirtualOccupationTrackLocation;
        private System.Windows.Forms.Label m_labelFrontCurrentTrackVirtualOccupationLocation;
        private System.Windows.Forms.TextBox m_textBoxFrontCurrentTrackVirtualOccupationTrackID;
        private System.Windows.Forms.Label m_labelFrontCurrentTrackVirtualOccupationTrackID;
        private System.Windows.Forms.TextBox m_textBoxFrontCurrentTrackMaxSpeedKM;
        private System.Windows.Forms.Label m_labelFrontCurrentTrackMaxSpeedKM;
        private System.Windows.Forms.TextBox m_textBoxFrontCurrentTrackSpeedChangeVMax;
        private System.Windows.Forms.TextBox m_textBoxFrontCurrentTrackStation;
        private System.Windows.Forms.TextBox m_textBoxFrontCurrentTrackDwellTime;
        private System.Windows.Forms.TextBox m_textBoxFrontCurrentTrackLength;
        private System.Windows.Forms.TextBox m_textBoxFrontCurrentTrackID;
        private System.Windows.Forms.Label m_labelFrontCurrentTrackStation;
        private System.Windows.Forms.Label m_labelFrontCurrentTrackSpeedChangeVMax;
        private System.Windows.Forms.Label m_labelFrontCurrentTrackDwellTime;
        private System.Windows.Forms.Label m_labelFrontCurrentTrackLength;
        private System.Windows.Forms.Label m_labelFrontCurrentTrackID;
        private System.Windows.Forms.TextBox m_textBoxDoorTimerCounter;
        private System.Windows.Forms.Label m_labelDoorTimerCounter;
        private System.Windows.Forms.ComboBox m_comboBoxTrain;
        private System.Windows.Forms.TextBox m_textBoxCurrentLocation;
        private System.Windows.Forms.TextBox m_textBoxDoorStatus;
        private System.Windows.Forms.TextBox m_textBoxCurrentAcceleration;
        private System.Windows.Forms.TextBox m_textBoxRearCurrentLocation;
        private System.Windows.Forms.TextBox m_textBoxCurrentTrainSpeedKM;
        private System.Windows.Forms.Label m_labelDoorStatus;
        private System.Windows.Forms.Label m_labelCurrentLocation;
        private System.Windows.Forms.Label m_labelCurrentAcceleration;
        private System.Windows.Forms.Label m_labelRearCurrentLocation;
        private System.Windows.Forms.Label m_labelCurrentTrainSpeedKM;
        private System.Windows.Forms.Label m_labelTrains;
        private System.Windows.Forms.Button m_buttonSave;
        private System.Windows.Forms.Button m_buttonStart;
        private System.Windows.Forms.ListView m_listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.DataGridView m_dataGridViewAllTrains;
        private System.Windows.Forms.MenuStrip m_mainMenu;
        private System.Windows.Forms.ToolStripMenuItem m_settingsPopup;
        private System.Windows.Forms.ToolStripMenuItem m_trainItem;
        private System.Windows.Forms.ToolStripMenuItem m_generalItem;
        private System.ComponentModel.BackgroundWorker m_backgroundWorker;
        private System.Windows.Forms.ToolStripMenuItem m_communicationItem;
        private System.Windows.Forms.ToolStripMenuItem m_trainSimItem;
        private System.Windows.Forms.ToolStripMenuItem m_routeItem;
        private System.Windows.Forms.ListView m_listViewVirtualOccupation;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        public System.Windows.Forms.RichTextBox m_richTextBox;
        private System.Windows.Forms.ListView m_listViewFootPrintTracks;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}

