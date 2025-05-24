using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace NumMeth_Lab2_var1_st3_MVR
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        // =========  Functions =========
        double u1(double x, double y) // U* Решение тестовой задачи
        {
            return Math.Exp( Math.Pow( Math.Sin(Math.PI*x*y),2 ) );
        }
        double f1(double x, double y) // Функция полученная через Лапласса
        {
            //return (2*(2*Math.Pow(x,2)-1)*Math.Exp(0-Math.Pow(x,2)-Math.Pow(y,2)+1) + 2 * (2 * Math.Pow(y, 2) - 1) * Math.Exp(0 - Math.Pow(x, 2) - Math.Pow(y, 2) + 1));
            //return -4 * Math.Exp(1 - Math.Pow(x, 2) - Math.Pow(y, 2)) * (x * x + y * y - 1);
            double pi = Math.PI;
            double sinTerm = Math.Sin(2 * pi * x * y);
            double cosTerm = Math.Cos(2 * pi * x * y);
            double exponent = Math.Sin(pi * x * y);

            double result = Math.Pow(pi, 2)
                          * (Math.Pow(x, 2) + Math.Pow(y, 2))
                          * (Math.Pow(sinTerm, 2) + 2 * cosTerm)
                          * Math.Exp(Math.Pow(exponent, 2));

            return (-1.0) * result;
        }

        double f2(double x, double y) // F*
        {
            //return Math.Abs(Math.Pow(x,2) - Math.Pow(y, 2));
            return Math.Pow(Math.Sin(Math.PI * x * y), 2);
        }

        double mu1(double y) //Граничное условие 1
        {
            return Math.Sin(Math.PI * y);
        }

        double mu2(double y) //Граничное условие 2
        {
            return Math.Sin(Math.PI * y);
        }

        double mu3(double x) //Граничное условие 3
        {
            return x - x*x;
        }

        double mu4(double x) //Граничное условие 4
        {
            //return 1 - x * x;
            return x - x*x;
        }

        double optimal_W(double n,double m)
        {
            double w_res = 0.0;
            double l = 1 - 0.5 * (1 / (Math.Pow((1 / (1 / n)), 2) + Math.Pow((1 / (1 / m)), 2))) * 4 * (Math.Pow((Math.Sin(Math.PI / (n * 2)) / ((1 / n))), 2) + Math.Pow((Math.Sin(Math.PI / (m * 2)) / ((1 / m))), 2));

            //double l = 1 - 0.5 * (1 / ( Math.Pow((1 / (2 / n)), 2) + Math.Pow((1 / (2 / m)), 2)) ) * 4 * (Math.Pow((Math.Sin(Math.PI / (n * 2))/((2 / n))), 2) + Math.Pow((Math.Sin(Math.PI / (m * 2)) / ((2 / m))), 2));
            w_res = 2 / (1 + Math.Sqrt(1-Math.Pow(l,2)));
            return w_res;
        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        //РАБОТАЕТ ВЕРНО, КАК ПО ПРОГЕ КАПКАЕВА
        //Вынес массивы для передачу в отрисовку
        double[][] v1;
        double[][] u;
        double[][] v2;
        double[][] v2_2;

        //Тестовая
        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            int m = Convert.ToInt32(textBox2.Text);
            int N_max = Convert.ToInt32(textBox3.Text);
            double Eps = Convert.ToDouble(textBox4.Text);
            double h = 1.0 / (double)n, k = 1.0 / (double)m; //Шаги по x и y
            double h2 = -1.0 / (h * h), k2 = -1.0 / (k * k); 
            double A = -2 * (h2 + k2);
            //double[][] v1;
            double[][] f;
            //double[][] u;
            double[] x, y;
            int p = 0; //Текущее число итераций
            char[] buffer = new char[65535 / (n + 2)];
            double MaxPogr = 0.0;
            double Pogr;
            double MaxF = 0.0;
            double maxR1 = 0.0;

            x = new double[n + 1];
            y = new double[m + 1];
            v1 = new double[n + 1][];
            f = new double[n + 1][];
            u = new double[n + 1][];

            for (int i = 0; i <= n; i++)
            {
                v1[i] = new double[m + 1];
                f[i] = new double[m + 1];
                u[i] = new double[m + 1];
            }
            
            for (int i = 0; i <= n; i++)  //Заполнение массива x
            {
                x[i] = 0 + i * h;
              
            }
     
            for (int j = 0; j <= m; j++)  //Заполнение массива y
            {
                y[j] = 0 + j * k;// БЫЛО 2+j
            
            }


            for (int j = 0; j <= m; j++)            //Заполнение массивов f и u
            {
                for (int i = 0; i <= n; i++)
                {
                    f[i][j] = f1(x[i], y[j]);
                    u[i][j] = u1(x[i], y[j]);
                    if (Math.Abs(f[i][j]) > MaxF) MaxF = Math.Abs(f[i][j]);
                    //MaxF += f[i][j] * f[i][j];
                }
            }

            //MaxF = Math.Sqrt(MaxF);

            for (int j = 0; j <= m; j++)  //Заполнение граничных условий в массив v1
            {
                v1[0][j] = u1(0, y[j]);
                v1[n][j] = u1(1, y[j]);
            }

            for (int i = 0; i <= n; i++)  //Заполнение граничных условий в массив v1
            {
                v1[i][0] = u1(x[i], 0);
                v1[i][m] = u1(x[i], 1);
            }

            for (int j = 1; j < m; j++)    //Нулевое начальное приближение
            {
                for (int i = 1; i < n; i++)
                {
                    v1[i][j] = 0.0;
                }
            }

            // UpRelaxMethod
            double temp, prev, currentEps;
            double Eps_max;
            double w = Convert.ToDouble(textBox28.Text);
            double w_opt = optimal_W(n, m);
            MessageBox.Show("Ваш оптимальный W = " + w_opt.ToString());

            if (checkBox1.Checked)
            {
                w = w_opt;
                textBox30.Text = Convert.ToString(w_opt);
            }

            var progressForm = new FormProgress1();
            progressForm.Show();

            int totalIterations = N_max;
            int currentIteration = 0;

            //double w = 1.99;
            while (true)
            {
                Eps_max = 0.0;
                for (int j = 1; j < m; j++)
                {
                    for (int i = 1; i < n; i++)
                    {

                        prev = v1[i][j]; 
                        temp = -w * (h2 * (v1[i + 1][j] + v1[i - 1][j]) + k2 * (v1[i][j + 1] + v1[i][j - 1]));
                        temp = temp + (1 - w) * A * v1[i][j] + w * f[i][j];
                        temp = temp / A;
                        currentEps = Math.Abs(prev - temp);
                        if (currentEps > Eps_max) { Eps_max = currentEps; };
                        v1[i][j] = temp;

                    }
                }

                currentIteration++;

                // Обновляем прогресс
                int percent = (int)((double)currentIteration / totalIterations * 100);
                progressForm.UpdateProgress(percent, $"Итерация {p} из {N_max}... Точность: {Eps_max:F6}");

                p++;
                if ((Eps_max < Eps) || (p > N_max))
                    break;
            }

            progressForm.Close();

            // nevyazka na vyhode
            temp = 0.0;
            for (int j = 1; j < m; j++)
            {
                for (int i = 1; i < n; i++)
                {
                    temp = A * v1[i][j] + h2 * (v1[i - 1][j] + v1[i + 1][j]) + k2 * (v1[i][j - 1] + v1[i][j + 1]) - f1(x[i], y[j]);
                    if (Math.Abs(temp) >= maxR1) maxR1 = Math.Abs(temp);
                    //maxR1 += temp * temp;
                }
            }
            maxR1 = Math.Sqrt(maxR1);

            //table



            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("C1", "");
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns.Add("C2", "i");
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[1].Frozen = true;

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("C2", "");
            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[0].Frozen = true;
            dataGridView2.Columns.Add("C3", "i");
            dataGridView2.Columns[1].Width = 50;
            dataGridView2.Columns[1].Frozen = true;

            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            dataGridView3.Columns.Add("C4", "");
            dataGridView3.Columns[0].Width = 50;
            dataGridView3.Columns[0].Frozen = true;
            dataGridView3.Columns.Add("C5", "i");
            dataGridView3.Columns[1].Width = 50;
            dataGridView3.Columns[1].Frozen = true;

            ////var progressForm3 = new FormProgress1();
            ////progressForm3.Show();

            ////int totalColumns = n + 1; // Общее количество столбцов
            ////int processedColumns = 0;

            //for (int i = 0; i <= n; i++)                        //Создание столбцов для таблиц
            //{
            //    //dataGridView1.Columns.Add(Convert.ToString(buffer), Convert.ToString(buffer));
            //    //dataGridView2.Columns.Add(Convert.ToString(buffer), Convert.ToString(buffer));
            //    //dataGridView3.Columns.Add(Convert.ToString(buffer), Convert.ToString(buffer));
            //    // Добавляем столбец
            //    dataGridView1.Columns.Add($"Column{i}", $"Column {i}");
            //    dataGridView2.Columns.Add($"Column{i}", $"Column {i}");
            //    dataGridView3.Columns.Add($"Column{i}", $"Column {i}");

            //    // Устанавливаем FillWeight так, чтобы сумма не превышала 65535
            //    int fillWeight = 65535 / (n + 2); // Равномерное распределение
            //    dataGridView1.Columns[i].FillWeight = fillWeight;
            //    dataGridView2.Columns[i].FillWeight = fillWeight;
            //    dataGridView3.Columns[i].FillWeight = fillWeight;

            //    // Обновление прогресса
            //    //processedColumns++;
            //    //int percent = (int)((double)processedColumns / totalColumns * 100);
            //    //progressForm3.UpdateProgress(percent, $"Создание столбцов: {processedColumns} из {totalColumns}");

            //    // Принудительное обновление UI (чтобы окно не зависало)
            //    //if (i % 10 == 0) // Обновляем каждые 10 столбцов
            //    // Application.DoEvents();
            //}
            //progressForm3.Close();
            using (var progressForm3 = new FormProgress1())
            {
                progressForm3.Show();
                int totalColumns = n + 1;

                for (int i = 0; i <= n; i++)
                {
                    // UI-операции в главном потоке
                    this.Invoke((MethodInvoker)delegate
                    {
                        dataGridView1.Columns.Add($"Column{i}", $"Column {i}");
                        dataGridView2.Columns.Add($"Column{i}", $"Column {i}");
                        dataGridView3.Columns.Add($"Column{i}", $"Column {i}");

                        int fillWeight = 65535 / (n + 2);
                        dataGridView1.Columns[i].FillWeight = fillWeight;
                        dataGridView2.Columns[i].FillWeight = fillWeight;
                        dataGridView3.Columns[i].FillWeight = fillWeight;
                    });

                    // Обновление прогресса
                    int percent = (int)((double)(i + 1) / totalColumns * 100);
                    progressForm3.UpdateProgress(percent, $"Создание столбцов: {i + 1} из {totalColumns}");

                    if (i % 10 == 0)
                        Application.DoEvents();
                }

                progressForm3.Close();
            }

            dataGridView1.Rows.Add("j", "Y\\X");  // Создание второй строки
            dataGridView2.Rows.Add("j", "Y\\X");  // Создание второй строки
            dataGridView3.Rows.Add("j", "Y\\X");  // Создание второй строки

            for (int i = 0; i <= n; i++)               //Заполнение второй строки
            {
                dataGridView1.Columns[i + 2].HeaderText = i.ToString();
                dataGridView2.Columns[i + 2].HeaderText = i.ToString();
                dataGridView3.Columns[i + 2].HeaderText = i.ToString();

                dataGridView1.Rows[0].Cells[i + 2].Value = x[i];//+2
                dataGridView2.Rows[0].Cells[i + 2].Value = x[i];
                dataGridView3.Rows[0].Cells[i + 2].Value = x[i];

            }
            for (int j = 0; j <= m; j++)          //Заполнение первых двух столбцов
            {
                dataGridView1.Rows.Add();
                dataGridView2.Rows.Add();
                dataGridView3.Rows.Add();

                for (int i = 0; i <= 1; i++)
                {
                    dataGridView1.Rows[j + 1].Cells[0].Value = j;
                    dataGridView1.Rows[j + 1].Cells[1].Value = y[j];
                    dataGridView2.Rows[j + 1].Cells[0].Value = j;
                    dataGridView2.Rows[j + 1].Cells[1].Value = y[j];
                    dataGridView3.Rows[j + 1].Cells[0].Value = j;
                    dataGridView3.Rows[j + 1].Cells[1].Value = y[j];
                }
            }
            double xMax = 0.0;
            double yMax = 0.0;

            var progressForm2 = new FormProgress1();
            progressForm2.Show();

            int totalCells = (m + 1) * (n + 1); // Общее количество ячеек
            int processedCells = 0;

            for (int j = 0; j <= m; j++)              //Заполнение таблиц значениями
            {
                for (int i = 0; i <= n; i++)
                {
                    Pogr = Math.Abs(u[i][j] - v1[i][j]);
                    v1[i][j] = Math.Round(v1[i][j] * 100000) / 100000;
                    u[i][j] = Math.Round(u[i][j] * 100000) / 100000;

                    dataGridView1.Rows[j + 1].Cells[i + 2].Value = u[i][j];

                    dataGridView2.Rows[j + 1].Cells[i + 2].Value = v1[i][j];

                    dataGridView3.Rows[j + 1].Cells[i + 2].Value = Pogr;

                    if (Pogr > MaxPogr)
                    {
                        MaxPogr = Pogr;
                        xMax = x[i];
                        yMax = y[j];
                    }
                }

                // Обновление прогресса
                processedCells++;
                int percent = (int)((double)processedCells / totalCells * 100);
                progressForm2.UpdateProgress(percent, $"Обработано: {processedCells} из {totalCells} ячеек");

                // Принудительное обновление UI (чтобы окно не зависало)
                if (processedCells % 100 == 0)
                    Application.DoEvents();

            }
            progressForm2.Close();

            // Справка
            textBox9.Text = Convert.ToString(p);
            textBox10.Text = Convert.ToString(Eps_max);
            textBox11.Text = Convert.ToString(MaxPogr);
            textBox15.Text = Convert.ToString(MaxF);
            textBox16.Text = Convert.ToString(maxR1);
            textBox12.Text = Convert.ToString(xMax);
            textBox13.Text = Convert.ToString(yMax);

            textBox14.Text = "Нулевое начальноe приближение";

        }
        //ПРОВЕРКА НА ДОСТОВЕРНОСТЬ ИДЁТ

        //Основная
        private void button2_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox8.Text);
            int m = Convert.ToInt32(textBox7.Text);
            int N_max = Convert.ToInt32(textBox6.Text);
            double Eps = Convert.ToDouble(textBox5.Text);
            double h = 1.0 / n, k = 1.0 / m; //Шаги по x и y
            double h2 = -1.0 / (h * h), k2 = -1.0 / (k * k); 
            double A = -2 * (h2 + k2);
            //double[][] v2;
            double[][] f;
            double[] x, y;

            int p = 0; //Текущее число итераций
            char[] buffer = new char[100];
            double MaxPogr = 0.0;
            double Pogr;
            double xMax = 0.0;
            double yMax = 0.0;
            double MaxF = 0.0, MaxF2 = 0.0;
            double maxR1 = 0.0;

            x = new double[n + 1];
            y = new double[m + 1];
            v2 = new double[n + 1][];
            f = new double[n + 1][];

            for (int i = 0; i <= n; i++)
            {
                v2[i] = new double[m + 1];
                f[i] = new double[m + 1];
            }

            for (int i = 0; i <= n; i++)  //Заполнение массива x
            {
                x[i] = 0 + i * h;
            }

            for (int j = 0; j <= m; j++)  //Заполнение массива y
            {
                y[j] = 0 + j * k;
            }

            for (int j = 0; j <= m; j++)            //Заполнение массива f 
            {
                for (int i = 0; i <= n; i++)
                {
                    f[i][j] = f2(x[i], y[j]);
                    //MaxF += f[i][j] * f[i][j];
                    if (Math.Abs(f[i][j]) > MaxF) MaxF = Math.Abs(f[i][j]);
                }
            }
            //MaxF = Math.Sqrt(MaxF);

            for (int j = 0; j <= m; j++)  //Заполнение граничных условий в массив v2
            {
                v2[0][j] = mu1(y[j]);
                v2[n][j] = mu2(y[j]);
            }

            for (int i = 0; i <= n; i++)  //Заполнение граничных условий в массив v2
            {
                v2[i][0] = mu3(x[i]);
                v2[i][m] = mu4(x[i]);
            }

            for (int j = 1; j < m; j++)    //Нулевое начальное приближение
            {
                for (int i = 1; i < n; i++)
                {
                    v2[i][j] = 0.0;
                }
            }
            // UpRelaxMethod 
            double temp, prev, currentEps;
            double Eps_max;
            double w = 0;//Convert.ToDouble(textBox29.Text);
            double w_opt = optimal_W(n, m);
            MessageBox.Show("Ваш оптимальный W = " + w_opt.ToString());
            w = w_opt;
            textBox31.Text = Convert.ToString(w_opt);

            var progressFormv1 = new FormProgress1();
            progressFormv1.Show();

            int totalIterations = N_max;
            int currentIteration = 0;

            //double w = 1.99;
            while (true)
            {
                Eps_max = 0.0;
                for (int j = 1; j < m; j++)
                {
                    for (int i = 1; i < n; i++)
                    {
                        prev = v2[i][j]; 
                        temp = -w * (h2 * (v2[i + 1][j] + v2[i - 1][j]) + k2 * (v2[i][j + 1] + v2[i][j - 1]));
                        temp = temp + (1 - w) * A * v2[i][j] + w * f[i][j];
                        temp = temp / A;
                        currentEps = Math.Abs(prev - temp);
                        if (currentEps > Eps_max) { Eps_max = currentEps; };
                        v2[i][j] = temp;
                    }
                }

                currentIteration++;

                // Обновляем прогресс
                int percent = (int)((double)currentIteration / totalIterations * 100);
                progressFormv1.UpdateProgress(percent, $"Одинарный шаг: Итерация {p} из {N_max}... Точность: {Eps_max:F6}");

                p++;
                if ((Eps_max < Eps) || (p > N_max))
                    break;
            }
            progressFormv1.Close();
            // nevyazka na vyhode
            temp = 0.0;
            for (int j = 1; j < m; j++)
            {
                for (int i = 1; i < n; i++)
                {
                    temp = A * v2[i][j] + h2 * (v2[i - 1][j] + v2[i + 1][j]) + k2 * (v2[i][j - 1] + v2[i][j + 1])  - f2(x[i], y[j]);
                    if (temp >= maxR1) maxR1 = Math.Abs(temp);
                    //maxR1 += temp * temp;
                }
            }
            //maxR1 = Math.Sqrt(maxR1);

            // solution whis step / 2
            n = 2 * n;
            m = 2 * m;
            //double[][] v2_2;
            x = new double[n + 1];
            y = new double[m + 1];
            v2_2 = new double[n + 1][];
            f = new double[n + 1][];

            h = 1.0 / n;
            k = 1.0 / m;
            h2 = -1.0 / (h * h); 
            k2 = -1.0 / (k * k); 
            A = -2 * (h2 + k2);

            int p2 = 0;
            double maxR = 0.0;

            for (int i = 0; i <= n; i++)
            {
                v2_2[i] = new double[m + 1];
                f[i] = new double[m + 1];
            }

            for (int i = 0; i <= n; i++)  //Заполнение массива x
            {
                x[i] = 0 + i * h;
            }

            for (int j = 0; j <= m; j++)  //Заполнение массива y
            {
                y[j] = 0 + j * k;
            }

            for (int j = 0; j <= m; j++)            //Заполнение массива f 
            {
                for (int i = 0; i <= n; i++)
                {
                    f[i][j] = f2(x[i], y[j]);
                    //MaxF2 += f[i][j] * f[i][j];
                    if (Math.Abs(f[i][j]) > MaxF2) MaxF2 = Math.Abs(f[i][j]);
                }
            }

            //MaxF2 = Math.Sqrt(MaxF2);

            for (int j = 0; j <= m; j++)  //Заполнение граничных условий в массив v
            {
                v2_2[0][j] = mu1(y[j]);
                v2_2[n][j] = mu2(y[j]);
            }

            for (int i = 0; i <= n; i++)  //Заполнение граничных условий в массив v
            {
                v2_2[i][0] = mu3(x[i]);
                v2_2[i][m] = mu4(x[i]);
            }

            for (int j = 1; j < m; j++)    //Нулевое начальное приближение
            {
                for (int i = 1; i < n; i++)
                {
                    v2_2[i][j] = 0.0;
                }
            }

            // UpRelaxMethod
            temp = 0.0;
            prev = 0.0;
            currentEps = 0.0;
            double Eps_max2;
            
            w = 0;// Convert.ToDouble(textBox29.Text);
            w_opt = optimal_W(n, m);
            MessageBox.Show("Ваш оптимальный для двойного шага W = " + w_opt.ToString());
            w = w_opt;
            textBox32.Text = Convert.ToString(w_opt);


            var progressFormv2 = new FormProgress1();
            progressFormv2.Show();

            totalIterations = N_max;
            currentIteration = 0;

            while (true)
            {
                Eps_max2 = 0.0;
                for (int j = 1; j < m; j++)
                {
                    for (int i = 1; i < n; i++)
                    {
                        prev = v2_2[i][j];
                        temp = -w * (h2 * (v2_2[i + 1][j] + v2_2[i - 1][j]) + k2 * (v2_2[i][j + 1] + v2_2[i][j - 1]));
                        temp = temp + (1 - w) * A * v2_2[i][j] + w * f2(x[i], y[j]);
                        temp = temp / A;
                        currentEps = Math.Abs(prev - temp);
                        if (currentEps > Eps_max2) { Eps_max2 = currentEps; };
                        v2_2[i][j] = temp;
                    }
                }
                currentIteration++;

                // Обновляем прогресс
                int percent = (int)((double)currentIteration / totalIterations * 100);
                progressFormv2.UpdateProgress(percent, $"Двойной шаг: Итерация {p2} из {N_max}... Точность: {Eps_max2:F6}");

                p2++;
                if ((Eps_max2 < Eps) || (p2 > N_max))
                    break;
            }
            progressFormv2.Close();

            // nevyazka na vyhode
            temp = 0.0;
            for (int j = 1; j < m; j++)
            {
                for (int i = 1; i < n; i++)
                {
                    temp = A * v2_2[i][j] + h2 * (v2_2[i - 1][j] + v2_2[i + 1][j]) + k2 * (v2_2[i][j - 1] + v2_2[i][j + 1]) - f2(x[i], y[j]);
                    if (temp >= maxR) maxR = Math.Abs(temp);
                    //maxR += temp * temp;
                }
            }

            //maxR = Math.Sqrt(maxR);

            n = n / 2;
            m = m / 2;
            // table

            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Clear();

            dataGridView5.Rows.Clear();
            dataGridView5.Columns.Clear();

            dataGridView6.Rows.Clear();
            dataGridView6.Columns.Clear();

            dataGridView4.Columns.Add("C1", "");
            dataGridView4.Columns[0].Width = 50;
            dataGridView4.Columns[0].Frozen = true;
            dataGridView4.Columns.Add("C2", "i");
            dataGridView4.Columns[1].Width = 50;
            dataGridView4.Columns[1].Frozen = true;

            dataGridView5.Columns.Add("C1", "");
            dataGridView5.Columns[0].Width = 50;
            dataGridView5.Columns[0].Frozen = true;
            dataGridView5.Columns.Add("C2", "i");
            dataGridView5.Columns[1].Width = 50;
            dataGridView5.Columns[1].Frozen = true;

            dataGridView6.Columns.Add("C1", "");
            dataGridView6.Columns[0].Width = 50;
            dataGridView6.Columns[0].Frozen = true;
            dataGridView6.Columns.Add("C2", "i");
            dataGridView6.Columns[1].Width = 50;
            dataGridView6.Columns[1].Frozen = true;

            using (var progressForm3v2 = new FormProgress1())
            {
                progressForm3v2.Show();
                int totalColumns = n + 1;

                for (int i = 0; i <= n; i++)
                {
                    // UI-операции в главном потоке
                    this.Invoke((MethodInvoker)delegate
                    {
                        dataGridView4.Columns.Add($"Column{i}", $"Column {i}");
                        dataGridView5.Columns.Add($"Column{i}", $"Column {i}");
                        dataGridView6.Columns.Add($"Column{i}", $"Column {i}");

                        int fillWeight = 65535 / (n + 2);
                        dataGridView4.Columns[i].FillWeight = fillWeight;
                        dataGridView5.Columns[i].FillWeight = fillWeight;
                        dataGridView6.Columns[i].FillWeight = fillWeight;
                    });

                    // Обновление прогресса
                    int percent = (int)((double)(i + 1) / totalColumns * 100);
                    progressForm3v2.UpdateProgress(percent, $"Создание столбцов: {i + 1} из {totalColumns}");

                    if (i % 10 == 0)
                        Application.DoEvents();
                }

                progressForm3v2.Close();
            }

            dataGridView4.Rows.Add("j", "Y\\X");  // Создание второй строки
            dataGridView5.Rows.Add("j", "Y\\X");  // Создание второй строки
            dataGridView6.Rows.Add("j", "Y\\X");  // Создание второй строки


            for (int i = 0; i <= n; i++)               //Заполнение второй строки
            {


                //dataGridView4.Columns[i + 2].HeaderText = i.ToString();
                //dataGridView5.Columns[i + 2].HeaderText = i.ToString();
                //dataGridView6.Columns[i + 2].HeaderText = i.ToString();
                //dataGridView4.Rows[0].Cells[i + 2].Value = x[2 * i];
                //dataGridView5.Rows[0].Cells[i + 2].Value = x[2 * i];
                //dataGridView6.Rows[0].Cells[i + 2].Value = x[2 * i];

            }
            for (int j = 0; j <= m; j++)          //Заполнение первых двух столбцов
            {
                dataGridView4.Rows.Add();
                dataGridView5.Rows.Add();
                dataGridView6.Rows.Add();
                for (int i = 0; i <= 1; i++)
                {
                    dataGridView4.Rows[j + 1].Cells[0].Value = j;
                    dataGridView4.Rows[j + 1].Cells[1].Value = y[2 * j];
                    dataGridView5.Rows[j + 1].Cells[0].Value = j;
                    dataGridView5.Rows[j + 1].Cells[1].Value = y[2 * j];
                    dataGridView6.Rows[j + 1].Cells[0].Value = j;
                    dataGridView6.Rows[j + 1].Cells[1].Value = y[2 * j];
                }
            }

            var progressForm2v2 = new FormProgress1();
            progressForm2v2.Show();

            int totalCells = (m + 1) * (n + 1); // Общее количество ячеек
            int processedCells = 0;

            for (int j = 0; j <= m; j++)              //Заполнение таблиц значениями
            {
                for (int i = 0; i <= n; i++)
                {
                    Pogr = 0.0;
                    Pogr = Math.Abs(v2[i][j] - v2_2[i * 2][j * 2]);
                    v2[i][j] = Math.Round(v2[i][j] * 1000) / 1000;
                    v2_2[2 * i][2 * j] = Math.Round(v2_2[2 * i][2 * j] * 1000) / 1000;

                    dataGridView4.Rows[j + 1].Cells[i + 2].Value = Math.Round(v2[i][j] * 100000) / 100000; ;

                    dataGridView5.Rows[j + 1].Cells[i + 2].Value = Math.Round(v2_2[2 * i][2 * j] * 100000) / 100000;

                    dataGridView6.Rows[j + 1].Cells[i + 2].Value = Pogr;
                    if (Pogr > MaxPogr)
                    {
                        MaxPogr = Pogr;
                        xMax = x[2 * i];
                        yMax = y[2 * j];
                    }
                }

                // Обновление прогресса
                processedCells++;
                int percent = (int)((double)processedCells / totalCells * 100);
                progressForm2v2.UpdateProgress(percent, $"Обработано: {processedCells} из {totalCells} ячеек");

                // Принудительное обновление UI (чтобы окно не зависало)
                if (processedCells % 100 == 0)
                    Application.DoEvents();
            }
            progressForm2v2.Close();

            //МОЖЕТ БЫТЬ НА ДОРАБОТКЕ, НУЖНО ДЛЯ ЗАПОЛНЕНИЯ ТАБИЦЫ С ПОЛОВИННЫМ ШАГОМ ПО КАЖДОМУ ШАГУ
            /*for (int j = 0; j <= m*2; j++)              //Заполнение таблиц значениями
            {
                for (int i = 0; i <= n*2; i++)
                {
                    v2_2[i][j] = Math.Round(v2_2[i][j] * 1000) / 1000;

                    dataGridView5.Rows[j + 1].Cells[i + 2].Value = v2_2[i][j];
                }
            }*/

            // Справка
            textBox17.Text = Convert.ToString(p);
            textBox18.Text = Convert.ToString(Eps_max);
            textBox19.Text = Convert.ToString(MaxF);
            textBox20.Text = Convert.ToString(maxR1);

            textBox26.Text = Convert.ToString(p2);
            textBox27.Text = Convert.ToString(Eps_max2);
            textBox21.Text = Convert.ToString(MaxF2);
            textBox22.Text = Convert.ToString(maxR);
            textBox23.Text = Convert.ToString(MaxPogr);
            textBox24.Text = Convert.ToString(xMax);
            textBox25.Text = Convert.ToString(yMax);


        
    }
        private void button3_Click_1(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            int m = Convert.ToInt32(textBox2.Text);
            Form8 form4 = new Form8(u,v1,n,m,false);
            form4.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox8.Text);
            int m = Convert.ToInt32(textBox7.Text);
            Form8 form4 = new Form8(v2, v2_2, n, m,true);
            form4.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
