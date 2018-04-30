namespace Checkers_LogicAndDataSection
{
    public struct Point
    {
        public int x;
        public int y;
    }

    public struct CheckersGameStep
    {

        private Point currentSoldierPosition;
        private Point requestedSoldierPosition;

        private Point setCurrentPosition
        {
            get { return currentSoldierPosition; }
            set
            {
                currentSoldierPosition.x = value.x;
                currentSoldierPosition.y = value.y;
            }
        }
        private Point setRequestedPosition
        {
            get { return requestedSoldierPosition; }
            set
            {
                requestedSoldierPosition.x = value.x;
                requestedSoldierPosition.y = value.y;
            }
        }

    }
}