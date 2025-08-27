using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Northwind
{
    public class ValidationMethods
    {
        public static bool isChoice(int choice)
        {
            if (string.IsNullOrWhiteSpace(choice.ToString().Trim()))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter the Choice!!");
                Console.ResetColor();
                return false;
            }
            if (choice == 0 || choice > 8)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Choice Please enter the choice between 1 to 8");
                Console.ResetColor();
                return false;
            }

            return true;
        }

        public static bool isValidID(string id)
        {
            if (string.IsNullOrWhiteSpace(id.Trim()))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please Enter the Customer ID!");
                Console.ResetColor();
                return false;
            }
            if (!Regex.IsMatch(id, @"([A-Z]){5}"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID Formate! Please Enter the ID in Uppercase!");
                Console.ResetColor();
                return false;
            }

            if (id.Length != 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ID must be in the length of 5!");
                Console.ResetColor();
                return false;
            }

            return true;
        }
    }
}
