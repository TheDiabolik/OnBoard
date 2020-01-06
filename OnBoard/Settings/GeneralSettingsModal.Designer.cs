namespace OnBoard
{
    partial class GeneralSettingsModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralSettingsModal));
            this.m_buttonApply = new System.Windows.Forms.Button();
            this.m_buttonSave = new System.Windows.Forms.Button();
            this.m_checkedListBoxTrains = new System.Windows.Forms.CheckedListBox();
            this.m_labelTrainFrequency = new System.Windows.Forms.Label();
            this.m_numericUpDownTrainFrequency = new System.Windows.Forms.NumericUpDown();
            this.m_groupBoxTrain = new System.Windows.Forms.GroupBox();
            this.m_groupBoxRoute = new System.Windows.Forms.GroupBox();
            this.m_textBoxEndRangeTrackID = new System.Windows.Forms.TextBox();
            this.m_textBoxStartRangeTrackID = new System.Windows.Forms.TextBox();
            this.m_labelEndTrackID = new System.Windows.Forms.Label();
            this.m_labelStartTrackID = new System.Windows.Forms.Label();
            this.m_radioButtonFromFileTracks = new System.Windows.Forms.RadioButton();
            this.m_radioButtonManuelInputTracks = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.m_numericUpDownTrainFrequency)).BeginInit();
            this.m_groupBoxTrain.SuspendLayout();
            this.m_groupBoxRoute.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_buttonApply
            // 
            this.m_buttonApply.Image = global::OnBoard.Properties.Resources.apply;
            this.m_buttonApply.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_buttonApply.Location = new System.Drawing.Point(498, 286);
            this.m_buttonApply.Margin = new System.Windows.Forms.Padding(2);
            this.m_buttonApply.Name = "m_buttonApply";
            this.m_buttonApply.Size = new System.Drawing.Size(83, 48);
            this.m_buttonApply.TabIndex = 10;
            this.m_buttonApply.Text = "Apply";
            this.m_buttonApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_buttonApply.UseVisualStyleBackColor = true;
            this.m_buttonApply.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // m_buttonSave
            // 
            this.m_buttonSave.Image = global::OnBoard.Properties.Resources.save;
            this.m_buttonSave.Location = new System.Drawing.Point(410, 286);
            this.m_buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.m_buttonSave.Name = "m_buttonSave";
            this.m_buttonSave.Size = new System.Drawing.Size(83, 48);
            this.m_buttonSave.TabIndex = 9;
            this.m_buttonSave.Text = "Save";
            this.m_buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_buttonSave.UseVisualStyleBackColor = true;
            this.m_buttonSave.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // m_checkedListBoxTrains
            // 
            this.m_checkedListBoxTrains.FormattingEnabled = true;
            this.m_checkedListBoxTrains.Location = new System.Drawing.Point(8, 56);
            this.m_checkedListBoxTrains.Name = "m_checkedListBoxTrains";
            this.m_checkedListBoxTrains.Size = new System.Drawing.Size(237, 199);
            this.m_checkedListBoxTrains.TabIndex = 11;
            // 
            // m_labelTrainFrequency
            // 
            this.m_labelTrainFrequency.AutoSize = true;
            this.m_labelTrainFrequency.Location = new System.Drawing.Point(5, 32);
            this.m_labelTrainFrequency.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelTrainFrequency.Name = "m_labelTrainFrequency";
            this.m_labelTrainFrequency.Size = new System.Drawing.Size(115, 13);
            this.m_labelTrainFrequency.TabIndex = 12;
            this.m_labelTrainFrequency.Text = "Train Frequency(min) : ";
            // 
            // m_numericUpDownTrainFrequency
            // 
            this.m_numericUpDownTrainFrequency.Location = new System.Drawing.Point(155, 31);
            this.m_numericUpDownTrainFrequency.Margin = new System.Windows.Forms.Padding(2);
            this.m_numericUpDownTrainFrequency.Name = "m_numericUpDownTrainFrequency";
            this.m_numericUpDownTrainFrequency.Size = new System.Drawing.Size(90, 20);
            this.m_numericUpDownTrainFrequency.TabIndex = 13;
            // 
            // m_groupBoxTrain
            // 
            this.m_groupBoxTrain.Controls.Add(this.m_checkedListBoxTrains);
            this.m_groupBoxTrain.Controls.Add(this.m_numericUpDownTrainFrequency);
            this.m_groupBoxTrain.Controls.Add(this.m_labelTrainFrequency);
            this.m_groupBoxTrain.Location = new System.Drawing.Point(12, 12);
            this.m_groupBoxTrain.Name = "m_groupBoxTrain";
            this.m_groupBoxTrain.Size = new System.Drawing.Size(259, 269);
            this.m_groupBoxTrain.TabIndex = 14;
            this.m_groupBoxTrain.TabStop = false;
            this.m_groupBoxTrain.Text = "Train Start";
            // 
            // m_groupBoxRoute
            // 
            this.m_groupBoxRoute.Controls.Add(this.m_textBoxEndRangeTrackID);
            this.m_groupBoxRoute.Controls.Add(this.m_textBoxStartRangeTrackID);
            this.m_groupBoxRoute.Controls.Add(this.m_labelEndTrackID);
            this.m_groupBoxRoute.Controls.Add(this.m_labelStartTrackID);
            this.m_groupBoxRoute.Controls.Add(this.m_radioButtonFromFileTracks);
            this.m_groupBoxRoute.Controls.Add(this.m_radioButtonManuelInputTracks);
            this.m_groupBoxRoute.Location = new System.Drawing.Point(277, 12);
            this.m_groupBoxRoute.Name = "m_groupBoxRoute";
            this.m_groupBoxRoute.Size = new System.Drawing.Size(342, 269);
            this.m_groupBoxRoute.TabIndex = 15;
            this.m_groupBoxRoute.TabStop = false;
            this.m_groupBoxRoute.Text = "Route";
            // 
            // m_textBoxEndRangeTrackID
            // 
            this.m_textBoxEndRangeTrackID.Location = new System.Drawing.Point(157, 116);
            this.m_textBoxEndRangeTrackID.Name = "m_textBoxEndRangeTrackID";
            this.m_textBoxEndRangeTrackID.Size = new System.Drawing.Size(147, 20);
            this.m_textBoxEndRangeTrackID.TabIndex = 5;
            // 
            // m_textBoxStartRangeTrackID
            // 
            this.m_textBoxStartRangeTrackID.Location = new System.Drawing.Point(157, 90);
            this.m_textBoxStartRangeTrackID.Name = "m_textBoxStartRangeTrackID";
            this.m_textBoxStartRangeTrackID.Size = new System.Drawing.Size(147, 20);
            this.m_textBoxStartRangeTrackID.TabIndex = 4;
            // 
            // m_labelEndTrackID
            // 
            this.m_labelEndTrackID.AutoSize = true;
            this.m_labelEndTrackID.Location = new System.Drawing.Point(41, 119);
            this.m_labelEndTrackID.Name = "m_labelEndTrackID";
            this.m_labelEndTrackID.Size = new System.Drawing.Size(80, 13);
            this.m_labelEndTrackID.TabIndex = 3;
            this.m_labelEndTrackID.Text = "End Track ID : ";
            // 
            // m_labelStartTrackID
            // 
            this.m_labelStartTrackID.AutoSize = true;
            this.m_labelStartTrackID.Location = new System.Drawing.Point(38, 93);
            this.m_labelStartTrackID.Name = "m_labelStartTrackID";
            this.m_labelStartTrackID.Size = new System.Drawing.Size(83, 13);
            this.m_labelStartTrackID.TabIndex = 2;
            this.m_labelStartTrackID.Text = "Start Track ID : ";
            // 
            // m_radioButtonFromFileTracks
            // 
            this.m_radioButtonFromFileTracks.AutoSize = true;
            this.m_radioButtonFromFileTracks.Location = new System.Drawing.Point(205, 160);
            this.m_radioButtonFromFileTracks.Name = "m_radioButtonFromFileTracks";
            this.m_radioButtonFromFileTracks.Size = new System.Drawing.Size(99, 17);
            this.m_radioButtonFromFileTracks.TabIndex = 1;
            this.m_radioButtonFromFileTracks.TabStop = true;
            this.m_radioButtonFromFileTracks.Text = "From File Tracls";
            this.m_radioButtonFromFileTracks.UseVisualStyleBackColor = true;
            // 
            // m_radioButtonManuelInputTracks
            // 
            this.m_radioButtonManuelInputTracks.AutoSize = true;
            this.m_radioButtonManuelInputTracks.Location = new System.Drawing.Point(41, 160);
            this.m_radioButtonManuelInputTracks.Name = "m_radioButtonManuelInputTracks";
            this.m_radioButtonManuelInputTracks.Size = new System.Drawing.Size(123, 17);
            this.m_radioButtonManuelInputTracks.TabIndex = 0;
            this.m_radioButtonManuelInputTracks.TabStop = true;
            this.m_radioButtonManuelInputTracks.Text = "Manuel Input Tracks";
            this.m_radioButtonManuelInputTracks.UseVisualStyleBackColor = true;
            // 
            // GeneralSettingsModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 339);
            this.Controls.Add(this.m_groupBoxRoute);
            this.Controls.Add(this.m_groupBoxTrain);
            this.Controls.Add(this.m_buttonApply);
            this.Controls.Add(this.m_buttonSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "GeneralSettingsModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "General Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GeneralSettingsModal_FormClosing);
            this.Load += new System.EventHandler(this.GeneralSettingsModal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_numericUpDownTrainFrequency)).EndInit();
            this.m_groupBoxTrain.ResumeLayout(false);
            this.m_groupBoxTrain.PerformLayout();
            this.m_groupBoxRoute.ResumeLayout(false);
            this.m_groupBoxRoute.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_buttonApply;
        private System.Windows.Forms.Button m_buttonSave;
        private System.Windows.Forms.CheckedListBox m_checkedListBoxTrains;
        private System.Windows.Forms.Label m_labelTrainFrequency;
        private System.Windows.Forms.NumericUpDown m_numericUpDownTrainFrequency;
        private System.Windows.Forms.GroupBox m_groupBoxTrain;
        private System.Windows.Forms.GroupBox m_groupBoxRoute;
        private System.Windows.Forms.TextBox m_textBoxEndRangeTrackID;
        private System.Windows.Forms.TextBox m_textBoxStartRangeTrackID;
        private System.Windows.Forms.Label m_labelEndTrackID;
        private System.Windows.Forms.Label m_labelStartTrackID;
        private System.Windows.Forms.RadioButton m_radioButtonFromFileTracks;
        private System.Windows.Forms.RadioButton m_radioButtonManuelInputTracks;
    }
}