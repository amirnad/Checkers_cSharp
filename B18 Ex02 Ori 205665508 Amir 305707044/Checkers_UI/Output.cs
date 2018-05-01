using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_UI
{
    class Output
    {
        public static void printPoint(Checkers_LogicAndDataSection.Point pt)
        {
            System.Console.SetCursorPosition(pt.x, pt.y);
            Console.Write("C");
        }

        public static void printSoldier(Checkers_LogicAndDataSection.GameBoard.Soldier s)
        {
            switch (s.Team)
            {

                case Checkers_LogicAndDataSection.ePlayerOptions.Player1:
                    Console.SetCursorPosition(s.Position.x, s.Position.y);
                    if (s.Rank == Checkers_LogicAndDataSection.GameBoard.eSoldierRanks.Regular)
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write('K');
                    }
                    break;
                case Checkers_LogicAndDataSection.ePlayerOptions.Player2:
                case Checkers_LogicAndDataSection.ePlayerOptions.ComputerPlayer:
                    Console.SetCursorPosition(s.Position.x, s.Position.y);
                    if (s.Rank == Checkers_LogicAndDataSection.GameBoard.eSoldierRanks.Regular)
                    {
                        Console.Write('O');
                    }
                    else
                    {
                        Console.Write('u');
                    }
                    break;

            }

        }
        public static void printMatrix(Checkers_LogicAndDataSection.GameBoard gb)
        {
            Checkers_LogicAndDataSection.GameBoard.Soldier localSoldier;
            Checkers_LogicAndDataSection.Point localPoint;
            for (localPoint.y = 0; localPoint.y < (int)Checkers_LogicAndDataSection.SessionData.m_BoardSize; localPoint.y++)
            {
                for (localPoint.x = 0; localPoint.x < (int)Checkers_LogicAndDataSection.SessionData.m_BoardSize; localPoint.x++)
                {
                    localSoldier = gb.GetSoldierFromMatrix(localPoint);
                    printSoldier(localSoldier);
                }

            }


        }
    }
}
