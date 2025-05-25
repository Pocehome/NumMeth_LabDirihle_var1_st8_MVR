
namespace NumMeth_Lab2_var1_st3_MVR
{
    partial class Form18
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
            this.graph3D2 = new Plot3D.Graph3D();
            this.graph3D1 = new Plot3D.Graph3D();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(681, 707);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(196, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "Вывести разность решений";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // graph3D2
            // 
            this.graph3D2.AxisX_Color = System.Drawing.Color.Black;
            this.graph3D2.AxisX_Legend = null;
            this.graph3D2.AxisY_Color = System.Drawing.Color.Black;
            this.graph3D2.AxisY_Legend = null;
            this.graph3D2.AxisZ_Color = System.Drawing.Color.Black;
            this.graph3D2.AxisZ_Legend = null;
            this.graph3D2.BackColor = System.Drawing.Color.White;
            this.graph3D2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.graph3D2.Location = new System.Drawing.Point(786, 15);
            this.graph3D2.Margin = new System.Windows.Forms.Padding(4);
            this.graph3D2.Name = "graph3D2";
            this.graph3D2.PolygonLineColor = System.Drawing.Color.Black;
            this.graph3D2.Raster = Plot3D.Graph3D.eRaster.Off;
            this.graph3D2.Size = new System.Drawing.Size(691, 648);
            this.graph3D2.TabIndex = 1;
            this.graph3D2.TopLegendColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(150)))));
            // 
            // graph3D1
            // 
            this.graph3D1.AxisX_Color = System.Drawing.Color.Black;
            this.graph3D1.AxisX_Legend = null;
            this.graph3D1.AxisY_Color = System.Drawing.Color.Black;
            this.graph3D1.AxisY_Legend = null;
            this.graph3D1.AxisZ_Color = System.Drawing.Color.Black;
            this.graph3D1.AxisZ_Legend = null;
            this.graph3D1.BackColor = System.Drawing.Color.White;
            this.graph3D1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.graph3D1.Location = new System.Drawing.Point(16, 15);
            this.graph3D1.Margin = new System.Windows.Forms.Padding(4);
            this.graph3D1.Name = "graph3D1";
            this.graph3D1.PolygonLineColor = System.Drawing.Color.Black;
            this.graph3D1.Raster = Plot3D.Graph3D.eRaster.Off;
            this.graph3D1.Size = new System.Drawing.Size(762, 648);
            this.graph3D1.TabIndex = 0;
            this.graph3D1.TopLegendColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(150)))));
            this.graph3D1.Load += new System.EventHandler(this.graph3D1_Load);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1492, 796);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.graph3D2);
            this.Controls.Add(this.graph3D1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form4";
            this.Text = "Точное и численное решение";
            this.ResumeLayout(false);

        }

        #endregion

        private Plot3D.Graph3D graph3D1;
        private Plot3D.Graph3D graph3D2;
        private System.Windows.Forms.Button button1;
    }
}