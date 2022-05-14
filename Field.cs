﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public class Field
    {
        private readonly Random rnd = new Random();

        /// <summary>
        /// Правило рождения новой клетки.
        /// </summary>
        public int[] B = new int[1] { 3 };

        /// <summary>
        /// Правило выживания клеток.
        /// </summary>
        public int[] S = new int[2] { 2, 3 };

        /// <summary>
        /// Номер поколения
        /// </summary>
        public int genCount = 0;

        /// <summary>
        /// Количество живых клеток
        /// </summary>
        public int populationCount = 0;

        /// <summary>
        /// Цвет фона.
        /// </summary>
        public readonly Color backgroundColor = Color.Black;

        /// <summary>
        /// Цвет клеток.
        /// </summary>
        public readonly Brush foregroundColor = Brushes.White;

        public bool[,] field;

        public int rows, cols;

        public Field(int cols, int rows, int[] B, int[] S)
        {
            this.B = B;
            this.S = S;

            this.cols = cols;
            this.rows = rows;
            field = new bool[cols, rows];
        }

        public Field(int cols, int rows)
        {
            this.cols = cols;
            this.rows = rows;
            field = new bool[cols, rows];
        }

        /// <summary>
        /// Генерация нового поколения
        /// </summary>
        public Field NextGeneration()
        {
            var newField = new Field(cols, rows);

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    int neighboursCount = CountNeighbours(i, j);

                    if (!field[i, j] && B.Contains(neighboursCount))
                    {
                        newField.field[i, j] = true;
                    }
                    else if (field[i, j] && !S.Contains(neighboursCount))
                        newField.field[i, j] = false;
                    else
                        newField.field[i, j] = field[i, j];

                    newField.populationCount += Convert.ToInt32(newField.field[i, j]);
                }
            }

            newField.genCount = ++genCount;
            return newField;
        }

        public void AddCell(int x, int y)
        {
            if (field[x, y])
                return;

            populationCount++;
            field[x, y] = true;
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
                    bool isSelfChecking = (col == x && row == y);

                    if (field[col, row] && !isSelfChecking)
                        count++;
                }
            }
            return count;
        }

        public void Clear()
        {
            genCount = 0;
            populationCount = 0;

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = false;
                }
            }
        }

        public void RandomCreate(int density)
        {
            Clear();
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (rnd.Next(density) == 0)
                        AddCell(i, j);
                }
            }
            genCount = 0;
        }

        public void Draw(int res, ref Graphics g, ref PictureBox pictureBox)
        {
            g.Clear(backgroundColor);

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (field[i, j])
                        g.FillRectangle(foregroundColor, i * res, j * res, res, res); // клетки
                }
            }

            for (int i = 0; i <= cols; i++) // Сетка
            {
                g.DrawLine(Pens.DarkSlateGray, i * res, 0, i * res, rows * res);
            }

            for (int i = 0; i <= rows; i++) // Сетка
            {
                g.DrawLine(Pens.DarkSlateGray, 0, i * res, cols * res, i * res);
            }

            pictureBox.Refresh();
        }

        public static bool operator ==(Field f1, Field f2)
        {
            return f1.GetSumOfAllNeighbours() == f2.GetSumOfAllNeighbours();
        }

        public static bool operator !=(Field f1, Field f2)
        {
            return !(f1 == f2);
        }

        public bool IsEmpty()
        {
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (field[i, j]) return false;
                }
            }
            return true;
        }

        public Field Clone()
        {
            Field tempF = new Field(cols, rows);
            tempF.Insert(this);
            return tempF;
        }

        public int GetSumOfAllNeighbours()
        {
            int sum = 0;
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (field[i, j])
                        sum += CountNeighbours(i, j);
                }
            }
            return sum;
        }

        public override bool Equals(object obj)
        {
            return obj is Field field &&
                   EqualityComparer<bool[,]>.Default.Equals(this.field, field.field);
        }

        public override int GetHashCode()
        {
            return -306121345 + EqualityComparer<bool[,]>.Default.GetHashCode(field);
        }

        /// <summary>
        /// Вставляет меньшее поле в большее с сохранением размеров большего. Если поля равного размера, переносит состояние второго поля в первое.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public void Insert(Field field, bool inCenter = false, int offsetX = 0, int offsetY = 0)
        {
            Clear();

            if (inCenter)
            {
                offsetX = cols / 2 - field.cols / 2;
                offsetY = rows / 2 - field.rows / 2;
            }

            if (cols < field.cols || rows < field.rows)
                throw new ArgumentException("Inserted field is larger than original");

            if (field.cols + offsetX > cols || field.rows + offsetY > rows || offsetX < 0 || offsetY < 0)
                throw new ArgumentException("Offset leads out of the field");

            for (int i = 0; i < field.cols; i++)
            {
                for (int j = 0; j < field.rows; j++)
                {
                    this.field[i + offsetX, j + offsetY] = field.field[i, j];
                }
            }
        }
    }
}