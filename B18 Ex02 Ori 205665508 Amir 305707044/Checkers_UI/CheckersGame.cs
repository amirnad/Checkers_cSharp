using System;
using Checkers_LogicAndDataSection;

namespace Checkers_UI
{

    public class CheckersGame
    {


        Checkers_LogicAndDataSection.eGameState gameState = eGameState.KeepGoing;
        private const int k_maxGamePlayers = 2;
        private bool m_IsGameOn = false;
        private GameBoard m_CheckersBoard = new Checkers_LogicAndDataSection.GameBoard();

        private Player m_currentActivePlayer;//o.k
        private bool m_isRequestedMoveLegal = false;
        private CheckersGameStep m_RequestedMove = new Checkers_LogicAndDataSection.CheckersGameStep();

        public void RunCheckersGame()
        {
            m_IsGameOn = true;
            string userMoveInput = String.Empty;

            Checkers_LogicAndDataSection.InitialGameSetting GameDemoSettings;//Checkers_UI.class.setup
            UI.ReadGameInitialInputFromUser(out GameDemoSettings);


            Checkers_LogicAndDataSection.SessionData.initializeSessionData(GameDemoSettings);
            SessionData.InitializePlayers(GameDemoSettings);
            m_CheckersBoard.InitializeCheckersBoard();

            Ex02.ConsoleUtils.Screen.Clear();
            UI.PrintCheckersBoard(m_CheckersBoard);


            while (gameState == eGameState.KeepGoing)
            {
                m_currentActivePlayer = SessionData.GetCurrentPlayer();
                m_isRequestedMoveLegal = false;

                while (!m_isRequestedMoveLegal)
                {
                    if (m_currentActivePlayer.Team != ePlayerOptions.ComputerPlayer)
                    {
                        m_RequestedMove = UI.ReadGameMove(ref userMoveInput);
                    }
                    //               else
                    //               {
                    //             m_RequestedMove = m_currentActivePlayer.
                    //                }

                    m_RequestedMove.moveTypeInfo = m_CheckersBoard.SortMoveType(m_RequestedMove);//been recently changed from check for logic wise -> at this time of writing the array of possible moves is working and there for we should only check if one of the moves is allowed.


                    if (m_RequestedMove.moveTypeInfo.moveType != eMoveTypes.Undefined || m_RequestedMove.quit)
                    {
                        m_isRequestedMoveLegal = true;
                    }
                    if (!m_isRequestedMoveLegal)
                    {
                        Output.InputException();
                    }
                }
                if (!m_RequestedMove.quit)
                {
                    m_currentActivePlayer.MakeAMove(m_RequestedMove, m_CheckersBoard); //at the end of this method - we are ready to get the next move in the game
                    Ex02.ConsoleUtils.Screen.Clear();
                    UI.PrintCheckersBoard(m_CheckersBoard);
                    UI.PrintLastMove(userMoveInput);
                    gameState = SessionData.checkGameState();
                }
                else
                {
                    if (SessionData.m_currentActivePlayer == ePlayerOptions.Player1)
                        gameState = eGameState.player1Quit;
                    else
                        gameState = eGameState.player2Quit;
                }
            }



        }






        //private Checkers_LogicAndDataSection.InitialGameSetting setup()
        //{
        //    Checkers_LogicAndDataSection.InitialGameSetting res = new Checkers_LogicAndDataSection.InitialGameSetting();

        //    res.player1Name = "amir";
        //    res.player2Name = "ori";
        //    res.gameType = Checkers_LogicAndDataSection.eTypeOfGame.singlePlayer;
        //    res.boardSize = Checkers_LogicAndDataSection.eBoardSizeOptions.MediumBoard;

        //    return res;
        //}
    }
}
