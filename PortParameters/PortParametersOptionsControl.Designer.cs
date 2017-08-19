namespace DataViewer.PortParameters
{
    partial class PortParametersOptionsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButtonMaDb = new System.Windows.Forms.RadioButton();
            this.radioButtonAng = new System.Windows.Forms.RadioButton();
            this.radioButtOnMa = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EnableViewerCheckBox = new System.Windows.Forms.CheckBox();
            this.ShowFilepathsCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ParamTypeComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonMaDb
            // 
            this.radioButtonMaDb.AutoSize = true;
            this.radioButtonMaDb.Cursor = System.Windows.Forms.Cursors.Default;
            this.radioButtonMaDb.Location = new System.Drawing.Point(60, 92);
            this.radioButtonMaDb.Name = "radioButtonMaDb";
            this.radioButtonMaDb.Size = new System.Drawing.Size(65, 17);
            this.radioButtonMaDb.TabIndex = 15;
            this.radioButtonMaDb.Text = "Mag. dB";
            this.radioButtonMaDb.UseVisualStyleBackColor = true;
            this.radioButtonMaDb.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // radioButtonAng
            // 
            this.radioButtonAng.AutoSize = true;
            this.radioButtonAng.Location = new System.Drawing.Point(127, 92);
            this.radioButtonAng.Name = "radioButtonAng";
            this.radioButtonAng.Size = new System.Drawing.Size(52, 17);
            this.radioButtonAng.TabIndex = 10;
            this.radioButtonAng.Text = "Angle";
            this.radioButtonAng.UseVisualStyleBackColor = true;
            this.radioButtonAng.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // radioButtOnMa
            // 
            this.radioButtOnMa.AutoSize = true;
            this.radioButtOnMa.Checked = true;
            this.radioButtOnMa.Location = new System.Drawing.Point(9, 92);
            this.radioButtOnMa.Name = "radioButtOnMa";
            this.radioButtOnMa.Size = new System.Drawing.Size(49, 17);
            this.radioButtOnMa.TabIndex = 9;
            this.radioButtOnMa.TabStop = true;
            this.radioButtOnMa.Text = "Mag.";
            this.radioButtOnMa.UseVisualStyleBackColor = true;
            this.radioButtOnMa.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ParamTypeComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioButtonMaDb);
            this.groupBox1.Controls.Add(this.ShowFilepathsCheckBox);
            this.groupBox1.Controls.Add(this.EnableViewerCheckBox);
            this.groupBox1.Controls.Add(this.radioButtonAng);
            this.groupBox1.Controls.Add(this.radioButtOnMa);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 300);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PortParametersOptions";
            // 
            // EnableViewerCheckBox
            // 
            this.EnableViewerCheckBox.AutoSize = true;
            this.EnableViewerCheckBox.Location = new System.Drawing.Point(7, 20);
            this.EnableViewerCheckBox.Name = "EnableViewerCheckBox";
            this.EnableViewerCheckBox.Size = new System.Drawing.Size(94, 17);
            this.EnableViewerCheckBox.TabIndex = 0;
            this.EnableViewerCheckBox.Text = "Enable Viewer";
            this.EnableViewerCheckBox.UseVisualStyleBackColor = true;
            this.EnableViewerCheckBox.CheckedChanged += new System.EventHandler(this.enableCheckEdit_CheckedChanged);
            // 
            // ShowFilepathsCheckBox
            // 
            this.ShowFilepathsCheckBox.AutoSize = true;
            this.ShowFilepathsCheckBox.Location = new System.Drawing.Point(7, 44);
            this.ShowFilepathsCheckBox.Name = "ShowFilepathsCheckBox";
            this.ShowFilepathsCheckBox.Size = new System.Drawing.Size(98, 17);
            this.ShowFilepathsCheckBox.TabIndex = 1;
            this.ShowFilepathsCheckBox.Text = "Show Filepaths";
            this.ShowFilepathsCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Parameters:";
            // 
            // ParamTypeComboBox
            // 
            this.ParamTypeComboBox.FormattingEnabled = true;
            this.ParamTypeComboBox.Items.AddRange(new object[] {
            "S",
            "Y",
            "Z"});
            this.ParamTypeComboBox.Location = new System.Drawing.Point(76, 65);
            this.ParamTypeComboBox.Name = "ParamTypeComboBox";
            this.ParamTypeComboBox.Size = new System.Drawing.Size(49, 21);
            this.ParamTypeComboBox.TabIndex = 3;
            this.ParamTypeComboBox.Text = "S";
            this.ParamTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ParamTypeComboBox_SelectedIndexChanged);
            // 
            // PortParametersOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.groupBox1);
            this.Name = "PortParametersOptionsControl";
            this.Size = new System.Drawing.Size(350, 300);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonMaDb;
        private System.Windows.Forms.RadioButton radioButtonAng;
        private System.Windows.Forms.RadioButton radioButtOnMa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox ParamTypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ShowFilepathsCheckBox;
        private System.Windows.Forms.CheckBox EnableViewerCheckBox;
    }
}
