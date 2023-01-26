using System;
using System.Collections.Generic;
using System.Drawing;
using MarsGame.Model;

namespace MarsGame.Entity
{
    public class Map
    {
        private readonly Cell[,] _data;

        public Cell[,] Data { get => _data; }

        public int Width => _data.GetLength(1);

        public int Height => _data.GetLength(0);

        public Map(Cell[,] data)
        {
            _data = data;
        }

        public Cell Get(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                return _data[y, x];
            }

            return null;
        }

        public Cell FindByCondition(Predicate<Cell> condition)
        {
            for (int m = 0; m < Height; m++)
            {
                for (int n = 0; n < Width; n++)
                {
                    if (condition(_data[m, n]))
                    {
                        return _data[m, n];
                    }
                }
            }

            return null;
        }

        public List<Cell> FindNearbyCells(Cell cell, Predicate<Cell> isEmpty)
        {
            List<Cell> res = new List<Cell>();

            int x = cell.X,
                y = cell.Y;

            // Top
            Cell c = Get(x, y - 1);
            if (c != null && isEmpty(c)) res.Add(c);

            // Bottom
            c = Get(x, y + 1);
            if (c != null && isEmpty(c)) res.Add(c);

            // Left
            c = Get(x - 1, y);
            if (c != null && isEmpty(c)) res.Add(c);

            // Right
            c = Get(x + 1, y);
            if (c != null && isEmpty(c)) res.Add(c);

            return res;
        }
    }
}
