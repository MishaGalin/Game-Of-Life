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
            int rank = (int)numericUpDownRank.Value;
            await Search(width, height, B, S, rank);
        }

        private async Task Search(int width, int height, List<int> b, List<int> s, int rank = 1)
        {
            timer1.Start();
            List<CellularAutomaton> favoriteFields = new List<CellularAutomaton>();
            int staticFieldCount = 0, periodicallyFieldCount = 0;

            await Task.Run(() =>
            {
                const int LimitOnSearchSteps = 30;
                CellularAutomaton field = new CellularAutomaton(30, 30, b, s, rank);
                List<CellularAutomaton> listOfFields = new List<CellularAutomaton>();
                List<int> checkedFields = new List<int>();
                ulong numOfCombinations = (ulong)Math.Pow(2, width * height);

                for (ulong i = 1; i <= numOfCombinations; i++) // основной цикл с перебором всех вариантов начальных условий
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
                            field.field[x + (field.cols / 2), y + (field.rows / 2)] = currentCell;
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

                        foreach (CellularAutomaton fieldFromList in listOfFields)
                        {
                            if (fieldFromList == field && !field.IsEmpty())  // сравнение с предыдущими состояниями для выявления фигур
                            {
                                CellularAutomaton prevGenField = field.Clone();
                                field.NextGeneration();
                                if (field.Equals(prevGenField))
                                {
                                    prevGenField.type = "Static";
                                    staticFieldCount++;
                                }
                                else
                                {
                                    prevGenField.type = "Periodically or other";
                                    periodicallyFieldCount++;
                                }

                                favoriteFields.Add(prevGenField.Crop());
                                checkedFields.Add(prevGenField.GetSumOfAllNeighbours());
                                isFound = true;
                                break;
                            }
                        }

                        if (isFound || field.IsEmpty())
                            break;
                    }

                    _ = Invoke((Action)(() =>
                      {
                          float progress = (float)(i + 1) / numOfCombinations * 100;
                          progressBar1.Value = (int)progress;
                      }));
                }
            });

            btnSearch.Enabled = true;
            timer1.Stop();

            if (favoriteFields.Count > 0)
            {
                DemonstrateForm form3 = new DemonstrateForm(favoriteFields, b, s, rank);
                form3.Show();
                _ = MessageBox.Show($"{width} by {height} search: \n" +
                    $"Total fields: {favoriteFields.Count}\n" +
                    $"Static fields: {staticFieldCount}\n" +
                    $"Periodically or other fields: {periodicallyFieldCount}\n" +
                    $"Static fields percent: {(float)staticFieldCount / favoriteFields.Count * 100} %\n" +
                    $"Periodically or other fields percent: {(float)periodicallyFieldCount / favoriteFields.Count * 100} %\n");
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