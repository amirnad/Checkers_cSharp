using System;
namespace Checkers_LogicAndDataSection
{
    public enum eTypeOfGame { Undefined, singlePlayer, doublePlayer }
    public enum eBoardSizeOptions { Undefined, SmallBoard = 6, MediumBoard = 8, LargeBoard = 10 }
    public enum eGameState { KeepGoing = 0, Tie, WinPlayer1, WinPlayer2, player1Quit, player2Quit, StartOver, Quit, Undefined }
    
    public class SessionData
    {
        public static eBoardSizeOptions m_BoardSize = eBoardSizeOptions.Undefined;
        public static int m_Player1OverallScore = 0;
        public static int m_Player2OverallScore = 0;
        public static int m_Player1Points = 0;
        public static int m_Player2Points = 0;
        public static eTypeOfGame gameType = eTypeOfGame.Undefined;
        public static string lastMove;
        public static ePlayerOptions m_currentActivePlayer = ePlayerOptions.Player1;
        public static ePlayerOptions m_theOtherPlayer;

        private readonly static Player m_Player1 = new Checkers_LogicAndDataSection.Player();
        private readonly static Player m_Player2 = new Checkers_LogicAndDataSection.Player();

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

        public static string GetPlayerName(ePlayerOptions i_PlayerNameToReturn)
        {
            string returnedName = String.Empty;
            switch (i_PlayerNameToReturn)
            {
                case ePlayerOptions.Player1:
                    returnedName = m_Player1.PlayerName;
                    break;
                case ePlayerOptions.Player2:
                    returnedName = m_Player2.PlayerName;
                    break;
            }
            return returnedName;
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
            if (!m_Player1.SomeBodyAlive())
            {
                resultState = eGameState.WinPlayer2;
            }
            else if (!m_Player2.SomeBodyAlive())
            {
                resultState = eGameState.WinPlayer1;
            }
            else
            {
                if (!m_Player1.ThereIsPossibleMovements() && !m_Player2.ThereIsPossibleMovements())
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

        public static void CalculateScore(eGameState io_gameState)
        {
            switch (io_gameState)
            {
                case eGameState.WinPlayer1:
                    updateSessionPoints(m_Player1, m_Player2, ref m_Player1Points);
                    //pointsTempHolder = m_Player1.NumberOfSoldiers - m_Player2.NumberOfSoldiers;
                    //m_Player1Points += updateSessionPoints(pointsTempHolder);
                    m_Player1OverallScore++;
                    break;
                case eGameState.WinPlayer2:
                    updateSessionPoints(m_Player2, m_Player1, ref m_Player2Points);
                    //pointsTempHolder = m_Player2.NumberOfSoldiers - m_Player1.NumberOfSoldiers;
                    //m_Player2Points = updateSessionPoints(pointsTempHolder);
                    m_Player2OverallScore++;
                    break;
                case eGameState.player1Quit:
                    m_Player2OverallScore++;
                    break;
                case eGameState.player2Quit:
                    m_Player1OverallScore++;
                    break;
            }
        }

        private static void updateSessionPoints(Player i_WinningPlayer, Player i_LosingPlayer, ref int o_WinningPlayerCurrentPoints)
        {
            int pointsHolder = i_WinningPlayer.CalculateTeamValue() - i_LosingPlayer.CalculateTeamValue();
            o_WinningPlayerCurrentPoints += pointsHolder;
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
