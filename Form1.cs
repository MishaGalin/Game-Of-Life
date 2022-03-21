using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Random rnd = new Random();

        private int[] B;

        private int[] S;

        /// <summary>
        /// Стандартное значение интервала таймера в миллисекундах
        /// </summary>
        private readonly int defaultTimerInterval = 20;

        /// <summary>
        /// Номер текущего поколения
        /// </summary>
        private int generationCount = 0;

        /// <summary>
        /// Цвет фона
        /// </summary>
        private readonly Color backgroundColor = Color.Black;

        /// <summary>
        /// Цвет клеток
        /// </summary>
        private readonly Brush foregroundColor = Brushes.White;

        /// <summary>
        /// Длина стороны квадрата, занимаемого одной клеткой, в пикселях
        /// </summary>
        private int resolution;

        /// <summary>
        /// Плотность (чем меньше значение, тем больше вероятность появления клетки)
        /// </summary>
        private int density;

        /// <summary>
        /// Двумерный массив поля
        /// </summary>
        private bool[,] field;

        private int rows, cols;

        private bool firstStart = true;

        public Form1()
        {
            InitializeComponent();
            pctrBox.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pctrBox.Image);
            g.Clear(backgroundColor);
            comboBoxTimer.Text = defaultTimerInterval.ToString();
            pctrBox.Refresh();
        }

        private void StartGame()
        {
            if (timer1.Enabled)
                return;

            timer1.Start();

            numUpDownRes.Enabled = false;
            numUpDownDensity.Enabled = false;
            btnStart.Enabled = false;
            btnNext.Enabled = false;
            btnClear.Enabled = false;
            btnStop.Enabled = true;
            /*
                        if (firstStart)
                        {
                            B = textBoxB.Text.Split(' ').Select(Int32.Parse).ToArray();
                            S = textBoxS.Text.Split(' ').Select(Int32.Parse).ToArray();

                            resolution = (int)numUpDownRes.Value;
                            density = (int)numUpDownDensity.Value;

                            rows = pctrBox.Height / resolution;
                            cols = pctrBox.Width / resolution;
                            field = new bool[cols, rows];

                            firstStart = false;
                        }*/
        }

        private void StopGame()
        {
            if (!timer1.Enabled)
                return;

            timer1.Stop();

            numUpDownRes.Enabled = true;
            numUpDownDensity.Enabled = true;
            btnStart.Enabled = true;
            btnNext.Enabled = true;
            btnClear.Enabled = true;
            btnStop.Enabled = false;
        }

        private void NextGeneration()
        {
            g.Clear(backgroundColor);

            var newField = new bool[cols, rows];

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    var neighboursCount = CountNeighbours(i, j);
                    var hasLife = field[i, j];

                    if (!hasLife && B.Contains(neighboursCount))
                        newField[i, j] = true;
                    else if (hasLife && !S.Contains(neighboursCount))
                        newField[i, j] = false;
                    else newField[i, j] = field[i, j];
                    Draw(i, j);
                }
            }
            field = newField;
            generationCount++;
            labelGenNum.Text = generationCount.ToString();
            pctrBox.Refresh();
        }

        private void Draw(int i, int j)
        {
            if (field[i, j])
                g.FillRectangle(foregroundColor, i * resolution, j * resolution, resolution, resolution);
        }

        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int col = (x + i + cols) % cols;
                    int row = (y + j + rows) % rows;
                    bool isSelfChecking = col == x && row == y;
                    bool hasLife = field[col, row];

                    if (hasLife && !isSelfChecking) count++;
                }
            }
            return count;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            g.Clear(backgroundColor);
            btnNext.Enabled = false;
            generationCount = 0;
            labelGenNum.Text = generationCount.ToString();
            firstStart = true;

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = false;
                }
            }
            pctrBox.Refresh();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                return;

            pctrBox.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pctrBox.Image);
            g.Clear(backgroundColor);
            pctrBox.Refresh();
        }

        private void comboBoxTimer_TextChanged(object sender, EventArgs e)
        {
            int interval;

            if (!Int32.TryParse(comboBoxTimer.Text, out interval) || comboBoxTimer.Text[0] == '0' ||
                comboBoxTimer.Text.Contains(" ") || comboBoxTimer.Text == "" ||
                Int32.Parse(comboBoxTimer.Text) <= 0)
            {
                timer1.Interval = defaultTimerInterval;
                comboBoxTimer.Text = defaultTimerInterval.ToString();
                return;
            }
            else timer1.Interval = interval;
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            g.Clear(backgroundColor);
            B = textBoxB.Text.Split(' ').Select(Int32.Parse).ToArray();
            S = textBoxS.Text.Split(' ').Select(Int32.Parse).ToArray();

            resolution = (int)numUpDownRes.Value;
            density = (int)numUpDownDensity.Value;

            rows = pctrBox.Height / resolution;
            cols = pctrBox.Width / resolution;
            field = new bool[cols, rows];
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = rnd.Next(density) == 0;
                    Draw(i, j);
                }
            }
            firstStart = false;
            generationCount = 0;
            labelGenNum.Text = generationCount.ToString();
            pctrBox.Invalidate();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }
    }
}