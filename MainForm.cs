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
        public int density = 10, res = 8;

        public CellularAutomaton field;

        public Graphics g;

        /// <summary>
        /// Длина стороны квадрата, занимаемого одной клеткой, в пикселях.
        /// </summary>

        /// <summary>
        /// Стандартное значение интервала таймера в миллисекундах.
        /// </summary>
        private const int defaultTimerInterval = 25;

        private bool animationIsStarted = false;

        private List<int> B, S;

        private int mouseLastPosX = 0, mouseLastPosY = 0;

        private int rank = 1;

        private int tickPerSecond = 0;

        private int widthWhenBeginResize, heightWhenBeginResize, widthWhenEndResize, heightWhenEndResize;

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

        private static bool CloseEnough(double d1, double d2, double maxDifference = 0.001)
        {
            return Math.Abs(d1 - d2) < maxDifference;
        }

        private void ApplySettings()
        {
            B = textBoxB.Text.Split(' ').Select(int.Parse).ToList();
            S = textBoxS.Text.Split(' ').Select(int.Parse).ToList();

            field = new CellularAutomaton(pctrBox.Width / res, pctrBox.Height / res, B, S, rank);
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (field.IsEmpty())
            {
                ApplySettings();
            }
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
            labelPopulation.Text = $"Population: {field.PopulationCount}";
            labelGen.Text = $"Generation: {field.GenCount}";

            field.Draw(res, g, pctrBox);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (MainTimer.Enabled)
            {
                return;
            }

            field.NextGeneration();
            labelPopulation.Text = $"Population: {field.PopulationCount}";
            labelGen.Text = $"Generation: {field.GenCount}";

            field.Draw(res, g, pctrBox);
        }

        private void BtnRandom_Click(object sender, EventArgs e)
        {
            StopGame();
            field.RandomCreate(density);
            labelPopulation.Text = $"Population: {field.PopulationCount}";
            labelGen.Text = $"Generation: {field.GenCount}";

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
            {
                MainTimer.Interval = interval;
            }
            else
            {
                SetDefaultInterval();
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            widthWhenEndResize = Size.Width;
            heightWhenEndResize = Size.Height;

            // событие срабатывает при перемещении окна, поэтому если размеры окна не изменились, то новое поле не создается
            if (widthWhenBeginResize == widthWhenEndResize && heightWhenBeginResize == heightWhenEndResize)
            {
                return;
            }

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

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            widthWhenBeginResize = Size.Width;
            heightWhenBeginResize = Size.Height;
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            field.NextGeneration();
            labelPopulation.Text = $"Population: {field.PopulationCount}";
            labelGen.Text = $"Generation: {field.GenCount}";
            tickPerSecond++;

            field.Draw(res, g, pctrBox);
        }

        private void numericUpDownRank_ValueChanged(object sender, EventArgs e)
        {
            rank = (int)numericUpDownRank.Value;
            field.rank = rank;
        }

        private void numUpDownDensity_ValueChanged(object sender, EventArgs e)
        {
            density = (int)numUpDownDensity.Value;
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
            {
                field.Draw(res, g, pctrBox);
            }
        }

        private void PctrBox_MouseClick(object sender, MouseEventArgs e)
        {
            int x = (int)((e.Location.X - field.zoomMemoryOffsetX) / field.zoom / res);
            int y = (int)((e.Location.Y - field.zoomMemoryOffsetY) / field.zoom / res);
            if (!ValidateMousePos(x, y))
            {
                return;
            }

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
                    field.UpdateOffset(e.X, e.Y);
                    labelX.Text = $"X: {Math.Abs(field.zoomOffsetX)}";
                    labelY.Text = $"Y: {Math.Abs(field.zoomOffsetY)}";
                    fieldChanged = true;
                    break;

                default:
                    return;
            }

            if (!MainTimer.Enabled && fieldChanged)
            {
                labelPopulation.Text = $"Population: {field.PopulationCount}";
                field.Draw(res, g, pctrBox);
            }
        }

        private void pctrBox_MouseDown(object sender, MouseEventArgs e)
        {
            int x = (int)((e.Location.X - field.zoomMemoryOffsetX) / field.zoom / res);
            int y = (int)((e.Location.Y - field.zoomMemoryOffsetY) / field.zoom / res);
            if (!ValidateMousePos(x, y))
            {
                return;
            }

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
                    field.StartChangingOffset(e.X, e.Y);
                    break;

                default:
                    break;
            }

            if (!MainTimer.Enabled && fieldChanged)
            {
                field.Draw(res, g, pctrBox);
            }
        }

        private void pctrBox_MouseLeave(object sender, EventArgs e)
        {
            if (!field.SelectionIsStarted)
            {
                return;
            }

            field.EndSelection(mouseLastPosX, mouseLastPosY);

            if (!MainTimer.Enabled)
            {
                field.Draw(res, g, pctrBox);
            }
        }

        private void pctrBox_MouseUp(object sender, MouseEventArgs e)
        {
            int x = (int)((e.Location.X - field.zoomMemoryOffsetX) / field.zoom / res);
            int y = (int)((e.Location.Y - field.zoomMemoryOffsetY) / field.zoom / res);
            if (!ValidateMousePos(x, y))
            {
                return;
            }

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
                    field.EndChangingOffset();
                    break;

                default:
                    break;
            }

            if (!MainTimer.Enabled && fieldChanged)
            {
                labelPopulation.Text = $"Population: {field.PopulationCount}";
                field.Draw(res, g, pctrBox);
            }
        }

        private async void PctrBox_MouseWheel(object sender, MouseEventArgs e)
        {
            await Zoom(e);
        }

        private void SetDefaultInterval()
        {
            MainTimer.Interval = defaultTimerInterval;
            comboBoxTimer.Text = MainTimer.Interval.ToString();
        }

        private void SpeedMeasurementTimer_Tick(object sender, EventArgs e)
        {
            labelSpeed.Text = $"Speed (gen/s): {tickPerSecond}";
            tickPerSecond = 0;
        }

        /// <summary>
        /// Запуск таймера.
        /// </summary>
        private void StartGame()
        {
            MainTimer.Start();

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

            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private bool ValidateMousePos(int x, int y)
        {
            return x >= 0 && y >= 0 && x < field.cols && y < field.rows;
        }

        private async Task Zoom(MouseEventArgs e, int minZoom = 1, int maxZoom = 4)
        {
            if (animationIsStarted || (e.Delta < 0 && CloseEnough(field.zoom, minZoom))
                                   || (e.Delta > 0 && CloseEnough(field.zoom, maxZoom)))
            {
                return;
            }

            double goalZoom = field.zoom + (e.Delta > 0 ? 1 : -1);
            const int framesForAnimation = 10;
            animationIsStarted = true;

            if (CloseEnough(1, goalZoom))
            {
                field.zoomOffsetX = 0;
                field.zoomOffsetY = 0;
                field.zoomMemoryOffsetX = 0;
                field.zoomMemoryOffsetY = 0;
                labelX.Text = "X: 0";
                labelY.Text = "Y: 0";
            }

            while (!CloseEnough(field.zoom, goalZoom))
            {
                field.zoom += e.Delta > 0 ? 1.0 / framesForAnimation : -1.0 / framesForAnimation;

                labelZoom.Text = $"Zoom: {field.zoom}x";
                if (!MainTimer.Enabled)
                {
                    field.Draw(res, g, pctrBox);
                }
                await Task.Delay(20);
            }

            animationIsStarted = false;
        }
    }
}