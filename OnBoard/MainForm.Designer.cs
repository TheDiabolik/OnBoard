namespace OnBoard
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
            this.m_textBoxDoorTimerCounter = new System.Windows.Forms.TextBox();
            this.m_labelDoorTimerCounter = new System.Windows.Forms.Label();
            this.m_comboBoxTrain = new System.Windows.Forms.ComboBox();
            this.m_textBoxCurrentLocation = new System.Windows.Forms.TextBox();
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
            this.m_groupBoxAllTrains = new System.Windows.Forms.GroupBox();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_pictureBoxDoorStatus = new System.Windows.Forms.PictureBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.m_groupBoxTrainSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridViewAllTrains)).BeginInit();
            this.m_mainMenu.SuspendLayout();
            this.m_groupBoxAllTrains.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pictureBoxDoorStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_groupBoxTrainSettings
            // 
            this.m_groupBoxTrainSettings.Controls.Add(this.m_pictureBoxDoorStatus);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxDoorTimerCounter);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_listView);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelDoorTimerCounter);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_comboBoxTrain);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_listViewFootPrintTracks);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxCurrentLocation);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_listViewVirtualOccupation);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxCurrentAcceleration);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxRearCurrentLocation);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxCurrentTrainSpeedKM);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelDoorStatus);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelCurrentLocation);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelCurrentAcceleration);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelRearCurrentLocation);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelCurrentTrainSpeedKM);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelTrains);
            this.m_groupBoxTrainSettings.Location = new System.Drawing.Point(18, 259);
            this.m_groupBoxTrainSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_groupBoxTrainSettings.Name = "m_groupBoxTrainSettings";
            this.m_groupBoxTrainSettings.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_groupBoxTrainSettings.Size = new System.Drawing.Size(1134, 585);
            this.m_groupBoxTrainSettings.TabIndex = 2;
            this.m_groupBoxTrainSettings.TabStop = false;
            this.m_groupBoxTrainSettings.Text = "Train Settings";
            // 
            // m_listView
            // 
            this.m_listView.BackColor = System.Drawing.Color.White;
            this.m_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5});
            this.m_listView.ForeColor = System.Drawing.Color.Black;
            this.m_listView.FullRowSelect = true;
            this.m_listView.GridLines = true;
            this.m_listView.HideSelection = false;
            this.m_listView.Location = new System.Drawing.Point(75, 213);
            this.m_listView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_listView.MultiSelect = false;
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(394, 324);
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
            // 
            // m_textBoxDoorTimerCounter
            // 
            this.m_textBoxDoorTimerCounter.Location = new System.Drawing.Point(748, 98);
            this.m_textBoxDoorTimerCounter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_textBoxDoorTimerCounter.Name = "m_textBoxDoorTimerCounter";
            this.m_textBoxDoorTimerCounter.Size = new System.Drawing.Size(165, 22);
            this.m_textBoxDoorTimerCounter.TabIndex = 12;
            // 
            // m_labelDoorTimerCounter
            // 
            this.m_labelDoorTimerCounter.AutoSize = true;
            this.m_labelDoorTimerCounter.Location = new System.Drawing.Point(543, 98);
            this.m_labelDoorTimerCounter.Name = "m_labelDoorTimerCounter";
            this.m_labelDoorTimerCounter.Size = new System.Drawing.Size(145, 17);
            this.m_labelDoorTimerCounter.TabIndex = 11;
            this.m_labelDoorTimerCounter.Text = "Door Timer Counter : ";
            // 
            // m_comboBoxTrain
            // 
            this.m_comboBoxTrain.FormattingEnabled = true;
            this.m_comboBoxTrain.Location = new System.Drawing.Point(288, 39);
            this.m_comboBoxTrain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_comboBoxTrain.Name = "m_comboBoxTrain";
            this.m_comboBoxTrain.Size = new System.Drawing.Size(625, 24);
            this.m_comboBoxTrain.TabIndex = 3;
            // 
            // m_textBoxCurrentLocation
            // 
            this.m_textBoxCurrentLocation.Location = new System.Drawing.Point(748, 70);
            this.m_textBoxCurrentLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_textBoxCurrentLocation.Name = "m_textBoxCurrentLocation";
            this.m_textBoxCurrentLocation.Size = new System.Drawing.Size(165, 22);
            this.m_textBoxCurrentLocation.TabIndex = 10;
            // 
            // m_textBoxCurrentAcceleration
            // 
            this.m_textBoxCurrentAcceleration.Location = new System.Drawing.Point(288, 126);
            this.m_textBoxCurrentAcceleration.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_textBoxCurrentAcceleration.Name = "m_textBoxCurrentAcceleration";
            this.m_textBoxCurrentAcceleration.Size = new System.Drawing.Size(165, 22);
            this.m_textBoxCurrentAcceleration.TabIndex = 8;
            // 
            // m_textBoxRearCurrentLocation
            // 
            this.m_textBoxRearCurrentLocation.Location = new System.Drawing.Point(288, 98);
            this.m_textBoxRearCurrentLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_textBoxRearCurrentLocation.Name = "m_textBoxRearCurrentLocation";
            this.m_textBoxRearCurrentLocation.Size = new System.Drawing.Size(165, 22);
            this.m_textBoxRearCurrentLocation.TabIndex = 7;
            // 
            // m_textBoxCurrentTrainSpeedKM
            // 
            this.m_textBoxCurrentTrainSpeedKM.Location = new System.Drawing.Point(288, 70);
            this.m_textBoxCurrentTrainSpeedKM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_textBoxCurrentTrainSpeedKM.Name = "m_textBoxCurrentTrainSpeedKM";
            this.m_textBoxCurrentTrainSpeedKM.Size = new System.Drawing.Size(165, 22);
            this.m_textBoxCurrentTrainSpeedKM.TabIndex = 6;
            // 
            // m_labelDoorStatus
            // 
            this.m_labelDoorStatus.AutoSize = true;
            this.m_labelDoorStatus.Location = new System.Drawing.Point(593, 124);
            this.m_labelDoorStatus.Name = "m_labelDoorStatus";
            this.m_labelDoorStatus.Size = new System.Drawing.Size(95, 17);
            this.m_labelDoorStatus.TabIndex = 5;
            this.m_labelDoorStatus.Text = "Door Status : ";
            // 
            // m_labelCurrentLocation
            // 
            this.m_labelCurrentLocation.AutoSize = true;
            this.m_labelCurrentLocation.Location = new System.Drawing.Point(531, 70);
            this.m_labelCurrentLocation.Name = "m_labelCurrentLocation";
            this.m_labelCurrentLocation.Size = new System.Drawing.Size(157, 17);
            this.m_labelCurrentLocation.TabIndex = 4;
            this.m_labelCurrentLocation.Text = "Current Location (cm) : ";
            // 
            // m_labelCurrentAcceleration
            // 
            this.m_labelCurrentAcceleration.AutoSize = true;
            this.m_labelCurrentAcceleration.Location = new System.Drawing.Point(105, 126);
            this.m_labelCurrentAcceleration.Name = "m_labelCurrentAcceleration";
            this.m_labelCurrentAcceleration.Size = new System.Drawing.Size(149, 17);
            this.m_labelCurrentAcceleration.TabIndex = 3;
            this.m_labelCurrentAcceleration.Text = "Current Acceleration : ";
            // 
            // m_labelRearCurrentLocation
            // 
            this.m_labelRearCurrentLocation.AutoSize = true;
            this.m_labelRearCurrentLocation.Location = new System.Drawing.Point(61, 98);
            this.m_labelRearCurrentLocation.Name = "m_labelRearCurrentLocation";
            this.m_labelRearCurrentLocation.Size = new System.Drawing.Size(192, 17);
            this.m_labelRearCurrentLocation.TabIndex = 2;
            this.m_labelRearCurrentLocation.Text = "Rear Current Location (cm) : ";
            // 
            // m_labelCurrentTrainSpeedKM
            // 
            this.m_labelCurrentTrainSpeedKM.AutoSize = true;
            this.m_labelCurrentTrainSpeedKM.Location = new System.Drawing.Point(99, 70);
            this.m_labelCurrentTrainSpeedKM.Name = "m_labelCurrentTrainSpeedKM";
            this.m_labelCurrentTrainSpeedKM.Size = new System.Drawing.Size(156, 17);
            this.m_labelCurrentTrainSpeedKM.TabIndex = 1;
            this.m_labelCurrentTrainSpeedKM.Text = "Current Speed (km/h) : ";
            // 
            // m_labelTrains
            // 
            this.m_labelTrains.AutoSize = true;
            this.m_labelTrains.Location = new System.Drawing.Point(195, 39);
            this.m_labelTrains.Name = "m_labelTrains";
            this.m_labelTrains.Size = new System.Drawing.Size(60, 17);
            this.m_labelTrains.TabIndex = 0;
            this.m_labelTrains.Text = "Trains : ";
            // 
            // m_buttonSave
            // 
            this.m_buttonSave.Location = new System.Drawing.Point(1362, 453);
            this.m_buttonSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_buttonSave.Name = "m_buttonSave";
            this.m_buttonSave.Size = new System.Drawing.Size(100, 43);
            this.m_buttonSave.TabIndex = 3;
            this.m_buttonSave.Text = "Save";
            this.m_buttonSave.UseVisualStyleBackColor = true;
            this.m_buttonSave.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // m_buttonStart
            // 
            this.m_buttonStart.Location = new System.Drawing.Point(1187, 453);
            this.m_buttonStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_buttonStart.Name = "m_buttonStart";
            this.m_buttonStart.Size = new System.Drawing.Size(100, 43);
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
            this.m_dataGridViewAllTrains.Location = new System.Drawing.Point(6, 20);
            this.m_dataGridViewAllTrains.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_dataGridViewAllTrains.MultiSelect = false;
            this.m_dataGridViewAllTrains.Name = "m_dataGridViewAllTrains";
            this.m_dataGridViewAllTrains.ReadOnly = true;
            this.m_dataGridViewAllTrains.RowHeadersVisible = false;
            this.m_dataGridViewAllTrains.RowTemplate.Height = 24;
            this.m_dataGridViewAllTrains.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_dataGridViewAllTrains.Size = new System.Drawing.Size(1128, 193);
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
            this.m_mainMenu.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.m_mainMenu.Size = new System.Drawing.Size(1586, 28);
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
            this.m_settingsPopup.Size = new System.Drawing.Size(74, 24);
            this.m_settingsPopup.Text = "&Settings";
            // 
            // m_generalItem
            // 
            this.m_generalItem.Name = "m_generalItem";
            this.m_generalItem.Size = new System.Drawing.Size(189, 26);
            this.m_generalItem.Text = "General";
            this.m_generalItem.Click += new System.EventHandler(this.m_generalItem_Click);
            // 
            // m_trainItem
            // 
            this.m_trainItem.Name = "m_trainItem";
            this.m_trainItem.Size = new System.Drawing.Size(189, 26);
            this.m_trainItem.Text = "Train";
            this.m_trainItem.Click += new System.EventHandler(this.m_trainItem_Click);
            // 
            // m_communicationItem
            // 
            this.m_communicationItem.Name = "m_communicationItem";
            this.m_communicationItem.Size = new System.Drawing.Size(189, 26);
            this.m_communicationItem.Text = "Communication";
            this.m_communicationItem.Click += new System.EventHandler(this.m_communicationItem_Click);
            // 
            // m_trainSimItem
            // 
            this.m_trainSimItem.Name = "m_trainSimItem";
            this.m_trainSimItem.Size = new System.Drawing.Size(189, 26);
            this.m_trainSimItem.Text = "Train Sim";
            this.m_trainSimItem.Click += new System.EventHandler(this.m_trainSimItem_Click);
            // 
            // m_routeItem
            // 
            this.m_routeItem.Name = "m_routeItem";
            this.m_routeItem.Size = new System.Drawing.Size(189, 26);
            this.m_routeItem.Text = "Tracks";
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
            this.m_listViewVirtualOccupation.ForeColor = System.Drawing.Color.Black;
            this.m_listViewVirtualOccupation.FullRowSelect = true;
            this.m_listViewVirtualOccupation.GridLines = true;
            this.m_listViewVirtualOccupation.HideSelection = false;
            this.m_listViewVirtualOccupation.Location = new System.Drawing.Point(475, 213);
            this.m_listViewVirtualOccupation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_listViewVirtualOccupation.MultiSelect = false;
            this.m_listViewVirtualOccupation.Name = "m_listViewVirtualOccupation";
            this.m_listViewVirtualOccupation.Size = new System.Drawing.Size(142, 325);
            this.m_listViewVirtualOccupation.TabIndex = 27;
            this.m_listViewVirtualOccupation.UseCompatibleStateImageBehavior = false;
            this.m_listViewVirtualOccupation.View = System.Windows.Forms.View.Details;
            this.m_listViewVirtualOccupation.SelectedIndexChanged += new System.EventHandler(this.m_listViewVirtualOccupation_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Virtual Occ. Tracks";
            this.columnHeader3.Width = 105;
            // 
            // m_richTextBox
            // 
            this.m_richTextBox.Location = new System.Drawing.Point(1152, 31);
            this.m_richTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_richTextBox.Name = "m_richTextBox";
            this.m_richTextBox.Size = new System.Drawing.Size(429, 345);
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
            this.m_listViewFootPrintTracks.LabelWrap = false;
            this.m_listViewFootPrintTracks.Location = new System.Drawing.Point(623, 213);
            this.m_listViewFootPrintTracks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_listViewFootPrintTracks.MultiSelect = false;
            this.m_listViewFootPrintTracks.Name = "m_listViewFootPrintTracks";
            this.m_listViewFootPrintTracks.Size = new System.Drawing.Size(142, 325);
            this.m_listViewFootPrintTracks.TabIndex = 28;
            this.m_listViewFootPrintTracks.UseCompatibleStateImageBehavior = false;
            this.m_listViewFootPrintTracks.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "FootPrint Tracks";
            this.columnHeader4.Width = 105;
            // 
            // m_groupBoxAllTrains
            // 
            this.m_groupBoxAllTrains.Controls.Add(this.m_dataGridViewAllTrains);
            this.m_groupBoxAllTrains.Location = new System.Drawing.Point(12, 31);
            this.m_groupBoxAllTrains.Name = "m_groupBoxAllTrains";
            this.m_groupBoxAllTrains.Size = new System.Drawing.Size(1140, 223);
            this.m_groupBoxAllTrains.TabIndex = 29;
            this.m_groupBoxAllTrains.TabStop = false;
            this.m_groupBoxAllTrains.Text = "Trains";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Speed Limit(km/sa)";
            this.columnHeader5.Width = 110;
            // 
            // m_pictureBoxDoorStatus
            // 
            this.m_pictureBoxDoorStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pictureBoxDoorStatus.Image = global::OnBoard.Properties.Resources.doorclose;
            this.m_pictureBoxDoorStatus.Location = new System.Drawing.Point(748, 126);
            this.m_pictureBoxDoorStatus.Name = "m_pictureBoxDoorStatus";
            this.m_pictureBoxDoorStatus.Size = new System.Drawing.Size(50, 43);
            this.m_pictureBoxDoorStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_pictureBoxDoorStatus.TabIndex = 29;
            this.m_pictureBoxDoorStatus.TabStop = false;
            // 
            // bindingSource1
            // 
            this.bindingSource1.CurrentChanged += new System.EventHandler(this.bindingSource1_CurrentChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1586, 839);
            this.Controls.Add(this.m_groupBoxAllTrains);
            this.Controls.Add(this.m_groupBoxTrainSettings);
            this.Controls.Add(this.m_richTextBox);
            this.Controls.Add(this.m_buttonStart);
            this.Controls.Add(this.m_buttonSave);
            this.Controls.Add(this.m_mainMenu);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.m_groupBoxTrainSettings.ResumeLayout(false);
            this.m_groupBoxTrainSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridViewAllTrains)).EndInit();
            this.m_mainMenu.ResumeLayout(false);
            this.m_mainMenu.PerformLayout();
            this.m_groupBoxAllTrains.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_pictureBoxDoorStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox m_groupBoxTrainSettings;
        private System.Windows.Forms.TextBox m_textBoxDoorTimerCounter;
        private System.Windows.Forms.Label m_labelDoorTimerCounter;
        private System.Windows.Forms.ComboBox m_comboBoxTrain;
        private System.Windows.Forms.TextBox m_textBoxCurrentLocation;
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
        private System.Windows.Forms.GroupBox m_groupBoxAllTrains;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.PictureBox m_pictureBoxDoorStatus;
        public System.Windows.Forms.ListView m_listView;
    }
}

