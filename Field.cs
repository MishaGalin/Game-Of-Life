using System;
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
        /// Номер текущего поколения.
        /// </summary>
        public int genCount = 0;

        /// <summary>
        /// Цвет фона.
        /// </summary>
        public readonly Color backgroundColor = Color.Black;

        /// <summary>
        /// Цвет клеток.
        /// </summary>
        public readonly Brush foregroundColor = Brushes.White;

        /// <summary>
        /// Поле.
        /// </summary>
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

        /*public Field(bool[,] field)
        {
            this.field = field;
            if (field != null)
            {
                this.cols = field;
                this.rows = field.Length;
            }
        }*/

        /// <summary>
        /// Генерация нового поколения
        /// </summary>
        public void NextGeneration()
        {
            bool[,] newField = new bool[cols, rows];

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    int neighboursCount = CountNeighbours(i, j);

                    if (!field[i, j] && B.Contains(neighboursCount))
                        newField[i, j] = true;
                    else if (field[i, j] && !S.Contains(neighboursCount))
                        newField[i, j] = false;
                    else
                        newField[i, j] = field[i, j];
                }
            }

            field = newField;
            genCount++;
        }

        /// <summary>
        /// Подсчет соседей клетки
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

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = false;
                }
            }
        }

        public void CreateRandom(int density)
        {
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = rnd.Next(density) == 0;
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
            if (f1.cols != f2.cols || f1.rows != f2.rows)
                return false;

            int c1 = 0, c2 = 0;

            for (int i = 0; i < f1.cols; i++)
            {
                for (int j = 0; j < f1.rows; j++)
                {
                    c1 += f1.CountNeighbours(i, j);
                    c2 += f2.CountNeighbours(i, j);

                    /*if (f1.CountNeighbours(i, j) != f2.CountNeighbours(i, j))
                        return false;*/
                }
            }
            return c1 == c2;
        }

        public static bool operator !=(Field f1, Field f2)
        {
            return !(f1 == f2);
        }

        public bool IsEmpty()
        {
            if (cols == 0 || rows == 0)
                return true;

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (field[i, j])
                        return false;
                }
            }
            return true;
        }

        public Field Clone()
        {
            Field tempF = new Field(cols, rows);
            tempF.field = this.field;
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
    }
}