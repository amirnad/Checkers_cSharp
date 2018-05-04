using System;
using System.Text;
using Checkers_LogicAndDataSection;
namespace Checkers_UI
{
    class UI
    {
        private static string s_StartMessage = "Hello! Welcome to our checkers game. Enjoy!";
        private static string s_UsernameMessage = "Please enter your name: ";
        private static string s_ChooseGameTypeMessge = "Please choose the game type: ";
        private static string s_ChooseBoardSizeMessage = "Please choose the board size: ";
        private static string s_InvalidInputMessage = "Input is invalid, please try again.";

        private const int k_PlayerXName = 0;
        private const int k_PlayerOName = 1;
        private const int k_BoardSize = 2;
        private const int k_GameType = 3;
        private const int k_NumberOfInputValues = 4;
        private const int k_MaxNameLength = 20;

        //consts for printing reasons
        private const char k_Space = ' ';
        private const char k_LineSeperator = '=';
        private const char k_ColumnSeperator = '|';
        private const int k_NumberOfSpacesBetweenLetters = 3;
        private const char k_Team1SoldierChar = 'X';
        private const char k_Team1KingChar = 'K';
        private const char k_Team2SoldierChar = 'O';
        private const char k_Team2KingChar = 'U';




        public static void ReadInputFromUser(out InitialGameSetting io_GameInitialValues)
        {
            string userInputValue;
            string tempPlayer1NameHolder = string.Empty;
            string tempPlayer2NameHolder = string.Empty;
            bool[] inputValidityArray = new bool[k_NumberOfInputValues]; //holds true/false for each input value in this order: player1name, player2name, boardSize, gameType
            bool gotAllInput = false;
            eBoardSizeOptions chosenBoardSize = eBoardSizeOptions.Undefined;
            eTypeOfGame chosenGameType = eTypeOfGame.Undefined;

            Console.WriteLine(s_StartMessage);
            while (!gotAllInput)
            {
                while (!inputValidityArray[k_PlayerXName])
                {
                    Console.WriteLine(s_UsernameMessage);
                    userInputValue = Console.ReadLine();
                    if (checkUsernameValidity(userInputValue))
                    {
                        inputValidityArray[k_PlayerXName] = true;
                        tempPlayer1NameHolder = userInputValue;
                    }
                    else
                    {
                        Console.WriteLine(s_InvalidInputMessage);
                    }
                }
                while (!inputValidityArray[k_BoardSize])
                {
                    Console.WriteLine(s_ChooseBoardSizeMessage);
                    userInputValue = Console.ReadLine();
                    if (checkBoardSizeValidity(userInputValue))
                    {
                        inputValidityArray[k_BoardSize] = true;
                        chosenBoardSize = (eBoardSizeOptions)Enum.Parse(typeof(eBoardSizeOptions), userInputValue);
                    }
                    else
                    {
                        Console.WriteLine(s_InvalidInputMessage);
                    }
                }
                while (!inputValidityArray[k_GameType])
                {
                    Console.WriteLine(s_ChooseGameTypeMessge);
                    userInputValue = Console.ReadLine();
                    if (checkGameTypeValidity(userInputValue))
                    {
                        inputValidityArray[k_GameType] = true;
                        chosenGameType = (eTypeOfGame)Enum.Parse(typeof(eTypeOfGame), userInputValue);
                        if (chosenGameType == eTypeOfGame.doublePlayer)
                        {
                            while (!inputValidityArray[k_PlayerOName])
                            {
                                Console.WriteLine(s_UsernameMessage);
                                userInputValue = Console.ReadLine();
                                if (checkUsernameValidity(userInputValue))
                                {
                                    inputValidityArray[k_PlayerOName] = true;
                                    tempPlayer2NameHolder = userInputValue;
                                }
                                else
                                {
                                    Console.WriteLine(s_InvalidInputMessage);
                                }
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine(s_InvalidInputMessage);
                    }
                }
                gotAllInput = true;
            }

            io_GameInitialValues = new InitialGameSetting();
            io_GameInitialValues.SetGameSettings(tempPlayer1NameHolder, tempPlayer2NameHolder, chosenBoardSize, chosenGameType);
        }
        public static void PrintCheckersBoard(GameBoard io_CheckersBoard)
        {
            /*The Idea:
                We create a StringBuilder object , and we call 3 diff methods to create the top, body, and bottom
                parts of the checkers board. After all these 3 function finished, the board is ready to be printed.
            */
            
            StringBuilder BoardWithFrames = new StringBuilder();

            CreateBoardHeader(BoardWithFrames);
            CreateBoardBody(BoardWithFrames, io_CheckersBoard);
            CreateBoardFooter(BoardWithFrames);

            Console.WriteLine(BoardWithFrames);

        }


        private static void CreateBoardHeader(StringBuilder o_BoardHeader)
        {
            string[] columnsLetters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            int boardSize = (int)SessionData.m_BoardSize;
            int numberOfLineSeperators = (boardSize * 4) + 1;

            for (int i = 0; i < boardSize; i++)
            {
                o_BoardHeader.Append(k_Space, k_NumberOfSpacesBetweenLetters);
                o_BoardHeader.Append(columnsLetters[i]);
            }

            o_BoardHeader.Append(Environment.NewLine);
            o_BoardHeader.Append(k_Space);
            o_BoardHeader.Append(k_LineSeperator, numberOfLineSeperators);
            o_BoardHeader.Append(Environment.NewLine);
        }

        private static void CreateBoardBody(StringBuilder o_BoardBody, GameBoard io_CheckersBoard)
        {
            string[] columnLetters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
            int boardSize = (int)SessionData.m_BoardSize;
            int numberOfLineSeperators = (boardSize * 4) + 1;
            for (int i = 0; i < boardSize; i++)
            {
                o_BoardBody.AppendFormat("{0}{1}", columnLetters[i], k_ColumnSeperator);
                for (int j = 0; j < boardSize; j++)
                {
                    Point soldierCoordinate = new Point(j, i);
                    char soldierCharToPrint = BringCharFromMatrix(io_CheckersBoard, soldierCoordinate);
                    o_BoardBody.AppendFormat("{0}{1}{0}{2}", k_Space, soldierCharToPrint, k_ColumnSeperator);

                }
                o_BoardBody.Append(Environment.NewLine);
                if (i != boardSize - 1)
                {
                    o_BoardBody.Append(k_Space);
                    o_BoardBody.Append(k_LineSeperator, numberOfLineSeperators);
                    o_BoardBody.Append(Environment.NewLine);

                }
            }
        }

        private static char BringCharFromMatrix(GameBoard i_CheckersBoard, Point i_SoldierCoordinate)
        {
            char charToPrint = k_Space;
            GameBoard.Soldier soldierInBoard = i_CheckersBoard.GetSoldierFromMatrix(i_SoldierCoordinate);
            if (soldierInBoard != null)
            {
                switch (soldierInBoard.Team)
                {
                    case ePlayerOptions.Player1:
                        if (soldierInBoard.Rank == GameBoard.eSoldierRanks.Regular)
                        {
                            charToPrint = k_Team1SoldierChar;
                        }
                        else
                        {
                            charToPrint = k_Team1KingChar;
                        }
                        break;
                    case ePlayerOptions.Player2:
                    case ePlayerOptions.ComputerPlayer:
                        if (soldierInBoard.Rank == GameBoard.eSoldierRanks.Regular)
                        {
                            charToPrint = k_Team2SoldierChar;
                        }
                        else
                        {
                            charToPrint = k_Team2KingChar;
                        }
                        break;
                }
            }

            return charToPrint;
        }

        private static void CreateBoardFooter(StringBuilder o_BoardFooter)
        {
            int boardSize = (int)SessionData.m_BoardSize;
            int numberOfLineSeperators = (boardSize * 4) + 1;
            o_BoardFooter.Append(k_Space);
            o_BoardFooter.Append(k_LineSeperator, numberOfLineSeperators);
            //  o_BoardFooter.Append(Environment.NewLine);

        }

        private static bool checkUsernameValidity(string i_InputValue)
        {
            return !(i_InputValue.Contains(k_Space.ToString())) && i_InputValue.Length < k_MaxNameLength;
        }
        private static bool checkBoardSizeValidity(string i_InputValue)
        {
            bool returnedValue;
            eBoardSizeOptions temporaryBoardSize;
            bool inputOk = Enum.TryParse(i_InputValue, out temporaryBoardSize);
            if (!inputOk)
            {
                returnedValue = false;
            }
            else
            {
                returnedValue = temporaryBoardSize.Equals(eBoardSizeOptions.SmallBoard) || temporaryBoardSize.Equals(eBoardSizeOptions.MediumBoard) || temporaryBoardSize.Equals(eBoardSizeOptions.LargeBoard);
            }
            return returnedValue;
        }
        private static bool checkGameTypeValidity(string i_InputValue)
        {
            bool returnedValue;
            eTypeOfGame temporaryGameType;
            bool inputOk = Enum.TryParse(i_InputValue, out temporaryGameType);
            if (!inputOk)
            {
                returnedValue = false;
            }
            else
            {
                returnedValue = temporaryGameType.Equals(eTypeOfGame.singlePlayer) || temporaryGameType.Equals(eTypeOfGame.doublePlayer);
            }
            return returnedValue;
        }


    }
}
