using DataViewer.Common;

namespace DataViewer.PortParameters
{
    partial class PortParametersGraphicsControl
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortParametersGraphicsControl));
            this.radioButtonMaDb = new System.Windows.Forms.RadioButton();
            this.radioButtonAng = new System.Windows.Forms.RadioButton();
            this.radioButtOnMa = new System.Windows.Forms.RadioButton();
            this.groupControlSettings = new System.Windows.Forms.Panel();
            this.radioButtonIm = new System.Windows.Forms.RadioButton();
            this.radioButtonRe = new System.Windows.Forms.RadioButton();
            this.ViewTypeLabel = new System.Windows.Forms.Label();
            this.ParamTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GraphSetControl = new DataViewer.Common.GraphSetControl();
            this.ViewTipeComboBox = new System.Windows.Forms.ComboBox();
            this.groupControlSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonMaDb
            // 
            this.radioButtonMaDb.AutoSize = true;
            this.radioButtonMaDb.Checked = true;
            this.radioButtonMaDb.Cursor = System.Windows.Forms.Cursors.Default;
            this.radioButtonMaDb.Location = new System.Drawing.Point(191, 4);
            this.radioButtonMaDb.Name = "radioButtonMaDb";
            this.radioButtonMaDb.Size = new System.Drawing.Size(65, 17);
            this.radioButtonMaDb.TabIndex = 8;
            this.radioButtonMaDb.TabStop = true;
            this.radioButtonMaDb.Text = "Mag. dB";
            this.radioButtonMaDb.UseVisualStyleBackColor = true;
            // 
            // radioButtonAng
            // 
            this.radioButtonAng.AutoSize = true;
            this.radioButtonAng.Location = new System.Drawing.Point(257, 4);
            this.radioButtonAng.Name = "radioButtonAng";
            this.radioButtonAng.Size = new System.Drawing.Size(52, 17);
            this.radioButtonAng.TabIndex = 1;
            this.radioButtonAng.Text = "Angle";
            this.radioButtonAng.UseVisualStyleBackColor = true;
            this.radioButtonAng.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // radioButtOnMa
            // 
            this.radioButtOnMa.AutoSize = true;
            this.radioButtOnMa.Location = new System.Drawing.Point(140, 4);
            this.radioButtOnMa.Name = "radioButtOnMa";
            this.radioButtOnMa.Size = new System.Drawing.Size(49, 17);
            this.radioButtOnMa.TabIndex = 0;
            this.radioButtOnMa.Text = "Mag.";
            this.radioButtOnMa.UseVisualStyleBackColor = true;
            this.radioButtOnMa.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // groupControlSettings
            // 
            this.groupControlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.groupControlSettings.Controls.Add(this.ViewTipeComboBox);
            this.groupControlSettings.Controls.Add(this.radioButtonIm);
            this.groupControlSettings.Controls.Add(this.radioButtonRe);
            this.groupControlSettings.Controls.Add(this.ViewTypeLabel);
            this.groupControlSettings.Controls.Add(this.ParamTypeComboBox);
            this.groupControlSettings.Controls.Add(this.label1);
            this.groupControlSettings.Controls.Add(this.radioButtonMaDb);
            this.groupControlSettings.Controls.Add(this.radioButtOnMa);
            this.groupControlSettings.Controls.Add(this.radioButtonAng);
            this.groupControlSettings.Location = new System.Drawing.Point(0, 376);
            this.groupControlSettings.Name = "groupControlSettings";
            this.groupControlSettings.Size = new System.Drawing.Size(672, 25);
            this.groupControlSettings.TabIndex = 5;
            // 
            // radioButtonIm
            // 
            this.radioButtonIm.AutoSize = true;
            this.radioButtonIm.Location = new System.Drawing.Point(351, 4);
            this.radioButtonIm.Name = "radioButtonIm";
            this.radioButtonIm.Size = new System.Drawing.Size(36, 17);
            this.radioButtonIm.TabIndex = 16;
            this.radioButtonIm.Text = "Im";
            this.radioButtonIm.UseVisualStyleBackColor = true;
            // 
            // radioButtonRe
            // 
            this.radioButtonRe.AutoSize = true;
            this.radioButtonRe.Location = new System.Drawing.Point(310, 4);
            this.radioButtonRe.Name = "radioButtonRe";
            this.radioButtonRe.Size = new System.Drawing.Size(39, 17);
            this.radioButtonRe.TabIndex = 15;
            this.radioButtonRe.Text = "Re";
            this.radioButtonRe.UseVisualStyleBackColor = true;
            // 
            // ViewTypeLabel
            // 
            this.ViewTypeLabel.AutoSize = true;
            this.ViewTypeLabel.Location = new System.Drawing.Point(436, 6);
            this.ViewTypeLabel.Name = "ViewTypeLabel";
            this.ViewTypeLabel.Size = new System.Drawing.Size(56, 13);
            this.ViewTypeLabel.TabIndex = 11;
            this.ViewTypeLabel.Text = "View type:";
            // 
            // ParamTypeComboBox
            // 
            this.ParamTypeComboBox.FormattingEnabled = true;
            this.ParamTypeComboBox.Items.AddRange(new object[] {
            "S",
            "Y",
            "Z"});
            this.ParamTypeComboBox.Location = new System.Drawing.Point(74, 2);
            this.ParamTypeComboBox.Name = "ParamTypeComboBox";
            this.ParamTypeComboBox.Size = new System.Drawing.Size(49, 21);
            this.ParamTypeComboBox.TabIndex = 10;
            this.ParamTypeComboBox.Text = "S";
            this.ParamTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ParamTypeComboBoxSelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Parameters:";
            // 
            // GraphSetControl
            // 
            this.GraphSetControl.BackColor = System.Drawing.Color.Transparent;
            this.GraphSetControl.Graphs = ((Indesys.SDK.Collection.EventDrivenList<Indesys.SDK.Graph.Graph>)(resources.GetObject("GraphSetControl.Graphs")));
            this.GraphSetControl.Location = new System.Drawing.Point(0, 0);
            this.GraphSetControl.Name = "GraphSetControl";
            this.GraphSetControl.Size = new System.Drawing.Size(350, 325);
            this.GraphSetControl.TabIndex = 3;
            // 
            // ViewTipeComboBox
            // 
            this.ViewTipeComboBox.FormattingEnabled = true;
            this.ViewTipeComboBox.Items.AddRange(new object[] {
            "Smith\\Rectangular",
            "Rectangular",
            "Smith"});
            this.ViewTipeComboBox.Location = new System.Drawing.Point(498, 2);
            this.ViewTipeComboBox.Name = "ViewTipeComboBox";
            this.ViewTipeComboBox.Size = new System.Drawing.Size(113, 21);
            this.ViewTipeComboBox.TabIndex = 17;
            this.ViewTipeComboBox.Text = "Smith\\Rectangular";
            this.ViewTipeComboBox.SelectedIndexChanged += new System.EventHandler(this.ViewTipeComboBoxSelectedIndexChanged);
            // 
            // PortParametersGraphicsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupControlSettings);
            this.Controls.Add(this.GraphSetControl);
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "PortParametersGraphicsControl";
            this.Size = new System.Drawing.Size(672, 401);
            this.SizeChanged += new System.EventHandler(this.PortParametrGraphicsControlSizeChanged);
            this.groupControlSettings.ResumeLayout(false);
            this.groupControlSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GraphSetControl GraphSetControl;
        private System.Windows.Forms.RadioButton radioButtonAng;
        private System.Windows.Forms.RadioButton radioButtOnMa;
        private System.Windows.Forms.RadioButton radioButtonMaDb;
        public System.Windows.Forms.Panel groupControlSettings;
        private System.Windows.Forms.ComboBox ParamTypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ViewTypeLabel;
        private System.Windows.Forms.RadioButton radioButtonIm;
        private System.Windows.Forms.RadioButton radioButtonRe;
        private System.Windows.Forms.ComboBox ViewTipeComboBox;
    }
}
