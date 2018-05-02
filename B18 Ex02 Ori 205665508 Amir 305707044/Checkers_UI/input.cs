using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers_LogicAndDataSection;
namespace Checkers_UI
{

    public class Input
    {
        public static int i = 0;
        public static void Main()
        {
            ReadAndCheckInput();
        }


        public static CheckersGameStep ReadAndCheckInput()
        {
            string[] inputs = { "Af>Be", "  Cf>De ","aa>bb" };
            bool[] validation = new bool[3];

            string i_inputFromUser = string.Empty;

            //goto xy clear and that stuff
            //i_inputFromUser = Console.ReadLine();

            CheckersGameStep result = new CheckersGameStep();

            Point currentPoint = new Point();
            Point NextPoint = new Point();
            bool valid = false;

            while (!valid)
            {
                validation[0] = false;
                validation[1] = false;
                validation[2] = false;

                i_inputFromUser = inputs[i];



                string processedString = i_inputFromUser.Replace(" ", "");

                if (char.IsUpper(processedString[0]) && char.IsUpper(processedString[3]))
                {
                    currentPoint.x = (int)(processedString[0] - 'A');
                    NextPoint.x = (int)(processedString[3] - 'A');
                    validation[0] = true;
                }
                if (char.IsLower(processedString[1]) && char.IsLower(processedString[4]))
                {
                    currentPoint.y = (int)(processedString[1] - 'a');
                    NextPoint.y = (int)(processedString[4] - 'a');

                    validation[1] = true;
                }
                if (processedString[2] == '>')
                {
                    validation[2] = true;
                }
                valid = (validation[0] && validation[1] && validation[2]);

                if (!valid)
                {
                    Output.InputException();
                }
                i++;
            }

            result.CurrentPosition = currentPoint;
            result.RequestedPosition= NextPoint;
            return result;


        }
    }
}
