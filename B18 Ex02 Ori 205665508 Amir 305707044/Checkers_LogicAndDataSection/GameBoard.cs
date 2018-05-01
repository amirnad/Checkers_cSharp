using System;
using System.Collections.Generic;

namespace Checkers_LogicAndDataSection
{
    public class GameBoard
    {


        private Soldier[,] m_CheckersBoard = null;
        public enum eSoldierRanks { Regular = 1, King = 4 }
        public class Soldier
        {
            private static Point nextPointToFillPlayer1;
            private static Point nextPointToFillPlayer2;
            private Point m_CoordinateInMatrix;
            internal List<CheckersGameStep> m_PossibleMovements = null;
            private ePlayerOptions m_SoldierTeam;
            private eSoldierRanks m_Rank;


            public static Point PointToFillPlayer1
            {
                get { return nextPointToFillPlayer1; }
                private set
                {
                    nextPointToFillPlayer1 = value;
                }
            }
            public static Point PointToFillPlayer2
            {
                get { return nextPointToFillPlayer2; }
                set
                {
                    nextPointToFillPlayer2 = value;
                }
            }
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

            public static Soldier InitializeSoldier(Point i_PositionInMatrix, ePlayerOptions i_Team)
            {
                Soldier returnedSoldier = new Soldier();

                returnedSoldier.Position = i_PositionInMatrix;
                returnedSoldier.PossibleMovements = calculatePossibleMovements(i_PositionInMatrix);
                returnedSoldier.Team = i_Team;
                returnedSoldier.Rank = eSoldierRanks.Regular;
                return returnedSoldier;
            }

            public static List<CheckersGameStep> calculatePossibleMovements(Point i_CurrentSoldierPosition)
            {


                List<CheckersGameStep> resultPossibleMovesArray = new List<CheckersGameStep>();
                switch (SessionData.m_BoardSize)
                {
                    case eBoardSizeOptions.SmallBoard:
                        resultPossibleMovesArray = resetPossibleMovesArray(1, i_CurrentSoldierPosition);
                        break;
                    case eBoardSizeOptions.MediumBoard:
                        resultPossibleMovesArray = resetPossibleMovesArray(2, i_CurrentSoldierPosition);
                        break;

                    case eBoardSizeOptions.LargeBoard:
                        resultPossibleMovesArray = resetPossibleMovesArray(3, i_CurrentSoldierPosition);
                        break;


                }
                return resultPossibleMovesArray;





            }
            private static List<CheckersGameStep> resetPossibleMovesArray(int indexOfTopRow, Point i_CurrentSoldierPosition)
            {
                List<CheckersGameStep> resultPossibleMovesArray = new List<CheckersGameStep>();

                CheckersGameStep stepToTheLeft = new CheckersGameStep();
                CheckersGameStep stepToTheRight = new CheckersGameStep();

                if (i_CurrentSoldierPosition.y == indexOfTopRow)
                {
                    Point MoveToTheLeft = new Point();
                    Point MoveToTheRight = new Point();


                    stepToTheLeft.CurrentPosition = i_CurrentSoldierPosition;
                    stepToTheRight.CurrentPosition = i_CurrentSoldierPosition;

                    MoveToTheLeft.x = i_CurrentSoldierPosition.x - 1;
                    MoveToTheLeft.y = i_CurrentSoldierPosition.y + 1;

                    MoveToTheRight.x = i_CurrentSoldierPosition.x + 1;
                    MoveToTheRight.y = i_CurrentSoldierPosition.y + 1;

                    stepToTheLeft.RequestedPosition = MoveToTheLeft;
                    stepToTheRight.RequestedPosition = MoveToTheRight;

                    if(stepToTheLeft.RequestedPosition.x >=0 && stepToTheLeft.RequestedPosition.x < (int)SessionData.m_BoardSize)
                    {
                    resultPossibleMovesArray.Add(stepToTheLeft);
                    }
                    if (stepToTheRight.RequestedPosition.x >= 0 && stepToTheRight.RequestedPosition.x < (int)SessionData.m_BoardSize)
                    {
                        resultPossibleMovesArray.Add(stepToTheRight);
                    }


                }

                    return resultPossibleMovesArray;


            }

            internal static void initializeNextPointToFill()
            {
                int boardSize = (int)SessionData.m_BoardSize;
                nextPointToFillPlayer2.x = 1;
                nextPointToFillPlayer2.y = 0;

                nextPointToFillPlayer1.x = 0;
                nextPointToFillPlayer1.y = boardSize - 1;
            }

            internal static void moveToNextPlace()
            {
                Point localPoint1 = PointToFillPlayer1;
                Point localPoint2 = PointToFillPlayer2;

                localPoint2.x += 2;
                int boardSize = (int)SessionData.m_BoardSize;
                if (localPoint2.x >= boardSize)
                {
                    localPoint2.y++;
                    if (localPoint2.y % 2 != 0)
                    {
                        localPoint2.x = 0;
                    }
                    else
                    {
                        localPoint2.x = 1;
                    }
                }
                localPoint1.x += 2;
                if (localPoint1.x >= boardSize)
                {
                    localPoint1.y--;
                    if (localPoint1.y % 2 != 0)
                    {
                        localPoint1.x = 0;
                    }
                    else
                    {
                        localPoint1.x = 1;
                    }
                }

                PointToFillPlayer1 = localPoint1;
                PointToFillPlayer2 = localPoint2;



            }
        }



        public void InitializeCheckersBoard()
        {

            Soldier.initializeNextPointToFill();
            switch (SessionData.m_BoardSize)
            {
                case eBoardSizeOptions.SmallBoard:
                    m_CheckersBoard = new Soldier[(int)eBoardSizeOptions.SmallBoard, (int)eBoardSizeOptions.SmallBoard];
                    break;
                case eBoardSizeOptions.MediumBoard:
                    m_CheckersBoard = new Soldier[(int)eBoardSizeOptions.MediumBoard, (int)eBoardSizeOptions.MediumBoard];
                    break;
                case eBoardSizeOptions.LargeBoard:
                    m_CheckersBoard = new Soldier[(int)eBoardSizeOptions.LargeBoard, (int)eBoardSizeOptions.LargeBoard];
                    break;
            }

            switch (SessionData.m_BoardSize)
            {

                case eBoardSizeOptions.SmallBoard:
                    InitializeAllSoldiersOnBoard(Player.k_NumberOfSoldiersInSmallBoard);
                    break;
                case eBoardSizeOptions.MediumBoard:
                    InitializeAllSoldiersOnBoard(Player.k_NumberOfSoldiersInMediumBoard);
                    break;
                case eBoardSizeOptions.LargeBoard:
                    InitializeAllSoldiersOnBoard(Player.k_NumberOfSoldiersInLargeBoard);
                    break;
            }


        }

        private void InitializeAllSoldiersOnBoard(int NumberOfSoldiers)
        {
            Point localPointPlayer1 = new Point();
            Point localPointPlayer2 = new Point();


            for (int i = 0; i < NumberOfSoldiers; i++)
            {

                localPointPlayer1 = Soldier.PointToFillPlayer1;
                localPointPlayer2 = Soldier.PointToFillPlayer2;

                m_CheckersBoard[localPointPlayer1.y, localPointPlayer1.x] = Soldier.InitializeSoldier(localPointPlayer1, ePlayerOptions.Player1);
                m_CheckersBoard[localPointPlayer2.y, localPointPlayer2.x] = Soldier.InitializeSoldier(localPointPlayer2, ePlayerOptions.Player2);

                Soldier.moveToNextPlace();

            }


        }

        public Soldier GetSoldierFromMatrix(Point i_GivenCoordinate)
        {
            return m_CheckersBoard[i_GivenCoordinate.y, i_GivenCoordinate.x];
        }
        //internal MovementType MoveSoldier(CheckersGameStep io_MoveToExecute)
        //{

        //}
        public eMoveTypes SortMoveType(CheckersGameStep i_RequestedMove)
        {
            Soldier currentPositonSoldier = GetSoldierFromMatrix(i_RequestedMove.CurrentPosition);
            Soldier NextPositonSoldier = GetSoldierFromMatrix(i_RequestedMove.RequestedPosition);
            bool validity = true;

            eMoveTypes result = eMoveTypes.Undefined;

            if (currentPositonSoldier == null)
                validity = false;



            if (!validity)
                result = eMoveTypes.Undefined;


            return result;
        }
    }
}
