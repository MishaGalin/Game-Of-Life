using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class SearchForm : Form
    {
        private decimal time = 0.0M;
        private const int LimitOnSearchSteps = 5;

        public SearchForm()
        {
            InitializeComponent();
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            time = 0.0M;
            btnSearch.Enabled = false;
            int height = (int)numUpDownHeight.Value;
            int width = (int)numUpDownWidth.Value;
            List<int> B = textBoxB.Text.Split(' ').Select(int.Parse).ToList();
            List<int> S = textBoxS.Text.Split(' ').Select(int.Parse).ToList();
            await Search(width, height, B, S);
        }

        private async Task Search(int width, int height, List<int> b, List<int> s)
        {
            timer1.Start();
            List<Field> favoriteFields = new List<Field>();

            await Task.Run(() =>
            {
                Field field = new Field(width * 2, height * 2, b, s);
                List<Field> listOfFields = new List<Field>();
                List<int> checkedFields = new List<int>();
                ulong numOfCombinations = (ulong)Math.Pow(2, width * height);

                for (ulong i = 1; i < numOfCombinations; i++) // основной цикл с перебором всех вариантов начальных условий
                {
                    field.Clear();
                    listOfFields.Clear();

                    // перевод счетчика основного цикла в двоичное число с дополнием справа нулями
                    string binaryString = Convert.ToString((int)i, 2).PadRight(width * height, '0');

                    // заполнение поля
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            bool currentCell = Math.Abs(char.GetNumericValue(binaryString[y + (x * height)]) - 1.0) < 0.001; // берем из строки один элемент
                            field.field[x + (width / 2), y + (height / 2)] = currentCell;
                        }
                    }

                    int sumOfNeighbours = field.GetSumOfAllNeighbours();
                    if (checkedFields.Contains(sumOfNeighbours)) continue;
                    else checkedFields.Add(sumOfNeighbours); // запоминание уже проверенных начальных условий

                    for (int j = 0; j < LimitOnSearchSteps; j++) // поиск состояния, совпадающего хотя бы с одним из предыдущих
                    {
                        bool isFound = false;
                        listOfFields.Add(field.Clone());
                        field.NextGeneration();

                        foreach (Field fieldFromList in listOfFields)
                        {
                            if (fieldFromList == field && !field.IsEmpty())  // сравнение с предыдущими состояниями для выявления фигур
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

                    _ = Invoke((Action)(() =>
                    {
                        double progress = (double)(i + 1) / numOfCombinations * 100;
                        progressBar1.Value = (int)progress;
                    }));
                }
            });

            btnSearch.Enabled = true;
            timer1.Stop();

            if (favoriteFields.Count > 0)
            {
                DemonstrateForm form3 = new DemonstrateForm(favoriteFields);
                form3.Show();
            }
            else _ = MessageBox.Show("Nothing found.");
        }

        private async void Timer1_Tick(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                time += (decimal)timer1.Interval / 1000;
                _ = Invoke((Action)(() =>
               {
                   label5.Text = $"{time} s";
               }));
            });
        }
    }
}