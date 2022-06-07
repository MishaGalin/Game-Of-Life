using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public class CellularAutomaton
    {
        /// <summary>
        /// Правило рождения новой клетки.
        /// </summary>
        public List<int> B;

        /// <summary>
        /// Массив клеток.
        /// </summary>
        public bool[,] field;

        /// <summary>
        /// Сохраненный фрагмент поля, который можно вставить в поле.
        /// </summary>
        public CellularAutomaton fieldInClipboard;

        /// <summary>
        /// Ранг клеточного автомата, т.е. радиус окрестности клетки при подсчете соседей.
        /// </summary>
        public int rank = 1;

        public int rows = 0, cols = 0;

        /// <summary>
        /// Правило выживания клеток.
        /// </summary>
        public List<int> S;

        /// <summary>
        /// Тип клеточной структуры (статичная или периодическая). Используется для вывода статистики после поиска.
        /// </summary>
        public string type = "";

        /// <summary>
        /// Цвет фона.
        /// </summary>
        private static readonly Color backgroundColor = Color.Black;

        /// <summary>
        /// Цвет клеток.
        /// </summary>
        private static readonly Brush cellColor = Brushes.White;

        /// <summary>
        /// Цвет сетки.
        /// </summary>
        private static readonly Pen gridColor = Pens.DarkSlateGray;

        private static readonly Random rnd = new Random();
        private static readonly Brush selectionColor = Brushes.Yellow;

        /// <summary>
        /// Промежуточный массив для генерации следующего поколения.
        /// </summary>
        private readonly bool[,] nextGenField;

        /// <summary>
        /// Точки для отрисовки прямоугльника выделения.
        /// </summary>
        private readonly Point startSelection = new Point(), endSelection = new Point();

        /// <summary>
        /// Фиксированная точка начала выделения.
        /// </summary>
        private readonly Point startSelectionPoint = new Point();

        private bool DrawingSelection = false;

        public CellularAutomaton()
        {
            B = new List<int>(1) { 3 };
            S = new List<int>(2) { 2, 3 };
            field = new bool[cols, rows];
            nextGenField = new bool[cols, rows];
        }

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

        public int GenCount { get; private set; } = 0;
        public int PopulationCount { get; private set; } = 0;

        public static bool operator !=(CellularAutomaton f1, CellularAutomaton f2)
        {
            return !(f1 == f2);
        }

        public static bool operator ==(CellularAutomaton f1, CellularAutomaton f2)
        {
            return f1.GetSumOfAllNeighbours() == f2.GetSumOfAllNeighbours();
        }

        public bool AddCell(int x, int y)
        {
            if (field[x, y])
                return false;

            PopulationCount++;
            field[x, y] = true;
            return true;
        }

        public void Clear()
        {
            GenCount = 0;
            PopulationCount = 0;

            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    field[i, j] = false;
        }

        public CellularAutomaton Clone()
        {
            CellularAutomaton tempField = new CellularAutomaton(cols, rows, B, S)
            {
                type = type,
                rank = rank,
                GenCount = GenCount,
                PopulationCount = PopulationCount
            };

            tempField.Insert(this);
            return tempField;
        }

        public void CopyField(CellularAutomaton anotherField)
        {
            cols = anotherField.cols;
            rows = anotherField.rows;
            field = (bool[,])anotherField.field.Clone();
        }

        /// <summary>
        /// Возвращает новое поле, оставляя от исходного только наименьшую часть с клетками.
        /// </summary>
        public CellularAutomaton Crop()
        {
            if (IsEmpty()) return new CellularAutomaton(0, 0, B, S, rank);

            int leftmostX = cols, rightmostX = 0, topmostY = rows, lowestY = 0;

            // поиск граничных точек
            _ = Parallel.For(0, cols, delegate (int i)
            {
                for (int j = 0; j < rows; j++)
                    if (field[i, j])
                    {
                        if (leftmostX > i)
                            leftmostX = i;
                        if (rightmostX < i)
                            rightmostX = i;
                        if (topmostY > j)
                            topmostY = j;
                        if (lowestY < j)
                            lowestY = j;
                    }
            });

            CellularAutomaton tempField = new CellularAutomaton(rightmostX - leftmostX + 1, lowestY - topmostY + 1, B, S)
            {
                type = type,
                rank = rank,
                GenCount = GenCount,
                PopulationCount = PopulationCount,
                fieldInClipboard = fieldInClipboard
            };

            for (int i = 0; i < tempField.cols; i++)
                for (int j = 0; j < tempField.rows; j++)
                    tempField.field[i, j] = field[leftmostX + i, topmostY + j];

            return tempField;
        }

        public void Draw(int res, Graphics g, PictureBox pictureBox)
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

            // Выделение
            if (DrawingSelection)
                DrawSelection(res, g);

            pictureBox.Refresh();
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
            _ = Parallel.For(0, cols, delegate (int i)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (field[i, j]) sum += CountNeighbours(i, j);
                }
            });
            return sum;
        }

        /// <summary>
        /// Вставляет одно поле в другое. Если поля равного размера, переносит состояние второго поля в первое.
        /// </summary>
        public void Insert(CellularAutomaton anotherfield, bool inCenter = false, int offsetX = 0, int offsetY = 0)
        {
            Clear();
            GenCount = anotherfield.GenCount;
            fieldInClipboard = anotherfield.fieldInClipboard;

            if (inCenter)
            {
                offsetX = (cols / 2) - (anotherfield.cols / 2);
                offsetY = (rows / 2) - (anotherfield.rows / 2);
            }

            for (int i = 0; i < Math.Min(cols, anotherfield.cols); i++)
                for (int j = 0; j < Math.Min(rows, anotherfield.rows); j++)
                {
                    int x = i + offsetX;
                    int y = j + offsetY;
                    if (x >= 0 && x <= cols && y >= 0 && y <= rows)
                        field[x, y] = anotherfield.field[i, j];
                }
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
            GenCount++;
            PopulationCount = 0;
            _ = Parallel.For(0, cols, delegate (int i)
            {
                for (int j = 0; j < rows; j++)
                {
                    int neighboursCount = CountNeighbours(i, j);
                    if (B.Contains(neighboursCount) || (S.Contains(neighboursCount) && field[i, j]))
                    {
                        nextGenField[i, j] = true;
                        PopulationCount++;
                    }
                    else nextGenField[i, j] = false;
                }
            });

            _ = Parallel.For(0, cols, delegate (int i)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = nextGenField[i, j];
                }
            });
        }

        public void RandomCreate(int density)
        {
            Clear();

            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    if (rnd.Next(density) == 0) _ = AddCell(i, j);
        }

        public bool RemoveCell(int x, int y)
        {
            if (!field[x, y])
                return false;

            PopulationCount--;
            field[x, y] = true;
            return true;
        }

        internal void EndSelection(int x, int y)
        {
            DrawingSelection = false;
            startSelection.x = Math.Min(startSelectionPoint.x, x);
            startSelection.y = Math.Min(startSelectionPoint.y, y);
            endSelection.x = Math.Max(startSelectionPoint.x, x);
            endSelection.y = Math.Max(startSelectionPoint.y, y);
            int fieldInClipboardSizeX = endSelection.x - startSelection.x - 1;
            int fieldInClipboardSizeY = endSelection.y - startSelection.y - 1;

            if (fieldInClipboardSizeX == -1 || fieldInClipboardSizeY == -1)
                return;

            fieldInClipboard = new CellularAutomaton(fieldInClipboardSizeX, fieldInClipboardSizeY, B, S);
            for (int i = 0; i < fieldInClipboard.cols; i++)
            {
                for (int j = 0; j < fieldInClipboard.rows; j++)
                {
                    fieldInClipboard.field[i, j] = field[i + startSelection.x + 1, j + startSelection.y + 1];
                }
            }

            fieldInClipboard = fieldInClipboard.Crop();
        }

        internal void Paste(int x, int y)
        {
            if (fieldInClipboard is null)
                return;

            _ = Parallel.For(0, fieldInClipboard.cols, delegate (int i)
            {
                for (int j = 0; j < fieldInClipboard.rows; j++)
                {
                    if (fieldInClipboard.field[i, j])
                        _ = AddCell((i + x - (fieldInClipboard.cols / 2) + cols) % cols, (j + y - (fieldInClipboard.rows / 2) + rows) % rows);
                }
            });
        }

        internal void StartSelection(int x, int y)
        {
            DrawingSelection = true;
            startSelectionPoint.x = x;
            startSelectionPoint.y = y;
            startSelection.x = x;
            startSelection.y = y;
            endSelection.x = x;
            endSelection.y = y;
        }

        internal void UpdateSelection(int x, int y)
        {
            if (!DrawingSelection)
                StartSelection(x, y);
            else
            {
                startSelection.x = Math.Min(startSelectionPoint.x, x);
                startSelection.y = Math.Min(startSelectionPoint.y, y);
                endSelection.x = Math.Max(startSelectionPoint.x, x);
                endSelection.y = Math.Max(startSelectionPoint.y, y);
            }
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
                    if (i == 0 && j == 0)
                        continue;

                    int col = (x + i + cols) % cols;
                    int row = (y + j + rows) % rows;
                    if (field[col, row])
                        count++;
                }

            return count;
        }

        private void DrawSelection(int res, Graphics g)
        {
            for (int i = startSelection.x; i <= endSelection.x; i++)
                for (int j = startSelection.y; j <= endSelection.y; j++)
                {
                    if (i == startSelection.x || j == startSelection.y || i == endSelection.x || j == endSelection.y) // если текущая клетка находится на границе прямоугольника выделения
                        g.FillRectangle(selectionColor, i * res, j * res, res, res);
                    else
                    {
                        j = endSelection.y; // если текущая клетка внутри прямоугольника, сразу переходим к нижней границе
                        g.FillRectangle(selectionColor, i * res, j * res, res, res);
                    }
                }
        }
    }

    public class Point
    {
        public int x, y;

        public Point()
        {
            x = 0;
            y = 0;
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}