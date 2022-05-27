﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public class Field
    {
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
        /// Правило рождения новой клетки.
        /// </summary>
        public List<int> B;

        /// <summary>
        /// Правило выживания клеток.
        /// </summary>
        public List<int> S;

        /// <summary>
        /// Цвет фона.
        /// </summary>
        private readonly Color backgroundColor = Color.Black;

        /// <summary>
        /// Цвет клеток.
        /// </summary>
        private readonly Brush foregroundColor = Brushes.White;

        /// <summary>
        /// Цвет сетки.
        /// </summary>
        private readonly Pen gridColor = Pens.DarkSlateGray;

        private readonly Random rnd = new Random();

        private readonly bool[,] nextGenField;

        public Field(int cols, int rows, List<int> B, List<int> S)
        {
            this.B = B;
            this.S = S;

            this.cols = cols;
            this.rows = rows;
            field = new bool[cols, rows];
            nextGenField = new bool[cols, rows];
        }

        public static bool operator !=(Field f1, Field f2)
        {
            return !(f1 == f2);
        }

        public static bool operator ==(Field f1, Field f2)
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

        public Field Clone()
        {
            Field tempField = new Field(cols, rows, B, S);
            tempField.Insert(this);
            return tempField;
        }

        public void Draw(int res, ref Graphics g, ref PictureBox pictureBox)
        {
            g.Clear(backgroundColor);

            // Клетки
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    if (field[i, j]) g.FillRectangle(foregroundColor, (i * res) + 1, (j * res) + 1, res - 1, res - 1);

            // Сетка
            for (int i = 0; i <= cols; i++)
                g.DrawLine(gridColor, i * res, 0, i * res, rows * res);

            for (int i = 0; i <= rows; i++)
                g.DrawLine(gridColor, 0, i * res, cols * res, i * res);

            pictureBox.Refresh();
        }

        /// <summary>
        /// Возвращает сумму соседей каждой живой клетки.
        /// </summary>
        /// <returns></returns>
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
        public void Insert(Field field, bool inCenter = false, int offsetX = 0, int offsetY = 0)
        {
            Clear();

            if (inCenter)
            {
                offsetX = (cols / 2) - (field.cols / 2) + 1;
                offsetY = (rows / 2) - (field.rows / 2) + 1;
            }

            if (cols < field.cols || rows < field.rows)
                throw new ArgumentException("Inserted field is larger than original");

            if (field.cols + offsetX > cols || field.rows + offsetY > rows || offsetX < 0 || offsetY < 0)
                throw new ArgumentException("Offset leads out of the original field");

            for (int i = 0; i < field.cols; i++)
                for (int j = 0; j < field.rows; j++)
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
                    populationCount += Convert.ToInt32(field[i, j]);
                }
            }

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = nextGenField[i, j];
                    nextGenField[i, j] = false;
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
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int col = (x + i + cols) % cols;
                    int row = (y + j + rows) % rows;
                    bool isSelfChecking = col == x && row == y;

                    if (field[col, row] && !isSelfChecking)
                        count++;
                }
            }
            return count;
        }
    }
}