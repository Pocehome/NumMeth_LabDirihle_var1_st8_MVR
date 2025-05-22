
namespace NumMeth_Lab2_var1_st3_MVR
{
    partial class Form4
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.graph3D2 = new Plot3D.Graph3D();
            this.graph3D1 = new Plot3D.Graph3D();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(737, 655);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "Расчёт";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(607, 661);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Вывести разницу";
            // 
            // graph3D2
            // 
            this.graph3D2.AxisX_Color = System.Drawing.Color.DarkBlue;
            this.graph3D2.AxisX_Legend = null;
            this.graph3D2.AxisY_Color = System.Drawing.Color.DarkGreen;
            this.graph3D2.AxisY_Legend = null;
            this.graph3D2.AxisZ_Color = System.Drawing.Color.DarkRed;
            this.graph3D2.AxisZ_Legend = null;
            this.graph3D2.BackColor = System.Drawing.Color.White;
            this.graph3D2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.graph3D2.Location = new System.Drawing.Point(752, 13);
            this.graph3D2.Margin = new System.Windows.Forms.Padding(4);
            this.graph3D2.Name = "graph3D2";
            this.graph3D2.PolygonLineColor = System.Drawing.Color.Black;
            this.graph3D2.Raster = Plot3D.Graph3D.eRaster.Off;
            this.graph3D2.Size = new System.Drawing.Size(692, 628);
            this.graph3D2.TabIndex = 1;
            this.graph3D2.TopLegendColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(150)))));
            // 
            // graph3D1
            // 
            this.graph3D1.AxisX_Color = System.Drawing.Color.DarkBlue;
            this.graph3D1.AxisX_Legend = null;
            this.graph3D1.AxisY_Color = System.Drawing.Color.DarkGreen;
            this.graph3D1.AxisY_Legend = null;
            this.graph3D1.AxisZ_Color = System.Drawing.Color.DarkRed;
            this.graph3D1.AxisZ_Legend = null;
            this.graph3D1.BackColor = System.Drawing.Color.White;
            this.graph3D1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.graph3D1.Location = new System.Drawing.Point(16, 15);
            this.graph3D1.Margin = new System.Windows.Forms.Padding(4);
            this.graph3D1.Name = "graph3D1";
            this.graph3D1.PolygonLineColor = System.Drawing.Color.Black;
            this.graph3D1.Raster = Plot3D.Graph3D.eRaster.Off;
            this.graph3D1.Size = new System.Drawing.Size(685, 626);
            this.graph3D1.TabIndex = 0;
            this.graph3D1.TopLegendColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(150)))));
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1457, 695);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.graph3D2);
            this.Controls.Add(this.graph3D1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form4";
            this.Text = "Два графика по таблицам";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Plot3D.Graph3D graph3D1;
        private Plot3D.Graph3D graph3D2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}