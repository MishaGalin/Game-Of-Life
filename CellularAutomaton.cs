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
        /// Цвет фона.
        /// </summary>
        public static readonly Color backgroundColor = Color.Black;

        /// <summary>
        /// Цвет клеток.
        /// </summary>
        public static readonly Brush cellColor = Brushes.White;

        /// <summary>
        /// Цвет сетки.
        /// </summary>
        public static readonly Pen gridColor = Pens.DarkSlateGray;

        /// <summary>
        /// Цвет прямоугольника выделения.
        /// </summary>
        public static readonly Brush selectionColor = Brushes.Yellow;

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

        public bool SelectionIsStarted = false;

        /// <summary>
        /// Тип клеточной структуры (статичная или периодическая). Используется для вывода статистики после поиска.
        /// </summary>
        public string type = "";

        public double zoom = 1;

        /// <summary>
        /// Параметры, используемые для сдвига видимой области при приближении.
        /// </summary>
        public int zoomOffsetX = 0, zoomOffsetY = 0, zoomMemoryOffsetX = 0, zoomMemoryOffsetY = 0,
         zoomStartOffsetX = 0, zoomStartOffsetY = 0, zoomEndOffsetX = 0, zoomEndOffsetY = 0;

        private static readonly Random rnd = new Random();

        /// <summary>
        /// Промежуточный массив для генерации следующего поколения.
        /// </summary>
        private readonly bool[,] nextGenField;

        /// <summary>
        /// Точки для отрисовки прямоугльника выделения.
        /// </summary>
        private PointF startSelectionDynamicPoint = new PointF(), endSelectionDynamicPoint = new PointF();

        /// <summary>
        /// Фиксированная точка начала выделения.
        /// </summary>
        private PointF startSelectionFixedPoint = new PointF();

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
            {
                return false;
            }

            PopulationCount++;
            field[x, y] = true;
            return true;
        }

        public void Clear()
        {
            GenCount = 0;
            PopulationCount = 0;

            _ = Parallel.For(0, cols, delegate (int i)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = false;
                }
            });
        }

        public CellularAutomaton Clone()
        {
            CellularAutomaton tempField = new CellularAutomaton(cols, rows, B, S)
            {
                type = type,
                rank = rank,
                GenCount = GenCount,
                PopulationCount = PopulationCount,
            };

            tempField.Insert(this);
            return tempField;
        }

        public int CountPopulation()
        {
            int count = 0;

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (field[i, j])
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Возвращает новое поле, оставляя от исходного только наименьшую часть с клетками.
        /// </summary>
        public CellularAutomaton Crop()
        {
            if (IsEmpty())
            {
                return new CellularAutomaton(0, 0, B, S, rank);
            }

            int leftmostX = cols, rightmostX = 0, topmostY = rows, lowestY = 0;

            // поиск граничных точек
            _ = Parallel.For(0, cols, delegate (int i)
           {
               for (int j = 0; j < rows; j++)
               {
                   if (field[i, j])
                   {
                       if (leftmostX > i)
                       {
                           leftmostX = i;
                       }

                       if (rightmostX < i)
                       {
                           rightmostX = i;
                       }

                       if (topmostY > j)
                       {
                           topmostY = j;
                       }

                       if (lowestY < j)
                       {
                           lowestY = j;
                       }
                   }
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

            _ = Parallel.For(0, tempField.cols, delegate (int i)
            {
                for (int j = 0; j < tempField.rows; j++)
                {
                    tempField.field[i, j] = field[leftmostX + i, topmostY + j];
                }
            });
            return tempField;
        }

        public void Draw(int res, Graphics g, PictureBox pictureBox)
        {
            g.Clear(backgroundColor);
            int scale = (int)(res * zoom);
            zoomOffsetX = zoomEndOffsetX - zoomStartOffsetX + zoomMemoryOffsetX;
            zoomOffsetY = zoomEndOffsetY - zoomStartOffsetY + zoomMemoryOffsetY;
            int borderX = (int)(cols * res * (1 - zoom));
            int borderY = (int)(rows * res * (1 - zoom));

            // checking the position of the visible area of the field
            if (zoomOffsetX > 0)
            {
                zoomOffsetX = 0;
            }
            else if (zoomOffsetX < borderX)
            {
                zoomOffsetX = borderX;
            }

            if (zoomOffsetY > 0)
            {
                zoomOffsetY = 0;
            }
            else if (zoomOffsetY < borderY)
            {
                zoomOffsetY = borderY;
            }

            // Клетки
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (field[i, j])
                    {
                        g.FillRectangle(brush: cellColor,
                                                     x: (i * scale) + zoomOffsetX,
                                                     y: (j * scale) + zoomOffsetY,
                                                     width: scale,
                                                     height: scale);
                    }
                }
            }

            // Вертикальные линии сетки
            for (int i = 0; i <= cols; i++)
            {
                g.DrawLine(pen: gridColor,
                           x1: (i * scale) + zoomOffsetX,
                           y1: 0,
                           x2: (i * scale) + zoomOffsetX,
                           y2: rows * scale);
            }

            // Горизонтальные линии сетки
            for (int i = 0; i <= rows; i++)
            {
                g.DrawLine(pen: gridColor,
                           x1: 0,
                           y1: (i * scale) + zoomOffsetY,
                           x2: cols * scale,
                           y2: (i * scale) + zoomOffsetY);
            }

            // Выделение
            if (SelectionIsStarted)
            {
                DrawSelection(scale, g);
            }

            pictureBox.Refresh();
        }

        public void EndChangingOffset()
        {
            zoomMemoryOffsetX = zoomOffsetX;
            zoomMemoryOffsetY = zoomOffsetY;
            zoomStartOffsetX = 0;
            zoomStartOffsetY = 0;
            zoomEndOffsetX = 0;
            zoomEndOffsetY = 0;
        }

        public bool Equals(CellularAutomaton anotherField)
        {
            if (rows != anotherField.rows || cols != anotherField.cols)
            {
                return false;
            }

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (field[i, j] != anotherField.field[i, j])
                    {
                        return false;
                    }
                }
            }

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
                    if (field[i, j])
                    {
                        sum += CountNeighbours(i, j);
                    }
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

            _ = Parallel.For(0, Math.Min(cols, anotherfield.cols), delegate (int i)
            {
                for (int j = 0; j < Math.Min(rows, anotherfield.rows); j++)
                {
                    int x = i + offsetX;
                    int y = j + offsetY;
                    if (x >= 0 && x <= cols && y >= 0 && y <= rows)
                    {
                        field[x, y] = anotherfield.field[i, j];
                    }
                }
            });
        }

        public bool IsEmpty()
        {
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (field[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Генерация следующего поколения.
        /// </summary>
        public void NextGeneration()
        {
            _ = Parallel.For(0, cols, delegate (int i)
            {
                _ = Parallel.For(0, rows, delegate (int j)
                {
                    int neighboursCount = CountNeighbours(i, j);
                    nextGenField[i, j] = B.Contains(neighboursCount) || (S.Contains(neighboursCount) && field[i, j]);
                });
            });

            _ = Parallel.For(0, cols, delegate (int i)
            {
                _ = Parallel.For(0, rows, delegate (int j)
                {
                    field[i, j] = nextGenField[i, j];
                });
            });

            GenCount++;
            PopulationCount = CountPopulation();
        }

        public void RandomCreate(int density)
        {
            Clear();

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (rnd.Next(density) == 0)
                    {
                        _ = AddCell(i, j);
                    }
                }
            }
        }

        public bool RemoveCell(int x, int y)
        {
            if (!field[x, y])
            {
                return false;
            }

            PopulationCount--;
            field[x, y] = false;
            return true;
        }

        public void StartChangingOffset(int x, int y)
        {
            zoomStartOffsetX = x;
            zoomStartOffsetY = y;
            zoomEndOffsetX = x;
            zoomEndOffsetY = y;
        }

        public void UpdateOffset(int x, int y)
        {
            zoomEndOffsetX = x;
            zoomEndOffsetY = y;
        }

        internal void EndSelection(int x, int y)
        {
            SelectionIsStarted = false;
            startSelectionDynamicPoint.X = Math.Min(startSelectionFixedPoint.X, x);
            startSelectionDynamicPoint.Y = Math.Min(startSelectionFixedPoint.Y, y);
            endSelectionDynamicPoint.X = Math.Max(startSelectionFixedPoint.X, x);
            endSelectionDynamicPoint.Y = Math.Max(startSelectionFixedPoint.Y, y);
            int sizeOfFieldInClipboardX = (int)(endSelectionDynamicPoint.X - startSelectionDynamicPoint.X - 1);
            int sizeOfFieldInClipboardY = (int)(endSelectionDynamicPoint.Y - startSelectionDynamicPoint.Y - 1);

            if (sizeOfFieldInClipboardX == -1 || sizeOfFieldInClipboardY == -1)
            {
                return;
            }

            fieldInClipboard = new CellularAutomaton(sizeOfFieldInClipboardX, sizeOfFieldInClipboardY, B, S);

            for (int i = 0; i < fieldInClipboard.cols; i++)
            {
                for (int j = 0; j < fieldInClipboard.rows; j++)
                {
                    fieldInClipboard.field[i, j] = field[(int)(i + startSelectionDynamicPoint.X + 1), (int)(j + startSelectionDynamicPoint.Y + 1)];
                }
            }

            fieldInClipboard = fieldInClipboard.Crop();
        }

        internal void Paste(int x, int y)
        {
            if (fieldInClipboard is null)
            {
                return;
            }

            _ = Parallel.For(0, fieldInClipboard.cols, delegate (int i)
           {
               for (int j = 0; j < fieldInClipboard.rows; j++)
               {
                   if (fieldInClipboard.field[i, j])
                   {
                       _ = AddCell((i + x - (fieldInClipboard.cols / 2) + cols) % cols,
                                   (j + y - (fieldInClipboard.rows / 2) + rows) % rows);
                   }
               }
           });
        }

        internal void StartSelection(int x, int y)
        {
            SelectionIsStarted = true;
            startSelectionFixedPoint.X = x;
            startSelectionFixedPoint.Y = y;
            startSelectionDynamicPoint.X = x;
            startSelectionDynamicPoint.Y = y;
            endSelectionDynamicPoint.X = x;
            endSelectionDynamicPoint.Y = y;
        }

        internal void UpdateSelection(int x, int y)
        {
            if (SelectionIsStarted)
            {
                startSelectionDynamicPoint.X = Math.Min(startSelectionFixedPoint.X, x);
                startSelectionDynamicPoint.Y = Math.Min(startSelectionFixedPoint.Y, y);
                endSelectionDynamicPoint.X = Math.Max(startSelectionFixedPoint.X, x);
                endSelectionDynamicPoint.Y = Math.Max(startSelectionFixedPoint.Y, y);
            }
            else
            {
                StartSelection(x, y);
            }
        }

        /// <summary>
        /// Количество соседей клетки.
        /// </summary>
        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -rank; i <= rank; i++)
            {
                for (int j = -rank; j <= rank; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    int col = (x + i + cols) % cols;
                    int row = (y + j + rows) % rows;
                    if (field[col, row])
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private void DrawSelection(int res, Graphics g)
        {
            PointF upperLeftPoint = new PointF((startSelectionDynamicPoint.X * res) + zoomOffsetX + (res / 2),
                                               (startSelectionDynamicPoint.Y * res) + zoomOffsetY + (res / 2));

            PointF upperRightPoint = new PointF((endSelectionDynamicPoint.X * res) + zoomOffsetX + (res / 2),
                                                (startSelectionDynamicPoint.Y * res) + zoomOffsetY + (res / 2));

            PointF lowerLeftPoint = new PointF((startSelectionDynamicPoint.X * res) + zoomOffsetX + (res / 2),
                                               (endSelectionDynamicPoint.Y * res) + zoomOffsetY + (res / 2));

            PointF lowerRightPoint = new PointF((endSelectionDynamicPoint.X * res) + zoomOffsetX + (res / 2),
                                                (endSelectionDynamicPoint.Y * res) + zoomOffsetY + (res / 2));

            PointF[] points = new PointF[5] { upperLeftPoint, upperRightPoint, lowerRightPoint, lowerLeftPoint, upperLeftPoint };

            g.DrawLines(new Pen(selectionColor, res / 2), points);
            g.FillRectangle(selectionColor, upperLeftPoint.X - (res / 4), upperLeftPoint.Y - (res / 4), res / 2, res / 2);
        }
    }
}