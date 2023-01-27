using System;
using MarsGame.Model;

namespace MarsGame.Entity
{
    public delegate void OnMove(Direction direction);

    public class Player
    {
        public Point Position { get; protected set; }

        public event OnMove OnMove;

        public Player() : this(new Point(0, 0)) { }

        public Player(Point position)
        {
            Position = position;
        }

        public void MoveTo(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Top:
                    Position.Y -= 1;
                    break;
                case Direction.Right:
                    Position.X += 1;
                    break;
                case Direction.Down:
                    Position.Y += 1;
                    break;
                case Direction.Left:
                    Position.X -= 1;
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
