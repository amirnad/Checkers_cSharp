using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_LogicAndDataSection
{
    public struct Point
    {
        public int x;
        public int y;

        public Point(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        public bool isInsideBoard()
        {
            bool result = true;
            int boardSize = (int)SessionData.m_BoardSize;
            if (x > boardSize - 1 || x < 0)
            {
                result = false;
            }
            if (y > boardSize - 1 || y < 0)
            {
                result = false;
            }

            return result;
        }
        public int YCooord
        {
            get { return y; }
            set { y = value; }
        }
        public int XCoord
        {
            get { return x; }
            set { y = value; }
        }

    }

}
