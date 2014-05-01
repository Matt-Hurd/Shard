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
            //using (ShardGame game = new ShardGame())
            //{
            //    game.Run();
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TitleScreen());
        }
    }
#endif
}

