using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
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

        private float tickPerSecond = 0;

        private int widthWhenBeginResize, heightWhenBeginResize, widthWhenEndResize, heightWhenEndResize;

        private List<int> B, S;
        private int rank = 1;
        private int mouseLastPosX = 0;
        private int mouseLastPosY = 0;
        private bool animationIsStarted = false;

        public MainForm()
        {
            InitializeComponent();
            SetDefaultInterval();
            pctrBox.MouseWheel += PctrBox_MouseWheel;

            pctrBox.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pctrBox.Image);

            ApplySettings();
            field.Draw(res, g, pctrBox);
        }

        private async void PctrBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (animationIsStarted
                || (e.Delta < 0 && Math.Abs(field.zoom - 1) < 0.01)
                || (e.Delta > 0 && Math.Abs(field.zoom - 4) < 0.01))
                return;

            //field.zoomOffsetX = e.X * (e.Delta < 0 ? 1 : -1);
            //field.zoomOffsetY = e.Y * (e.Delta < 0 ? 1 : -1);

            double goalZoom = field.zoom + (e.Delta > 0 ? 1 : -1);
            animationIsStarted = true;
            if (Math.Abs(1 - goalZoom) < 0.01)
            {
                field.zoomOffsetX = 0;
                field.zoomOffsetY = 0;
                field.zoomMemoryOffsetX = 0;
                field.zoomMemoryOffsetY = 0;
            }

            while (Math.Abs(field.zoom - goalZoom) > 0.01)
            {
                field.zoom += e.Delta > 0 ? 0.1 : -0.1;

                labelZoom.Text = $"Zoom: {field.zoom}x";
                field.Draw(res, g, pctrBox);
                await Task.Delay(1);
            }

            animationIsStarted = false;
        }

        /// <summary>
        /// Запуск таймера.
        /// </summary>
        private void StartGame()
        {
            MainTimer.Start();
            SpeedMeasurementTimer.Start();

            btnNext.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        /// <summary>
        /// Остановка таймера.
        /// </summary>
        private void StopGame()
        {
            MainTimer.Stop();
            SpeedMeasurementTimer.Stop();

            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            tickPerSecond = 0;
            labelSpeedNum.Text = "0";
        }

        private void ApplySettings()
        {
            B = textBoxB.Text.Split(' ').Select(int.Parse).ToList();
            S = textBoxS.Text.Split(' ').Select(int.Parse).ToList();

            field = new CellularAutomaton(pctrBox.Width / res, pctrBox.Height / res, B, S, rank);
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (field.IsEmpty()) ApplySettings();
            else
            {
                CellularAutomaton tempfield = field.Clone();
                ApplySettings();
                field.Insert(tempfield);
            }

            field.Draw(res, g, pctrBox);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            StopGame();
            field.Clear();
            labelPopulationCount.Text = field.PopulationCount.ToString();
            labelGenCount.Text = field.GenCount.ToString();

            field.Draw(res, g, pctrBox);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (MainTimer.Enabled)
                return;

            field.NextGeneration();
            labelPopulationCount.Text = field.PopulationCount.ToString();
            labelGenCount.Text = field.GenCount.ToString();

            field.Draw(res, g, pctrBox);
        }

        private void BtnRandom_Click(object sender, EventArgs e)
        {
            StopGame();
            field.RandomCreate(density);
            labelPopulationCount.Text = field.PopulationCount.ToString();
            labelGenCount.Text = field.GenCount.ToString();

            field.Draw(res, g, pctrBox);
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

            // событие срабатывает при перемещении окна, поэтому если размеры окна не изменились, то ничего не делаем
            if (widthWhenBeginResize == widthWhenEndResize && heightWhenBeginResize == heightWhenEndResize)
                return;

            pctrBox.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pctrBox.Image);

            if (field.IsEmpty())
            {
                field = new CellularAutomaton(pctrBox.Width / res, pctrBox.Height / res, B, S, rank);
            }
            else
            {
                CellularAutomaton tempField = field.Crop();
                field = new CellularAutomaton(pctrBox.Width / res, pctrBox.Height / res, B, S, rank);
                field.Insert(tempField, inCenter: true);
            }

            field.Draw(res, g, pctrBox);
        }

        private void PctrBox_MouseClick(object sender, MouseEventArgs e)
        {
            int x = (int)((e.Location.X - field.zoomMemoryOffsetX) / field.zoom / res);
            int y = (int)((e.Location.Y - field.zoomMemoryOffsetY) / field.zoom / res);
            if (!ValidateMousePos(x, y))
                return;
            mouseLastPosX = x;
            mouseLastPosY = y;

            bool fieldChanged;
            switch (e.Button)
            {
                case MouseButtons.Left when ModifierKeys == Keys.Control:
                    field.UpdateSelection(mouseLastPosX, mouseLastPosY);
                    fieldChanged = true;
                    break;

                case MouseButtons.Left:
                    fieldChanged = field.AddCell(mouseLastPosX, mouseLastPosY);
                    break;

                case MouseButtons.Right:
                    fieldChanged = field.RemoveCell(mouseLastPosX, mouseLastPosY);
                    break;

                case MouseButtons.Middle when field.zoom > 1:
                    field.zoomEndOffsetX = e.Location.X;
                    field.zoomEndOffsetY = e.Location.Y;
                    labelX.Text = $"X: {Math.Abs(field.zoomOffsetX)}";
                    labelY.Text = $"Y: {Math.Abs(field.zoomOffsetY)}";
                    fieldChanged = true;
                    break;

                default:
                    return;
            }

            if (!MainTimer.Enabled && fieldChanged)
                field.Draw(res, g, pctrBox);
        }

        private void numUpDownRes_ValueChanged(object sender, EventArgs e)
        {
            field.zoom = 1;
            labelZoom.Text = $"Zoom {field.zoom}x";
            res = (int)numUpDownRes.Value;
            CellularAutomaton tempField = new CellularAutomaton(pctrBox.Width / res, pctrBox.Height / res, B, S, rank);
            tempField.Insert(field.Crop(), true);
            field = tempField;

            if (!MainTimer.Enabled)
                field.Draw(res, g, pctrBox);
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
            int x = (int)(e.Location.X / res / field.zoom);
            int y = (int)(e.Location.Y / res / field.zoom);
            if (!ValidateMousePos(x, y))
                return;

            bool fieldChanged = false;
            mouseLastPosX = x;
            mouseLastPosY = y;
            switch (e.Button)
            {
                case MouseButtons.Left when ModifierKeys == Keys.Control:
                    field.StartSelection(mouseLastPosX, mouseLastPosY);
                    fieldChanged = true;
                    break;

                case MouseButtons.Middle when field.zoom > 1:
                    field.zoomStartOffsetX = e.X;
                    field.zoomStartOffsetY = e.Y;
                    field.zoomEndOffsetX = e.X;
                    field.zoomEndOffsetY = e.Y;
                    break;

                default:
                    break;
            }

            if (!MainTimer.Enabled && fieldChanged)
                field.Draw(res, g, pctrBox);
        }

        private void pctrBox_MouseUp(object sender, MouseEventArgs e)
        {
            int x = (int)(e.Location.X / res / field.zoom);
            int y = (int)(e.Location.Y / res / field.zoom);
            if (!ValidateMousePos(x, y))
                return;

            bool fieldChanged = false;
            mouseLastPosX = x;
            mouseLastPosY = y;
            switch (e.Button)
            {
                case MouseButtons.Left when ModifierKeys == Keys.Control:
                    field.EndSelection(mouseLastPosX, mouseLastPosY);
                    fieldChanged = true;
                    break;

                case MouseButtons.Right when ModifierKeys == Keys.Control:
                    field.Paste(mouseLastPosX, mouseLastPosY);
                    fieldChanged = true;
                    break;

                case MouseButtons.Middle when field.zoom > 1:
                    field.zoomMemoryOffsetX = field.zoomOffsetX;
                    field.zoomMemoryOffsetY = field.zoomOffsetY;
                    field.zoomStartOffsetX = 0;
                    field.zoomStartOffsetY = 0;
                    field.zoomEndOffsetX = 0;
                    field.zoomEndOffsetY = 0;
                    break;

                default:
                    break;
            }

            if (!MainTimer.Enabled && fieldChanged)
            {
                labelPopulationCount.Text = field.PopulationCount.ToString();
                field.Draw(res, g, pctrBox);
            }
        }

        private void pctrBox_MouseLeave(object sender, EventArgs e)
        {
            if (!field.SelectionIsStarted)
                return;

            field.EndSelection(mouseLastPosX, mouseLastPosY);

            if (!MainTimer.Enabled)
                field.Draw(res, g, pctrBox);
        }

        private void pctrBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            _ = pctrBox.Focus();

            switch (e.KeyCode)
            {
                case Keys.Left:
                    field.zoomMemoryOffsetX += 10;
                    break;

                default:
                    break;
            }
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            widthWhenBeginResize = Size.Width;
            heightWhenBeginResize = Size.Height;
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            field.NextGeneration();
            labelPopulationCount.Text = field.PopulationCount.ToString();
            labelGenCount.Text = field.GenCount.ToString();
            tickPerSecond++;

            field.Draw(res, g, pctrBox);
        }

        private void SpeedMeasurementTimer_Tick(object sender, EventArgs e)
        {
            labelSpeedNum.Text = tickPerSecond.ToString();
            tickPerSecond = 0;
        }

        private bool ValidateMousePos(int x, int y)
        {
            return x >= 0 && y >= 0 && x < field.cols && y < field.rows;
        }
    }
}