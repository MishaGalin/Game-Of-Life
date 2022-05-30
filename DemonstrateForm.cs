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
        private const int res = 20;
        private readonly List<Field> favoriteFields;
        private int currentField;

        public DemonstrateForm(List<Field> favoriteFields)
        {
            InitializeComponent();
            numUpDown.Maximum = favoriteFields.Count;
            numUpDown.Minimum = 1;
            buttonStop.Enabled = false;
            this.favoriteFields = favoriteFields;
            currentField = 0;

            pictureBox1.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pictureBox1.Image);
            field = new Field(pictureBox1.Width / res, pictureBox1.Height / res, favoriteFields[0].B, favoriteFields[0].S);
            field.Insert(favoriteFields[0], inCenter: true);
            field.Draw(res, ref g, ref pictureBox1);

            label.Text = $"Found {favoriteFields.Count} figures";
            labelType.Text = "Type: " + favoriteFields[0].type;
        }

        private void numUpDown_ValueChanged(object sender, EventArgs e)
        {
            StopTimer();
            currentField = decimal.ToInt32(numUpDown.Value) - 1;
            field.Insert(favoriteFields[currentField], inCenter: true);
            field.Draw(res, ref g, ref pictureBox1);

            labelType.Text = "Type: " + favoriteFields[currentField].type;
        }

        private void DemonstrationForm_ResizeEnd(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pictureBox1.Image);
            field = new Field(pictureBox1.Width / res, pictureBox1.Height / res, favoriteFields[0].B, favoriteFields[0].S);
            field.Insert(favoriteFields[currentField], inCenter: true);
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
        }

        private void StopTimer()
        {
            timer1.Stop();
            buttonStop.Enabled = false;
            buttonNext.Enabled = true;
            buttonStart.Enabled = true;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            StopTimer();
            field.Insert(favoriteFields[currentField], inCenter: true);
            field.Draw(res, ref g, ref pictureBox1);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            field.NextGeneration();
            field.Draw(res, ref g, ref pictureBox1);
        }
    }
}