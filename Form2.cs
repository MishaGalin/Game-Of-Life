using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class SearchForm : Form
    {
        private double time = 0.0;

        public SearchForm()
        {
            InitializeComponent();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            time = 0.0;
            timer1.Start();

            List<Field> favoriteFields = new List<Field>();
            Dictionary<int, bool> checkedFields = new Dictionary<int, bool>();

            Invoke((Action)(() => { btnSearch.Enabled = false; }));
            await Task.Run(() =>
            {
                int width = (int)numUpDownWidth.Value;
                int height = (int)numUpDownHeight.Value;
                int[] b = textBoxB.Text.Split(' ').Select(int.Parse).ToArray();
                int[] s = textBoxS.Text.Split(' ').Select(int.Parse).ToArray();
                Field field;

                string binaryString;

                List<Field> listOfFields;

                ulong numOfCombinations = (ulong)Math.Pow(2, width * height);

                for (ulong i = 0; i < numOfCombinations; i++) // основной цикл с перебором всех вариантов начальных условий
                {
                    binaryString = Convert.ToString((int)i, 2).PadLeft(width * height, '0'); // перевод счетчика основного цикла в двоичное число с дополнием слева нулями
                    field = new Field(width, height, b, s);
                    listOfFields = new List<Field>(0);
                    bool AlreadyChecked = false;

                    //if (binaryString == binaryString.Reverse().ToString() || favoriteFields.Contains(field.Clone()))
                    //   continue;

                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            field.field[x, y] = Math.Abs(char.GetNumericValue(binaryString[y + (x * height)]) - 1) < 0.1; // учитываем отклонение числа типа double
                        }
                    }

                    checkedFields.TryGetValue(field.GetSumOfAllNeighbours(), out AlreadyChecked);
                    if (AlreadyChecked)
                        continue;
                    else
                        checkedFields.Add(field.GetSumOfAllNeighbours(), true);

                    for (int j = 0; j < 30; j++) // поиск состояния, совпадающего хотя бы с одним из предыдущих
                    {
                        bool isFound = false;
                        listOfFields.Add(field.Clone());
                        field.NextGeneration();

                        foreach (var fieldFromList in listOfFields)
                        {
                            if (fieldFromList == field && !fieldFromList.IsEmpty())  // сравнение с предыдущими состояниями для выявления интересующих нас фигур
                            {
                                favoriteFields.Add(fieldFromList);
                                isFound = true;
                                break;
                            }
                        }
                        if (isFound || field.IsEmpty())
                            break;
                    }
                    Invoke((Action)(() =>
                    {
                        progressBar1.Value = (int)((double)i / (numOfCombinations - 1) * 100);
                    }));
                }
                timer1.Stop();
            });

            Invoke((Action)(() => { btnSearch.Enabled = true; }));

            if (favoriteFields.Count > 0)
            {
                var form3 = new DemonstrationForm(favoriteFields);
                form3.Show();
            }
            else
                MessageBox.Show("Nothing found");
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                time += (double)timer1.Interval / 1000;
                Invoke((Action)(() =>
                {
                    label5.Text = $"{time.ToString("F", CultureInfo.CreateSpecificCulture("en-CA"))} s";
                })); ;
            });
        }
    }
}