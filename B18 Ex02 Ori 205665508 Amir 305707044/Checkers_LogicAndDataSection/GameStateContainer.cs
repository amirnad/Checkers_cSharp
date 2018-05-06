using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_LogicAndDataSection
{
    public class gameStateContainer
    {
        public GameBoard board;
        public Player Myplayer;
        public Player enemyPlayer;

        public static gameStateContainer createCloneForState(GameBoard gb, Player MyplayerArmy, Player enemyPlayerArmy)
        {
            gameStateContainer NewContainer = new gameStateContainer();

            NewContainer.board = gb.Clone();
            NewContainer.Myplayer = MyplayerArmy;
            NewContainer.enemyPlayer = enemyPlayerArmy;


            return NewContainer;
        }
        public gameStateContainer Clone()
        {
            gameStateContainer clonedContainer = new gameStateContainer();

            clonedContainer.board = board.Clone();
            clonedContainer.enemyPlayer = enemyPlayer.Clone();
            clonedContainer.Myplayer = Myplayer.Clone();

            return clonedContainer;

            
        }

        public gameStateContainer CloneWithMove(CheckersGameStep move,Player activPlayer)
        {
            gameStateContainer clonedContainer = new gameStateContainer();

            clonedContainer.board = board.Clone();
            clonedContainer.enemyPlayer = enemyPlayer.Clone();
            clonedContainer.Myplayer = Myplayer.Clone();

            clonedContainer.board.MoveSoldier(move,ref activPlayer);

            return clonedContainer;
        }

    }
}
