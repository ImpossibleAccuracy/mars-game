using System;
using System.Drawing;

namespace MarsGame.Entity
{
    public delegate void OnMove(Direction direction);

    public class Player
    {
        public int X { get; protected set; }

        public int Y { get; protected set; }

        public event OnMove OnMove;

        public Player() : this(new Point(0, 0)) { }

        public Player(Point position)
        {
            X = position.X;
            Y = position.Y;
        }

        public void MoveTo(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Top:
                    Y -= 1;
                    break;
                case Direction.Right:
                    X += 1;
                    break;
                case Direction.Down:
                    Y += 1;
                    break;
                case Direction.Left:
                    X -= 1;
                    break;
            }

            OnMove?.Invoke(direction);
        }
    }

    public enum Direction
    {
        Top,
        Right,
        Down,
        Left
    }

    static class DirectionMethods
    {
        public static char GetName(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Top: return 'T';
                case Direction.Right: return 'R';
                case Direction.Down: return 'D';
                case Direction.Left: return 'L';
                default: throw new NotImplementedException();
            }
        }
    }
}
