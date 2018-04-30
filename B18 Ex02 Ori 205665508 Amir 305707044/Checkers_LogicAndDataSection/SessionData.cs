using System;
namespace Checkers_LogicAndDataSection
{
    public enum eTypeOfGame { Undefined,singlePlayer,doublePlayer}
    public enum eBoardSizeOptions { Undefined, SmallBoard = 6, MediumBoard = 8, LargeBoard = 10 }

    public class InitialGameSetting
    {
        public string player1Name = string.Empty;
        public string player2Name = string.Empty;
        public Checkers_LogicAndDataSection.eBoardSizeOptions boardSize = Checkers_LogicAndDataSection.eBoardSizeOptions.Undefined;
        public Checkers_LogicAndDataSection.eTypeOfGame gameType = Checkers_LogicAndDataSection.eTypeOfGame.Undefined;
    }

    public class SessionData
    {
        public static eBoardSizeOptions m_BoardSize = eBoardSizeOptions.Undefined;
        public static int m_SessionScore;
        public static int m_SessionPoints;
        public static eTypeOfGame gameType = eTypeOfGame.Undefined;
        public static string lastMove;
        public static ePlayerOptions m_currentActivePlayer = ePlayerOptions.Player1;

        public static void Main()
        {
            string name = System.Console.ReadLine();
            Player p1 = new Player();

            p1.InitializePlayer(name);

        }
        public static void initializeSessionData(InitialGameSetting gameSettings)
        {
            m_BoardSize = gameSettings.boardSize;
            gameType = gameSettings.gameType;

            

        }

    }
}
