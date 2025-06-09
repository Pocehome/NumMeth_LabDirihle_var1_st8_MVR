using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NumMeth_Lab2_var1_st3_MVR
{
    public partial class Form1 : Form
    {
        // Инициализация массивов данных
        double[] x;
        double[] y;
        double[][] u;
        double[][] v;
        double[][] f;

        // Границы области
        double a = 0, b = 1, c = 0, d = 1;
        int[][] domainMatrix;  // Матрица области (0 - снаружи, 1 - внутри, 2 - граница)

        // Инициализация парамаетров
        int n, m, N_max, iter_count;
        double Eps, h, k, h2, k2, A, w_opt, MaxF, xMax, yMax, MaxDiff_UV, Eps_max, MaxR;
        double evcR1;

        public Form1()
        {
            InitializeComponent();
        }

        // Функции
        double u1(double x, double y) // Решение u*(x,y) для тестовой задачи
        {
            return Math.Exp(Math.Pow(Math.Sin(Math.PI * x * y), 2));
        }

        double f1(double x, double y)
        {
            double piXY = Math.PI * x * y;
            double sinPiXY = Math.Sin(piXY);
            double expTerm = Math.Exp(Math.Pow(sinPiXY, 2));
            return -Math.Pow(Math.PI, 2) * expTerm * (x * x + y * y) *
                   (2 * Math.Cos(2 * piXY) + Math.Pow(Math.Sin(2 * piXY), 2));
        }

        double optimal_W(double n, double m)  // Оптимальное значение параметра
        {
            // Шаги сетки (предполагая область [0,1]x[0,1])
            double h = 1.0 / n;
            double k = 1.0 / m;

            // Спектральный радиус метода Якоби (p)
            double rho = Math.Cos(Math.PI * h) / (h * h) + Math.Cos(Math.PI * k) / (k * k);
            rho /= (1 / (h * h) + 1 / (k * k));

            // Оптимальный параметр релаксации
            double w_opt = 2.0 / (1.0 + Math.Sqrt(1.0 - rho * rho));

            return w_opt;
        }

        bool CheckGridParameters()  // Проверка кратности n и m 4
        {
            if (n % 4 != 0 || m % 4 != 0 || n < 1 || m < 1)
            {
                MessageBox.Show("Ошибка: n и m должны быть больше 0 и кратны 4", "Некорректные параметры сетки");
                return false;
            }
            return true;
        }

        void InitializeDomainMatrix()  // Инициализация матрицы области
        {
            domainMatrix = new int[n + 1][];
            for (int i = 0; i <= n; i++)
            {
                domainMatrix[i] = new int[m + 1];
            }

            // Количество нулевых узлов с каждого края
            int zeroNodesX = n / 4;
            int zeroNodesY = m / 4;

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    // Проверка на исключённые узлы (0)
                    if (i < zeroNodesX && j > m - zeroNodesY
                        || i < zeroNodesX && j < zeroNodesY
                        || i > n - zeroNodesX && j < zeroNodesY)
                    {
                        domainMatrix[i][j] = 0;
                    }

                    // Проверка на граничные узлы (2)
                    else if (i == 0 || i == n || j == 0 || j == m ||
                        (i == zeroNodesX && j <= zeroNodesY) ||
                        (i <= zeroNodesX && j == zeroNodesY) ||
                        (i == zeroNodesX && j >= m - zeroNodesY) ||
                        (i >= n - zeroNodesX && j == zeroNodesY) ||
                        (i == n - zeroNodesX && j <= zeroNodesY) ||
                        (i <= zeroNodesX && j == m - zeroNodesY)
                        )
                    {
                        domainMatrix[i][j] = 2;
                    }

                    // Заполняем 1 все внутренние узлы
                    else
                    {
                        domainMatrix[i][j] = 1;
                    }
                }
            }
        }

        void SolvePoissonSOR()  // Метод верхней релаксации (SOR)
        {
            // Инициализация начального приближения
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    switch (domainMatrix[i][j])
                    {
                        case 0: // Исключённые узлы
                            v[i][j] = 0.0;
                            break;
                        case 2: // Граничные узлы
                            v[i][j] = u1(a + i * h, c + j * k); // Точное решение
                            break;
                        default: // Внутренние узлы (1)
                            v[i][j] = 0.0; // Нулевое начальное приближение
                            break;
                    }
                }
            }

            iter_count = 0;
            double temp, prev, currentEps;

            do
            {
                Eps_max = 0.0;

                for (int j = 1; j < m; j++)
                {
                    for (int i = 1; i < n; i++)
                    {
                        if (domainMatrix[i][j] == 1)
                        {
                            prev = v[i][j];
                            temp = -w_opt * (h2 * (v[i + 1][j] + v[i - 1][j]) + k2 * (v[i][j + 1] + v[i][j - 1]));
                            temp = temp + (1 - w_opt) * A * v[i][j] + w_opt * f[i][j];
                            temp = temp / A;

                            currentEps = Math.Abs(prev - temp);
                            if (currentEps > Eps_max) Eps_max = currentEps;

                            v[i][j] = temp;
                        }
                    }
                }
                iter_count++;

            } while (Eps_max > Eps && iter_count < N_max);
        }

        void calk_MaxR_MaxDiffUV()  // Вычисление максимальной невязки и максимальной разности U и V
        {
            MaxR = 0.0;

            xMax = 0.0;
            yMax = 0.0;
            MaxDiff_UV = 0.0;


            double temp = 0.0;
            evcR1 = 0.0;

            for (int j = 0; j <= m; j++)
            {
                for (int i = 0; i <= n; i++)
                {
                    if (domainMatrix[i][j] == 1)
                    {
                        // Невязка
                        //double residual = Math.Abs(
                        //    (2.0 / (h * h) + 2.0 / (k * k)) * v[i][j] -
                        //    (v[i - 1][j] + v[i + 1][j]) / (h * h) -
                        //    (v[i][j - 1] + v[i][j + 1]) / (k * k) -
                        //    f[i][j]);
                        //if (residual > MaxR) MaxR = residual;

                        //temp = (-1) * A * v[i][j] - h2 * (v[i - 1][j] + v[i + 1][j]) - k2 * (v[i][j - 1] + v[i][j + 1]) - f1(x[i], y[j]);
                        temp = A * v[i][j] + h2 * (v[i - 1][j] + v[i + 1][j]) + k2 * (v[i][j - 1] + v[i][j + 1]) - f1(x[i], y[j]);
                        evcR1 += temp * temp;
                        if (Math.Abs(temp) >= MaxR) MaxR = Math.Abs(temp);

                        //for (int j = 1; j < m; j++)
                        //{
                        //    for (int i = 1; i < n; i++)
                        //    {
                        //        //temp = A * v[i][j] + h2 * (v[i - 1][j] + v[i + 1][j]) + k2 * (v[i][j - 1] + v[i][j + 1]) - f1(x[i], y[j]);

                        //        temp = (-1) * A * v[i][j] - h2 * (v[i - 1][j] + v[i + 1][j]) - k2 * (v[i][j - 1] + v[i][j + 1]) - f1(x[i], y[j]);
                        //        evcR1 += temp * temp;
                        //        if (Math.Abs(temp) >= MaxR) MaxR = Math.Abs(temp);
                        //    }
                        //}

                        // Разность U и V
                        double error = Math.Abs(u[i][j] - v[i][j]);

                        if (error > MaxDiff_UV)
                        {
                            MaxDiff_UV = error;
                            xMax = x[i];
                            yMax = y[j];
                        }
                    }
                }
            }

            evcR1 = Math.Sqrt(evcR1);
        }

        void fillReference()  // Заполнение справки
        {
            textBox5.Text = Convert.ToString(w_opt);
            textBox9.Text = Convert.ToString(iter_count);
            textBox10.Text = Convert.ToString(Eps_max);
            textBox11.Text = Convert.ToString(MaxDiff_UV);
            textBox15.Text = Convert.ToString(MaxF);
            textBox16.Text = Convert.ToString(MaxR);
            textBox12.Text = Convert.ToString(xMax);
            textBox13.Text = Convert.ToString(yMax);

            textBox6.Text = Convert.ToString(evcR1);

            textBox14.Text = "Нулевое начальноe приближение";
        }

        // Очистка и настройка таблиц
        private void SetupDataGridViews()  // Таблица для U
        {
            // Настройка dataGridView1
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // Добавляем служебные столбцы
            dataGridView1.Columns.Add("empty1", "");
            dataGridView1.Columns.Add("empty2", "");
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 50;

            // Добавляем столбцы для x значений
            for (int i = 0; i <= n; i++)
            {
                dataGridView1.Columns.Add($"x{i}", $"x{i}");
                dataGridView1.Columns[i + 2].Width = 60;
            }

            // Добавляем заголовочную строку с x[i] значениями
            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].Cells[0].Value = "";
            dataGridView1.Rows[0].Cells[1].Value = "";
            for (int i = 0; i <= n; i++)
            {
                dataGridView1.Rows[0].Cells[i + 2].Value = $"{x[i]:F3}";
            }

            // Добавляем строки с данными
            for (int j = 0; j <= m; j++)
            {
                int rowIdx = dataGridView1.Rows.Add();
                dataGridView1.Rows[rowIdx].Cells[0].Value = $"y{j}";
                dataGridView1.Rows[rowIdx].Cells[1].Value = $"{y[j]:0.####}";

                for (int i = 0; i <= n; i++)
                {
                    dataGridView1.Rows[rowIdx].Cells[i + 2].Value = u[i][j].ToString("0.######");
                }
            }
        }

        private void SetupDataGridView2()  // Таблица для V
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            dataGridView2.Columns.Add("empty1", "");
            dataGridView2.Columns.Add("empty2", "");
            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 50;

            for (int i = 0; i <= n; i++)
            {
                dataGridView2.Columns.Add($"x{i}", $"x{i}");
                dataGridView2.Columns[i + 2].Width = 60;
            }

            dataGridView2.Rows.Add();
            dataGridView2.Rows[0].Cells[0].Value = "";
            dataGridView2.Rows[0].Cells[1].Value = "";

            for (int i = 0; i <= n; i++)
            {
                dataGridView2.Rows[0].Cells[i + 2].Value = $"{x[i]:0.####}";
            }

            for (int j = 0; j <= m; j++)
            {
                int rowIdx = dataGridView2.Rows.Add();
                dataGridView2.Rows[rowIdx].Cells[0].Value = $"y{j}";
                dataGridView2.Rows[rowIdx].Cells[1].Value = $"{y[j]:0.####}";

                for (int i = 0; i <= n; i++)
                {
                    dataGridView2.Rows[rowIdx].Cells[i + 2].Value = v[i][j].ToString("0.######");
                }
            }
        }

        private void SetupDataGridView3()  // Таблица |U-V|
        {
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();

            dataGridView3.Columns.Add("empty1", "");
            dataGridView3.Columns.Add("empty2", "");
            dataGridView3.Columns[0].Width = 50;
            dataGridView3.Columns[1].Width = 50;

            for (int i = 0; i <= n; i++)
            {
                dataGridView3.Columns.Add($"x{i}", $"x{i}");
                dataGridView3.Columns[i + 2].Width = 60;
            }

            dataGridView3.Rows.Add();
            dataGridView3.Rows[0].Cells[0].Value = "";
            dataGridView3.Rows[0].Cells[1].Value = "";

            for (int i = 0; i <= n; i++)
            {
                dataGridView3.Rows[0].Cells[i + 2].Value = $"{x[i]:0.####}";
            }

            for (int j = 0; j <= m; j++)
            {
                int rowIdx = dataGridView3.Rows.Add();
                dataGridView3.Rows[rowIdx].Cells[0].Value = $"y{j}";
                dataGridView3.Rows[rowIdx].Cells[1].Value = $"{y[j]:0.####}";

                for (int i = 0; i <= n; i++)
                {
                    double error = Math.Abs(u[i][j] - v[i][j]);
                    dataGridView3.Rows[rowIdx].Cells[i + 2].Value = error.ToString("0.######");
                }
            }
        }

        // Обработка нажатий
        private void тестоваяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void пример05e6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Чтение данных из полей
            n = Convert.ToInt32(textBox1.Text);
            m = Convert.ToInt32(textBox2.Text);
            N_max = Convert.ToInt32(textBox3.Text);
            Eps = Convert.ToDouble(textBox4.Text);

            // Проверка n и m
            if (!CheckGridParameters()) return;

            // Расчёт оптимального параметра w
            w_opt = optimal_W(n, m);
            MessageBox.Show("Ваш оптимальный W = " + w_opt.ToString());

            // Инициализация матрицы области
            InitializeDomainMatrix();

            // Расчёт констант
            h = (b - a) / (double)n;
            k = (d - c) / (double)m;
            h2 = -1 / (h * h);
            k2 = -1 / (k * k);
            A = -2 * (h2 + k2);

            // Инициализация массивов
            u = new double[n + 1][];
            v = new double[n + 1][];
            f = new double[n + 1][];
            x = new double[n + 1];
            y = new double[m + 1];
            for (int i = 0; i <= n; i++)
            {
                u[i] = new double[m + 1];
                v[i] = new double[m + 1];
                f[i] = new double[m + 1];
            }

            // Заполнение массива x
            for (int i = 0; i <= n; i++)
            {
                x[i] = a + i * h;
            }

            // Заполнение массива y
            for (int j = 0; j <= m; j++)
            {
                y[j] = c + j * k;
            }

            // Заполнение массивов f и u
            MaxF = 0.0;
            for (int j = 0; j <= m; j++)
            {
                for (int i = 0; i <= n; i++)
                {
                    switch (domainMatrix[i][j])
                    {
                        case 0: // Исключённые узлы
                            f[i][j] = 0.0;
                            u[i][j] = 0.0;
                            break;
                        default: // Граничные и внутренние узлы (1), (2)
                            f[i][j] = f1(x[i], y[j]);
                            u[i][j] = u1(x[i], y[j]);

                            if (Math.Abs(f[i][j]) > MaxF) MaxF = Math.Abs(f[i][j]);

                            break;
                    }
                }
            }

            // Решение методом верхней релаксации
            SolvePoissonSOR();

            // Справка
            calk_MaxR_MaxDiffUV();
            fillReference();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (u == null)
            {
                MessageBox.Show("Ошибка: Сначала произведите вычисления", "Отсутствуют данные");
                return;
            }

            // Таблица
            SetupDataGridViews();
            SetupDataGridView2();
            SetupDataGridView3();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (u == null)
            {
                MessageBox.Show("Ошибка: Сначала произведите вычисления", "Отсутствуют данные");
                return;
            }

            // 2D графики
            int n = Convert.ToInt32(textBox1.Text);
            int m = Convert.ToInt32(textBox2.Text);
            Form20 form20 = new Form20(u, v, n, m);
            form20.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (u == null)
            {
                MessageBox.Show("Ошибка: Сначала произведите вычисления", "Отсутствуют данные");
                return;
            }

            // 3D графики
            int n = Convert.ToInt32(textBox1.Text);
            int m = Convert.ToInt32(textBox2.Text);
            Form4 form4 = new Form4(u, v, n, m);
            form4.Show();
        }
    }
}