using System;
using System.Threading;

namespace MarsGame
{
    public sealed class Executor
    {


        public static void RunOnNewThread(Action action)
        {
            Thread thread = new Thread(new ThreadStart(action))
            {
                IsBackground = true
            };
            thread.Start();
        }
    }
}
