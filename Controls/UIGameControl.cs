using System.Threading;
using System.Windows.Forms;
using MarsGame.Loader;

namespace MarsGame.Controls
{
    public sealed class UIGameControl : LoggerGameControl
    {
        public static readonly int CellSize = 30;

        public UIGameControl(IMapLoader loader) : base(loader)
        {
        }

        public override void Start()
        {
            var handle = ConsoleService.GetConsoleWindow();
            ConsoleService.ShowWindow(handle, ConsoleService.SW_HIDE);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Executor.RunOnNewThread(StartAIControl);

            Application.Run(CreateWindow());
        }

        private MainForm CreateWindow()
        {
            return new MainForm(Map, Player, Textures, CellSize)
            {
                MaximizeBox = false,
                MinimizeBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Mars Game"
            };
        }

        public override void MakeDelay()
        {
            Thread.Sleep(500);
        }

        public override void OnWayFinish()
        {
            base.OnWayFinish();
            Application.Exit();
        }
    }
}
