using System;

namespace Checkers_LogicAndDataSection
{
    public enum ePlayerOptions { Player1, Player2, ComputerPlayer }
    public class Player
    {
        private const int k_NoSoldiers = 0;
        private const int k_NumberOfSoldiersInSmallBoard = 9;
        private const int k_NumberOfSoldiersInMediumBoard = 12;
        private const int k_NumberOfSoldiersInLargeBoard = 15;


        private ePlayerOptions m_PlayerId;
        private string m_PlayerName = string.Empty;
        private short m_NumberOfSoldiers;

        private string playerName
        {
            get { return m_PlayerName; }
            set
            {
                m_PlayerName = value;
            }
        }

        private short numberOfSoldiers
        {
            get { return m_NumberOfSoldiers; }
            set
            {
                switch (SessionData.m_BoardSize)
                {
                    case eBoardSizeOptions.SmallBoard:
                        if (value > k_NoSoldiers && value <= k_NumberOfSoldiersInSmallBoard)
                            m_NumberOfSoldiers = value;
                        break;
                    case eBoardSizeOptions.MediumBoard:
                        if (value > k_NoSoldiers && value <= k_NumberOfSoldiersInMediumBoard)
                            m_NumberOfSoldiers = value;
                        break;
                    case eBoardSizeOptions.LargeBoard:
                        if (value > k_NoSoldiers && value <= k_NumberOfSoldiersInLargeBoard)
                            m_NumberOfSoldiers = value;
                        break;
                }

            }
        }
        public void InitializePlayer(string i_PlayerName, ePlayerOptions i_PlayerId = ePlayerOptions.Player1) // input is already checked in Manager and UI projects
        {
            playerName = i_PlayerName;
            m_PlayerId = i_PlayerId;

            switch (SessionData.m_BoardSize)
            {
                case eBoardSizeOptions.SmallBoard:
                    numberOfSoldiers = k_NumberOfSoldiersInSmallBoard;
                    break;
                case eBoardSizeOptions.MediumBoard:
                    numberOfSoldiers = k_NumberOfSoldiersInMediumBoard;
                    break;
                case eBoardSizeOptions.LargeBoard:
                    numberOfSoldiers = k_NumberOfSoldiersInLargeBoard;
                    break;
            }
        }

        public void MakeAMove(CheckersGameStep m_RequestedMove, GameBoard i_CheckersBoard)
        {
            
        }

        //public void move()
        //{
        //    switch(m_PlayerId)
        //    {
        //        case ePlayerOptions.ComputerPlayer:
        //            movePcPlayer();
        //            break;
        //        case ePlayerOptions.Player1:
        //        case ePlayerOptions.Player2:
        //            moveHumanPlayer();
        //            break;

        //    }
        //}
    }
}
