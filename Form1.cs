using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class Form1 : Form
    {
        public Field field;
        public Graphics g;

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
            MainTimer.Interval = defaultTimerInterval;
            comboBoxTimer.Text = defaultTimerInterval.ToString();

            pctrBox.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pctrBox.Image);

            field.Draw(res, ref g, ref pctrBox);
        }

        /// <summary>
        /// Запуск таймера
        /// </summary>
        protected void StartGame()
        {
            MainTimer.Start();
            SpeedMeasurementTimer.Start();

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
        protected void StopGame()
        {
            MainTimer.Stop();
            SpeedMeasurementTimer.Stop();

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
            field = field.NextGeneration();
            labelPopulationCount.Text = field.populationCount.ToString();
            genPerSecond++;
            labelGenCount.Text = field.genCount.ToString();

            field.Draw(res, ref g, ref pctrBox);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            field.Clear();
            labelPopulationCount.Text = field.populationCount.ToString();
            labelGenCount.Text = field.genCount.ToString();

            field.Draw(res, ref g, ref pctrBox);
            StopGame();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (MainTimer.Enabled)
                return;

            field = field.NextGeneration();
            labelPopulationCount.Text = field.populationCount.ToString();
            labelGenCount.Text = field.genCount.ToString();

            field.Draw(res, ref g, ref pctrBox);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (MainTimer.Enabled)
                return;

            pctrBox.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pctrBox.Image);
            field = new Field(pctrBox.Width / res, pctrBox.Height / res);

            field.Draw(res, ref g, ref pctrBox);
        }

        private void ComboBoxTimer_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboBoxTimer.Text, out int interval) && interval > 0) // корректный интервал таймера
                MainTimer.Interval = interval;
            else
            {
                MainTimer.Interval = defaultTimerInterval;
                comboBoxTimer.Text = defaultTimerInterval.ToString();
            }
        }

        private void BtnRandom_Click(object sender, EventArgs e)
        {
            StopGame();
            field.RandomCreate(density);
            labelPopulationCount.Text = field.populationCount.ToString();
            labelGenCount.Text = field.genCount.ToString();

            field.Draw(res, ref g, ref pctrBox);
        }

        private void pctrBox_MouseClick(object sender, MouseEventArgs e)
        {
            bool changed = false;
            int i = e.Location.X / res;
            int j = e.Location.Y / res;
            if (!ValidateMousePosition(i, j))
                return;

            if (e.Button == MouseButtons.Left && !field.field[i, j])
            {
                field.AddCell(i, j);
                changed = true;
            }
            else if (e.Button == MouseButtons.Right && field.field[i, j])
            {
                field.RemoveCell(i, j);
                changed = true;
            }

            if (!MainTimer.Enabled && changed)
            {
                labelPopulationCount.Text = field.populationCount.ToString();
                field.Draw(res, ref g, ref pctrBox);
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (MainTimer.Enabled)
                return;

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
            labelGenCount.Text = field.genCount.ToString();
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