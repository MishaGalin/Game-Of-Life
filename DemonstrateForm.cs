using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class DemonstrateForm : Form
    {
        private Field field;
        private Graphics g;
        private const int res = 30;
        private readonly List<Field> favoriteFields;

        public DemonstrateForm(List<Field> favoriteFields)
        {
            InitializeComponent();
            numUpDown.Maximum = favoriteFields.Count;
            numUpDown.Minimum = 1;
            buttonStop.Enabled = false;
            this.favoriteFields = favoriteFields;

            pictureBox1.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pictureBox1.Image);
            field = new Field(pictureBox1.Width / res, pictureBox1.Height / res, favoriteFields[0].B, favoriteFields[0].S);
            field.Insert(favoriteFields[0], inCenter: true);
            field.Draw(res, ref g, ref pictureBox1);

            label.Text = $"Found {favoriteFields.Count} figures";
            label1.Text = $"{(float)(favoriteFields.Count / Math.Pow(2, favoriteFields[0].rows * favoriteFields[0].cols / 4))} %";
        }

        private void numUpDown_ValueChanged(object sender, EventArgs e)
        {
            StopTimer();
            field.Insert(favoriteFields[Decimal.ToInt32(numUpDown.Value) - 1], inCenter: true);
            field.Draw(res, ref g, ref pictureBox1);
        }

        private void DemonstrationForm_ResizeEnd(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                return;

            pictureBox1.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pictureBox1.Image);
            field = new Field(pictureBox1.Width / res, pictureBox1.Height / res, favoriteFields[0].B, favoriteFields[0].S);
            field.Insert(favoriteFields[Decimal.ToInt32(numUpDown.Value) - 1], inCenter: true);
            field.Draw(res, ref g, ref pictureBox1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            field.NextGeneration();
            field.Draw(res, ref g, ref pictureBox1);
        }

        private void btnStart_Click(object sender, EventArgs e)
        { StartTimer(); }

        private void buttonStop_Click(object sender, EventArgs e)
        { StopTimer(); }

        private void StartTimer()
        {
            timer1.Start();
            buttonStop.Enabled = true;
            buttonNext.Enabled = false;
            buttonStart.Enabled = false;

            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void StopTimer()
        {
            timer1.Stop();
            buttonStop.Enabled = false;
            buttonNext.Enabled = true;
            buttonStart.Enabled = true;

            FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            StopTimer();
            field.Insert(favoriteFields[Decimal.ToInt32(numUpDown.Value) - 1], inCenter: true);
            field.Draw(res, ref g, ref pictureBox1);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            field.NextGeneration();
            field.Draw(res, ref g, ref pictureBox1);
        }
    }
}