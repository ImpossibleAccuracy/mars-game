using MarsGame.Model;
using MarsGame.Entity;

namespace MarsGame.Loader
{
    public class MemoryMapLoader : IMapLoader
    {
        private static readonly string MapExample =
            "X X X X X X X X X X\n" +
            "X S . . . . . . . X\n" +
            "X X X X X.X X X X\n" +
            "X. . . . .X. .X\n" +
            "X.X.X X X X.X\n" +
            "X . X. . .X X . X\n" +
            "X . X X X. . . .X\n" +
            "X . X.X.X X X X\n" +
            "X. . .X. . .Q X\n" +
            "X X X X X X X X X X\n" +
            "X X X X X X X X X X";

        public Map Load()
        {
            string[] arr = MapExample.Split('\n');

            Cell[,] map = CreateArray(arr);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                string line = arr[i].Replace(" ", "");

                for (int j = 0; j < line.Length; j++)
                {
                    map[i, j] = new Cell()
                    {
                        Type = line[j],
                        X = j,
                        Y = i
                    };
                }
            }

            return new Map(map);
        }

        public Cell[,] CreateArray(string[] arr)
        {
            string line = arr[0].Replace(" ", "");

            int w = arr.Length,
                h = line.Length;

            return new Cell[w, h];
        }
    }
}
