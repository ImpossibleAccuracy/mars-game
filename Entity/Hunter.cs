using System;
using System.Linq;
using System.Collections.Generic;
using MarsGame.Model;

namespace MarsGame.Entity
{
    public class Hunter
    {
        public Map Map { get; set; }

        public Hunter() { }

        public Hunter(Map map)
        {
            Map = map;
        }

        public Way FindWay(Cell start, Cell quit)
        {
            Way shortestWay = FindShortestWay(start, quit, c => !c.IsWall);
            return shortestWay;

            /*List<Cell> doors = FindAllMatches(shortestWay, c => c.IsDoor);

            if (doors.Count == 0)
            {
                return shortestWay;
            }
            else
            {
                List<Cell> keys = FindAllKeys(
                    c => doors.Exists(d => char.ToLower(d.Type).Equals(c.Type)));

                return shortestWay;
            }*/
        }

        /*public List<Cell> FindAllKeys(Predicate<Cell> predicate)
        {
            List<Cell> keys = new List<Cell>();

            foreach (var item in Map.Data)
            {
                if (predicate(item))
                {
                    keys.Add(item);
                }
            }

            return keys;
        }

        public List<Cell> FindAllMatches(Way way, Predicate<Cell> predicate)
        {
            return way.History.FindAll(predicate);
        }*/

        public Way FindShortestWay(Cell start, Cell quit, Predicate<Cell> isCellPassable)
        {
            List<Way> ways = FindAllWays(start, quit, isCellPassable);
            if (ways.Count == 0)
            {
                return null;
            }

            ways.Sort(Comparer<Way>.Create((w1, w2) => w1.Size.CompareTo(w2.Size)));

            return ways[0];
        }

        public List<Way> FindAllWays(Cell start, Cell quit, Predicate<Cell> isCellPassable)
        {
            Way startWay = new Way();
            startWay.Reachable.Add(start);

            List<Way> queue = new List<Way>
            {
                startWay
            };

            List<Way> result = new List<Way>();

            while (queue.Count > 0)
            {
                Way way = queue[0];

                while (way.HasReachable)
                {
                    Cell reachable = way.Reachable[0];
                    way.History.Add(reachable);
                    way.Reachable.Remove(reachable);

                    if (reachable.Equals(quit))
                    {
                        result.Add(way);
                        break;
                    }

                    List<Cell> fork = Map.FindNearbyCells(
                        reachable,
                        (cell) => isCellPassable(cell) && !way.History.Contains(cell));

                    if (fork.Count == 1)
                    {
                        way.Reachable.Add(fork[0]);
                    }
                    else if (fork.Count > 1)
                    {
                        foreach (Cell node in fork)
                        {
                            Way newWay = new Way(way);
                            newWay.Reachable.Add(node);
                            queue.Add(newWay);
                        }
                        break;
                    }
                }

                queue.Remove(way);
            }

            return result;
        }
    }
}
