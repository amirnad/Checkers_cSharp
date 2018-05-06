using System;
using System.Collections.Generic;

namespace Checkers_LogicAndDataSection
{
    public enum ePlayerOptions { Player1, Player2, ComputerPlayer }
    public enum eMoveTypes { EatMove, RegularMove, Undefined }


    public class Player
    {
        private ComputerPlayer computer;

        public void UseComputer(gameStateContainer CloneCurrentstate)
        {
            computer.PickBestMove(CloneCurrentstate);
        }
        internal class ComputerPlayer
        {
            private const int k_miniMaxTreeHeight = 5;


            public void PickBestMove(gameStateContainer CloneCurrentstate)
            {
                MiniMaxTree tree = BuildMiniMax(CloneCurrentstate);
            }

            private MiniMaxTree BuildMiniMax(gameStateContainer currentstate)
            {
                MiniMaxTree tr = MiniMaxTree.CreateNewTree();
                currentstate = currentstate.Clone();
                tr.root = MiniMaxTreeNode.createTreeNode(currentstate, Hueristics(currentstate.Myplayer, currentstate.enemyPlayer));
                List<CheckersGameStep> rootCounterAttacks = currentstate.Myplayer.GetAllMoves();
                foreach (CheckersGameStep cgs in rootCounterAttacks)
                {
                    CheckersGameStep theStep = cgs;
                    theStep.moveTypeInfo = currentstate.board.SortMoveType(cgs, currentstate.Myplayer);
                    if (theStep.moveTypeInfo.moveType != eMoveTypes.Undefined)
                    {
                        buildminiMaxRec(currentstate.Clone(), 4, tr.root, theStep);
                    }
                }
                return tr;
            }

            private void buildminiMaxRec(gameStateContainer currentState, int recursionIndex, MiniMaxTreeNode parent, CheckersGameStep stepToExecute)
            {
                List<CheckersGameStep> allmoves;
                Player p;
                if (recursionIndex == 0)
                {
                    gameStateContainer gameState = currentState.CloneWithMove(stepToExecute, currentState.Myplayer);
                    MiniMaxTreeNode leaf = MiniMaxTreeNode.createTreeNode(gameState, Hueristics(currentState.Myplayer, currentState.enemyPlayer));
                    parent.addSon(leaf);

                }
                else
                {
                    if (recursionIndex % 2 == 0)
                    {
                        gameStateContainer gameState = currentState.CloneWithMove(stepToExecute, currentState.Myplayer);
                        MiniMaxTreeNode node = MiniMaxTreeNode.createTreeNode(gameState, Hueristics(currentState.Myplayer, currentState.enemyPlayer));
                        parent.addSon(node);
                        allmoves = gameState.enemyPlayer.GetAllMoves();
                        foreach (CheckersGameStep move in allmoves)
                        {
                            CheckersGameStep theStep = move;
                            theStep.moveTypeInfo = currentState.board.SortMoveType(move, currentState.enemyPlayer);
                            
                            if (theStep.moveTypeInfo.moveType != eMoveTypes.Undefined)
                            {

                                buildminiMaxRec(gameState.Clone(), recursionIndex - 1,node, theStep);
                            }
                        }

                    }

                    else
                    {
                        gameStateContainer gameState = currentState.CloneWithMove(stepToExecute, currentState.enemyPlayer);
                        MiniMaxTreeNode node = MiniMaxTreeNode.createTreeNode(gameState, Hueristics(currentState.Myplayer, currentState.enemyPlayer));
                        parent.addSon(node);
                        allmoves = gameState.enemyPlayer.GetAllMoves();
                        foreach (CheckersGameStep move in allmoves)
                        {
                            CheckersGameStep theStep = move;
                            theStep.moveTypeInfo = currentState.board.SortMoveType(move, currentState.enemyPlayer);
                            if (theStep.moveTypeInfo.moveType != eMoveTypes.Undefined)
                            {
                                buildminiMaxRec(gameState.Clone(), recursionIndex - 1, node, theStep);
                            }
                        }

                    }

                }
            }

            private int Hueristics(Player player, Player enemy)
            {
                return player.NumberOfSoldiers - enemy.NumberOfSoldiers;
            }

            private class MiniMaxTree
            {
                public MiniMaxTreeNode root = null;

                internal static MiniMaxTree CreateNewTree()
                {
                    MiniMaxTree tree = new MiniMaxTree();
                    tree.root = null;
                    return tree;
                }
            }
            ///
            public static void printMatrix(Checkers_LogicAndDataSection.GameBoard gb)
            {
                Checkers_LogicAndDataSection.GameBoard.Soldier localSoldier;
                Checkers_LogicAndDataSection.Point localPoint;
                Console.Clear();
                Console.Write("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ ");

                for (localPoint.y = 0; localPoint.y < (int)Checkers_LogicAndDataSection.SessionData.m_BoardSize; localPoint.y++)
                {
                    for (localPoint.x = 0; localPoint.x < (int)Checkers_LogicAndDataSection.SessionData.m_BoardSize; localPoint.x++)
                    {
                        localSoldier = gb.GetSoldierFromMatrix(localPoint);
                        if (localSoldier != null)
                            printSoldier(localSoldier);
                        else
                            Console.Write(" ");
                    }

                }
                Console.Write("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ ");


            }
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
            ///
            private class MiniMaxTreeNode
            {
                gameStateContainer Statecontainer;
                int grade;
                List<MiniMaxTreeNode> oppositeTeamMoves = new List<MiniMaxTreeNode>();

                public static MiniMaxTreeNode createTreeNode(gameStateContainer stateContainer, int grade)
                {
                    MiniMaxTreeNode node = new MiniMaxTreeNode();
                    node.Statecontainer = stateContainer;
                    node.grade = grade;
                    return node;
                }

                public void addSon(MiniMaxTreeNode son)
                {
                    oppositeTeamMoves.Add(son);
                }

            }






        }

        private List<CheckersGameStep> GetAllMoves()
        {
            List<CheckersGameStep> allPoosibleMoves = new List<CheckersGameStep>();

            foreach (GameBoard.Soldier s in playerArmy)
            {
                foreach (CheckersGameStep step in s.regularPossibleMovements)
                {
                    allPoosibleMoves.Add(step);
                }
                foreach (CheckersGameStep step in s.m_PossibleEatMovements)
                {
                    allPoosibleMoves.Add(step);
                }
            }
            return allPoosibleMoves;
        }

        public Player Clone()
        {
            Player TheClone = new Player();

            TheClone.m_NumberOfSoldiers = m_NumberOfSoldiers;
            TheClone.m_PlayerId = m_PlayerId;
            TheClone.m_PlayerName = m_PlayerName;
            TheClone.playerArmy = new List<GameBoard.Soldier>();

            foreach (GameBoard.Soldier s in playerArmy)
            {
                TheClone.playerArmy.Add(s.Clone());
            }


            return TheClone;



        }

        public const int k_NoSoldiers = 0;
        public const int k_NumberOfSoldiersInSmallBoard = 6;
        public const int k_NumberOfSoldiersInMediumBoard = 12;
        public const int k_NumberOfSoldiersInLargeBoard = 20;


        private ePlayerOptions m_PlayerId;
        private string m_PlayerName = string.Empty;
        private short m_NumberOfSoldiers;
        private List<GameBoard.Soldier> playerArmy = null;
        internal void addToPlayerArmy(GameBoard.Soldier soldier)
        {
            playerArmy.Add(soldier);
        }
        internal void removeFromPlayerArmy(GameBoard.Soldier soldier)
        {
            playerArmy.Remove(soldier);
        }


        public void decrementNumberOfSoldier()
        {
            m_NumberOfSoldiers--;
        }

        private string PlayerName
        {
            get { return m_PlayerName; }
            set
            {
                m_PlayerName = value;
            }
        }

        private short NumberOfSoldiers
        {
            get { return m_NumberOfSoldiers; }
            set
            {
                switch (SessionData.m_BoardSize)
                {
                    case eBoardSizeOptions.SmallBoard:
                        if (value > k_NoSoldiers && value <= k_NumberOfSoldiersInSmallBoard)
                            m_NumberOfSoldiers = value;
                        break;
                    case eBoardSizeOptions.MediumBoard:
                        if (value > k_NoSoldiers && value <= k_NumberOfSoldiersInMediumBoard)
                            m_NumberOfSoldiers = value;
                        break;
                    case eBoardSizeOptions.LargeBoard:
                        if (value > k_NoSoldiers && value <= k_NumberOfSoldiersInLargeBoard)
                            m_NumberOfSoldiers = value;
                        break;
                }

            }
        }
        //changed from public to private
        public ePlayerOptions Team
        {
            get { return m_PlayerId; }
            private set
            {
                m_PlayerId = value;
            }
        }

        public void InitializePlayer(string i_PlayerName, ePlayerOptions i_PlayerId = ePlayerOptions.Player1) // input is already checked in Manager and UI projects
        {
            PlayerName = i_PlayerName;
            Team = i_PlayerId;

            switch (SessionData.m_BoardSize)
            {
                case eBoardSizeOptions.SmallBoard:
                    NumberOfSoldiers = k_NumberOfSoldiersInSmallBoard;
                    break;
                case eBoardSizeOptions.MediumBoard:
                    NumberOfSoldiers = k_NumberOfSoldiersInMediumBoard;

                    break;
                case eBoardSizeOptions.LargeBoard:
                    NumberOfSoldiers = k_NumberOfSoldiersInLargeBoard;
                    break;
            }
            playerArmy = new List<GameBoard.Soldier>(NumberOfSoldiers);
            if (i_PlayerId == ePlayerOptions.ComputerPlayer)
            {
                computer = new ComputerPlayer();
            }

        }

        public void MakeAMove(CheckersGameStep io_MoveToExecute, GameBoard io_CheckersBoard)
        {
            GameBoard.Soldier currentSoldierToMove = io_CheckersBoard.GetSoldierFromMatrix(io_MoveToExecute.CurrentPosition);

            Player activatingPlayer = this;

            io_CheckersBoard.MoveSoldier(io_MoveToExecute, ref activatingPlayer);



            if (io_MoveToExecute.moveTypeInfo.kingMove)
            {
                currentSoldierToMove.BecomeAKing();
            }

            if (io_MoveToExecute.moveTypeInfo.moveType != eMoveTypes.EatMove)
            {
                SessionData.ChangeTurn();
            }
            else
            {
                if (currentSoldierToMove.eatPossibleMovements.Count == 0)
                {
                    SessionData.ChangeTurn();
                }
            }
            //if three well be an addition to what we do if a player has eaten another player and he have a chnce to eat another one itll be here
            //here was supposed to be else --> do nothing cuz we dont want switch turns --> player ate a soldier and can creat a combo
        }

        public bool SomeBodyAlive()
        {
            return m_NumberOfSoldiers > 0;
        }
        public bool ThereIsPossibleMovements()
        {
            bool someBodyAlive = false;
            foreach (GameBoard.Soldier s in playerArmy)
            {
                if (s.m_PossibleEatMovements.Capacity != 0 || s.m_PossibleRegularMovements.Capacity != 0)
                {
                    someBodyAlive = true;
                    break;
                }
            }
            return someBodyAlive;
        }
    }
}
