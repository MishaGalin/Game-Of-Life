using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private readonly Random rnd = new Random();

        /// <summary>
        /// Правило рождения новой клетки.
        /// </summary>
        private int[] B;

        /// <summary>
        /// Правило выживания клеток.
        /// </summary>
        private int[] S;

        /// <summary>
        /// Стандартное значение интервала таймера в миллисекундах.
        /// </summary>
        private readonly int defaultTimerInterval = 25;

        /// <summary>
        /// Номер текущего поколения.
        /// </summary>
        private int genCount = 0;

        /// <summary>
        /// Цвет фона
        /// </summary>
        private readonly Color backgroundColor = Color.Black;

        /// <summary>
        /// Цвет клеток.
        /// </summary>
        private readonly Brush foregroundColor = Brushes.LightGray;

        /// <summary>
        /// Длина стороны квадрата, занимаемого одной клеткой, в пикселях.
        /// </summary>
        private int res = 4;

        /// <summary>
        /// Плотность (чем меньше значение, тем больше вероятность появления клетки). Используется для создания случайного поля.
        /// </summary>
        private int density = 10;

        /// <summary>
        /// Поле
        /// </summary>
        private bool[,] field;

        private int rows, cols;

        public Form1()
        {
            InitializeComponent();
            ApplySettings();
            CreateField();
            timer1.Interval = defaultTimerInterval;
            comboBoxTimer.Text = defaultTimerInterval.ToString();
            DrawField();
        }

        /// <summary>
        /// Запуск таймера
        /// </summary>
        private void StartGame()
        {
            timer1.Start();

            btnApply.Enabled = false;
            numUpDownRes.Enabled = false;
            numUpDownDensity.Enabled = false;
            btnNext.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        /// <summary>
        /// Остановка таймера
        /// </summary>
        private void StopGame()
        {
            timer1.Stop();

            btnApply.Enabled = true;
            numUpDownRes.Enabled = true;
            numUpDownDensity.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            FormBorderStyle = FormBorderStyle.Sizable;
        }

        /// <summary>
        /// Генерация нового поколения
        /// </summary>
        private void NextGeneration()
        {
            var newField = new bool[cols, rows];
            int neighboursCount;

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    neighboursCount = CountNeighbours(i, j);

                    if (!field[i, j] && B.Contains(neighboursCount))
                        newField[i, j] = true;
                    else if (field[i, j] && !S.Contains(neighboursCount))
                        newField[i, j] = false;
                    else newField[i, j] = field[i, j];
                }
            }

            field = newField;
            genCount++;
            labelGenCount.Text = genCount.ToString();
        }

        /// <summary>
        /// Подсчет соседей клетки
        /// </summary>
        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int col = (x + i + cols) % cols;
                    int row = (y + j + rows) % rows;
                    bool isSelfChecking = (col == x && row == y);

                    if (field[col, row] && !isSelfChecking)
                        count++;
                }
            }
            return count;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            NextGeneration();
            DrawField();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            genCount = 0;
            labelGenCount.Text = genCount.ToString();

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = false;
                }
            }
            StopGame();
            DrawField();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            NextGeneration();
            DrawField();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                CreateField();
                DrawField();
            }
        }

        private void ComboBoxTimer_TextChanged(object sender, EventArgs e)
        {
            if (!Int32.TryParse(comboBoxTimer.Text, out int interval) || comboBoxTimer.Text[0] == '0' ||
                comboBoxTimer.Text.Contains(" ") || comboBoxTimer.Text == "" ||
                Int32.Parse(comboBoxTimer.Text) <= 0)
            {
                timer1.Interval = defaultTimerInterval;
                comboBoxTimer.Text = defaultTimerInterval.ToString();
                return;
            }
            else timer1.Interval = interval;
        }

        private void BtnRandom_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = rnd.Next(density) == 0;
                }
            }
            DrawField();
            genCount = 0;
            labelGenCount.Text = genCount.ToString();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        /// <summary>
        /// Перерисовка поля.
        /// </summary>
        private void DrawField()
        {
            g.Clear(backgroundColor);
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (field[i, j])
                        g.FillRectangle(foregroundColor, i * res, j * res, res, res); // клетки
                }
            }

            for (int i = 0; i <= cols; i++) // Сетка
            {
                g.DrawLine(Pens.DarkSlateGray, i * res, 0, i * res, rows * res);
            }

            for (int i = 0; i <= rows; i++) // Сетка
            {
                g.DrawLine(Pens.DarkSlateGray, 0, i * res, cols * res, i * res);
            }

            pctrBox.Refresh();
        }

        private void CreateField()
        {
            pctrBox.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pctrBox.Image);

            rows = pctrBox.Height / res;
            cols = pctrBox.Width / res;
            field = new bool[cols, rows];
        }

        private void pctrBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var i = e.Location.X / res;
                var j = e.Location.Y / res;
                if (ValidateMousePosition(i, j))
                    field[i, j] = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                var i = e.Location.X / res;
                var j = e.Location.Y / res;
                if (ValidateMousePosition(i, j))
                    field[i, j] = false;
            }

            if (!timer1.Enabled)
                DrawField();
        }

        private void BtnApply_Click(object sender, EventArgs e) => ApplySettings();

        private void ApplySettings()
        {
            B = textBoxB.Text.Split(' ').Select(Int32.Parse).ToArray();
            S = textBoxS.Text.Split(' ').Select(Int32.Parse).ToArray();

            res = (int)numUpDownRes.Value;
            density = (int)numUpDownDensity.Value;
            CreateField();
            DrawField();
        }

        private bool ValidateMousePosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }
    }
}