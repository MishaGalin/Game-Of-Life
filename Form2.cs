using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Game_Of_Life.Form1;

namespace Game_Of_Life
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                btnSearch.Enabled = false;
                int width = (int)numUpDownWidth.Value;
                int height = (int)numUpDownHeight.Value;
                int[] b = textBoxB.Text.Split(' ').Select(int.Parse).ToArray();
                int[] s = textBoxS.Text.Split(' ').Select(int.Parse).ToArray();

                ulong numOfCombinations = (ulong)Math.Pow(2, width * height);

                for (ulong i = 0; i < numOfCombinations; i++)
                {
                    //Task.Delay(10);
                    //label4.Text = Convert.ToString((int)i, 2).PadLeft(width * height, '0');

                    progressBar1.Value = (int)((double)i / numOfCombinations * 100);
                }
                progressBar1.Value = 100;
                btnSearch.Enabled = true;
            });
        }
    }
}