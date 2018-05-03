using System;
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
        private const string k_Space = " ";
        private const int k_MaxNameLength = 20;


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

        private static bool checkUsernameValidity(string i_InputValue)
        {
            return !(i_InputValue.Contains(k_Space)) && i_InputValue.Length < k_MaxNameLength;
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
            if(!inputOk)
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
