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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 st3_khamkov = new Form1();
            st3_khamkov.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form7 st1_Maksimovich = new Form7();
            st1_Maksimovich.Show();
        }
    }
}
