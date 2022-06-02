﻿using System;
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

        public CellularAutomaton field;
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

        private List<int> B, S;
        private int rank = 1;

        public MainForm()
        {
            InitializeComponent();
            SetDefaultInterval();
            pctrBox.MouseWheel += PctrBox_MouseWheel;

            pctrBox.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pctrBox.Image);

            ApplySettings();
            field.Draw(res, ref g, ref pctrBox);
        }

        private void PctrBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && res + 1 <= numUpDownRes.Maximum)
                res++;
            else if (e.Delta < 0 && res - 1 >= numUpDownRes.Minimum)
                res--;

            CellularAutomaton tempField = new CellularAutomaton(pctrBox.Width / res, pctrBox.Height / res, B, S, rank);
            tempField.Insert(field.Crop(), inCenter: true);
            field = tempField;
            numUpDownRes.Value = res;

            if (!MainTimer.Enabled)
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
            btnNext.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            textBoxB.Enabled = false;
            textBoxS.Enabled = false;
        }

        /// <summary>
        /// Остановка таймера.
        /// </summary>
        private void StopGame()
        {
            MainTimer.Stop();
            SpeedMeasurementTimer.Stop();

            btnApply.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            textBoxB.Enabled = true;
            textBoxS.Enabled = true;
            btnStop.Enabled = false;

            genPerSecond = 0;
            labelSpeedNum.Text = "0";
        }

        private void ApplySettings()
        {
            B = textBoxB.Text.Split(' ').Select(int.Parse).ToList();
            S = textBoxS.Text.Split(' ').Select(int.Parse).ToList();

            field = new CellularAutomaton(pctrBox.Width / res, pctrBox.Height / res, B, S, rank);
            labelGenCount.Text = "0";
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (MainTimer.Enabled)
                return;

            if (field.IsEmpty()) ApplySettings();
            else
            {
                CellularAutomaton tempfield = field.Clone();
                ApplySettings();
                field.CopyField(tempfield);
            }

            field.Draw(res, ref g, ref pctrBox);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            StopGame();
            field.Clear();
            labelPopulationCount.Text = field.PopulationCount.ToString();
            labelGenCount.Text = field.GenCount.ToString();

            field.Draw(res, ref g, ref pctrBox);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (MainTimer.Enabled)
                return;

            field.NextGeneration();
            labelPopulationCount.Text = field.PopulationCount.ToString();
            labelGenCount.Text = field.GenCount.ToString();

            field.Draw(res, ref g, ref pctrBox);
        }

        private void BtnRandom_Click(object sender, EventArgs e)
        {
            StopGame();
            field.RandomCreate(density);
            labelPopulationCount.Text = field.PopulationCount.ToString();
            labelGenCount.Text = field.GenCount.ToString();

            field.Draw(res, ref g, ref pctrBox);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchForm form2 = new SearchForm();
            form2.Show();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        { StartGame(); }

        private void BtnStop_Click(object sender, EventArgs e)
        { StopGame(); }

        private void ComboBoxTimer_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboBoxTimer.Text, out int interval) && interval > 0) // корректный интервал таймера
                MainTimer.Interval = interval;
            else SetDefaultInterval();
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
            if (widthWhenBeginResize == widthWhenEndResize && heightWhenBeginResize == heightWhenEndResize)
                return;

            pctrBox.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pctrBox.Image);

            if (field.IsEmpty())
                field = new CellularAutomaton(pctrBox.Width / res, pctrBox.Height / res, B, S, rank);
            else
            {
                CellularAutomaton tempField = field.Crop();
                field = new CellularAutomaton(pctrBox.Width / res, pctrBox.Height / res, B, S, rank);
                field.Insert(tempField, inCenter: true);
            }

            field.Draw(res, ref g, ref pctrBox);
        }

        private void PctrBox_MouseClick(object sender, MouseEventArgs e)
        {
            int i = e.Location.X / res;
            int j = e.Location.Y / res;
            if (!ValidateMousePos(i, j))
                return;

            bool fieldChanged = false;
            switch (e.Button)
            {
                case MouseButtons.Left when ModifierKeys == Keys.Control:
                    field.UpdateSelection(i, j);
                    fieldChanged = true;
                    break;

                case MouseButtons.Left when !field.field[i, j]:
                    field.AddCell(i, j);
                    fieldChanged = true;
                    break;

                case MouseButtons.Right when field.field[i, j]:
                    field.RemoveCell(i, j);
                    fieldChanged = true;
                    break;

                default:
                    break;
            }

            if (!MainTimer.Enabled && fieldChanged)
            {
                labelPopulationCount.Text = field.PopulationCount.ToString();
                field.Draw(res, ref g, ref pctrBox);
            }
        }

        private void numUpDownRes_ValueChanged(object sender, EventArgs e)
        {
            res = (int)numUpDownRes.Value;
            CellularAutomaton tempField = new CellularAutomaton(pctrBox.Width / res, pctrBox.Height / res, B, S, rank);
            tempField.Insert(field.Crop(), true);
            field = tempField;
            numUpDownRes.Value = res;

            if (!MainTimer.Enabled)
                field.Draw(res, ref g, ref pctrBox);
        }

        private void numUpDownDensity_ValueChanged(object sender, EventArgs e)
        {
            density = (int)numUpDownDensity.Value;
        }

        private void numericUpDownRank_ValueChanged(object sender, EventArgs e)
        {
            rank = (int)numericUpDownRank.Value;
            field.rank = rank;
        }

        private void pctrBox_MouseDown(object sender, MouseEventArgs e)
        {
            int i = e.Location.X / res;
            int j = e.Location.Y / res;
            if (!ValidateMousePos(i, j))
                return;

            switch (e.Button)
            {
                case MouseButtons.Left when ModifierKeys == Keys.Control:
                    field.StartSelection(i, j);
                    break;

                default:
                    break;
            }
        }

        private void pctrBox_MouseUp(object sender, MouseEventArgs e)
        {
            int i = e.Location.X / res;
            int j = e.Location.Y / res;
            if (!ValidateMousePos(i, j))
                return;

            bool fieldChanged = false;
            switch (e.Button)
            {
                case MouseButtons.Left when ModifierKeys == Keys.Control:
                    field.EndSelection(i, j);
                    fieldChanged = true;
                    break;

                case MouseButtons.Right when ModifierKeys == Keys.Control:
                    field.Paste(i, j);
                    fieldChanged = true;
                    break;

                default:
                    break;
            }

            if (!MainTimer.Enabled && fieldChanged)
                field.Draw(res, ref g, ref pctrBox);
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            widthWhenBeginResize = Size.Width;
            heightWhenBeginResize = Size.Height;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            field.NextGeneration();
            labelPopulationCount.Text = field.PopulationCount.ToString();
            labelGenCount.Text = field.GenCount.ToString();
            genPerSecond++;

            field.Draw(res, ref g, ref pctrBox);
        }

        private void SpeedMeasurementTimer_Tick(object sender, EventArgs e)
        {
            labelSpeedNum.Text = genPerSecond.ToString();
            genPerSecond = 0;
        }

        private bool ValidateMousePos(int x, int y)
        {
            return x >= 0 && y >= 0 && x < field.cols && y < field.rows;
        }
    }
}