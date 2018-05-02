using System;
using System.Collections.Generic;
using static System.Math;
namespace Checkers_LogicAndDataSection
{
    public class GameBoard
    {

        private List <Soldier> computerArmy = null;//in case of cpu this well get filled
       // private List<Soldier> playerArmy = null;//in case of cpu that we want to practice against each other this well get filled


        private Soldier[,] m_CheckersBoard = null;
        public enum eSoldierRanks { Regular = 1, King = 4 }
        public class Soldier
        {
            private static Point nextPointToFillPlayer1;
            private static Point nextPointToFillPlayer2;
            private Point m_CoordinateInMatrix;
            internal List<CheckersGameStep> m_PossibleRegularMovements = null;
            internal List<CheckersGameStep> m_PossibleEatMovements = null;

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

            public List<CheckersGameStep> eatPossibleMovements
            {
                get { return m_PossibleEatMovements; }
                set { m_PossibleEatMovements = value; }
            }
            public List<CheckersGameStep> regularPossibleMovements
            {
                get { return m_PossibleRegularMovements; }
                set { m_PossibleRegularMovements = value; }
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
                returnedSoldier.regularPossibleMovements = calculateInitPossibleMovements(i_PositionInMatrix,i_Team);
                returnedSoldier.Team = i_Team;
                returnedSoldier.Rank = eSoldierRanks.Regular;
                return returnedSoldier;
            }

            public static List<CheckersGameStep> calculateInitPossibleMovements(Point i_CurrentSoldierPosition,ePlayerOptions playerId)
            {


                List<CheckersGameStep> resultPossibleMovesArray = null;

                int indexForTopRow = 0;
                if (SessionData.m_currentActivePlayer == ePlayerOptions.Player1)
                {
                    switch (SessionData.m_BoardSize)
                    {
                        case eBoardSizeOptions.SmallBoard:
                            indexForTopRow = 4;
                            break;
                        case eBoardSizeOptions.MediumBoard:
                            indexForTopRow = 5;
                            break;

                        case eBoardSizeOptions.LargeBoard:
                            indexForTopRow = 6;
                            break;


                    }
                }
                else
                {
                    switch (SessionData.m_BoardSize)
                    {
                        case eBoardSizeOptions.SmallBoard:
                            indexForTopRow = 1;

                            break;
                        case eBoardSizeOptions.MediumBoard:
                            indexForTopRow = 2;
                            break;

                        case eBoardSizeOptions.LargeBoard:
                            indexForTopRow = 3;
                            break;


                    }
                }
                resultPossibleMovesArray = resetPossibleMovesArray(indexForTopRow, i_CurrentSoldierPosition,playerId);
                return resultPossibleMovesArray;





            }
            private static List<CheckersGameStep> resetPossibleMovesArray(int indexOfTopRow, Point i_CurrentSoldierPosition,ePlayerOptions playerId)
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
                    if (playerId == ePlayerOptions.Player1)
                    {
                    MoveToTheLeft.x = i_CurrentSoldierPosition.x - 1;
                    MoveToTheLeft.y = i_CurrentSoldierPosition.y - 1;

                    
                    MoveToTheRight.x = i_CurrentSoldierPosition.x + 1;
                    MoveToTheRight.y = i_CurrentSoldierPosition.y - 1;
                    }
                    else
                    {
                        MoveToTheLeft.x = i_CurrentSoldierPosition.x - 1;
                        MoveToTheLeft.y = i_CurrentSoldierPosition.y + 1;


                        MoveToTheRight.x = i_CurrentSoldierPosition.x + 1;
                        MoveToTheRight.y = i_CurrentSoldierPosition.y + 1;
                    }


                    stepToTheLeft.RequestedPosition = MoveToTheLeft;
                    stepToTheRight.RequestedPosition = MoveToTheRight;

                    if (stepToTheLeft.RequestedPosition.x >= 0 && stepToTheLeft.RequestedPosition.x < (int)SessionData.m_BoardSize)
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

        internal void MoveSoldier(CheckersGameStep io_MoveToExecute)
        {
            Soldier theOneWeMove = GetSoldierFromMatrix(io_MoveToExecute.CurrentPosition);
            m_CheckersBoard[io_MoveToExecute.CurrentPosition.x, io_MoveToExecute.CurrentPosition.y] = null;
            m_CheckersBoard[io_MoveToExecute.RequestedPosition.x, io_MoveToExecute.RequestedPosition.y] = theOneWeMove;
            
            if(io_MoveToExecute.moveTypeInfo.moveType == eMoveTypes.EatMove)
            {
                Point eatenSoldierLocation = calculatePositionOfEatenSoldier(io_MoveToExecute);
                Soldier eatenSoldier = GetSoldierFromMatrix(eatenSoldierLocation);
                m_CheckersBoard[eatenSoldier.Position.x, eatenSoldier.Position.y] = null;
            }

            
        }
        private Point calculatePositionOfEatenSoldier(CheckersGameStep i_move)
        {
            Point resultPosition = new Point();

            resultPosition.x = i_move.CurrentPosition.x + ((i_move.CurrentPosition.x-i_move.RequestedPosition.x)/2);
            resultPosition.y = i_move.CurrentPosition.y + ((i_move.CurrentPosition.y - i_move.RequestedPosition.y) / 2);

            return resultPosition;
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

                if(SessionData.gameType==eTypeOfGame.singlePlayer)//preperation for minimal dataBase of moves for AI
                {
                    if(computerArmy == null)
                    {
                        computerArmy = new List<Soldier>(NumberOfSoldiers);
                        //playerArmy = new List<Soldier>(NumberOfSoldiers); //AI-practice-MODE
                    }
                    computerArmy.Add(m_CheckersBoard[localPointPlayer2.y, localPointPlayer2.x]);
                    //playerArmy.Add(m_CheckersBoard[localPointPlayer2.y, localPointPlayer2.x]);////AI-Practice-MODE

                }



                Soldier.moveToNextPlace();

            }


        }
        public CheckersGameStep.MoveType SortMoveType(CheckersGameStep i_RequestedMove)
        {
            Soldier currentPositonSoldier = GetSoldierFromMatrix(i_RequestedMove.CurrentPosition);
            Soldier NextPositonSoldier = GetSoldierFromMatrix(i_RequestedMove.RequestedPosition);
            CheckersGameStep.MoveType result = new CheckersGameStep.MoveType();
            List<CheckersGameStep> arrayOfMovements;
            bool validity = true;
            bool exists = false;

            if (currentPositonSoldier == null)
            {
                validity = false;
            }
            if (validity && currentPositonSoldier.Team != SessionData.m_currentActivePlayer)
            {
                validity = false;
            }
            if (validity && NextPositonSoldier != null)
            {
                validity = false;
            }
            if (validity)
            {
                result = CheckersGameStep.MoveType.CalculateMoveType(i_RequestedMove);

                if (result.moveType != eMoveTypes.Undefined)
                    if (result.moveType == eMoveTypes.EatMove)
                        arrayOfMovements = currentPositonSoldier.eatPossibleMovements;
                    else
                        arrayOfMovements = currentPositonSoldier.regularPossibleMovements;
                else
                    arrayOfMovements = null;



                foreach (CheckersGameStep step in arrayOfMovements)
                {
                    if (step.Equals(i_RequestedMove))
                    {
                        exists = true;
                    }
                }
            }

            if (!validity || !exists)
                result.moveType = eMoveTypes.Undefined;


            return result;
        }
        public Soldier GetSoldierFromMatrix(Point i_GivenCoordinate)
        {
            return m_CheckersBoard[i_GivenCoordinate.y, i_GivenCoordinate.x];
        }
        //internal MovementType MoveSoldier(CheckersGameStep io_MoveToExecute)
        //{

        //}

    }
}
