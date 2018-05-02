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

                distanceY = Abs(i_requestedStep.RequestedPosition.y - i_requestedStep.CurrentPosition.y);
                distanceX = Abs(i_requestedStep.RequestedPosition.x - i_requestedStep.CurrentPosition.x);


                if (distanceY == 2)
                {
                    result.m_moveType = eMoveTypes.EatMove;
                }
                else if (distanceY == 1)
                {
                    result.m_moveType = eMoveTypes.RegularMove;
                }
                else
                {
                    result.m_moveType = eMoveTypes.Undefined;
                }
                if(distanceX>1)
                {
                    result.m_moveType = eMoveTypes.Undefined;
                }

                switch (SessionData.m_currentActivePlayer)
                {

                    case ePlayerOptions.Player1:
                        indexForLastLineOnBoard = (int)SessionData.m_BoardSize - 1;
                        break;

                    case ePlayerOptions.ComputerPlayer:
                    case ePlayerOptions.Player2:
                        indexForLastLineOnBoard = 0;
                        break;
                }
                if (i_requestedStep.RequestedPosition.y == indexForLastLineOnBoard)
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

        public Point CurrentPosition
        {
            get { return m_currentSoldierPosition; }
            set
            {
                m_currentSoldierPosition.x = value.x;
                m_currentSoldierPosition.y = value.y;
            }
        }
        public Point RequestedPosition
        {
            get { return m_requestedSoldierPosition; }
            set
            {
                m_requestedSoldierPosition.x = value.x;
                m_requestedSoldierPosition.y = value.y;
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


    }
}