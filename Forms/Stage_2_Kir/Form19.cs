using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using delRendererFunction = Plot3D.Graph3D.delRendererFunction;
using cPoint3D = Plot3D.Graph3D.cPoint3D;
using eRaster = Plot3D.Graph3D.eRaster;
using cScatter = Plot3D.Graph3D.cScatter;
using eNormalize = Plot3D.Graph3D.eNormalize;
using eSchema = Plot3D.ColorSchema.eSchema;

namespace NumMeth_Lab2_var1_st3_MVR
{
    public partial class Form19 : Form
    {
        public Form19(double[,] ARR, int n, int m)
        {
            InitializeComponent();


            cPoint3D[,] i_Points3D = new cPoint3D[ARR.GetLength(0), ARR.GetLength(1)];
            for (int X = 0; X < ARR.GetLength(0); X++)
            {
                for (int Y = 0; Y < ARR.GetLength(1); Y++)
                {
                    i_Points3D[X, Y] = new cPoint3D(X * 1.0 / (double)n , Y * 1.0 / (double)m , ARR[X, Y]);
                }
            }


            graph3D1.SetSurfacePoints(i_Points3D, eNormalize.Separate);

            graph3D1.Raster = (eRaster)3;
            Color[] c_Colors = Plot3D.ColorSchema.GetSchema((eSchema)12);
            graph3D1.SetColorScheme(c_Colors, 4);

        }
    }
}
