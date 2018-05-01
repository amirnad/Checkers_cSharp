using System;

namespace Checkers_LogicAndDataSection
{
    public enum ePlayerOptions { Player1, Player2, ComputerPlayer }
    public enum eMoveTypes { EatMove, RegularMove , Undefined }
    

    public class Player
    {
        public const int k_NoSoldiers = 0;
        public const int k_NumberOfSoldiersInSmallBoard = 6;
        public const int k_NumberOfSoldiersInMediumBoard = 12;
        public const int k_NumberOfSoldiersInLargeBoard = 20;


        private ePlayerOptions m_PlayerId;
        private string m_PlayerName = string.Empty;
        private short m_NumberOfSoldiers;

        private string PlayerName
        {
            get { return m_PlayerName; }
            set
            {
                m_PlayerName = value;
            }
        }

        private short NumberOfSoldiers
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
        public ePlayerOptions Team
        {
            get { return m_PlayerId; }
            set
            {
                m_PlayerId = value;
            }
        }

        public void InitializePlayer(string i_PlayerName, ePlayerOptions i_PlayerId = ePlayerOptions.Player1) // input is already checked in Manager and UI projects
        {
            PlayerName = i_PlayerName;
            Team = i_PlayerId;

            switch (SessionData.m_BoardSize)
            {
                case eBoardSizeOptions.SmallBoard:
                    NumberOfSoldiers = k_NumberOfSoldiersInSmallBoard;
                    break;
                case eBoardSizeOptions.MediumBoard:
                    NumberOfSoldiers = k_NumberOfSoldiersInMediumBoard;
                    break;
                case eBoardSizeOptions.LargeBoard:
                    NumberOfSoldiers = k_NumberOfSoldiersInLargeBoard;
                    break;
            }
        }

        //public void MakeAMove(CheckersGameStep io_MoveToExecute, GameBoard io_CheckersBoard)
        //{
        //    GameBoard.Soldier currentSoldierToMove = io_CheckersBoard.GetSoldierFromMatrix(io_MoveToExecute.CurrentPosition);
        //    MovementType typeOfMove;
        //    switch (m_PlayerId)
        //    {
        //        case ePlayerOptions.ComputerPlayer:
        //            break;
        //        case ePlayerOptions.Player1:
        //        case ePlayerOptions.Player2:
        //            typeOfMove = io_CheckersBoard.MoveSoldier(io_MoveToExecute);
        //            break;
        //    }

        //    currentSoldierToMove.UpdatePossibleMovements(typeOfMove);
        //    if (typeOfMove.KingMove)
        //    {
        //        currentSoldierToMove.BecomeAKing();
        //    }

        //    if (typeOfMove.Type != eMoveTypes.EatMove)
        //    {
        //        SessionData.ChangeTurn();
        //    }
        //    else
        //    {
        //        if (currentSoldierToMove.PossibleMovements.length == 0)
        //        {
        //            SessionData.ChangeTurn();
        //        }
        //    }
        //    //here was supposed to be else --> do nothing cuz we dont want switch turns --> player ate a soldier and can creat a combo
        //}

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
