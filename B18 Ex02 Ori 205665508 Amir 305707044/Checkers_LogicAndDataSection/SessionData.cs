using System;
namespace Checkers_LogicAndDataSection
{
    public enum eBoardSizeOptions { Undefined, SmallBoard = 6, MediumBoard = 8, LargeBoard = 10 }
    public class SessionData
    {
        public static eBoardSizeOptions m_BoardSize = eBoardSizeOptions.Undefined;
        private static int m_SessionScore;
        private static int m_SessionPoints;
        public static string lastMove;

        public static void Main()
        {
            string name = System.Console.ReadLine();
            Player p1 = new Player();

            p1.InitializePlayer(name);

        }
    }
}
