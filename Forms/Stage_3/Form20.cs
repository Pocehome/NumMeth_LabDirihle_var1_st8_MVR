using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NumMeth_Lab2_var1_st3_MVR
{
    public partial class Form20 : Form
    {
        double[][] u, v;
        int n, m;

        public Form20(double[][] _u, double[][] _v, int _n, int _m)
        {
            u = _u;
            v = _v;
            n = _n;
            m = _m;

            InitializeComponent();
            InitializeCustomComponents();
            PlotArrays(u, v, n + 1, m + 1); // Учитываем размер n+1 на m+1
        }

        private void InitializeCustomComponents() { }

        private void PlotArrays(double[][] u, double[][] v, int n, int m)
        {
            // Создание битмапов для отображения
            Bitmap bmpU = new Bitmap(pictureBoxU.Width, pictureBoxU.Height);
            Bitmap bmpV = new Bitmap(pictureBoxV.Width, pictureBoxV.Height);

            // Заливаем фон белым цветом
            using (Graphics gU = Graphics.FromImage(bmpU))
            using (Graphics gV = Graphics.FromImage(bmpV))
            {
                gU.Clear(Color.White);
                gV.Clear(Color.White);
            }

            // Находим минимальные и максимальные значения для нормализации (исключая нули)
            double minU = FindMinNonZero(u, n, m);
            double maxU = FindMax(u, n, m);
            double minV = FindMinNonZero(v, n, m);
            double maxV = FindMax(v, n, m);

            // Устанавливаем диапазоны для легенд
            legendU.SetRange(minU, maxU);
            legendV.SetRange(minV, maxV);

            // Рассчитываем размер точки в зависимости от количества точек
            int pointSizeU = CalculatePointSize(n, m, pictureBoxU.Width, pictureBoxU.Height);
            int pointSizeV = CalculatePointSize(n, m, pictureBoxV.Width, pictureBoxV.Height);

            // Отрисовка осей и сетки
            DrawAxesAndGrid(bmpU, n, m);
            DrawAxesAndGrid(bmpV, n, m);

            // Отрисовка массива U (точка [0][0] в левом нижнем углу)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (Math.Abs(u[i][j]) < double.Epsilon) continue;

                    double normalizedValue = (u[i][j] - minU) / (maxU - minU);
                    Color color = GetColorFromValue(normalizedValue);

                    // Перевернутый индекс по Y для размещения [0][0] внизу слева
                    int x = (int)((double)j / (m - 1) * (bmpU.Width - 1));
                    int y = bmpU.Height - 1 - (int)((double)i / (n - 1) * (bmpU.Height - 1));

                    DrawPoint(bmpU, x, y, pointSizeU, color);
                }
            }

            // Отрисовка массива V (точка [0][0] в левом нижнем углу)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (Math.Abs(v[i][j]) < double.Epsilon) continue;

                    double normalizedValue = (v[i][j] - minV) / (maxV - minV);
                    Color color = GetColorFromValue(normalizedValue);

                    // Перевернутый индекс по Y для размещения [0][0] внизу слева
                    int x = (int)((double)j / (m - 1) * (bmpV.Width - 1));
                    int y = bmpV.Height - 1 - (int)((double)i / (n - 1) * (bmpV.Height - 1));

                    DrawPoint(bmpV, x, y, pointSizeV, color);
                }
            }

            // Отображение битмапов в PictureBox
            pictureBoxU.Image = bmpU;
            pictureBoxV.Image = bmpV;
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
            // Базовый размер точки
            //int baseSize = 1;

            // Рассчитываем примерное количество точек по горизонтали и вертикали
            int pointsX = m;
            int pointsY = n;

            // Рассчитываем среднее расстояние между точками
            double avgDistanceX = (double)width / pointsX;
            double avgDistanceY = (double)height / pointsY;
            double avgDistance = Math.Min(avgDistanceX, avgDistanceY);

            // Размер точки зависит от расстояния между точками
            int size = (int)Math.Max(1, Math.Min(avgDistance, 10));

            return size;
        }

        private void DrawPoint(Bitmap bmp, int x, int y, int size, Color color)
        {
            // Рисуем квадратную точку заданного размера
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(new SolidBrush(color),
                                x - size / 2, y - size / 2,
                                size, size);
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
            return min == double.MaxValue ? 0 : min; // Если все нули, вернем 0
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
            // Ограничение значения в диапазоне [0, 1]
            value = Math.Max(0, Math.Min(1, value));

            // Создание цветовой карты от синего (минимум) к красному (максимум)
            byte r = (byte)(255 * value);
            byte g = 0;
            byte b = (byte)(255 * (1 - value));

            return Color.FromArgb(r, g, b);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 2D графики
            Form21 form21 = new Form21(u, v, n, m);
            form21.Show();
        }
    }

    public class LegendControl : Control
    {
        private double minValue;
        private double maxValue;
        private string numberFormat = "0.######"; // Формат по умолчанию

        public LegendControl()
        {
            this.Paint += new PaintEventHandler(LegendControl_Paint);
            minValue = 0;
            maxValue = 1;
        }

        public void SetRange(double min, double max, string format = null)
        {
            minValue = min;
            maxValue = max;
            if (!string.IsNullOrEmpty(format))
                numberFormat = format;
            this.Invalidate();
        }

        private void LegendControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = this.ClientRectangle;

            if (rect.Width < 10 || rect.Height < 30) return;

            try
            {
                using (Font font = new Font("Arial", 8))
                using (Brush brush = new SolidBrush(Color.Black))
                {
                    // Рисуем цветовую шкалу
                    int gradientHeight = Math.Max(10, rect.Height - 25);
                    for (int x = 0; x < rect.Width; x++)
                    {
                        double value = x / (double)rect.Width;
                        using (Pen pen = new Pen(GetColorFromValue(value)))
                        {
                            g.DrawLine(pen, x, 5, x, 5 + gradientHeight);
                        }
                    }

                    // Форматируем числа с учетом выбранного формата
                    string minText = FormatNumber(minValue);
                    string maxText = FormatNumber(maxValue);
                    string title = "Значения";

                    SizeF minSize = g.MeasureString(minText, font);
                    SizeF maxSize = g.MeasureString(maxText, font);
                    SizeF titleSize = g.MeasureString(title, font);

                    float textY = rect.Height - minSize.Height - 2;
                    if (textY < 0) textY = 0;

                    if (minSize.Width < rect.Width / 2)
                    {
                        g.DrawString(minText, font, brush, 2, textY);
                    }

                    if (maxSize.Width < rect.Width / 2)
                    {
                        float maxX = rect.Width - maxSize.Width - 2;
                        g.DrawString(maxText, font, brush, maxX, textY);
                    }

                    if (titleSize.Width < rect.Width * 0.8)
                    {
                        float titleX = (rect.Width - titleSize.Width) / 2;
                        g.DrawString(title, font, brush, titleX, textY);
                    }
                }
            }
            catch
            {
                // В случае ошибки просто не рисуем легенду
            }
        }

        private string FormatNumber(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                return "N/A";

            // Автоматический выбор формата:
            // Для очень больших/маленьких чисел - экспоненциальная запись
            // Для обычных чисел - указанный формат
            if (Math.Abs(value) > 1e5 || (Math.Abs(value) < 1e-3 && Math.Abs(value) > 1e-20))
            {
                return value.ToString("0.###e+0");
            }
            return value.ToString(numberFormat);
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