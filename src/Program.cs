using System;

namespace GameXna
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameXna game = new GameXna())
            {
                game.Run();
            }
        }
    }
}

