using System;
using MarsGame.Loader;
using MarsGame.Controls;
using System.Linq;

namespace MarsGame
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            IMapLoader loader = CreateLoader();
            GameControl control = CreateControl(loader);

            control.Init();
            control.Start();
        }

        static IMapLoader CreateLoader()
        {
            return new FileMapLoader();
        }

        static GameControl CreateControl(IMapLoader loader)
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Any("-console".Contains))
            {
                return new ConsoleGameControls(loader);
            }
            else
            {
                return new UIGameControl(loader);
            }
        }
    }
}
