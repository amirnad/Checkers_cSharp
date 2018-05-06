using System;
using Checkers_LogicAndDataSection;

namespace Checkers_UI
{

    public class CheckersGame
    {


        private eGameState m_gameState = eGameState.KeepGoing;
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
            setup(out GameDemoSettings);

            //UI.ReadGameInitialInputFromUser(out GameDemoSettings);


            Checkers_LogicAndDataSection.SessionData.initializeSessionData(GameDemoSettings);
            SessionData.InitializePlayers(GameDemoSettings);
            m_CheckersBoard.InitializeCheckersBoard();

            Ex02.ConsoleUtils.Screen.Clear();
            UI.PrintCheckersBoard(m_CheckersBoard);


            while (m_gameState == eGameState.KeepGoing || m_gameState == eGameState.StartOver)
            {
                if (m_gameState == eGameState.StartOver)
                {
                    InitializeAnotherGame(GameDemoSettings);
                    Ex02.ConsoleUtils.Screen.Clear();
                    UI.PrintCheckersBoard(m_CheckersBoard);
                    }
                m_currentActivePlayer = SessionData.GetCurrentPlayer();
                m_currentActivePlayer.updateArmy(m_CheckersBoard);
                m_isRequestedMoveLegal = false;

                while (!m_isRequestedMoveLegal)
                {
                    if (m_currentActivePlayer.Team != ePlayerOptions.ComputerPlayer)
                    {
                        m_RequestedMove = UI.ReadGameMove(ref userMoveInput);
                    }
                    //               else
                    //               {
                    //             m_RequestedMove = m_currentActivePlayer.Com
                    //                }

                    m_RequestedMove.moveTypeInfo = m_CheckersBoard.SortMoveType(m_RequestedMove, m_currentActivePlayer);//been recently changed from check for logic wise -> at this time of writing the array of possible moves is working and there for we should only check if one of the moves is allowed.


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
                    UI.PrintLastMove(userMoveInput, m_currentActivePlayer.PlayerName);
                    m_gameState = SessionData.checkGameState();
                }
                else
                {
                    if (SessionData.m_currentActivePlayer == ePlayerOptions.Player1)
                        m_gameState = eGameState.player1Quit;
                    else
                        m_gameState = eGameState.player2Quit;
                }
                if (m_gameState != eGameState.KeepGoing)
                {
                    SessionData.CalculateScore(m_gameState);
                    UI.PrintGameResult(m_gameState);
                    m_gameState = UI.CheckIfPlayerWantsAnotherGame();

                }
            }

        }

        private void InitializeAnotherGame(InitialGameSetting o_GameDemoSettings)
        {
            SessionData.m_currentActivePlayer = ePlayerOptions.Player1;
            m_CheckersBoard.InitializeCheckersBoard();
        }

        private void setup(out InitialGameSetting o_Settings)
        {
            o_Settings = new InitialGameSetting();
            o_Settings.SetGameSettings("Ori", "Amir", eBoardSizeOptions.SmallBoard, eTypeOfGame.doublePlayer);
        }
    }
}
