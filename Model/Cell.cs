namespace MarsGame.Model
{
    public class Cell
    {
        public char Type { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public Cell() { }

        public Cell(char type)
        {
            Type = type;
        }

        public bool IsWall => Type == 'X';

        public bool IsFree => Type == '.';

        public bool IsStart => Type == 'S';

        public bool IsQuit => Type == 'Q';

        public bool IsDoor => !IsWall && !IsStart && !IsQuit && char.IsUpper(Type);

        public bool IsKey => !IsWall && !IsStart && !IsQuit && char.IsLower(Type);
    }
}
