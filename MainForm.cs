using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Плотность (чем меньше значение, тем больше вероятность появления клетки). Используется для создания случайного поля.
        /// </summary>
        public int density = 10;

        public Field field;
        public Graphics g;

        /// <summary>
        /// Длина стороны квадрата, занимаемого одной клеткой, в пикселях.
        /// </summary>
        public int res = 8;

        /// <summary>
        /// Стандартное значение интервала таймера в миллисекундах.
        /// </summary>
        private const int defaultTimerInterval = 25;

        private int genPerSecond = 0;

        private int widthWhenBeginResize, heightWhenBeginResize, widthWhenEndResize, heightWhenEndResize;

        public MainForm()
        {
            InitializeComponent();
            SetDefaultInterval();

            pctrBox.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pctrBox.Image);

            ApplySettings();
            field.Draw(res, ref g, ref pctrBox);
        }

        /// <summary>
        /// Запуск таймера.
        /// </summary>
        private void StartGame()
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
        /// Остановка таймера.
        /// </summary>
        private void StopGame()
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

        private void ApplySettings()
        {
            List<int> B = textBoxB.Text.Split(' ').Select(int.Parse).ToList();
            List<int> S = textBoxS.Text.Split(' ').Select(int.Parse).ToList();

            res = (int)numUpDownRes.Value;
            density = (int)numUpDownDensity.Value;
            field = new Field(pctrBox.Width / res, pctrBox.Height / res, B, S);
            labelGenCount.Text = field.genCount.ToString();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (MainTimer.Enabled)
                return;

            ApplySettings();
            field.Draw(res, ref g, ref pctrBox);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            StopGame();
            field.Clear();
            labelPopulationCount.Text = field.populationCount.ToString();
            labelGenCount.Text = field.genCount.ToString();

            field.Draw(res, ref g, ref pctrBox);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (MainTimer.Enabled)
                return;

            field.NextGeneration();
            labelPopulationCount.Text = field.populationCount.ToString();
            labelGenCount.Text = field.genCount.ToString();

            field.Draw(res, ref g, ref pctrBox);
        }

        private void BtnRandom_Click(object sender, EventArgs e)
        {
            StopGame();
            field.RandomCreate(density);
            labelPopulationCount.Text = field.populationCount.ToString();
            labelGenCount.Text = field.genCount.ToString();

            field.Draw(res, ref g, ref pctrBox);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchForm form2 = new SearchForm();
            form2.Show();
        }

        private void btnStart_Click(object sender, EventArgs e)
        { StartGame(); }

        private void BtnStop_Click(object sender, EventArgs e)
        { StopGame(); }

        private void ComboBoxTimer_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboBoxTimer.Text, out int interval) && interval > 0) // корректный интервал таймера
                MainTimer.Interval = interval;
            else
            {
                SetDefaultInterval();
            }
        }

        private void SetDefaultInterval()
        {
            MainTimer.Interval = defaultTimerInterval;
            comboBoxTimer.Text = MainTimer.Interval.ToString();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            widthWhenEndResize = Size.Width;
            heightWhenEndResize = Size.Height;
            if (MainTimer.Enabled || (widthWhenBeginResize == widthWhenEndResize && heightWhenBeginResize == heightWhenEndResize))
                return;

            pctrBox.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pctrBox.Image);

            ApplySettings();
            field.Draw(res, ref g, ref pctrBox);
        }

        private void pctrBox_MouseClick(object sender, MouseEventArgs e)
        {
            bool fieldChanged = false;
            int i = e.Location.X / res;
            int j = e.Location.Y / res;
            if (!ValidateMousePosition(i, j))
                return;

            switch (e.Button)
            {
                case MouseButtons.Left when !field.field[i, j]:
                    field.AddCell(i, j);
                    fieldChanged = true;
                    break;

                case MouseButtons.Right when field.field[i, j]:
                    field.RemoveCell(i, j);
                    fieldChanged = true;
                    break;
            }

            if (!MainTimer.Enabled && fieldChanged)
            {
                labelPopulationCount.Text = field.populationCount.ToString();
                field.Draw(res, ref g, ref pctrBox);
            }
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            widthWhenBeginResize = Size.Width;
            heightWhenBeginResize = Size.Height;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            field.NextGeneration();
            labelPopulationCount.Text = field.populationCount.ToString();
            genPerSecond++;
            labelGenCount.Text = field.genCount.ToString();

            field.Draw(res, ref g, ref pctrBox);
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