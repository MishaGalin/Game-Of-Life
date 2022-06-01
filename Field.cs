using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public class CellularAutomaton
    {
        /// <summary>
        /// Правило рождения новой клетки.
        /// </summary>
        public List<int> B;

        public bool[,] field;

        /// <summary>
        /// Номер поколения.
        /// </summary>
        public int genCount = 0;

        /// <summary>
        /// Количество живых клеток.
        /// </summary>
        public int populationCount = 0;

        public int rows, cols;

        /// <summary>
        /// Правило выживания клеток.
        /// </summary>
        public List<int> S;

        public string type = "";

        /// <summary>
        /// Цвет фона.
        /// </summary>
        private readonly Color backgroundColor = Color.Black;

        /// <summary>
        /// Цвет клеток.
        /// </summary>
        private readonly Brush cellColor = Brushes.White;

        /// <summary>
        /// Цвет сетки.
        /// </summary>
        private readonly Pen gridColor = Pens.DarkSlateGray;

        private readonly bool[,] nextGenField;

        private readonly Random rnd = new Random();

        /// <summary>
        /// Ранг клеточного автомата, т.е. радиус окрестности клетки при подсчете соседей
        /// </summary>
        private int rank;

        public CellularAutomaton(int cols, int rows, List<int> B, List<int> S, int rank = 1)
        {
            this.cols = cols;
            this.rows = rows;
            this.B = B;
            this.S = S;
            this.rank = rank;

            field = new bool[cols, rows];
            nextGenField = new bool[cols, rows];
        }

        public static bool operator !=(CellularAutomaton f1, CellularAutomaton f2)
        {
            return !(f1 == f2);
        }

        public static bool operator ==(CellularAutomaton f1, CellularAutomaton f2)
        {
            return f1.GetSumOfAllNeighbours() == f2.GetSumOfAllNeighbours();
        }

        public void AddCell(int x, int y)
        {
            if (field[x, y])
                return;

            populationCount++;
            field[x, y] = true;
        }

        public void Clear()
        {
            genCount = 0;
            populationCount = 0;

            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    field[i, j] = false;
        }

        public CellularAutomaton Clone()
        {
            CellularAutomaton tempField = new CellularAutomaton(cols, rows, B, S)
            {
                type = type,
                rank = rank
            };

            tempField.Insert(this);
            return tempField;
        }

        /// <summary>
        /// Возвращает новое поле, оставляя от исходного только наименьшую часть с клетками.
        /// </summary>
        public CellularAutomaton Crop()
        {
            int leftmostX = cols, rightmostX = 0, topmostY = rows, lowestY = 0;

            // поиск граничных точек
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    if (field[i, j])
                    {
                        if (leftmostX > i) leftmostX = i;
                        if (rightmostX < i) rightmostX = i;

                        if (topmostY > j) topmostY = j;
                        if (lowestY < j) lowestY = j;
                    }

            CellularAutomaton tempField = new CellularAutomaton(rightmostX - leftmostX + 1, lowestY - topmostY + 1, B, S)
            {
                type = type,
                rank = rank
            };

            for (int i = 0; i < tempField.cols; i++)
                for (int j = 0; j < tempField.rows; j++)
                    tempField.field[i, j] = field[leftmostX + i, topmostY + j];

            return tempField;
        }

        public void Draw(int res, ref Graphics g, ref PictureBox pictureBox)
        {
            g.Clear(backgroundColor);

            // Клетки
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    if (field[i, j]) g.FillRectangle(cellColor, i * res, j * res, res, res);

            // Сетка
            for (int i = 0; i <= cols; i++)
                g.DrawLine(gridColor, i * res, 0, i * res, rows * res);

            for (int i = 0; i <= rows; i++)
                g.DrawLine(gridColor, 0, i * res, cols * res, i * res);

            pictureBox.Invalidate();
        }

        public bool Equals(CellularAutomaton anotherField)
        {
            if (rows != anotherField.rows || cols != anotherField.cols)
                return false;

            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    if (field[i, j] != anotherField.field[i, j]) return false;

            return true;
        }

        /// <summary>
        /// Возвращает сумму соседей каждой живой клетки.
        /// </summary>
        public int GetSumOfAllNeighbours()
        {
            int sum = 0;
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    if (field[i, j]) sum += CountNeighbours(i, j);

            return sum;
        }

        /// <summary>
        /// Вставляет меньшее поле в большее с сохранением размеров большего. Если поля равного размера, переносит состояние второго поля в первое.
        /// </summary>
        public void Insert(CellularAutomaton field, bool inCenter = false, int offsetX = 0, int offsetY = 0)
        {
            Clear();

            if (inCenter)
            {
                offsetX = (cols / 2) - (field.cols / 2) + 1;
                offsetY = (rows / 2) - (field.rows / 2) + 1;
            }

            for (int i = 0; i < Math.Min(field.cols, cols + offsetX); i++)
                for (int j = 0; j < Math.Min(field.rows, rows + offsetY); j++)
                    this.field[i + offsetX, j + offsetY] = field.field[i, j];
        }

        public bool IsEmpty()
        {
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    if (field[i, j]) return false;

            return true;
        }

        /// <summary>
        /// Генерация следующего поколения.
        /// </summary>
        public void NextGeneration()
        {
            genCount++;
            populationCount = 0;
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    int neighboursCount = CountNeighbours(i, j);
                    nextGenField[i, j] = B.Contains(neighboursCount) || (S.Contains(neighboursCount) && field[i, j]);
                }
            }

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = nextGenField[i, j];
                    populationCount += Convert.ToInt32(field[i, j]);
                    nextGenField[i, j] = false;
                }
            }
        }

        public void RandomCreate(int density)
        {
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                {
                    if (rnd.Next(density) == 0) AddCell(i, j);
                    else RemoveCell(i, j);
                }
        }

        public void RemoveCell(int x, int y)
        {
            if (!field[x, y])
                return;

            populationCount--;
            field[x, y] = false;
        }

        /// <summary>
        /// Количество соседей клетки.
        /// </summary>
        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -rank; i <= rank; i++)
                for (int j = -rank; j <= rank; j++)
                {
                    int col = (x + i + cols) % cols;
                    int row = (y + j + rows) % rows;
                    bool isSelfChecking = col == x && row == y;
                    if (field[col, row] && !isSelfChecking)
                        count++;
                }

            return count;
        }
    }
}