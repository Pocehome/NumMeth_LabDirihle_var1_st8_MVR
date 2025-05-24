using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumMeth_Lab2_var1_st3_MVR
{
    public partial class FormProgress1 : Form
    {
        public ProgressBar ProgressBar { get; }
        public Label LabelStatus { get; }

        public FormProgress1() 
        {
            InitializeComponent();
            this.Text = "Выполнение...";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;

            ProgressBar = new ProgressBar
            {
                Dock = DockStyle.Top,
                Minimum = 0,
                Maximum = 100,
                Value = 0
            };

            LabelStatus = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Инициализация..."
            };

            this.Controls.Add(LabelStatus);
            this.Controls.Add(ProgressBar);
            this.Height = 100;
        }

        public void UpdateProgress(int percent, string status)
        {
            ProgressBar.Value = percent;
            LabelStatus.Text = status;
            Application.DoEvents(); // Обновляем UI
        }
    }
}
