using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_LogicAndDataSection
{
    public class InitialGameSetting
    {
        public string player1Name = string.Empty;
        public string player2Name = string.Empty;
        public Checkers_LogicAndDataSection.eBoardSizeOptions boardSize = Checkers_LogicAndDataSection.eBoardSizeOptions.Undefined;
        public Checkers_LogicAndDataSection.eTypeOfGame gameType = Checkers_LogicAndDataSection.eTypeOfGame.Undefined;
    }
}
