using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers_LogicAndDataSection;
using Checkers_UI;

namespace Checkers_GameManager
{

    class Program
    {

        public static void Main()
        {
            CheckersGame myGame = new CheckersGame();
            myGame.RunCheckersGame();

        }
    }

    public class CheckersGame
    {
        private const int k_maxGamePlayers = 2;
        private bool m_IsGameOn = false;
        private Checkers_LogicAndDataSection.GameBoard m_CheckersBoard = new Checkers_LogicAndDataSection.GameBoard();
        private Checkers_LogicAndDataSection.Player m_Player1 = new Checkers_LogicAndDataSection.Player();
        private Checkers_LogicAndDataSection.Player m_Player2 = new Checkers_LogicAndDataSection.Player();
        private Player m_currentActivePlayer;
        private bool m_isRequestedMoveLegal = false;
        private Checkers_LogicAndDataSection.CheckersGameStep m_RequestedMove = new Checkers_LogicAndDataSection.CheckersGameStep();

        public void RunCheckersGame()
        {
            m_IsGameOn = true;
            Checkers_LogicAndDataSection.InitialGameSetting GameDemoSettings = setup();//Checkers_UI.class.setup
            Checkers_LogicAndDataSection.SessionData.initializeSessionData(GameDemoSettings);

            InitializePlayers(GameDemoSettings);
            InitializeGameBoard();

            while (m_IsGameOn)
            {
                m_currentActivePlayer = GetNextPlayer();

                while (!m_isRequestedMoveLegal)
                {
                    
                    m_RequestedMove = input.ReadAndCheckInput();
                    if (Checkers_LogicAndDataSection.GameBoard.CheckIfMoveIsOk(m_RequestedMove))
                    {
                        m_isRequestedMoveLegal = true;
                    }
                }
                m_currentActivePlayer.MakeAMove(m_RequestedMove, m_CheckersBoard); //at the end of this method - we are ready to get the next move in the game
            }



        }

  
        private Player GetNextPlayer()
        {

        }

        private void InitializePlayers(InitialGameSetting i_NameSettings)
        {
            m_Player1.InitializePlayer(i_NameSettings.player1Name, Checkers_LogicAndDataSection.ePlayerOptions.Player1);
            switch (Checkers_LogicAndDataSection.SessionData.gameType)
            {
                case Checkers_LogicAndDataSection.eTypeOfGame.singlePlayer:
                    m_Player2.InitializePlayer("PC", Checkers_LogicAndDataSection.ePlayerOptions.ComputerPlayer);
                    break;
                case Checkers_LogicAndDataSection.eTypeOfGame.doublePlayer:
                    m_Player2.InitializePlayer(i_NameSettings.player2Name, Checkers_LogicAndDataSection.ePlayerOptions.Player2);
                    break;
            }
        }

        private Checkers_LogicAndDataSection.InitialGameSetting setup()
        {
            Checkers_LogicAndDataSection.InitialGameSetting res = new Checkers_LogicAndDataSection.InitialGameSetting();

            res.player1Name = "amir";
            res.player2Name = "ori";
            res.gameType = Checkers_LogicAndDataSection.eTypeOfGame.doublePlayer;
            res.boardSize = Checkers_LogicAndDataSection.eBoardSizeOptions.MediumBoard;

            return res;
        }
    }


}

