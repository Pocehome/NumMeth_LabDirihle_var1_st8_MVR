using System;
using System.Drawing;
using System.Windows.Forms;

namespace NumMeth_Lab2_var1_st3_MVR
{
    public partial class Form21 : Form
    {
        public Form21(double[][] u, double[][] v, int n, int m)
        {
            InitializeComponent();
            InitializeCustomComponents();
            PlotDifference(u, v, n + 1, m + 1); // Учитываем размер n+1 на m+1
        }

        private void InitializeCustomComponents() { }

        private void PlotDifference(double[][] u, double[][] v, int n, int m)
        {
            // Создание битмапа для отображения
            Bitmap bmpDiff = new Bitmap(pictureBoxDiff.Width, pictureBoxDiff.Height);

            // Заливаем фон белым цветом
            using (Graphics g = Graphics.FromImage(bmpDiff))
            {
                g.Clear(Color.White);
            }

            // Вычисляем модуль разности
            double[][] diff = new double[n][];
            for (int i = 0; i < n; i++)
            {
                diff[i] = new double[m];
                for (int j = 0; j < m; j++)
                {
                    diff[i][j] = Math.Abs(u[i][j] - v[i][j]);
                }
            }

            // Находим минимальное и максимальное значения разности
            double minDiff = FindMinNonZero(diff, n, m);
            double maxDiff = FindMax(diff, n, m);

            // Устанавливаем диапазон для легенды
            legendDiff.SetRange(minDiff, maxDiff, "0.000000");

            // Рассчитываем размер точки
            int pointSize = CalculatePointSize(n, m, pictureBoxDiff.Width, pictureBoxDiff.Height);

            // Отрисовка осей и сетки
            DrawAxesAndGrid(bmpDiff, n, m);

            // Отрисовка разности
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (Math.Abs(diff[i][j]) < double.Epsilon) continue;

                    // Нормализация значения к диапазону 0-1
                    double normalizedValue = (diff[i][j] - minDiff) / (maxDiff - minDiff);
                    Color color = GetColorFromValue(normalizedValue);

                    // Координаты с [0][0] в левом нижнем углу
                    int x = (int)((double)j / (m - 1) * (bmpDiff.Width - 1));
                    int y = bmpDiff.Height - 1 - (int)((double)i / (n - 1) * (bmpDiff.Height - 1));

                    // Рисуем точку
                    DrawPoint(bmpDiff, x, y, pointSize, color);
                }
            }

            // Отображение битмапа
            pictureBoxDiff.Image = bmpDiff;
        }

        private void DrawAxesAndGrid(Bitmap bmp, int n, int m)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Настройки рисования
                Pen axisPen = new Pen(Color.Black, 2);
                Pen gridPen = new Pen(Color.LightGray, 1);
                Font labelFont = new Font("Arial", 8, FontStyle.Bold); // Жирный шрифт
                Brush labelBrush = Brushes.Black;

                // Рисуем оси
                g.DrawLine(axisPen, 0, bmp.Height - 1, bmp.Width - 1, bmp.Height - 1); // Ось X
                g.DrawLine(axisPen, 0, bmp.Height - 1, 0, 0); // Ось Y

                // Подписи осей (X и Y)
                g.DrawString("X", labelFont, labelBrush, bmp.Width - 15, bmp.Height - 30);
                g.DrawString("Y", labelFont, labelBrush, 20, 10);

                // Рисуем сетку и подписи
                int xStep = bmp.Width / 10;
                int yStep = bmp.Height / 10;

                // Подписи для оси X
                for (int i = 0; i <= 10; i++)
                {
                    int x = i * xStep;
                    g.DrawLine(gridPen, x, 0, x, bmp.Height - 1);

                    double value = i / 10.0;
                    string label = value.ToString("0.0");
                    g.DrawString(label, labelFont, labelBrush, x - 10, bmp.Height - 20);
                }

                // Подписи для оси Y
                for (int j = 1; j <= 10; j++)
                {
                    int y = bmp.Height - 1 - j * yStep;
                    g.DrawLine(gridPen, 0, y, bmp.Width - 1, y - 10);

                    double value = j / 10.0;
                    string label = value.ToString("0.0");
                    g.DrawString(label, labelFont, labelBrush, 2, y + 3);
                }
            }
        }

        private int CalculatePointSize(int n, int m, int width, int height)
        {
            int pointsX = m;
            int pointsY = n;
            double avgDistanceX = (double)width / pointsX;
            double avgDistanceY = (double)height / pointsY;
            double avgDistance = Math.Min(avgDistanceX, avgDistanceY);
            return (int)Math.Max(1, Math.Min(avgDistance, 10));
        }

        private void DrawPoint(Bitmap bmp, int x, int y, int size, Color color)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(new SolidBrush(color), x - size / 2, y - size / 2, size, size);
            }
        }

        private double FindMinNonZero(double[][] array, int n, int m)
        {
            double min = double.MaxValue;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (Math.Abs(array[i][j]) > double.Epsilon && array[i][j] < min)
                        min = array[i][j];
                }
            }
            return min == double.MaxValue ? 0 : min;
        }

        private double FindMax(double[][] array, int n, int m)
        {
            double max = array[0][0];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (array[i][j] > max) max = array[i][j];
                }
            }
            return max;
        }

        private Color GetColorFromValue(double value)
        {
            value = Math.Max(0, Math.Min(1, value));
            byte r = (byte)(255 * value);
            byte g = 0;
            byte b = (byte)(255 * (1 - value));
            return Color.FromArgb(r, g, b);
        }
    }
}