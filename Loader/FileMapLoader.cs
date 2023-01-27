using System;
using System.IO;
using System.Linq;
using MarsGame.Model;
using MarsGame.Entity;

namespace MarsGame.Loader
{
    public class FileMapLoader : IMapLoader
    {
        private static readonly string MapsPath = "./maps";

        public Map Load()
        {
            using StreamReader file = GetMapFileReader();

            string sizes = file.ReadLine();
            Cell[,] arr = CreateArray(sizes);

            int w = arr.GetLength(1),
                h = arr.GetLength(0);

            for (int i = 0; i < h; i++)
            {
                string line = file.ReadLine().Replace(" ", "");

                for (int j = 0; j < w; j++)
                {
                    char block = line[j];

                    arr[i, j] = new Cell()
                    {
                        Type = block,
                        Position = new Point(j, i),
                    };
                }
            }

            return new Map(arr);
        }

        private StreamReader GetMapFileReader()
        {
            string[] args = Environment.GetCommandLineArgs();

            string mapFile = (args.Length == 2 ? args[1] : args[2]);
            string mapPath = Path.Combine(MapsPath, mapFile);

            return new StreamReader(mapPath);
        }

        private Cell[,] CreateArray(string line)
        {
            int[] sizes = line.Split(" ").Select(int.Parse).ToArray();
            return new Cell[sizes[0], sizes[1]];
        }
    }
}
