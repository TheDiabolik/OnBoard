﻿namespace OnBoard
{
    partial class TrainSettingsModal
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
            this.m_groupBoxTrainSettings = new System.Windows.Forms.GroupBox();
            this.m_textBoxTrainDeceleration = new System.Windows.Forms.TextBox();
            this.m_textBoxTrainSpeedLimit = new System.Windows.Forms.TextBox();
            this.m_textBoxTrainAcceleration = new System.Windows.Forms.TextBox();
            this.m_textBoxTrainLength = new System.Windows.Forms.TextBox();
            this.m_labelTrainSpeedLimit = new System.Windows.Forms.Label();
            this.m_labelMaxTrainAcceleration = new System.Windows.Forms.Label();
            this.m_labelMaxTrainDeceleration = new System.Windows.Forms.Label();
            this.m_labelTrainLengthCM = new System.Windows.Forms.Label();
            this.m_buttonApply = new System.Windows.Forms.Button();
            this.m_buttonSave = new System.Windows.Forms.Button();
            this.m_groupBoxTrainSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_groupBoxTrainSettings
            // 
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxTrainDeceleration);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxTrainSpeedLimit);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxTrainAcceleration);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_textBoxTrainLength);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelTrainSpeedLimit);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelMaxTrainAcceleration);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelMaxTrainDeceleration);
            this.m_groupBoxTrainSettings.Controls.Add(this.m_labelTrainLengthCM);
            this.m_groupBoxTrainSettings.Location = new System.Drawing.Point(12, 22);
            this.m_groupBoxTrainSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_groupBoxTrainSettings.Name = "m_groupBoxTrainSettings";
            this.m_groupBoxTrainSettings.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_groupBoxTrainSettings.Size = new System.Drawing.Size(960, 116);
            this.m_groupBoxTrainSettings.TabIndex = 2;
            this.m_groupBoxTrainSettings.TabStop = false;
            this.m_groupBoxTrainSettings.Text = "General Train Settings";
            // 
            // m_textBoxTrainDeceleration
            // 
            this.m_textBoxTrainDeceleration.Location = new System.Drawing.Point(748, 68);
            this.m_textBoxTrainDeceleration.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_textBoxTrainDeceleration.Name = "m_textBoxTrainDeceleration";
            this.m_textBoxTrainDeceleration.Size = new System.Drawing.Size(165, 22);
            this.m_textBoxTrainDeceleration.TabIndex = 7;
            // 
            // m_textBoxTrainSpeedLimit
            // 
            this.m_textBoxTrainSpeedLimit.Location = new System.Drawing.Point(748, 39);
            this.m_textBoxTrainSpeedLimit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_textBoxTrainSpeedLimit.Name = "m_textBoxTrainSpeedLimit";
            this.m_textBoxTrainSpeedLimit.Size = new System.Drawing.Size(165, 22);
            this.m_textBoxTrainSpeedLimit.TabIndex = 6;
            // 
            // m_textBoxTrainAcceleration
            // 
            this.m_textBoxTrainAcceleration.Location = new System.Drawing.Point(288, 68);
            this.m_textBoxTrainAcceleration.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_textBoxTrainAcceleration.Name = "m_textBoxTrainAcceleration";
            this.m_textBoxTrainAcceleration.Size = new System.Drawing.Size(165, 22);
            this.m_textBoxTrainAcceleration.TabIndex = 5;
            // 
            // m_textBoxTrainLength
            // 
            this.m_textBoxTrainLength.Location = new System.Drawing.Point(288, 39);
            this.m_textBoxTrainLength.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_textBoxTrainLength.Name = "m_textBoxTrainLength";
            this.m_textBoxTrainLength.Size = new System.Drawing.Size(165, 22);
            this.m_textBoxTrainLength.TabIndex = 4;
            // 
            // m_labelTrainSpeedLimit
            // 
            this.m_labelTrainSpeedLimit.AutoSize = true;
            this.m_labelTrainSpeedLimit.Location = new System.Drawing.Point(539, 39);
            this.m_labelTrainSpeedLimit.Name = "m_labelTrainSpeedLimit";
            this.m_labelTrainSpeedLimit.Size = new System.Drawing.Size(175, 17);
            this.m_labelTrainSpeedLimit.TabIndex = 3;
            this.m_labelTrainSpeedLimit.Text = "Train Speed Limit (km/h) : ";
            // 
            // m_labelMaxTrainAcceleration
            // 
            this.m_labelMaxTrainAcceleration.AutoSize = true;
            this.m_labelMaxTrainAcceleration.Location = new System.Drawing.Point(85, 68);
            this.m_labelMaxTrainAcceleration.Name = "m_labelMaxTrainAcceleration";
            this.m_labelMaxTrainAcceleration.Size = new System.Drawing.Size(168, 17);
            this.m_labelMaxTrainAcceleration.TabIndex = 2;
            this.m_labelMaxTrainAcceleration.Text = "Max Acceleration (m/s²) : ";
            // 
            // m_labelMaxTrainDeceleration
            // 
            this.m_labelMaxTrainDeceleration.AutoSize = true;
            this.m_labelMaxTrainDeceleration.Location = new System.Drawing.Point(544, 68);
            this.m_labelMaxTrainDeceleration.Name = "m_labelMaxTrainDeceleration";
            this.m_labelMaxTrainDeceleration.Size = new System.Drawing.Size(170, 17);
            this.m_labelMaxTrainDeceleration.TabIndex = 1;
            this.m_labelMaxTrainDeceleration.Text = "Max Deceleration (m/s²) : ";
            // 
            // m_labelTrainLengthCM
            // 
            this.m_labelTrainLengthCM.AutoSize = true;
            this.m_labelTrainLengthCM.Location = new System.Drawing.Point(121, 39);
            this.m_labelTrainLengthCM.Name = "m_labelTrainLengthCM";
            this.m_labelTrainLengthCM.Size = new System.Drawing.Size(133, 17);
            this.m_labelTrainLengthCM.TabIndex = 0;
            this.m_labelTrainLengthCM.Text = "Train Length (cm) : ";
            // 
            // m_buttonApply
            // 
            this.m_buttonApply.Image = global::OnBoard.Properties.Resources.apply;
            this.m_buttonApply.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_buttonApply.Location = new System.Drawing.Point(814, 170);
            this.m_buttonApply.Name = "m_buttonApply";
            this.m_buttonApply.Size = new System.Drawing.Size(111, 59);
            this.m_buttonApply.TabIndex = 8;
            this.m_buttonApply.Text = "Apply";
            this.m_buttonApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_buttonApply.UseVisualStyleBackColor = true;
            this.m_buttonApply.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // m_buttonSave
            // 
            this.m_buttonSave.Image = global::OnBoard.Properties.Resources.save;
            this.m_buttonSave.Location = new System.Drawing.Point(697, 170);
            this.m_buttonSave.Name = "m_buttonSave";
            this.m_buttonSave.Size = new System.Drawing.Size(111, 59);
            this.m_buttonSave.TabIndex = 7;
            this.m_buttonSave.Text = "Save";
            this.m_buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_buttonSave.UseVisualStyleBackColor = true;
            this.m_buttonSave.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // TrainSettingsModal
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 240);
            this.Controls.Add(this.m_buttonApply);
            this.Controls.Add(this.m_buttonSave);
            this.Controls.Add(this.m_groupBoxTrainSettings);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "TrainSettingsModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Train Settings";
            this.Load += new System.EventHandler(this.TrainSettings_Load);
            this.m_groupBoxTrainSettings.ResumeLayout(false);
            this.m_groupBoxTrainSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox m_groupBoxTrainSettings;
        private System.Windows.Forms.TextBox m_textBoxTrainDeceleration;
        private System.Windows.Forms.TextBox m_textBoxTrainSpeedLimit;
        private System.Windows.Forms.TextBox m_textBoxTrainAcceleration;
        private System.Windows.Forms.TextBox m_textBoxTrainLength;
        private System.Windows.Forms.Label m_labelTrainSpeedLimit;
        private System.Windows.Forms.Label m_labelMaxTrainAcceleration;
        private System.Windows.Forms.Label m_labelMaxTrainDeceleration;
        private System.Windows.Forms.Label m_labelTrainLengthCM;
        private System.Windows.Forms.Button m_buttonApply;
        private System.Windows.Forms.Button m_buttonSave;
    }
}