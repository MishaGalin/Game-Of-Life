using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class Form1 : Form
    {
        private Field field;
        private Graphics g;
        private readonly Random rnd = new Random();

        /// <summary>
        /// Стандартное значение интервала таймера в миллисекундах.
        /// </summary>
        private readonly int defaultTimerInterval = 25;

        private int genPerSecond = 0;

        /// <summary>
        /// Длина стороны квадрата, занимаемого одной клеткой, в пикселях.
        /// </summary>
        public int res = 8;

        /// <summary>
        /// Плотность (чем меньше значение, тем больше вероятность появления клетки). Используется для создания случайного поля.
        /// </summary>
        public int density = 10;

        public Form1()
        {
            InitializeComponent();
            ApplySettings();

            field = new Field(pctrBox.Width / res, pctrBox.Height / res);

            timer1.Interval = defaultTimerInterval;
            comboBoxTimer.Text = defaultTimerInterval.ToString();

            pctrBox.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pctrBox.Image);

            field.Draw(res, ref g, ref pctrBox);
        }

        /// <summary>
        /// Запуск таймера
        /// </summary>
        private void StartGame()
        {
            timer1.Start();
            timer2.Start();

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
            timer2.Stop();

            btnApply.Enabled = true;
            numUpDownRes.Enabled = true;
            numUpDownDensity.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            labelSpeedNum.Text = "0";

            FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            field.NextGeneration();
            genPerSecond++;
            labelGenCount.Text = field.genCount.ToString();
            field.Draw(res, ref g, ref pctrBox);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            field.Clear();
            labelGenCount.Text = field.genCount.ToString();
            field.Draw(res, ref g, ref pctrBox);
            StopGame();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            field.NextGeneration();
            field.Draw(res, ref g, ref pctrBox);
            labelGenCount.Text = field.genCount.ToString();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                pctrBox.Image = new Bitmap(Width, Height);
                g = Graphics.FromImage(pctrBox.Image);
                field = new Field(pctrBox.Width / res, pctrBox.Height / res);
                field.Draw(res, ref g, ref pctrBox);
            }
        }

        private void ComboBoxTimer_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(comboBoxTimer.Text, out int interval) || comboBoxTimer.Text[0] == '0' ||
                comboBoxTimer.Text.Contains(" ") || comboBoxTimer.Text == "" ||
                int.Parse(comboBoxTimer.Text) <= 0)
            {
                timer1.Interval = defaultTimerInterval;
                comboBoxTimer.Text = defaultTimerInterval.ToString();
                return;
            }
            else
                timer1.Interval = interval;
        }

        private void BtnRandom_Click(object sender, EventArgs e)
        {
            field.CreateRandom(density);
            field.Draw(res, ref g, ref pctrBox);
            labelGenCount.Text = field.genCount.ToString();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void pctrBox_MouseClick(object sender, MouseEventArgs e)
        {
            int i = e.Location.X / res;
            int j = e.Location.Y / res;
            if (e.Button == MouseButtons.Left)
            {
                if (ValidateMousePosition(i, j))
                    field.field[i, j] = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (ValidateMousePosition(i, j))
                    field.field[i, j] = false;
            }

            if (!timer1.Enabled)

                field.Draw(res, ref g, ref pctrBox);
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            ApplySettings();
            field.Draw(res, ref g, ref pctrBox);
        }

        private void ApplySettings()
        {
            int[] B = textBoxB.Text.Split(' ').Select(int.Parse).ToArray();
            int[] S = textBoxS.Text.Split(' ').Select(int.Parse).ToArray();

            res = (int)numUpDownRes.Value;
            density = (int)numUpDownDensity.Value;
            field = new Field(pctrBox.Width / res, pctrBox.Height / res, B, S);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchForm form2 = new SearchForm();
            form2.Show();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            labelSpeedNum.Text = genPerSecond.ToString();
            genPerSecond = 0;
        }

        private bool ValidateMousePosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < field.cols && y < field.rows;
        }
    }
}