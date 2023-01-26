using MarsGame.Model;
using MarsGame.Loader;
using MarsGame.Entity;

namespace MarsGame.Controls
{
    public abstract class AIGameControls : GameControl
    {
        public AIGameControls(IMapLoader loader) : base(loader)
        {
        }

        public virtual void OnWayFinish() { }

        public virtual void MakeDelay() { }

        public void StartAIControl()
        {
            Cell start = Map.FindByCondition((cell) => cell.IsStart);
            Cell quit = Map.FindByCondition((cell) => cell.IsQuit);

            Way way = Hunter.FindWay(start, quit);
            if (way != null)
            {
                Loop(way);
            }

            OnWayFinish();
        }

        private void Loop(Way way)
        {
            for (int i = 0; i < way.Size; i++)
            {
                MakeDelay();

                Cell curr = way.History[i];
                Cell next = way.History[i + 1];

                Direction d = GetDirectionByNodes(curr, next);
                Player.Move(d);
            }
        }

        private Direction GetDirectionByNodes(Cell c1, Cell c2)
        {
            if (c1.X == c2.X)
            {
                return (c1.Y > c2.Y ? Direction.Top : Direction.Down);
            }
            else
            {
                return (c1.X > c2.X ? Direction.Left : Direction.Right);
            }
        }
    }
}
