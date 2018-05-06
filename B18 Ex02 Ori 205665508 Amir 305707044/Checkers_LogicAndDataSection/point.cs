using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_LogicAndDataSection
{
    public struct Point
    {
        public int m_x;
        public int m_y;

        public Point(int i_x, int i_y)
        {
            m_x = i_x;
            m_y = i_y;
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
