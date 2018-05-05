using System;
using System.Collections.Generic;
using static System.Math;
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
                returnedSoldier.regularPossibleMovements = calculateInitPossibleMovements(i_PositionInMatrix, i_Team);
                returnedSoldier.eatPossibleMovements = new List<CheckersGameStep>();
                returnedSoldier.Team = i_Team;
                returnedSoldier.Rank = eSoldierRanks.Regular;
                return returnedSoldier;
            }

            public static List<CheckersGameStep> calculateInitPossibleMovements(Point i_CurrentSoldierPosition, ePlayerOptions playerId)
            {


                List<CheckersGameStep> resultPossibleMovesArray = null;

                int indexForTopRow = 0;
                if (playerId == ePlayerOptions.Player1)
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
                resultPossibleMovesArray = resetPossibleMovesArray(indexForTopRow, i_CurrentSoldierPosition, playerId);
                return resultPossibleMovesArray;





            }

            internal void BecomeAKing()
            {
                throw new NotImplementedException();
            }



            private static List<CheckersGameStep> resetPossibleMovesArray(int indexOfTopRow, Point i_CurrentSoldierPosition, ePlayerOptions playerId)
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

            internal void calculatePossibleMovements(ref GameBoard board)
            {

                m_PossibleEatMovements.Clear();
                m_PossibleRegularMovements.Clear();

                CheckersGameStep gameStep;
                List<Point> inspectedPoints = bringPossibleNeigboursPositions(m_CoordinateInMatrix);
                foreach (Point p in inspectedPoints)
                {
                    Soldier s = board.GetSoldierFromMatrix(p);
                    if (s == null)
                    {
                        if (Rank == eSoldierRanks.King)
                        {
                            gameStep = CheckersGameStep.CreateCheckersGameStep(m_CoordinateInMatrix, p);
                            m_PossibleRegularMovements.Add(gameStep);
                        }
                        else//regular soldier
                        {
                            if (Team == ePlayerOptions.Player1)
                            {
                                if (p.y - Position.y < 0)
                                {
                                    gameStep = CheckersGameStep.CreateCheckersGameStep(m_CoordinateInMatrix, p);
                                    m_PossibleRegularMovements.Add(gameStep);
                                }
                            }
                            else
                            {
                                if (p.y - Position.y > 0)
                                {
                                    gameStep = CheckersGameStep.CreateCheckersGameStep(m_CoordinateInMatrix, p);
                                    m_PossibleRegularMovements.Add(gameStep);
                                }
                            }
                        }
                    }
                    else//there is a soldier there
                    {
                        Point PossibleEatingNextPosition;

                        Point localPointDiffrenceBetweenPoints;
                        localPointDiffrenceBetweenPoints.x = p.x - m_CoordinateInMatrix.x;
                        localPointDiffrenceBetweenPoints.y = p.y - m_CoordinateInMatrix.y;
                        if (Team != s.Team)
                        {
                            if (Rank == eSoldierRanks.King)
                            {

                                PossibleEatingNextPosition.x = p.x + localPointDiffrenceBetweenPoints.x;
                                PossibleEatingNextPosition.y = p.y + localPointDiffrenceBetweenPoints.y;

                                if (board.GetSoldierFromMatrix(PossibleEatingNextPosition) == null)//means the spot is clear
                                {
                                    gameStep = CheckersGameStep.CreateCheckersGameStep(m_CoordinateInMatrix, PossibleEatingNextPosition);
                                    m_PossibleEatMovements.Add(gameStep);
                                }
                            }
                            else//regular soldier
                            {
                                if (Team == ePlayerOptions.Player1)
                                {
                                    if (p.y - Position.y < 0)
                                    {
                                        PossibleEatingNextPosition.x = p.x + localPointDiffrenceBetweenPoints.x;
                                        PossibleEatingNextPosition.y = p.y + localPointDiffrenceBetweenPoints.y;
                                        if (PossibleEatingNextPosition.isInsideBoard())
                                        {

                                            if (board.GetSoldierFromMatrix(PossibleEatingNextPosition) == null)//means the spot is clear
                                            {
                                                gameStep = CheckersGameStep.CreateCheckersGameStep(m_CoordinateInMatrix, PossibleEatingNextPosition);
                                                m_PossibleEatMovements.Add(gameStep);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (p.y - Position.y > 0)
                                    {
                                        PossibleEatingNextPosition.x = p.x + localPointDiffrenceBetweenPoints.x;
                                        PossibleEatingNextPosition.y = p.y + localPointDiffrenceBetweenPoints.y;
                                        if (PossibleEatingNextPosition.isInsideBoard())
                                        {

                                            if (board.GetSoldierFromMatrix(PossibleEatingNextPosition) == null)//means the spot is clear
                                            {
                                                gameStep = CheckersGameStep.CreateCheckersGameStep(m_CoordinateInMatrix, PossibleEatingNextPosition);
                                                m_PossibleEatMovements.Add(gameStep);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


            }

            internal void killed(GameBoard gb)
            {

                Player p = SessionData.GetOtherPlayer();
                p.decrementNumberOfSoldier();
                Soldier eatenSoldier = this;
                p.removeFromPlayerArmy(eatenSoldier);
            }
        }

        internal void MoveSoldier(CheckersGameStep io_MoveToExecute)
        {
            Soldier theOneWeMove = GetSoldierFromMatrix(io_MoveToExecute.CurrentPosition);
            theOneWeMove.Position = io_MoveToExecute.RequestedPosition;
            m_CheckersBoard[io_MoveToExecute.CurrentPosition.y, io_MoveToExecute.CurrentPosition.x] = null;
            m_CheckersBoard[io_MoveToExecute.RequestedPosition.y, io_MoveToExecute.RequestedPosition.x] = theOneWeMove;

            if (io_MoveToExecute.moveTypeInfo.moveType == eMoveTypes.EatMove)
            {
                Point eatenSoldierPosition = calculatePositionOfEatenSoldier(io_MoveToExecute);

                Soldier eatenSoldier = GetSoldierFromMatrix(eatenSoldierPosition);
                GameBoard gb = this;
                eatenSoldier.killed(gb);
                m_CheckersBoard[eatenSoldier.Position.y, eatenSoldier.Position.x] = null;

            }


            updateBoardAfterMove(io_MoveToExecute);


        }

        private void updateBoardAfterMove(CheckersGameStep io_MoveToExecute)//afterMovement
        {
            Soldier TheMovedSoldier = GetSoldierFromMatrix(io_MoveToExecute.RequestedPosition);
            Point beforeMovementSoldierLocation = io_MoveToExecute.CurrentPosition;
            Point afterMovementSoldierLocation = TheMovedSoldier.Position;
            UpdatePossibleMovements(beforeMovementSoldierLocation);
            UpdatePossibleMovements(afterMovementSoldierLocation);
            GameBoard gb = this;
            TheMovedSoldier.calculatePossibleMovements(ref gb);

        }

        private void UpdatePossibleMovements(Point centerPoint)
        {
            List<Point> affectedPoints = bringPossibleNeigboursPositions(centerPoint);
            if (affectedPoints != null)
            {

                foreach (Point p in affectedPoints)
                {
                    Soldier s = GetSoldierFromMatrix(p);
                    if (s != null)
                    {
                        GameBoard gb = this;
                        s.calculatePossibleMovements(ref gb);

                    }
                }

            }
        }

        private static List<Point> bringPossibleNeigboursPositions(Point centerPoint)
        {
            const int TopLeft = 0;
            const int TopRight = 1;
            const int BottomLeft = 2;
            const int BottomRight = 3;


            Point LocalPoint = centerPoint;
            List<Point> affectedSoldiersPositions = new List<Point>();
            Point[] points = new Point[4];


            points[TopLeft].x = centerPoint.x - 1;
            points[TopLeft].y = centerPoint.y + 1;

            points[TopRight].x = centerPoint.x + 1;
            points[TopRight].y = centerPoint.y + 1;

            points[BottomRight].x = centerPoint.x + 1;
            points[BottomRight].y = centerPoint.y - 1;

            points[BottomLeft].x = centerPoint.x - 1;
            points[BottomLeft].y = centerPoint.y - 1;



            foreach (Point p in points)
            {
                if (p.isInsideBoard())
                {
                    affectedSoldiersPositions.Add(p);
                }
            }


            return affectedSoldiersPositions;
        }

        private Point calculatePositionOfEatenSoldier(CheckersGameStep i_move)
        {

            Point resultPosition = new Point();

            resultPosition.x = i_move.CurrentPosition.x + ((i_move.RequestedPosition.x - i_move.CurrentPosition.x) / 2);
            resultPosition.y = i_move.CurrentPosition.y + ((i_move.RequestedPosition.y - i_move.CurrentPosition.y) / 2);

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



                SessionData.GetCurrentPlayer().addToPlayerArmy(m_CheckersBoard[localPointPlayer1.y, localPointPlayer1.x]);
                SessionData.GetOtherPlayer().addToPlayerArmy(m_CheckersBoard[localPointPlayer2.y, localPointPlayer2.x]);////AI-Practice-MODE





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
                    if (currentPositonSoldier.m_PossibleEatMovements.Capacity != 0 && result.moveType == eMoveTypes.EatMove)
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
