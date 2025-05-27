namespace NumMeth_Lab2_var1_st3_MVR
{
    partial class Form20
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
            this.pictureBoxU = new System.Windows.Forms.PictureBox();
            this.pictureBoxV = new System.Windows.Forms.PictureBox();
            this.labelU = new System.Windows.Forms.Label();
            this.labelV = new System.Windows.Forms.Label();
            this.legendV = new NumMeth_Lab2_var1_st3_MVR.LegendControl();
            this.legendU = new NumMeth_Lab2_var1_st3_MVR.LegendControl();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxV)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxU
            // 
            this.pictureBoxU.Location = new System.Drawing.Point(12, 40);
            this.pictureBoxU.Name = "pictureBoxU";
            this.pictureBoxU.Size = new System.Drawing.Size(525, 532);
            this.pictureBoxU.TabIndex = 0;
            this.pictureBoxU.TabStop = false;
            // 
            // pictureBoxV
            // 
            this.pictureBoxV.Location = new System.Drawing.Point(593, 40);
            this.pictureBoxV.Name = "pictureBoxV";
            this.pictureBoxV.Size = new System.Drawing.Size(525, 532);
            this.pictureBoxV.TabIndex = 1;
            this.pictureBoxV.TabStop = false;
            // 
            // labelU
            // 
            this.labelU.AutoSize = true;
            this.labelU.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelU.Location = new System.Drawing.Point(12, 5);
            this.labelU.Name = "labelU";
            this.labelU.Size = new System.Drawing.Size(44, 32);
            this.labelU.TabIndex = 2;
            this.labelU.Text = "U:";
            // 
            // labelV
            // 
            this.labelV.AutoSize = true;
            this.labelV.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelV.Location = new System.Drawing.Point(598, 5);
            this.labelV.Name = "labelV";
            this.labelV.Size = new System.Drawing.Size(43, 32);
            this.labelV.TabIndex = 3;
            this.labelV.Text = "V:";
            // 
            // legendV
            // 
            this.legendV.Location = new System.Drawing.Point(593, 578);
            this.legendV.Name = "legendV";
            this.legendV.Size = new System.Drawing.Size(525, 78);
            this.legendV.TabIndex = 5;
            this.legendV.Text = "legendControl2";
            // 
            // legendU
            // 
            this.legendU.Location = new System.Drawing.Point(12, 578);
            this.legendU.Name = "legendU";
            this.legendU.Size = new System.Drawing.Size(525, 78);
            this.legendU.TabIndex = 4;
            this.legendU.Text = "legendControl1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(456, 708);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(217, 45);
            this.button1.TabIndex = 6;
            this.button1.Text = "Вывести разницу";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form20
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 765);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.legendV);
            this.Controls.Add(this.legendU);
            this.Controls.Add(this.labelV);
            this.Controls.Add(this.labelU);
            this.Controls.Add(this.pictureBoxV);
            this.Controls.Add(this.pictureBoxU);
            this.Name = "Form20";
            this.Text = "2D визуализация U и V";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxU;
        private System.Windows.Forms.PictureBox pictureBoxV;
        private System.Windows.Forms.Label labelU;
        private System.Windows.Forms.Label labelV;
        private LegendControl legendU;
        private LegendControl legendV;
        private System.Windows.Forms.Button button1;
    }
}