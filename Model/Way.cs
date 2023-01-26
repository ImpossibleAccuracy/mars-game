using System.Collections.Generic;

namespace MarsGame.Model
{
    public class Way
    {
        private static int _autoinrement = 0;

        public int ID { get; private set; }

        public List<Cell> History { get; private set; }
        public List<Cell> Reachable { get; private set; }

        public int Size => History.Count - 1;

        public bool HasReachable => Reachable.Count > 0;

        public Way() : this(new List<Cell>())
        {
        }

        public Way(Way way) : this(way.History)
        {
        }

        public Way(List<Cell> history)
        {
            History = new List<Cell>(history);
            Reachable = new List<Cell>();

            ID = ++_autoinrement;
        }
    }
}
