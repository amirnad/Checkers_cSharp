using System;
using System.Collections.Generic;

namespace Checkers_LogicAndDataSection
{
    public enum ePlayerOptions { Player1, Player2, ComputerPlayer }
    public enum eMoveTypes { EatMove, RegularMove, Undefined }


    public class Player
    {
        public const int k_NoSoldiers = 0;
        public const int k_NumberOfSoldiersInSmallBoard = 6;
        public const int k_NumberOfSoldiersInMediumBoard = 12;
        public const int k_NumberOfSoldiersInLargeBoard = 20;


        private ePlayerOptions m_PlayerId;
        private string m_PlayerName = string.Empty;
        private short m_NumberOfSoldiers;
        private List<GameBoard.Soldier> playerArmy = null;

        public void updateArmy(GameBoard i_gameboard)
        {
            foreach(GameBoard.Soldier s in playerArmy)
            {
                s.calculatePossibleMovements(ref i_gameboard);
            }
        }


        internal void addToPlayerArmy(GameBoard.Soldier soldier)
        {
            playerArmy.Add(soldier);
        }
        internal void removeFromPlayerArmy(GameBoard.Soldier soldier)
        {
            playerArmy.Remove(soldier);
        }


        public void decrementNumberOfSoldier()
        {
            m_NumberOfSoldiers--;
        }

        public string PlayerName
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
        //changed from public to private
        public ePlayerOptions Team
        {
            get { return m_PlayerId; }
            private set
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
            playerArmy = new List<GameBoard.Soldier>(NumberOfSoldiers);

        }

        public void MakeAMove(CheckersGameStep io_MoveToExecute, GameBoard io_CheckersBoard)
        {
            GameBoard.Soldier currentSoldierToMove = io_CheckersBoard.GetSoldierFromMatrix(io_MoveToExecute.CurrentPosition);

            io_CheckersBoard.MoveSoldier(io_MoveToExecute);



            if (io_MoveToExecute.moveTypeInfo.kingMove)
            {
                currentSoldierToMove.BecomeAKing();
            }

            if (io_MoveToExecute.moveTypeInfo.moveType != eMoveTypes.EatMove)
            {
                SessionData.ChangeTurn();
            }
            else
            {
                if (currentSoldierToMove.eatPossibleMovements.Count == 0)
                {
                    SessionData.ChangeTurn();
                }
            }
            //if three well be an addition to what we do if a player has eaten another player and he have a chnce to eat another one itll be here
            //here was supposed to be else --> do nothing cuz we dont want switch turns --> player ate a soldier and can creat a combo
        }

        public bool SomeBodyAlive()
        {
            return m_NumberOfSoldiers>0;
        }
        public bool ThereIsPossibleMovements()
        {
            bool someBodyAlive = false;
            foreach (GameBoard.Soldier s in playerArmy)
            {
                if (s.m_PossibleEatMovements.Capacity != 0 || s.m_PossibleRegularMovements.Capacity != 0)
                {
                    someBodyAlive = true;
                    break;
                }
            }
            return someBodyAlive;
        }
    }
}
