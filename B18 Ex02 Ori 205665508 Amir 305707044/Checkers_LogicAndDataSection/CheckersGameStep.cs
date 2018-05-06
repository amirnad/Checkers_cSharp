﻿using static System.Math;

namespace Checkers_LogicAndDataSection
{
    public struct CheckersGameStep
    {

        public struct MoveType
        {
            private bool m_KingMove;
            private eMoveTypes m_moveType;

            public eMoveTypes moveType
            {
                get { return m_moveType; }
                set
                {
                    m_moveType = value;
                }
            }
            public bool kingMove
            {
                get { return m_KingMove; }
                set
                {
                    m_KingMove = value;
                }
            }

            public static MoveType initalize()
            {
                MoveType result = new MoveType();

                result.m_moveType = eMoveTypes.Undefined;
                result.m_KingMove = false;

                return result;

            }



            public static MoveType CalculateMoveType(CheckersGameStep i_requestedStep)
            {
                MoveType result = new MoveType();

                int distanceY = 0;
                int distanceX = 0;

                int indexForLastLineOnBoard = 0;

                distanceY = Abs(i_requestedStep.RequestedPosition.YCooord - i_requestedStep.CurrentPosition.YCooord);
                distanceX = Abs(i_requestedStep.RequestedPosition.XCoord - i_requestedStep.CurrentPosition.XCoord);


                if (distanceY == 2 && distanceX == 2)
                {
                    result.m_moveType = eMoveTypes.EatMove;
                }
                else if (distanceY == 1 && distanceX == 1)
                {
                    result.m_moveType = eMoveTypes.RegularMove;
                }
                else
                {
                    result.m_moveType = eMoveTypes.Undefined;
                }
                if (distanceX > 2 || distanceY > 2 || distanceX < 1 || distanceY < 1)
                {
                    result.m_moveType = eMoveTypes.Undefined;
                }

                switch (SessionData.m_currentActivePlayer)
                {

                    case ePlayerOptions.Player1:
                        indexForLastLineOnBoard = 0;
                        break;

                    case ePlayerOptions.ComputerPlayer:
                    case ePlayerOptions.Player2:
                        indexForLastLineOnBoard = (int)SessionData.m_BoardSize - 1;
                        break;
                }
                if (i_requestedStep.RequestedPosition.YCooord == indexForLastLineOnBoard)
                {
                    result.kingMove = true;
                }
                else
                {
                    result.kingMove = false;
                }

                return result;


            }
        }
        private Point m_currentSoldierPosition;
        private Point m_requestedSoldierPosition;
        private MoveType m_moveInfo;
        private bool m_quit;

        public static CheckersGameStep CreateCheckersGameStep(Point i_currentSoldierPosition, Point i_requestedSoldierPosition, bool ToQuit = false)
        {
            CheckersGameStep result = new CheckersGameStep();

            result.CurrentPosition = i_currentSoldierPosition;
            result.RequestedPosition = i_requestedSoldierPosition;
            result.moveTypeInfo = MoveType.CalculateMoveType(result);
            result.m_quit = ToQuit;

            return result;

        }

        public bool quit
        {
            get { return m_quit; }
        }
        public Point CurrentPosition
        {
            get { return m_currentSoldierPosition; }
            set
            {
                m_currentSoldierPosition.XCoord = value.XCoord;
                m_currentSoldierPosition.YCooord = value.YCooord;
            }
        }
        public Point RequestedPosition
        {
            get { return m_requestedSoldierPosition; }
            set
            {
                m_requestedSoldierPosition.XCoord = value.XCoord;
                m_requestedSoldierPosition.YCooord = value.YCooord;
            }
        }
        public MoveType moveTypeInfo
        {
            get { return m_moveInfo; }
            set
            {
                m_moveInfo = value;
            }
        }

        public bool Equals(CheckersGameStep step)
        {
            bool validity = true;
            if (!(step.CurrentPosition.XCoord == CurrentPosition.XCoord && step.CurrentPosition.XCoord == CurrentPosition.XCoord && step.CurrentPosition.YCooord == CurrentPosition.YCooord && step.CurrentPosition.YCooord == CurrentPosition.YCooord))
            {
                validity = false;
            }
            return validity;
        }
    }
}