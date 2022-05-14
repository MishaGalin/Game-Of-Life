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
        private readonly int LimitOnSearchSteps = 50;

        public SearchForm()
        {
            InitializeComponent();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            int width = (int)numUpDownWidth.Value;
            int height = (int)numUpDownHeight.Value;
            int[] b = textBoxB.Text.Split(' ').Select(int.Parse).ToArray();
            int[] s = textBoxS.Text.Split(' ').Select(int.Parse).ToArray();
            await Search(width, height, b, s);
        }

        private async Task Search(int width, int height, int[] b, int[] s)
        {
            timer1.Start();
            btnSearch.Enabled = false;

            var favoriteFields = new List<Field>();
            await Task.Run(() =>
            {
                List<int> checkedFields = new List<int>();
                ulong numOfCombinations = (ulong)Math.Pow(2, width * height);

                for (ulong i = 0; i < numOfCombinations; i++) // основной цикл с перебором всех вариантов начальных условий
                {
                    var binaryString = Convert.ToString((int)i, 2).PadRight(width * height, '0'); // перевод счетчика основного цикла в двоичное число с дополнием справа нулями
                    var field = new Field(width * 2, height * 2, b, s);
                    var listOfFields = new List<Field>();

                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            bool currentCell = Math.Abs(char.GetNumericValue(binaryString[y + (x * height)]) - 1) < 0.01; // берем из строки один элемент
                            field.field[x + width / 2, y + height / 2] = currentCell;
                        }
                    }

                    int sumOfNeighbours = field.GetSumOfAllNeighbours();
                    if (checkedFields.Contains(sumOfNeighbours)) continue;
                    else checkedFields.Add(sumOfNeighbours); // запоминание уже проверенных начальных условий

                    for (int j = 0; j < LimitOnSearchSteps; j++) // поиск состояния, совпадающего хотя бы с одним из предыдущих
                    {
                        bool isFound = false;
                        listOfFields.Add(field.Clone());
                        field = field.NextGeneration();

                        foreach (Field fieldFromList in listOfFields)
                        {
                            if (fieldFromList == field && !field.IsEmpty())  // сравнение с предыдущими состояниями для выявления интересующих нас фигур
                            {
                                favoriteFields.Add(fieldFromList.Clone());
                                checkedFields.Add(fieldFromList.GetSumOfAllNeighbours());
                                isFound = true;
                                break;
                            }
                        }

                        if (isFound || field.IsEmpty())
                            break;
                    }

                    Invoke((Action)(() =>
                    {
                        progressBar1.Value = (int)((double)i / (numOfCombinations - 1) * 100); // прогресс в процентах
                    }));
                }
            });

            btnSearch.Enabled = true;
            timer1.Stop();

            if (favoriteFields.Count > 0)
            {
                DemonstrationForm form3 = new DemonstrationForm(favoriteFields);
                form3.Show();
            }
            else MessageBox.Show("Nothing found.");
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                time += ((double)timer1.Interval / 1000);
                Invoke((Action)(() =>
                {
                    label5.Text = $"{time.ToString("F", CultureInfo.CreateSpecificCulture("en-CA"))} s";
                })); ;
            });
        }
    }
}