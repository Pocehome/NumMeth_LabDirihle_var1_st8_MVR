namespace NumMeth_Lab2_var1_st3_MVR
{
    partial class Form21
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
            this.pictureBoxDiff = new System.Windows.Forms.PictureBox();
            this.legendDiff = new NumMeth_Lab2_var1_st3_MVR.LegendControl();
            this.labelDiff = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDiff)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxDiff
            // 
            this.pictureBoxDiff.Location = new System.Drawing.Point(12, 58);
            this.pictureBoxDiff.Name = "pictureBoxDiff";
            this.pictureBoxDiff.Size = new System.Drawing.Size(525, 532);
            this.pictureBoxDiff.TabIndex = 1;
            this.pictureBoxDiff.TabStop = false;
            // 
            // legendDiff
            // 
            this.legendDiff.Location = new System.Drawing.Point(12, 596);
            this.legendDiff.Name = "legendDiff";
            this.legendDiff.Size = new System.Drawing.Size(525, 78);
            this.legendDiff.TabIndex = 5;
            this.legendDiff.Text = "legendControl1";
            // 
            // labelDiff
            // 
            this.labelDiff.AutoSize = true;
            this.labelDiff.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDiff.Location = new System.Drawing.Point(12, 9);
            this.labelDiff.Name = "labelDiff";
            this.labelDiff.Size = new System.Drawing.Size(90, 32);
            this.labelDiff.TabIndex = 6;
            this.labelDiff.Text = "|U-V|:";
            // 
            // Form21
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 691);
            this.Controls.Add(this.labelDiff);
            this.Controls.Add(this.legendDiff);
            this.Controls.Add(this.pictureBoxDiff);
            this.Name = "Form21";
            this.Text = "2D визуализация |U-V|";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDiff)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxDiff;
        private LegendControl legendDiff;
        private System.Windows.Forms.Label labelDiff;
    }
}