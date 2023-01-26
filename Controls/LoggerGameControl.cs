using System.IO;
using MarsGame.Entity;
using MarsGame.Loader;

namespace MarsGame.Controls
{
    public abstract class LoggerGameControl : AIGameControls
    {
        private static readonly string ResultFile = "moves.txt";

        private string _way;

        public LoggerGameControl(IMapLoader loader) : base(loader)
        {
        }

        public override void Init()
        {
            base.Init();
            Player.OnMove += OnPlayerMove;
        }

        private void OnPlayerMove(Direction direction)
        {
            _way += direction.GetName();
        }

        public override void OnWayFinish()
        {
            using FileStream fileStream = File.Create(ResultFile);
            using StreamWriter writer = new StreamWriter(fileStream);

            writer.WriteLine(_way.Length);
            writer.Write(_way);

            writer.Flush();
        }
    }
}
