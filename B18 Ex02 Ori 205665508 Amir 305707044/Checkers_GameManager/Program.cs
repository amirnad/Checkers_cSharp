//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Checkers_LogicAndDataSection;
//using Checkers_UI;

//namespace Checkers_GameManager
//{

//    class Program
//    {

//        public static void Main()
//        {
//            CheckersGame myGame = new CheckersGame();


//            myGame.RunCheckersGame();

//        }
//    }

//    public class CheckersGame
//    {
//        private const int k_maxGamePlayers = 2;
//        private bool m_IsGameOn = false;
//        private Checkers_LogicAndDataSection.GameBoard m_CheckersBoard = new Checkers_LogicAndDataSection.GameBoard();

//        private Player m_currentActivePlayer;//o.k
//        private bool m_isRequestedMoveLegal = false;
//        private Checkers_LogicAndDataSection.CheckersGameStep m_RequestedMove = new Checkers_LogicAndDataSection.CheckersGameStep();

//        public void RunCheckersGame()
//        {
//            m_IsGameOn = true;

//            Checkers_LogicAndDataSection.InitialGameSetting GameDemoSettings = setup();//Checkers_UI.class.setup
//            Checkers_LogicAndDataSection.SessionData.initializeSessionData(GameDemoSettings);

//            SessionData.InitializePlayers(GameDemoSettings);
//            m_CheckersBoard.InitializeCheckersBoard();







            //while (m_IsGameOn)
            //{
            //    m_currentActivePlayer = SessionData.GetCurrentPlayer();
            //    m_isRequestedMoveLegal = false;

            //    while (!m_isRequestedMoveLegal)
            //    {
            //        m_RequestedMove = Input.ReadAndCheckInput();
            //        m_RequestedMove.moveTypeInfo = m_CheckersBoard.SortMoveType(m_RequestedMove);//been recently changed from check for logic wise -> at this time of writing the array of possible moves is working and there for we should only check if one of the moves is allowed.



//                    if (m_RequestedMove.moveTypeInfo.moveType != eMoveTypes.Undefined)
//                    {
//                        m_isRequestedMoveLegal = true;
//                    }
//                    if (!m_isRequestedMoveLegal)
//                    {
//                        Output.InputException();
//                    }
//                }
//                m_currentActivePlayer.MakeAMove(m_RequestedMove, m_CheckersBoard); //at the end of this method - we are ready to get the next move in the game
//            }



//        }









            //res.player1Name = "amir";
            //res.player2Name = "ori";
            //res.gameType = Checkers_LogicAndDataSection.eTypeOfGame.doublePlayer;
            //res.boardSize = Checkers_LogicAndDataSection.eBoardSizeOptions.MediumBoard;


//            return res;
//        }

//    }


//}

