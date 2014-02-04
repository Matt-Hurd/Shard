using System;
using System.Windows.Forms;

namespace Shard
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
            //using (Game1 game = new Game1())
            //{
            //    game.Run();
            //}
        }
    }
#endif
}

