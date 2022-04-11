using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class DemonstrationForm : Form
    {
        private Field field;
        private Graphics g;
        private int res = 50;
        private Field[] favoriteFields;

        public DemonstrationForm(List<Field> favoriteFields)
        {
            InitializeComponent();
            numUpDown.Maximum = favoriteFields.Count - 1;
            this.favoriteFields = favoriteFields.ToArray();

            pictureBox1.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pictureBox1.Image);
            field = new Field(pictureBox1.Width / res, pictureBox1.Height / res);

            field = this.favoriteFields[0];

            /*for (int i = 0; i < Math.Min(field.cols, favoriteFields[0].cols); i++)
            {
                for (int j = 0; j < Math.Min(field.rows, favoriteFields[0].rows); j++)
                {
                    field.field[i, j] = favoriteFields[0].field[i, j];
                }
            }*/

            field.Draw(res, ref g, ref pictureBox1);

            label.Text = $"Found {favoriteFields.Count} figures";
        }

        private void numUpDown_ValueChanged(object sender, EventArgs e)
        {
            field = favoriteFields[((int)numUpDown.Value)];

            /*for (int i = 0; i < Math.Min(field.cols, favoriteFields[((int)numUpDown.Value)].cols); i++)
            {
                for (int j = 0; j < Math.Min(field.rows, favoriteFields[((int)numUpDown.Value)].rows); j++)
                {
                    field.field[i, j] = favoriteFields[((int)numUpDown.Value)].field[i, j];
                }
            }*/

            field.Draw(res, ref g, ref pictureBox1);
        }
    }
}