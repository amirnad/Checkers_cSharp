namespace Checkers_LogicAndDataSection
{

    public struct CheckersGameStep
    {

        private Point currentSoldierPosition;
        private Point requestedSoldierPosition;

        public Point setCurrentPosition
        {
            get { return currentSoldierPosition; }
            set
            {
                currentSoldierPosition.x = value.x;
                currentSoldierPosition.y = value.y;
            }
        }
        public Point setRequestedPosition
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