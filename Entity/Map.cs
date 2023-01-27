using System;
using System.Linq;
using System.Collections.Generic;
using MarsGame.Model;

namespace MarsGame.Entity
{
    public class Map
    {
        public Cell[,] Data { get; private set; }

        public int Width => Data.GetLength(1);

        public int Height => Data.GetLength(0);

        public Point[] Doors => FindAllByCondition(c => c.IsDoor)
            .Select(c => c.Position)
            .ToArray();

        public Point[] Keys => FindAllByCondition(c => c.IsKey)
            .Select(c => c.Position)
            .ToArray();

        public Map(Cell[,] data)
        {
            Data = data;
        }

        public Cell Get(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                return Data[y, x];
            }

            return null;
        }

        public Cell FindByCondition(Predicate<Cell> condition)
        {
            for (int m = 0; m < Height; m++)
            {
                for (int n = 0; n < Width; n++)
                {
                    if (condition(Data[m, n]))
                    {
                        return Data[m, n];
                    }
                }
            }

            return null;
        }

        public List<Cell> FindAllByCondition(Predicate<Cell> condition)
        {
            List<Cell> result = new List<Cell>();

            for (int m = 0; m < Height; m++)
            {
                for (int n = 0; n < Width; n++)
                {
                    if (condition(Data[m, n]))
                    {
                        result.Add(Data[m, n]);
                    }
                }
            }

            return result;
        }

        public List<Cell> FindNearbyCells(Cell cell, Predicate<Cell> isEmpty)
        {
            List<Cell> res = new List<Cell>();

            int x = cell.Position.X,
                y = cell.Position.Y;

            Cell c = Get(x, y - 1);
            if (c != null && isEmpty(c)) res.Add(c);

            c = Get(x, y + 1);
            if (c != null && isEmpty(c)) res.Add(c);

            c = Get(x - 1, y);
            if (c != null && isEmpty(c)) res.Add(c);

            c = Get(x + 1, y);
            if (c != null && isEmpty(c)) res.Add(c);

            return res;
        }
    }
}
