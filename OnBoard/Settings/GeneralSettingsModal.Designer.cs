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
            this.m_buttonApply = new System.Windows.Forms.Button();
            this.m_buttonSave = new System.Windows.Forms.Button();
            this.m_checkedListBoxTrains = new System.Windows.Forms.CheckedListBox();
            this.m_labelTrainFrequency = new System.Windows.Forms.Label();
            this.m_numericUpDownTrainFrequency = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.m_numericUpDownTrainFrequency)).BeginInit();
            this.SuspendLayout();
            // 
            // m_buttonApply
            // 
            this.m_buttonApply.Image = global::OnBoard.Properties.Resources.apply;
            this.m_buttonApply.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_buttonApply.Location = new System.Drawing.Point(427, 236);
            this.m_buttonApply.Name = "m_buttonApply";
            this.m_buttonApply.Size = new System.Drawing.Size(111, 59);
            this.m_buttonApply.TabIndex = 10;
            this.m_buttonApply.Text = "Apply";
            this.m_buttonApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_buttonApply.UseVisualStyleBackColor = true;
            this.m_buttonApply.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // m_buttonSave
            // 
            this.m_buttonSave.Image = global::OnBoard.Properties.Resources.save;
            this.m_buttonSave.Location = new System.Drawing.Point(310, 236);
            this.m_buttonSave.Name = "m_buttonSave";
            this.m_buttonSave.Size = new System.Drawing.Size(111, 59);
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
            this.m_checkedListBoxTrains.Location = new System.Drawing.Point(13, 35);
            this.m_checkedListBoxTrains.Margin = new System.Windows.Forms.Padding(4);
            this.m_checkedListBoxTrains.Name = "m_checkedListBoxTrains";
            this.m_checkedListBoxTrains.Size = new System.Drawing.Size(225, 174);
            this.m_checkedListBoxTrains.TabIndex = 11;
            // 
            // m_labelTrainFrequency
            // 
            this.m_labelTrainFrequency.AutoSize = true;
            this.m_labelTrainFrequency.Location = new System.Drawing.Point(282, 35);
            this.m_labelTrainFrequency.Name = "m_labelTrainFrequency";
            this.m_labelTrainFrequency.Size = new System.Drawing.Size(156, 17);
            this.m_labelTrainFrequency.TabIndex = 12;
            this.m_labelTrainFrequency.Text = "Train Frequency(min) : ";
            // 
            // m_numericUpDownTrainFrequency
            // 
            this.m_numericUpDownTrainFrequency.Location = new System.Drawing.Point(482, 33);
            this.m_numericUpDownTrainFrequency.Name = "m_numericUpDownTrainFrequency";
            this.m_numericUpDownTrainFrequency.Size = new System.Drawing.Size(120, 22);
            this.m_numericUpDownTrainFrequency.TabIndex = 13;
            // 
            // GeneralSettingsModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 339);
            this.Controls.Add(this.m_numericUpDownTrainFrequency);
            this.Controls.Add(this.m_labelTrainFrequency);
            this.Controls.Add(this.m_checkedListBoxTrains);
            this.Controls.Add(this.m_buttonApply);
            this.Controls.Add(this.m_buttonSave);
            this.MaximizeBox = false;
            this.Name = "GeneralSettingsModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "General Settings";
            this.Load += new System.EventHandler(this.GeneralSettingsModal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_numericUpDownTrainFrequency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_buttonApply;
        private System.Windows.Forms.Button m_buttonSave;
        private System.Windows.Forms.CheckedListBox m_checkedListBoxTrains;
        private System.Windows.Forms.Label m_labelTrainFrequency;
        private System.Windows.Forms.NumericUpDown m_numericUpDownTrainFrequency;
    }
}