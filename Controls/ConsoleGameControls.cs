using MarsGame.Entity;
using MarsGame.Loader;
using System;

namespace MarsGame.Controls
{
    public class ConsoleGameControls : LoggerGameControl
    {
        private int _stepNum = 0;

        public ConsoleGameControls(IMapLoader loader) : base(loader)
        {
        }

        public override void Init()
        {
            base.Init();
            Player.OnMove += OnPlayerMove;
        }

        public override void Start()
        {
            var handle = ConsoleService.GetConsoleWindow();
            ConsoleService.ShowWindow(handle, ConsoleService.SW_SHOW);

            StartAIControl();
        }

        public void OnPlayerMove(Direction direction)
        {
            Console.WriteLine($"{++_stepNum}: Player move to ({Player.X}, {Player.Y})");

            if (Map.Get(Player.X, Player.Y).IsQuit)
            {
                Console.WriteLine("Quit");
                Console.WriteLine("---------------------");
            }
        }
    }
}
