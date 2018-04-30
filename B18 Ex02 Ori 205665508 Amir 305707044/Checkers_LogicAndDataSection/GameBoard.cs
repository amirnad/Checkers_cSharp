using System;
using System.Collections.Generic;

namespace Checkers_LogicAndDataSection
{
    public class GameBoard
    {
        public enum eSoldierRanks { Regular = 1, King = 4 }
        private class Soldier
        {
            private Point m_CoordinateInMatrix;
            internal List<CheckersGameStep> m_PossibleMovements = new List<CheckersGameStep>(4);
            private ePlayerOptions m_SoldierTeam;
            private eSoldierRanks m_Rank;

            public Point Position
            {
                get { return m_CoordinateInMatrix; }
                set
                {
                    m_CoordinateInMatrix = value;
                }
            }

            public List<CheckersGameStep> PossibleMovements
            {
                get { return m_PossibleMovements; }
                set { m_PossibleMovements = value; }
            }
            public ePlayerOptions Team
            {
                get { return m_SoldierTeam; }
                set { m_SoldierTeam = value; }
            }
            public eSoldierRanks Rank
            {
                get { return m_Rank; }
                set { m_Rank = value; }
            }

            public void InitializeSoldier(Point i_PositionInMatrix)
            {

                Position = i_PositionInMatrix;
                PossibleMovements = calculatePossibleMovements(Position);
                Team = SessionData.m_currentActivePlayer;
                Rank = eSoldierRanks.Regular;
            }

        }

        private Soldier[,] m_CheckersBoard = new Soldier[(int)SessionData.m_BoardSize, (int)SessionData.m_BoardSize];

        public void InitializeCheckersBoard(eBoardSizeOptions io_RequestedSize)
        {
            //switch(io_RequestedSize)
            //{
            //    case eBoardSizeOptions.SmallBoard:
            //        m_CheckersBoard = new Soldier[(int)eBoardSizeOptions.SmallBoard, (int)eBoardSizeOptions.SmallBoard];
            //        break;
            //    case eBoardSizeOptions.MediumBoard:
            //        m_CheckersBoard = new Soldier[(int)eBoardSizeOptions.MediumBoard, (int)eBoardSizeOptions.MediumBoard];
            //        break;
            //    case eBoardSizeOptions.LargeBoard:
            //        m_CheckersBoard = new Soldier[(int)eBoardSizeOptions.LargeBoard, (int)eBoardSizeOptions.LargeBoard];
            //        break;
            //}
            switch (SessionData.m_BoardSize)
            {

                case eBoardSizeOptions.SmallBoard:
                    Soldier[] Player1Soldiers = new Soldier[Player.k_NumberOfSoldiersInSmallBoard];
                    Soldier[] Player2Soldiers = new Soldier[Player.k_NumberOfSoldiersInSmallBoard];
                    foreach(Soldier s in Player1Soldiers)
                    {
                        s.InitializeSoldier()
                    }
                    break;
            }


        }


        internal Soldier GetSoldierFromMatrix(Point i_GivenCoordinate)
        {
            return m_CheckersBoard[i_GivenCoordinate.y, i_GivenCoordinate.x];


        }
        internal MovementType MoveSoldier(CheckersGameStep io_MoveToExecute)
        {

        }
    }
}
