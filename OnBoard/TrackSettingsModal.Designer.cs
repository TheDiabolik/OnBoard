namespace OnBoard
{
    partial class TrackSettingsModal
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
            this.m_dataGridViewTrackRoute = new System.Windows.Forms.DataGridView();
            this.m_buttonApply = new System.Windows.Forms.Button();
            this.m_buttonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridViewTrackRoute)).BeginInit();
            this.SuspendLayout();
            // 
            // m_dataGridViewTrackRoute
            // 
            this.m_dataGridViewTrackRoute.AllowUserToAddRows = false;
            this.m_dataGridViewTrackRoute.AllowUserToDeleteRows = false;
            this.m_dataGridViewTrackRoute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dataGridViewTrackRoute.Location = new System.Drawing.Point(0, 0);
            this.m_dataGridViewTrackRoute.MultiSelect = false;
            this.m_dataGridViewTrackRoute.Name = "m_dataGridViewTrackRoute";
            this.m_dataGridViewTrackRoute.RowTemplate.Height = 24;
            this.m_dataGridViewTrackRoute.Size = new System.Drawing.Size(1059, 380);
            this.m_dataGridViewTrackRoute.TabIndex = 27;
            this.m_dataGridViewTrackRoute.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.m_dataGridViewTrackRoute_RowsAdded);
            // 
            // m_buttonApply
            // 
            this.m_buttonApply.Image = global::OnBoard.Properties.Resources.apply;
            this.m_buttonApply.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_buttonApply.Location = new System.Drawing.Point(918, 409);
            this.m_buttonApply.Name = "m_buttonApply";
            this.m_buttonApply.Size = new System.Drawing.Size(111, 59);
            this.m_buttonApply.TabIndex = 30;
            this.m_buttonApply.Text = "Apply";
            this.m_buttonApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_buttonApply.UseVisualStyleBackColor = true;
            this.m_buttonApply.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // m_buttonSave
            // 
            this.m_buttonSave.Image = global::OnBoard.Properties.Resources.save;
            this.m_buttonSave.Location = new System.Drawing.Point(801, 409);
            this.m_buttonSave.Name = "m_buttonSave";
            this.m_buttonSave.Size = new System.Drawing.Size(111, 59);
            this.m_buttonSave.TabIndex = 29;
            this.m_buttonSave.Text = "Save";
            this.m_buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_buttonSave.UseVisualStyleBackColor = true;
            this.m_buttonSave.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // TrackSettingsModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 480);
            this.Controls.Add(this.m_buttonApply);
            this.Controls.Add(this.m_buttonSave);
            this.Controls.Add(this.m_dataGridViewTrackRoute);
            this.MaximizeBox = false;
            this.Name = "TrackSettingsModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TrackSettingsModal";
            this.Load += new System.EventHandler(this.TrackSettingsModal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridViewTrackRoute)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView m_dataGridViewTrackRoute;
        private System.Windows.Forms.Button m_buttonApply;
        private System.Windows.Forms.Button m_buttonSave;
    }
}