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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommunicationSettingsModal));
            this.m_textBoxATSToOBATPPort = new System.Windows.Forms.TextBox();
            this.m_labelATSToOBATPPort = new System.Windows.Forms.Label();
            this.m_labelATSToOBATPIPAddress = new System.Windows.Forms.Label();
            this.m_ıpAddressControlATSToOBATP = new IPAddressControlLib.IPAddressControl();
            this.m_buttonApply = new System.Windows.Forms.Button();
            this.m_buttonSave = new System.Windows.Forms.Button();
            this.m_textBoxOBATPToATSPort = new System.Windows.Forms.TextBox();
            this.m_labelOBATPToATSPort = new System.Windows.Forms.Label();
            this.m_labelOBATPToATSIPAddress = new System.Windows.Forms.Label();
            this.m_ıpAddressControlOBATPToATS = new IPAddressControlLib.IPAddressControl();
            this.m_radioButtonOBATPToWSATCClient = new System.Windows.Forms.RadioButton();
            this.m_radioButtonOBATPToWSATCServer = new System.Windows.Forms.RadioButton();
            this.m_textBoxOBATPToWSATCPort = new System.Windows.Forms.TextBox();
            this.m_ıpAddressControlOBATPToWSATC = new IPAddressControlLib.IPAddressControl();
            this.m_labelOBATPToWSATCIPAddress = new System.Windows.Forms.Label();
            this.m_labelOBATPToWSATCPort = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.m_tabPageOBATPToWSATC = new System.Windows.Forms.TabPage();
            this.m_tabPageOBATPToATS = new System.Windows.Forms.TabPage();
            this.m_radioButtonOBATPToATSClient = new System.Windows.Forms.RadioButton();
            this.m_radioButtonOBATPToATSServer = new System.Windows.Forms.RadioButton();
            this.m_tabPageATSToOBATP = new System.Windows.Forms.TabPage();
            this.m_radioButtonATSToOBATPClient = new System.Windows.Forms.RadioButton();
            this.m_radioButtonATSToOBATPServer = new System.Windows.Forms.RadioButton();
            this.m_tabPageWSATCToOBATP = new System.Windows.Forms.TabPage();
            this.m_radioButtonWSATCToOBATPClient = new System.Windows.Forms.RadioButton();
            this.m_radioButtonWSATCToOBATPServer = new System.Windows.Forms.RadioButton();
            this.m_textBoxWSATCToOBATPPort = new System.Windows.Forms.TextBox();
            this.m_ıpAddressControlWSATCToOBATP = new IPAddressControlLib.IPAddressControl();
            this.m_labelWSATCToOBATPIPAddress = new System.Windows.Forms.Label();
            this.m_labelWSATCToOBATPPort = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.m_tabPageOBATPToWSATC.SuspendLayout();
            this.m_tabPageOBATPToATS.SuspendLayout();
            this.m_tabPageATSToOBATP.SuspendLayout();
            this.m_tabPageWSATCToOBATP.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_textBoxATSToOBATPPort
            // 
            this.m_textBoxATSToOBATPPort.Location = new System.Drawing.Point(225, 64);
            this.m_textBoxATSToOBATPPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_textBoxATSToOBATPPort.Name = "m_textBoxATSToOBATPPort";
            this.m_textBoxATSToOBATPPort.Size = new System.Drawing.Size(175, 22);
            this.m_textBoxATSToOBATPPort.TabIndex = 5;
            // 
            // m_labelATSToOBATPPort
            // 
            this.m_labelATSToOBATPPort.AutoSize = true;
            this.m_labelATSToOBATPPort.Location = new System.Drawing.Point(144, 59);
            this.m_labelATSToOBATPPort.Name = "m_labelATSToOBATPPort";
            this.m_labelATSToOBATPPort.Size = new System.Drawing.Size(46, 17);
            this.m_labelATSToOBATPPort.TabIndex = 4;
            this.m_labelATSToOBATPPort.Text = "Port : ";
            // 
            // m_labelATSToOBATPIPAddress
            // 
            this.m_labelATSToOBATPIPAddress.AutoSize = true;
            this.m_labelATSToOBATPIPAddress.Location = new System.Drawing.Point(101, 32);
            this.m_labelATSToOBATPIPAddress.Name = "m_labelATSToOBATPIPAddress";
            this.m_labelATSToOBATPIPAddress.Size = new System.Drawing.Size(88, 17);
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
            this.m_ıpAddressControlATSToOBATP.Location = new System.Drawing.Point(225, 26);
            this.m_ıpAddressControlATSToOBATP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_ıpAddressControlATSToOBATP.Name = "m_ıpAddressControlATSToOBATP";
            this.m_ıpAddressControlATSToOBATP.ReadOnly = false;
            this.m_ıpAddressControlATSToOBATP.Size = new System.Drawing.Size(176, 22);
            this.m_ıpAddressControlATSToOBATP.TabIndex = 2;
            this.m_ıpAddressControlATSToOBATP.Text = "...";
            // 
            // m_buttonApply
            // 
            this.m_buttonApply.Image = global::OnBoard.Properties.Resources.apply;
            this.m_buttonApply.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_buttonApply.Location = new System.Drawing.Point(417, 229);
            this.m_buttonApply.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_buttonApply.Name = "m_buttonApply";
            this.m_buttonApply.Size = new System.Drawing.Size(111, 59);
            this.m_buttonApply.TabIndex = 12;
            this.m_buttonApply.Text = "Apply";
            this.m_buttonApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_buttonApply.UseVisualStyleBackColor = true;
            this.m_buttonApply.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // m_buttonSave
            // 
            this.m_buttonSave.Image = global::OnBoard.Properties.Resources.save;
            this.m_buttonSave.Location = new System.Drawing.Point(299, 229);
            this.m_buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_buttonSave.Name = "m_buttonSave";
            this.m_buttonSave.Size = new System.Drawing.Size(111, 59);
            this.m_buttonSave.TabIndex = 11;
            this.m_buttonSave.Text = "Save";
            this.m_buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_buttonSave.UseVisualStyleBackColor = true;
            this.m_buttonSave.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // m_textBoxOBATPToATSPort
            // 
            this.m_textBoxOBATPToATSPort.Location = new System.Drawing.Point(225, 64);
            this.m_textBoxOBATPToATSPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_textBoxOBATPToATSPort.Name = "m_textBoxOBATPToATSPort";
            this.m_textBoxOBATPToATSPort.Size = new System.Drawing.Size(175, 22);
            this.m_textBoxOBATPToATSPort.TabIndex = 5;
            // 
            // m_labelOBATPToATSPort
            // 
            this.m_labelOBATPToATSPort.AutoSize = true;
            this.m_labelOBATPToATSPort.Location = new System.Drawing.Point(144, 59);
            this.m_labelOBATPToATSPort.Name = "m_labelOBATPToATSPort";
            this.m_labelOBATPToATSPort.Size = new System.Drawing.Size(46, 17);
            this.m_labelOBATPToATSPort.TabIndex = 4;
            this.m_labelOBATPToATSPort.Text = "Port : ";
            // 
            // m_labelOBATPToATSIPAddress
            // 
            this.m_labelOBATPToATSIPAddress.AutoSize = true;
            this.m_labelOBATPToATSIPAddress.Location = new System.Drawing.Point(101, 32);
            this.m_labelOBATPToATSIPAddress.Name = "m_labelOBATPToATSIPAddress";
            this.m_labelOBATPToATSIPAddress.Size = new System.Drawing.Size(88, 17);
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
            this.m_ıpAddressControlOBATPToATS.Location = new System.Drawing.Point(225, 26);
            this.m_ıpAddressControlOBATPToATS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_ıpAddressControlOBATPToATS.Name = "m_ıpAddressControlOBATPToATS";
            this.m_ıpAddressControlOBATPToATS.ReadOnly = false;
            this.m_ıpAddressControlOBATPToATS.Size = new System.Drawing.Size(176, 22);
            this.m_ıpAddressControlOBATPToATS.TabIndex = 2;
            this.m_ıpAddressControlOBATPToATS.Text = "...";
            // 
            // m_radioButtonOBATPToWSATCClient
            // 
            this.m_radioButtonOBATPToWSATCClient.AutoSize = true;
            this.m_radioButtonOBATPToWSATCClient.Location = new System.Drawing.Point(333, 111);
            this.m_radioButtonOBATPToWSATCClient.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_radioButtonOBATPToWSATCClient.Name = "m_radioButtonOBATPToWSATCClient";
            this.m_radioButtonOBATPToWSATCClient.Size = new System.Drawing.Size(64, 21);
            this.m_radioButtonOBATPToWSATCClient.TabIndex = 10;
            this.m_radioButtonOBATPToWSATCClient.TabStop = true;
            this.m_radioButtonOBATPToWSATCClient.Text = "Client";
            this.m_radioButtonOBATPToWSATCClient.UseVisualStyleBackColor = true;
            // 
            // m_radioButtonOBATPToWSATCServer
            // 
            this.m_radioButtonOBATPToWSATCServer.AutoSize = true;
            this.m_radioButtonOBATPToWSATCServer.Location = new System.Drawing.Point(225, 111);
            this.m_radioButtonOBATPToWSATCServer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_radioButtonOBATPToWSATCServer.Name = "m_radioButtonOBATPToWSATCServer";
            this.m_radioButtonOBATPToWSATCServer.Size = new System.Drawing.Size(71, 21);
            this.m_radioButtonOBATPToWSATCServer.TabIndex = 9;
            this.m_radioButtonOBATPToWSATCServer.TabStop = true;
            this.m_radioButtonOBATPToWSATCServer.Text = "Server";
            this.m_radioButtonOBATPToWSATCServer.UseVisualStyleBackColor = true;
            // 
            // m_textBoxOBATPToWSATCPort
            // 
            this.m_textBoxOBATPToWSATCPort.Location = new System.Drawing.Point(225, 64);
            this.m_textBoxOBATPToWSATCPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_textBoxOBATPToWSATCPort.Name = "m_textBoxOBATPToWSATCPort";
            this.m_textBoxOBATPToWSATCPort.Size = new System.Drawing.Size(175, 22);
            this.m_textBoxOBATPToWSATCPort.TabIndex = 8;
            // 
            // m_ıpAddressControlOBATPToWSATC
            // 
            this.m_ıpAddressControlOBATPToWSATC.AllowInternalTab = false;
            this.m_ıpAddressControlOBATPToWSATC.AutoHeight = true;
            this.m_ıpAddressControlOBATPToWSATC.BackColor = System.Drawing.SystemColors.Window;
            this.m_ıpAddressControlOBATPToWSATC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_ıpAddressControlOBATPToWSATC.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.m_ıpAddressControlOBATPToWSATC.Location = new System.Drawing.Point(225, 26);
            this.m_ıpAddressControlOBATPToWSATC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_ıpAddressControlOBATPToWSATC.Name = "m_ıpAddressControlOBATPToWSATC";
            this.m_ıpAddressControlOBATPToWSATC.ReadOnly = false;
            this.m_ıpAddressControlOBATPToWSATC.Size = new System.Drawing.Size(176, 22);
            this.m_ıpAddressControlOBATPToWSATC.TabIndex = 7;
            this.m_ıpAddressControlOBATPToWSATC.Text = "...";
            // 
            // m_labelOBATPToWSATCIPAddress
            // 
            this.m_labelOBATPToWSATCIPAddress.AutoSize = true;
            this.m_labelOBATPToWSATCIPAddress.Location = new System.Drawing.Point(101, 32);
            this.m_labelOBATPToWSATCIPAddress.Name = "m_labelOBATPToWSATCIPAddress";
            this.m_labelOBATPToWSATCIPAddress.Size = new System.Drawing.Size(88, 17);
            this.m_labelOBATPToWSATCIPAddress.TabIndex = 5;
            this.m_labelOBATPToWSATCIPAddress.Text = "IP Address : ";
            // 
            // m_labelOBATPToWSATCPort
            // 
            this.m_labelOBATPToWSATCPort.AutoSize = true;
            this.m_labelOBATPToWSATCPort.Location = new System.Drawing.Point(144, 59);
            this.m_labelOBATPToWSATCPort.Name = "m_labelOBATPToWSATCPort";
            this.m_labelOBATPToWSATCPort.Size = new System.Drawing.Size(46, 17);
            this.m_labelOBATPToWSATCPort.TabIndex = 6;
            this.m_labelOBATPToWSATCPort.Text = "Port : ";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.m_tabPageOBATPToWSATC);
            this.tabControl1.Controls.Add(this.m_tabPageOBATPToATS);
            this.tabControl1.Controls.Add(this.m_tabPageATSToOBATP);
            this.tabControl1.Controls.Add(this.m_tabPageWSATCToOBATP);
            this.tabControl1.Location = new System.Drawing.Point(16, 15);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(533, 208);
            this.tabControl1.TabIndex = 15;
            // 
            // m_tabPageOBATPToWSATC
            // 
            this.m_tabPageOBATPToWSATC.Controls.Add(this.m_radioButtonOBATPToWSATCClient);
            this.m_tabPageOBATPToWSATC.Controls.Add(this.m_ıpAddressControlOBATPToWSATC);
            this.m_tabPageOBATPToWSATC.Controls.Add(this.m_radioButtonOBATPToWSATCServer);
            this.m_tabPageOBATPToWSATC.Controls.Add(this.m_labelOBATPToWSATCPort);
            this.m_tabPageOBATPToWSATC.Controls.Add(this.m_textBoxOBATPToWSATCPort);
            this.m_tabPageOBATPToWSATC.Controls.Add(this.m_labelOBATPToWSATCIPAddress);
            this.m_tabPageOBATPToWSATC.Location = new System.Drawing.Point(4, 25);
            this.m_tabPageOBATPToWSATC.Margin = new System.Windows.Forms.Padding(4);
            this.m_tabPageOBATPToWSATC.Name = "m_tabPageOBATPToWSATC";
            this.m_tabPageOBATPToWSATC.Padding = new System.Windows.Forms.Padding(4);
            this.m_tabPageOBATPToWSATC.Size = new System.Drawing.Size(525, 179);
            this.m_tabPageOBATPToWSATC.TabIndex = 0;
            this.m_tabPageOBATPToWSATC.Text = "OBATP To WSATC";
            this.m_tabPageOBATPToWSATC.UseVisualStyleBackColor = true;
            // 
            // m_tabPageOBATPToATS
            // 
            this.m_tabPageOBATPToATS.Controls.Add(this.m_radioButtonOBATPToATSClient);
            this.m_tabPageOBATPToATS.Controls.Add(this.m_radioButtonOBATPToATSServer);
            this.m_tabPageOBATPToATS.Controls.Add(this.m_textBoxOBATPToATSPort);
            this.m_tabPageOBATPToATS.Controls.Add(this.m_ıpAddressControlOBATPToATS);
            this.m_tabPageOBATPToATS.Controls.Add(this.m_labelOBATPToATSPort);
            this.m_tabPageOBATPToATS.Controls.Add(this.m_labelOBATPToATSIPAddress);
            this.m_tabPageOBATPToATS.Location = new System.Drawing.Point(4, 25);
            this.m_tabPageOBATPToATS.Margin = new System.Windows.Forms.Padding(4);
            this.m_tabPageOBATPToATS.Name = "m_tabPageOBATPToATS";
            this.m_tabPageOBATPToATS.Padding = new System.Windows.Forms.Padding(4);
            this.m_tabPageOBATPToATS.Size = new System.Drawing.Size(525, 179);
            this.m_tabPageOBATPToATS.TabIndex = 1;
            this.m_tabPageOBATPToATS.Text = "OBATP To ATS";
            this.m_tabPageOBATPToATS.UseVisualStyleBackColor = true;
            // 
            // m_radioButtonOBATPToATSClient
            // 
            this.m_radioButtonOBATPToATSClient.AutoSize = true;
            this.m_radioButtonOBATPToATSClient.Location = new System.Drawing.Point(333, 111);
            this.m_radioButtonOBATPToATSClient.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_radioButtonOBATPToATSClient.Name = "m_radioButtonOBATPToATSClient";
            this.m_radioButtonOBATPToATSClient.Size = new System.Drawing.Size(64, 21);
            this.m_radioButtonOBATPToATSClient.TabIndex = 12;
            this.m_radioButtonOBATPToATSClient.TabStop = true;
            this.m_radioButtonOBATPToATSClient.Text = "Client";
            this.m_radioButtonOBATPToATSClient.UseVisualStyleBackColor = true;
            // 
            // m_radioButtonOBATPToATSServer
            // 
            this.m_radioButtonOBATPToATSServer.AutoSize = true;
            this.m_radioButtonOBATPToATSServer.Location = new System.Drawing.Point(225, 111);
            this.m_radioButtonOBATPToATSServer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_radioButtonOBATPToATSServer.Name = "m_radioButtonOBATPToATSServer";
            this.m_radioButtonOBATPToATSServer.Size = new System.Drawing.Size(71, 21);
            this.m_radioButtonOBATPToATSServer.TabIndex = 11;
            this.m_radioButtonOBATPToATSServer.TabStop = true;
            this.m_radioButtonOBATPToATSServer.Text = "Server";
            this.m_radioButtonOBATPToATSServer.UseVisualStyleBackColor = true;
            // 
            // m_tabPageATSToOBATP
            // 
            this.m_tabPageATSToOBATP.Controls.Add(this.m_radioButtonATSToOBATPClient);
            this.m_tabPageATSToOBATP.Controls.Add(this.m_radioButtonATSToOBATPServer);
            this.m_tabPageATSToOBATP.Controls.Add(this.m_textBoxATSToOBATPPort);
            this.m_tabPageATSToOBATP.Controls.Add(this.m_ıpAddressControlATSToOBATP);
            this.m_tabPageATSToOBATP.Controls.Add(this.m_labelATSToOBATPPort);
            this.m_tabPageATSToOBATP.Controls.Add(this.m_labelATSToOBATPIPAddress);
            this.m_tabPageATSToOBATP.Location = new System.Drawing.Point(4, 25);
            this.m_tabPageATSToOBATP.Margin = new System.Windows.Forms.Padding(4);
            this.m_tabPageATSToOBATP.Name = "m_tabPageATSToOBATP";
            this.m_tabPageATSToOBATP.Padding = new System.Windows.Forms.Padding(4);
            this.m_tabPageATSToOBATP.Size = new System.Drawing.Size(525, 179);
            this.m_tabPageATSToOBATP.TabIndex = 2;
            this.m_tabPageATSToOBATP.Text = "ATS To OBATP";
            this.m_tabPageATSToOBATP.UseVisualStyleBackColor = true;
            // 
            // m_radioButtonATSToOBATPClient
            // 
            this.m_radioButtonATSToOBATPClient.AutoSize = true;
            this.m_radioButtonATSToOBATPClient.Location = new System.Drawing.Point(333, 111);
            this.m_radioButtonATSToOBATPClient.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_radioButtonATSToOBATPClient.Name = "m_radioButtonATSToOBATPClient";
            this.m_radioButtonATSToOBATPClient.Size = new System.Drawing.Size(64, 21);
            this.m_radioButtonATSToOBATPClient.TabIndex = 12;
            this.m_radioButtonATSToOBATPClient.TabStop = true;
            this.m_radioButtonATSToOBATPClient.Text = "Client";
            this.m_radioButtonATSToOBATPClient.UseVisualStyleBackColor = true;
            // 
            // m_radioButtonATSToOBATPServer
            // 
            this.m_radioButtonATSToOBATPServer.AutoSize = true;
            this.m_radioButtonATSToOBATPServer.Location = new System.Drawing.Point(225, 111);
            this.m_radioButtonATSToOBATPServer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_radioButtonATSToOBATPServer.Name = "m_radioButtonATSToOBATPServer";
            this.m_radioButtonATSToOBATPServer.Size = new System.Drawing.Size(71, 21);
            this.m_radioButtonATSToOBATPServer.TabIndex = 11;
            this.m_radioButtonATSToOBATPServer.TabStop = true;
            this.m_radioButtonATSToOBATPServer.Text = "Server";
            this.m_radioButtonATSToOBATPServer.UseVisualStyleBackColor = true;
            // 
            // m_tabPageWSATCToOBATP
            // 
            this.m_tabPageWSATCToOBATP.Controls.Add(this.m_radioButtonWSATCToOBATPClient);
            this.m_tabPageWSATCToOBATP.Controls.Add(this.m_radioButtonWSATCToOBATPServer);
            this.m_tabPageWSATCToOBATP.Controls.Add(this.m_textBoxWSATCToOBATPPort);
            this.m_tabPageWSATCToOBATP.Controls.Add(this.m_ıpAddressControlWSATCToOBATP);
            this.m_tabPageWSATCToOBATP.Controls.Add(this.m_labelWSATCToOBATPIPAddress);
            this.m_tabPageWSATCToOBATP.Controls.Add(this.m_labelWSATCToOBATPPort);
            this.m_tabPageWSATCToOBATP.Location = new System.Drawing.Point(4, 25);
            this.m_tabPageWSATCToOBATP.Margin = new System.Windows.Forms.Padding(4);
            this.m_tabPageWSATCToOBATP.Name = "m_tabPageWSATCToOBATP";
            this.m_tabPageWSATCToOBATP.Padding = new System.Windows.Forms.Padding(4);
            this.m_tabPageWSATCToOBATP.Size = new System.Drawing.Size(525, 179);
            this.m_tabPageWSATCToOBATP.TabIndex = 3;
            this.m_tabPageWSATCToOBATP.Text = "WSATC To OBATP";
            this.m_tabPageWSATCToOBATP.UseVisualStyleBackColor = true;
            // 
            // m_radioButtonWSATCToOBATPClient
            // 
            this.m_radioButtonWSATCToOBATPClient.AutoSize = true;
            this.m_radioButtonWSATCToOBATPClient.Location = new System.Drawing.Point(333, 111);
            this.m_radioButtonWSATCToOBATPClient.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_radioButtonWSATCToOBATPClient.Name = "m_radioButtonWSATCToOBATPClient";
            this.m_radioButtonWSATCToOBATPClient.Size = new System.Drawing.Size(64, 21);
            this.m_radioButtonWSATCToOBATPClient.TabIndex = 14;
            this.m_radioButtonWSATCToOBATPClient.TabStop = true;
            this.m_radioButtonWSATCToOBATPClient.Text = "Client";
            this.m_radioButtonWSATCToOBATPClient.UseVisualStyleBackColor = true;
            // 
            // m_radioButtonWSATCToOBATPServer
            // 
            this.m_radioButtonWSATCToOBATPServer.AutoSize = true;
            this.m_radioButtonWSATCToOBATPServer.Location = new System.Drawing.Point(225, 111);
            this.m_radioButtonWSATCToOBATPServer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_radioButtonWSATCToOBATPServer.Name = "m_radioButtonWSATCToOBATPServer";
            this.m_radioButtonWSATCToOBATPServer.Size = new System.Drawing.Size(71, 21);
            this.m_radioButtonWSATCToOBATPServer.TabIndex = 13;
            this.m_radioButtonWSATCToOBATPServer.TabStop = true;
            this.m_radioButtonWSATCToOBATPServer.Text = "Server";
            this.m_radioButtonWSATCToOBATPServer.UseVisualStyleBackColor = true;
            // 
            // m_textBoxWSATCToOBATPPort
            // 
            this.m_textBoxWSATCToOBATPPort.Location = new System.Drawing.Point(225, 64);
            this.m_textBoxWSATCToOBATPPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_textBoxWSATCToOBATPPort.Name = "m_textBoxWSATCToOBATPPort";
            this.m_textBoxWSATCToOBATPPort.Size = new System.Drawing.Size(175, 22);
            this.m_textBoxWSATCToOBATPPort.TabIndex = 12;
            // 
            // m_ıpAddressControlWSATCToOBATP
            // 
            this.m_ıpAddressControlWSATCToOBATP.AllowInternalTab = false;
            this.m_ıpAddressControlWSATCToOBATP.AutoHeight = true;
            this.m_ıpAddressControlWSATCToOBATP.BackColor = System.Drawing.SystemColors.Window;
            this.m_ıpAddressControlWSATCToOBATP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_ıpAddressControlWSATCToOBATP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.m_ıpAddressControlWSATCToOBATP.Location = new System.Drawing.Point(225, 26);
            this.m_ıpAddressControlWSATCToOBATP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_ıpAddressControlWSATCToOBATP.Name = "m_ıpAddressControlWSATCToOBATP";
            this.m_ıpAddressControlWSATCToOBATP.ReadOnly = false;
            this.m_ıpAddressControlWSATCToOBATP.Size = new System.Drawing.Size(176, 22);
            this.m_ıpAddressControlWSATCToOBATP.TabIndex = 11;
            this.m_ıpAddressControlWSATCToOBATP.Text = "...";
            // 
            // m_labelWSATCToOBATPIPAddress
            // 
            this.m_labelWSATCToOBATPIPAddress.AutoSize = true;
            this.m_labelWSATCToOBATPIPAddress.Location = new System.Drawing.Point(101, 32);
            this.m_labelWSATCToOBATPIPAddress.Name = "m_labelWSATCToOBATPIPAddress";
            this.m_labelWSATCToOBATPIPAddress.Size = new System.Drawing.Size(88, 17);
            this.m_labelWSATCToOBATPIPAddress.TabIndex = 9;
            this.m_labelWSATCToOBATPIPAddress.Text = "IP Address : ";
            // 
            // m_labelWSATCToOBATPPort
            // 
            this.m_labelWSATCToOBATPPort.AutoSize = true;
            this.m_labelWSATCToOBATPPort.Location = new System.Drawing.Point(144, 59);
            this.m_labelWSATCToOBATPPort.Name = "m_labelWSATCToOBATPPort";
            this.m_labelWSATCToOBATPPort.Size = new System.Drawing.Size(46, 17);
            this.m_labelWSATCToOBATPPort.TabIndex = 10;
            this.m_labelWSATCToOBATPPort.Text = "Port : ";
            // 
            // CommunicationSettingsModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 292);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.m_buttonApply);
            this.Controls.Add(this.m_buttonSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "CommunicationSettingsModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Communication Settings";
            this.Load += new System.EventHandler(this.CommunicationSettingsModal_Load);
            this.tabControl1.ResumeLayout(false);
            this.m_tabPageOBATPToWSATC.ResumeLayout(false);
            this.m_tabPageOBATPToWSATC.PerformLayout();
            this.m_tabPageOBATPToATS.ResumeLayout(false);
            this.m_tabPageOBATPToATS.PerformLayout();
            this.m_tabPageATSToOBATP.ResumeLayout(false);
            this.m_tabPageATSToOBATP.PerformLayout();
            this.m_tabPageWSATCToOBATP.ResumeLayout(false);
            this.m_tabPageWSATCToOBATP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox m_textBoxATSToOBATPPort;
        private System.Windows.Forms.Label m_labelATSToOBATPPort;
        private System.Windows.Forms.Label m_labelATSToOBATPIPAddress;
        private IPAddressControlLib.IPAddressControl m_ıpAddressControlATSToOBATP;
        private System.Windows.Forms.Button m_buttonApply;
        private System.Windows.Forms.Button m_buttonSave;
        private System.Windows.Forms.TextBox m_textBoxOBATPToATSPort;
        private System.Windows.Forms.Label m_labelOBATPToATSPort;
        private System.Windows.Forms.Label m_labelOBATPToATSIPAddress;
        private IPAddressControlLib.IPAddressControl m_ıpAddressControlOBATPToATS;
        private System.Windows.Forms.TextBox m_textBoxOBATPToWSATCPort;
        private IPAddressControlLib.IPAddressControl m_ıpAddressControlOBATPToWSATC;
        private System.Windows.Forms.Label m_labelOBATPToWSATCIPAddress;
        private System.Windows.Forms.Label m_labelOBATPToWSATCPort;
        private System.Windows.Forms.RadioButton m_radioButtonOBATPToWSATCClient;
        private System.Windows.Forms.RadioButton m_radioButtonOBATPToWSATCServer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage m_tabPageOBATPToWSATC;
        private System.Windows.Forms.TabPage m_tabPageOBATPToATS;
        private System.Windows.Forms.TabPage m_tabPageATSToOBATP;
        private System.Windows.Forms.TabPage m_tabPageWSATCToOBATP;
        private System.Windows.Forms.TextBox m_textBoxWSATCToOBATPPort;
        private IPAddressControlLib.IPAddressControl m_ıpAddressControlWSATCToOBATP;
        private System.Windows.Forms.Label m_labelWSATCToOBATPIPAddress;
        private System.Windows.Forms.Label m_labelWSATCToOBATPPort;
        private System.Windows.Forms.RadioButton m_radioButtonOBATPToATSClient;
        private System.Windows.Forms.RadioButton m_radioButtonOBATPToATSServer;
        private System.Windows.Forms.RadioButton m_radioButtonATSToOBATPClient;
        private System.Windows.Forms.RadioButton m_radioButtonATSToOBATPServer;
        private System.Windows.Forms.RadioButton m_radioButtonWSATCToOBATPClient;
        private System.Windows.Forms.RadioButton m_radioButtonWSATCToOBATPServer;
    }
}