using System;
namespace Checkers_LogicAndDataSection
{
    public enum eTypeOfGame { Undefined,singlePlayer,doublePlayer}
    public enum eBoardSizeOptions { Undefined, SmallBoard = 6, MediumBoard = 8, LargeBoard = 10 }
    public enum eGameState { KeepGoing = 0, Tie, WinPlayer1, WinPlayer2 ,player1Quit,player2Quit}



    public class SessionData
    {
        public static eBoardSizeOptions m_BoardSize = eBoardSizeOptions.Undefined;
        public static int m_SessionScore;
        public static int m_SessionPoints;
        public static eTypeOfGame gameType = eTypeOfGame.Undefined;
        public static string lastMove;
        public static ePlayerOptions m_currentActivePlayer = ePlayerOptions.Player1;
        public static ePlayerOptions m_theOtherPlayer;

        readonly static Player m_Player1 = new Checkers_LogicAndDataSection.Player();
        readonly static Player m_Player2 = new Checkers_LogicAndDataSection.Player();

        public static Player GetCurrentPlayer()
        {
            if (SessionData.m_currentActivePlayer == ePlayerOptions.Player1)
            {
                return m_Player1;
            }
            else//pc also sits at player2 spot
            {
                return m_Player2;
            }
        }
        public static Player GetOtherPlayer()
        {
            if (SessionData.m_theOtherPlayer == ePlayerOptions.Player1)
            {
                return m_Player1;
            }
            else//pc also sits at player2 spot
            {
                return m_Player2;
            }
        }

        public static void InitializePlayers(InitialGameSetting i_NameSettings)
        {
            m_Player1.InitializePlayer(i_NameSettings.getPlayerName(ePlayerOptions.Player1), Checkers_LogicAndDataSection.ePlayerOptions.Player1);
            switch (Checkers_LogicAndDataSection.SessionData.gameType)
            {
                case Checkers_LogicAndDataSection.eTypeOfGame.singlePlayer:
                    m_Player2.InitializePlayer("PC", Checkers_LogicAndDataSection.ePlayerOptions.ComputerPlayer);
                    m_theOtherPlayer = ePlayerOptions.ComputerPlayer;
                    break;
                case Checkers_LogicAndDataSection.eTypeOfGame.doublePlayer:
                    m_Player2.InitializePlayer(i_NameSettings.getPlayerName(ePlayerOptions.Player2), Checkers_LogicAndDataSection.ePlayerOptions.Player2);
                    m_theOtherPlayer = ePlayerOptions.Player2;
                    break;
            }
        }

        public static eGameState checkGameState()
        {
            eGameState resultState;
            if(!m_Player1.SomeBodyAlive())
            {
                resultState = eGameState.WinPlayer2;
            }
            else if(!m_Player2.SomeBodyAlive())
            {
                resultState = eGameState.WinPlayer1;
            }
            else
            {
                if(!m_Player1.ThereIsPossibleMovements() && !m_Player2.ThereIsPossibleMovements())
                {
                    resultState = eGameState.Tie;
                }
                else
                {
                    resultState = eGameState.KeepGoing;
                }
            }
            return resultState;
        }

        public static void Main()
        {
            string name = System.Console.ReadLine();
            Player p1 = new Player();

            p1.InitializePlayer(name);

        }
        public static void initializeSessionData(InitialGameSetting gameSettings)
        {
            m_BoardSize = gameSettings.getBoardSize();
            gameType = gameSettings.getGameType();

            

        }

        internal static void ChangeTurn()
        {
            ePlayerOptions temp = m_currentActivePlayer;
            m_currentActivePlayer = m_theOtherPlayer;
            m_theOtherPlayer = temp;
        }
    }
}
