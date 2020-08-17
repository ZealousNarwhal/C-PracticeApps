using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveDelegates
{
    class Program
    {
        enum Options //Make sure to update switch statement below to reflect any changes
        {
            Add,
            Subtract,
            Multiply,
            Divide
        };

        delegate double CalcFunc(double d1, double d2);
        static CalcFunc operation;

        static int numberOfOptions;

        static void Main(string[] args)
        {
            Program instance = new Program();
            instance.Initialize();
            DisplayOptions();
        }

        void Initialize()
        {
            numberOfOptions = Enum.GetNames(typeof(Options)).Length;
        }

        static void DisplayOptions()
        {
            StringBuilder displayString = new StringBuilder("Please select an option from the following list of commands:\n");

            for (int i = 0; i < numberOfOptions; i++)
            {
                displayString.Append((i + 1).ToString()).Append(". ").Append((Options)i).AppendLine();
            }

            Console.WriteLine(displayString.ToString());

            GetOptionInput();
        }

        static void GetOptionInput()
        {
            switch (GetKeyPress()) //This should not use GetNumericInput because it must be an int
            {
                //hard coded cases are fine because the Enum is hardcoded.
                case '1':
                    Console.WriteLine("What Two numbers do you want to add?");
                    operation = new CalcFunc(Add);
                    break;

                case '2':
                    Console.WriteLine("What Two numbers do you want to subtract?");
                    operation = new CalcFunc(Subtract);
                    break;

                case '3':
                    Console.WriteLine("What Two numbers do you want to multiply?");
                    operation = new CalcFunc(Multiply);
                    break;

                case '4':
                    Console.WriteLine("What Two numbers do you want to divide?");
                    operation = new CalcFunc(Divide);
                    break;

                default:
                    Console.WriteLine("Invalid Input, please input the number corresponding to the items listed above.");
                    DisplayOptions();
                    break;
            }

            double val = operation(GetNumericalInput(), GetNumericalInput());
            StringBuilder o = new StringBuilder();
            o.Append("= ").Append(val).AppendLine();
            Console.WriteLine(o);
            PromptRestart();
        }

        static double GetNumericalInput()
        {
            double output = 0.0f;
            bool isNumeric = false;
            isNumeric = double.TryParse(Console.ReadLine(), out output);

            if(!isNumeric)
            {
                Console.WriteLine("Invalid Input, please input a valid number.");
                output += GetNumericalInput();
            }

            return output;
        }

        static char GetKeyPress()
        {
            char input = Console.ReadKey().KeyChar;
            Console.WriteLine(); //Shifts to a new line
            return input;
            
        }

        static void PromptRestart()
        {
            Console.WriteLine("Press (Y) to do another calculation.");
            char input = GetKeyPress();
            if (input == 'Y' || input == 'y')
            {
                DisplayOptions();
            }
        }

        static double Add(double val1, double val2)
        {
            return val1 + val2;
        }

        static double Subtract(double val1, double val2)
        {
            return val1 - val2;
        }

        static double Multiply(double val1, double val2)
        {
            return val1 * val2;
        }

        static double Divide(double val1, double val2)
        {
            return val1 / val2;
        }
    }
}
