﻿using System;
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

        /// <summary>
        /// Массив клеток.
        /// </summary>
        public bool[,] field;

        public CellularAutomaton fieldInClipboard;

        /// <summary>
        /// Номер поколения.
        /// </summary>
        private int genCount = 0;

        /// <summary>
        /// Количество живых клеток.
        /// </summary>
        private int populationCount = 0;

        /// <summary>
        /// Ранг клеточного автомата, т.е. радиус окрестности клетки при подсчете соседей.
        /// </summary>
        public int rank = 1;

        public int rows = 0, cols = 0;

        private bool ctrlIsPressed = false;

        private int xWhenStartSelection, yWhenStartSelection, xStart, yStart, xEnd, yEnd;

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

        /// <summary>
        /// Промежуточный массив для генерации следующего поколения.
        /// </summary>
        private readonly bool[,] nextGenField;

        private readonly Random rnd = new Random();

        public int PopulationCount { get => populationCount; }
        public int GenCount { get => genCount; }

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
                rank = rank,
                genCount = genCount,
                populationCount = populationCount
            };

            tempField.Insert(this);
            return tempField;
        }

        public void CopyField(CellularAutomaton anotherField)
        {
            cols = anotherField.cols;
            rows = anotherField.rows;
            field = anotherField.field;
        }

        /// <summary>
        /// Возвращает новое поле, оставляя от исходного только наименьшую часть с клетками.
        /// </summary>
        public CellularAutomaton Crop()
        {
            if (IsEmpty())
                return new CellularAutomaton(0, 0, B, S, rank);

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
                rank = rank,
                genCount = genCount,
                populationCount = populationCount
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

            if (ctrlIsPressed)
            {
                for (int i = xStart; i <= xEnd; i++)
                    for (int j = yStart; j <= yEnd; j++)
                    {
                        if (i == xStart || j == yStart || i == xEnd || j == yEnd)
                            g.FillRectangle(Brushes.GreenYellow, i * res, j * res, res, res);
                    }
            }

            // Сетка
            for (int i = 0; i <= cols; i++)
                g.DrawLine(gridColor, i * res, 0, i * res, rows * res);

            for (int i = 0; i <= rows; i++)
                g.DrawLine(gridColor, 0, i * res, cols * res, i * res);

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

        internal void UpdateSelection(int x, int y)
        {
            xStart = Math.Min(xWhenStartSelection, x);
            yStart = Math.Min(yWhenStartSelection, y);
            xEnd = Math.Max(xWhenStartSelection, x);
            yEnd = Math.Max(yWhenStartSelection, y);
        }

        internal void Paste(int x, int y)
        {
            for (int i = 0; i < fieldInClipboard.cols; i++)
            {
                for (int j = 0; j < fieldInClipboard.rows; j++)
                {
                    field[(i + x - fieldInClipboard.cols / 2 + cols) % cols, (j + y - fieldInClipboard.rows / 2 + rows) % rows] = fieldInClipboard.field[i, j];
                }
            }
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
        /// Вставляет одно поле в другое. Если поля равного размера, переносит состояние второго поля в первое.
        /// </summary>
        public void Insert(CellularAutomaton anotherfield, bool inCenter = false, int offsetX = 0, int offsetY = 0)
        {
            Clear();
            genCount = anotherfield.GenCount;

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
                    if (field[i, j]) populationCount++;
                }
            }
        }

        public void RandomCreate(int density)
        {
            Clear();

            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    if (rnd.Next(density) == 0) AddCell(i, j);
        }

        internal void StartSelection(int x, int y)
        {
            ctrlIsPressed = true;
            xWhenStartSelection = x;
            yWhenStartSelection = y;
        }

        internal void EndSelection(int x, int y)
        {
            ctrlIsPressed = false;
            xStart = Math.Min(xWhenStartSelection, x);
            yStart = Math.Min(yWhenStartSelection, y);
            xEnd = Math.Max(xWhenStartSelection, x);
            yEnd = Math.Max(yWhenStartSelection, y);
            int fieldInClipboardSizeX = xEnd - xStart - 1;
            int fieldInClipboardSizeY = yEnd - yStart - 1;

            if (fieldInClipboardSizeX == -1 || fieldInClipboardSizeY == -1)
                return;

            fieldInClipboard = new CellularAutomaton(fieldInClipboardSizeX, fieldInClipboardSizeY, B, S);
            for (int i = 0; i < fieldInClipboard.cols; i++)
            {
                for (int j = 0; j < fieldInClipboard.rows; j++)
                {
                    fieldInClipboard.field[i, j] = field[i + xStart + 1, j + yStart + 1];
                }
            }
            fieldInClipboard = fieldInClipboard.Crop();
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
                    if (i == 0 && j == 0)
                        continue;

                    int col = (x + i + cols) % cols;
                    int row = (y + j + rows) % rows;
                    if (field[col, row])
                        count++;
                }

            return count;
        }
    }
}