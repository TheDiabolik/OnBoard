﻿namespace OnBoard
{
    partial class CommunicationSettingsModal
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
            this.m_groupBoxATS = new System.Windows.Forms.GroupBox();
            this.m_textBoxATSToOBATPPort = new System.Windows.Forms.TextBox();
            this.m_labelATSToOBATPPort = new System.Windows.Forms.Label();
            this.m_labelATSToOBATPIPAddress = new System.Windows.Forms.Label();
            this.m_ıpAddressControlATSToOBATP = new IPAddressControlLib.IPAddressControl();
            this.m_groupBoxWSATCToOBATP = new System.Windows.Forms.GroupBox();
            this.m_textBoxWSATCToOBATPPort = new System.Windows.Forms.TextBox();
            this.m_ıpAddressControlWSATCToOBATP = new IPAddressControlLib.IPAddressControl();
            this.m_labelWSATCToOBATPIPAddress = new System.Windows.Forms.Label();
            this.m_labelWSATCToOBATPPort = new System.Windows.Forms.Label();
            this.m_buttonApply = new System.Windows.Forms.Button();
            this.m_buttonSave = new System.Windows.Forms.Button();
            this.m_groupBoxOBATPToATS = new System.Windows.Forms.GroupBox();
            this.m_textBoxOBATPToATSPort = new System.Windows.Forms.TextBox();
            this.m_labelOBATPToATSPort = new System.Windows.Forms.Label();
            this.m_labelOBATPToATSIPAddress = new System.Windows.Forms.Label();
            this.m_ıpAddressControlOBATPToATS = new IPAddressControlLib.IPAddressControl();
            this.m_groupBoxOBATPToWSATC = new System.Windows.Forms.GroupBox();
            this.m_textBoxOBATPToWSATCPort = new System.Windows.Forms.TextBox();
            this.m_ıpAddressControlOBATPToWSATC = new IPAddressControlLib.IPAddressControl();
            this.m_labelOBATPToWSATCIPAddress = new System.Windows.Forms.Label();
            this.m_labelOBATPToWSATCPort = new System.Windows.Forms.Label();
            this.m_groupBoxATS.SuspendLayout();
            this.m_groupBoxWSATCToOBATP.SuspendLayout();
            this.m_groupBoxOBATPToATS.SuspendLayout();
            this.m_groupBoxOBATPToWSATC.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_groupBoxATS
            // 
            this.m_groupBoxATS.Controls.Add(this.m_textBoxATSToOBATPPort);
            this.m_groupBoxATS.Controls.Add(this.m_labelATSToOBATPPort);
            this.m_groupBoxATS.Controls.Add(this.m_labelATSToOBATPIPAddress);
            this.m_groupBoxATS.Controls.Add(this.m_ıpAddressControlATSToOBATP);
            this.m_groupBoxATS.Location = new System.Drawing.Point(9, 10);
            this.m_groupBoxATS.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_groupBoxATS.Name = "m_groupBoxATS";
            this.m_groupBoxATS.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_groupBoxATS.Size = new System.Drawing.Size(305, 81);
            this.m_groupBoxATS.TabIndex = 0;
            this.m_groupBoxATS.TabStop = false;
            this.m_groupBoxATS.Text = "ATS To OBATP";
            // 
            // m_textBoxATSToOBATPPort
            // 
            this.m_textBoxATSToOBATPPort.Location = new System.Drawing.Point(121, 49);
            this.m_textBoxATSToOBATPPort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_textBoxATSToOBATPPort.Name = "m_textBoxATSToOBATPPort";
            this.m_textBoxATSToOBATPPort.Size = new System.Drawing.Size(126, 20);
            this.m_textBoxATSToOBATPPort.TabIndex = 5;
            // 
            // m_labelATSToOBATPPort
            // 
            this.m_labelATSToOBATPPort.AutoSize = true;
            this.m_labelATSToOBATPPort.Location = new System.Drawing.Point(60, 49);
            this.m_labelATSToOBATPPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelATSToOBATPPort.Name = "m_labelATSToOBATPPort";
            this.m_labelATSToOBATPPort.Size = new System.Drawing.Size(35, 13);
            this.m_labelATSToOBATPPort.TabIndex = 4;
            this.m_labelATSToOBATPPort.Text = "Port : ";
            // 
            // m_labelATSToOBATPIPAddress
            // 
            this.m_labelATSToOBATPIPAddress.AutoSize = true;
            this.m_labelATSToOBATPIPAddress.Location = new System.Drawing.Point(28, 26);
            this.m_labelATSToOBATPIPAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelATSToOBATPIPAddress.Name = "m_labelATSToOBATPIPAddress";
            this.m_labelATSToOBATPIPAddress.Size = new System.Drawing.Size(67, 13);
            this.m_labelATSToOBATPIPAddress.TabIndex = 3;
            this.m_labelATSToOBATPIPAddress.Text = "IP Address : ";
            // 
            // m_ıpAddressControlATSToOBATP
            // 
            this.m_ıpAddressControlATSToOBATP.AllowInternalTab = false;
            this.m_ıpAddressControlATSToOBATP.AutoHeight = true;
            this.m_ıpAddressControlATSToOBATP.BackColor = System.Drawing.SystemColors.Window;
            this.m_ıpAddressControlATSToOBATP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_ıpAddressControlATSToOBATP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.m_ıpAddressControlATSToOBATP.Location = new System.Drawing.Point(161, 32);
            this.m_ıpAddressControlATSToOBATP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_ıpAddressControlATSToOBATP.Name = "m_ıpAddressControlATSToOBATP";
            this.m_ıpAddressControlATSToOBATP.ReadOnly = false;
            this.m_ıpAddressControlATSToOBATP.Size = new System.Drawing.Size(125, 20);
            this.m_ıpAddressControlATSToOBATP.TabIndex = 2;
            this.m_ıpAddressControlATSToOBATP.Text = "...";
            // 
            // m_groupBoxWSATCToOBATP
            // 
            this.m_groupBoxWSATCToOBATP.Controls.Add(this.m_textBoxWSATCToOBATPPort);
            this.m_groupBoxWSATCToOBATP.Controls.Add(this.m_ıpAddressControlWSATCToOBATP);
            this.m_groupBoxWSATCToOBATP.Controls.Add(this.m_labelWSATCToOBATPIPAddress);
            this.m_groupBoxWSATCToOBATP.Controls.Add(this.m_labelWSATCToOBATPPort);
            this.m_groupBoxWSATCToOBATP.Location = new System.Drawing.Point(9, 96);
            this.m_groupBoxWSATCToOBATP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_groupBoxWSATCToOBATP.Name = "m_groupBoxWSATCToOBATP";
            this.m_groupBoxWSATCToOBATP.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_groupBoxWSATCToOBATP.Size = new System.Drawing.Size(305, 81);
            this.m_groupBoxWSATCToOBATP.TabIndex = 1;
            this.m_groupBoxWSATCToOBATP.TabStop = false;
            this.m_groupBoxWSATCToOBATP.Text = "WSATC To OBATP";
            // 
            // m_textBoxWSATCToOBATPPort
            // 
            this.m_textBoxWSATCToOBATPPort.Location = new System.Drawing.Point(121, 49);
            this.m_textBoxWSATCToOBATPPort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_textBoxWSATCToOBATPPort.Name = "m_textBoxWSATCToOBATPPort";
            this.m_textBoxWSATCToOBATPPort.Size = new System.Drawing.Size(126, 20);
            this.m_textBoxWSATCToOBATPPort.TabIndex = 8;
            // 
            // m_ıpAddressControlWSATCToOBATP
            // 
            this.m_ıpAddressControlWSATCToOBATP.AllowInternalTab = false;
            this.m_ıpAddressControlWSATCToOBATP.AutoHeight = true;
            this.m_ıpAddressControlWSATCToOBATP.BackColor = System.Drawing.SystemColors.Window;
            this.m_ıpAddressControlWSATCToOBATP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_ıpAddressControlWSATCToOBATP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.m_ıpAddressControlWSATCToOBATP.Location = new System.Drawing.Point(161, 27);
            this.m_ıpAddressControlWSATCToOBATP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_ıpAddressControlWSATCToOBATP.Name = "m_ıpAddressControlWSATCToOBATP";
            this.m_ıpAddressControlWSATCToOBATP.ReadOnly = false;
            this.m_ıpAddressControlWSATCToOBATP.Size = new System.Drawing.Size(125, 20);
            this.m_ıpAddressControlWSATCToOBATP.TabIndex = 7;
            this.m_ıpAddressControlWSATCToOBATP.Text = "...";
            // 
            // m_labelWSATCToOBATPIPAddress
            // 
            this.m_labelWSATCToOBATPIPAddress.AutoSize = true;
            this.m_labelWSATCToOBATPIPAddress.Location = new System.Drawing.Point(28, 22);
            this.m_labelWSATCToOBATPIPAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelWSATCToOBATPIPAddress.Name = "m_labelWSATCToOBATPIPAddress";
            this.m_labelWSATCToOBATPIPAddress.Size = new System.Drawing.Size(67, 13);
            this.m_labelWSATCToOBATPIPAddress.TabIndex = 5;
            this.m_labelWSATCToOBATPIPAddress.Text = "IP Address : ";
            // 
            // m_labelWSATCToOBATPPort
            // 
            this.m_labelWSATCToOBATPPort.AutoSize = true;
            this.m_labelWSATCToOBATPPort.Location = new System.Drawing.Point(60, 45);
            this.m_labelWSATCToOBATPPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelWSATCToOBATPPort.Name = "m_labelWSATCToOBATPPort";
            this.m_labelWSATCToOBATPPort.Size = new System.Drawing.Size(35, 13);
            this.m_labelWSATCToOBATPPort.TabIndex = 6;
            this.m_labelWSATCToOBATPPort.Text = "Port : ";
            // 
            // m_buttonApply
            // 
            this.m_buttonApply.Image = global::OnBoard.Properties.Resources.apply;
            this.m_buttonApply.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_buttonApply.Location = new System.Drawing.Point(482, 209);
            this.m_buttonApply.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_buttonApply.Name = "m_buttonApply";
            this.m_buttonApply.Size = new System.Drawing.Size(83, 48);
            this.m_buttonApply.TabIndex = 12;
            this.m_buttonApply.Text = "Apply";
            this.m_buttonApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_buttonApply.UseVisualStyleBackColor = true;
            this.m_buttonApply.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // m_buttonSave
            // 
            this.m_buttonSave.Image = global::OnBoard.Properties.Resources.save;
            this.m_buttonSave.Location = new System.Drawing.Point(394, 209);
            this.m_buttonSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_buttonSave.Name = "m_buttonSave";
            this.m_buttonSave.Size = new System.Drawing.Size(83, 48);
            this.m_buttonSave.TabIndex = 11;
            this.m_buttonSave.Text = "Save";
            this.m_buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_buttonSave.UseVisualStyleBackColor = true;
            this.m_buttonSave.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // m_groupBoxOBATPToATS
            // 
            this.m_groupBoxOBATPToATS.Controls.Add(this.m_textBoxOBATPToATSPort);
            this.m_groupBoxOBATPToATS.Controls.Add(this.m_labelOBATPToATSPort);
            this.m_groupBoxOBATPToATS.Controls.Add(this.m_labelOBATPToATSIPAddress);
            this.m_groupBoxOBATPToATS.Controls.Add(this.m_ıpAddressControlOBATPToATS);
            this.m_groupBoxOBATPToATS.Location = new System.Drawing.Point(336, 10);
            this.m_groupBoxOBATPToATS.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_groupBoxOBATPToATS.Name = "m_groupBoxOBATPToATS";
            this.m_groupBoxOBATPToATS.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_groupBoxOBATPToATS.Size = new System.Drawing.Size(305, 81);
            this.m_groupBoxOBATPToATS.TabIndex = 13;
            this.m_groupBoxOBATPToATS.TabStop = false;
            this.m_groupBoxOBATPToATS.Text = "OBATP To ATS";
            // 
            // m_textBoxOBATPToATSPort
            // 
            this.m_textBoxOBATPToATSPort.Location = new System.Drawing.Point(121, 49);
            this.m_textBoxOBATPToATSPort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_textBoxOBATPToATSPort.Name = "m_textBoxOBATPToATSPort";
            this.m_textBoxOBATPToATSPort.Size = new System.Drawing.Size(132, 20);
            this.m_textBoxOBATPToATSPort.TabIndex = 5;
            // 
            // m_labelOBATPToATSPort
            // 
            this.m_labelOBATPToATSPort.AutoSize = true;
            this.m_labelOBATPToATSPort.Location = new System.Drawing.Point(60, 49);
            this.m_labelOBATPToATSPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelOBATPToATSPort.Name = "m_labelOBATPToATSPort";
            this.m_labelOBATPToATSPort.Size = new System.Drawing.Size(35, 13);
            this.m_labelOBATPToATSPort.TabIndex = 4;
            this.m_labelOBATPToATSPort.Text = "Port : ";
            // 
            // m_labelOBATPToATSIPAddress
            // 
            this.m_labelOBATPToATSIPAddress.AutoSize = true;
            this.m_labelOBATPToATSIPAddress.Location = new System.Drawing.Point(28, 26);
            this.m_labelOBATPToATSIPAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelOBATPToATSIPAddress.Name = "m_labelOBATPToATSIPAddress";
            this.m_labelOBATPToATSIPAddress.Size = new System.Drawing.Size(67, 13);
            this.m_labelOBATPToATSIPAddress.TabIndex = 3;
            this.m_labelOBATPToATSIPAddress.Text = "IP Address : ";
            // 
            // m_ıpAddressControlOBATPToATS
            // 
            this.m_ıpAddressControlOBATPToATS.AllowInternalTab = false;
            this.m_ıpAddressControlOBATPToATS.AutoHeight = true;
            this.m_ıpAddressControlOBATPToATS.BackColor = System.Drawing.SystemColors.Window;
            this.m_ıpAddressControlOBATPToATS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_ıpAddressControlOBATPToATS.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.m_ıpAddressControlOBATPToATS.Location = new System.Drawing.Point(161, 32);
            this.m_ıpAddressControlOBATPToATS.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_ıpAddressControlOBATPToATS.Name = "m_ıpAddressControlOBATPToATS";
            this.m_ıpAddressControlOBATPToATS.ReadOnly = false;
            this.m_ıpAddressControlOBATPToATS.Size = new System.Drawing.Size(130, 20);
            this.m_ıpAddressControlOBATPToATS.TabIndex = 2;
            this.m_ıpAddressControlOBATPToATS.Text = "...";
            // 
            // m_groupBoxOBATPToWSATC
            // 
            this.m_groupBoxOBATPToWSATC.Controls.Add(this.m_textBoxOBATPToWSATCPort);
            this.m_groupBoxOBATPToWSATC.Controls.Add(this.m_ıpAddressControlOBATPToWSATC);
            this.m_groupBoxOBATPToWSATC.Controls.Add(this.m_labelOBATPToWSATCIPAddress);
            this.m_groupBoxOBATPToWSATC.Controls.Add(this.m_labelOBATPToWSATCPort);
            this.m_groupBoxOBATPToWSATC.Location = new System.Drawing.Point(336, 96);
            this.m_groupBoxOBATPToWSATC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_groupBoxOBATPToWSATC.Name = "m_groupBoxOBATPToWSATC";
            this.m_groupBoxOBATPToWSATC.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_groupBoxOBATPToWSATC.Size = new System.Drawing.Size(305, 81);
            this.m_groupBoxOBATPToWSATC.TabIndex = 14;
            this.m_groupBoxOBATPToWSATC.TabStop = false;
            this.m_groupBoxOBATPToWSATC.Text = "OBATP To WSATC";
            // 
            // m_textBoxOBATPToWSATCPort
            // 
            this.m_textBoxOBATPToWSATCPort.Location = new System.Drawing.Point(121, 49);
            this.m_textBoxOBATPToWSATCPort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_textBoxOBATPToWSATCPort.Name = "m_textBoxOBATPToWSATCPort";
            this.m_textBoxOBATPToWSATCPort.Size = new System.Drawing.Size(132, 20);
            this.m_textBoxOBATPToWSATCPort.TabIndex = 8;
            // 
            // m_ıpAddressControlOBATPToWSATC
            // 
            this.m_ıpAddressControlOBATPToWSATC.AllowInternalTab = false;
            this.m_ıpAddressControlOBATPToWSATC.AutoHeight = true;
            this.m_ıpAddressControlOBATPToWSATC.BackColor = System.Drawing.SystemColors.Window;
            this.m_ıpAddressControlOBATPToWSATC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_ıpAddressControlOBATPToWSATC.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.m_ıpAddressControlOBATPToWSATC.Location = new System.Drawing.Point(161, 27);
            this.m_ıpAddressControlOBATPToWSATC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.m_ıpAddressControlOBATPToWSATC.Name = "m_ıpAddressControlOBATPToWSATC";
            this.m_ıpAddressControlOBATPToWSATC.ReadOnly = false;
            this.m_ıpAddressControlOBATPToWSATC.Size = new System.Drawing.Size(130, 20);
            this.m_ıpAddressControlOBATPToWSATC.TabIndex = 7;
            this.m_ıpAddressControlOBATPToWSATC.Text = "...";
            // 
            // m_labelOBATPToWSATCIPAddress
            // 
            this.m_labelOBATPToWSATCIPAddress.AutoSize = true;
            this.m_labelOBATPToWSATCIPAddress.Location = new System.Drawing.Point(28, 22);
            this.m_labelOBATPToWSATCIPAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelOBATPToWSATCIPAddress.Name = "m_labelOBATPToWSATCIPAddress";
            this.m_labelOBATPToWSATCIPAddress.Size = new System.Drawing.Size(67, 13);
            this.m_labelOBATPToWSATCIPAddress.TabIndex = 5;
            this.m_labelOBATPToWSATCIPAddress.Text = "IP Address : ";
            // 
            // m_labelOBATPToWSATCPort
            // 
            this.m_labelOBATPToWSATCPort.AutoSize = true;
            this.m_labelOBATPToWSATCPort.Location = new System.Drawing.Point(60, 45);
            this.m_labelOBATPToWSATCPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_labelOBATPToWSATCPort.Name = "m_labelOBATPToWSATCPort";
            this.m_labelOBATPToWSATCPort.Size = new System.Drawing.Size(35, 13);
            this.m_labelOBATPToWSATCPort.TabIndex = 6;
            this.m_labelOBATPToWSATCPort.Text = "Port : ";
            // 
            // CommunicationSettingsModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 275);
            this.Controls.Add(this.m_groupBoxOBATPToWSATC);
            this.Controls.Add(this.m_groupBoxOBATPToATS);
            this.Controls.Add(this.m_buttonApply);
            this.Controls.Add(this.m_buttonSave);
            this.Controls.Add(this.m_groupBoxWSATCToOBATP);
            this.Controls.Add(this.m_groupBoxATS);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "CommunicationSettingsModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Communication Settings";
            this.Load += new System.EventHandler(this.CommunicationSettingsModal_Load);
            this.m_groupBoxATS.ResumeLayout(false);
            this.m_groupBoxATS.PerformLayout();
            this.m_groupBoxWSATCToOBATP.ResumeLayout(false);
            this.m_groupBoxWSATCToOBATP.PerformLayout();
            this.m_groupBoxOBATPToATS.ResumeLayout(false);
            this.m_groupBoxOBATPToATS.PerformLayout();
            this.m_groupBoxOBATPToWSATC.ResumeLayout(false);
            this.m_groupBoxOBATPToWSATC.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox m_groupBoxATS;
        private System.Windows.Forms.TextBox m_textBoxATSToOBATPPort;
        private System.Windows.Forms.Label m_labelATSToOBATPPort;
        private System.Windows.Forms.Label m_labelATSToOBATPIPAddress;
        private IPAddressControlLib.IPAddressControl m_ıpAddressControlATSToOBATP;
        private System.Windows.Forms.GroupBox m_groupBoxWSATCToOBATP;
        private System.Windows.Forms.TextBox m_textBoxWSATCToOBATPPort;
        private IPAddressControlLib.IPAddressControl m_ıpAddressControlWSATCToOBATP;
        private System.Windows.Forms.Label m_labelWSATCToOBATPIPAddress;
        private System.Windows.Forms.Label m_labelWSATCToOBATPPort;
        private System.Windows.Forms.Button m_buttonApply;
        private System.Windows.Forms.Button m_buttonSave;
        private System.Windows.Forms.GroupBox m_groupBoxOBATPToATS;
        private System.Windows.Forms.TextBox m_textBoxOBATPToATSPort;
        private System.Windows.Forms.Label m_labelOBATPToATSPort;
        private System.Windows.Forms.Label m_labelOBATPToATSIPAddress;
        private IPAddressControlLib.IPAddressControl m_ıpAddressControlOBATPToATS;
        private System.Windows.Forms.GroupBox m_groupBoxOBATPToWSATC;
        private System.Windows.Forms.TextBox m_textBoxOBATPToWSATCPort;
        private IPAddressControlLib.IPAddressControl m_ıpAddressControlOBATPToWSATC;
        private System.Windows.Forms.Label m_labelOBATPToWSATCIPAddress;
        private System.Windows.Forms.Label m_labelOBATPToWSATCPort;
    }
}