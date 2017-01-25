using System;
using System.Linq;

namespace Ddmurthare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var proceed = "y";

            do
            {
                var chemicalName = GetInput("Enter proposed chemical name");
                var chemicalSymbol = GetInput("Enter proposed chemical symbol");
                var result = VerifyProposal(chemicalName.Trim(), chemicalSymbol.Trim());
                var status = result ? "Accepted" : "Rejected";

                Console.WriteLine($"Status:  {status}");
                Console.Write("Would you like to continue?  Type in 'y' and hit enter:  ");

                proceed = Console.ReadLine();

                Console.WriteLine();
            }
            while (proceed.ToLower().Equals("y"));
        }

        #region Helpers

        private static string GetInput(string message)
        {
            try
            {
                Console.Write($"{message}:  ");
                return Console.ReadLine();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private static bool VerifyProposal(string chemicalName, string chemicalSymbol)
        {
            // Ensure that name and symbol are valid strings
            if (string.IsNullOrWhiteSpace(chemicalName) || string.IsNullOrWhiteSpace(chemicalSymbol))
                return false;

            // Ensure that the symbol length is exactly two
            if (chemicalSymbol.Length != 2)
                return false;

            // Ensure that name and symbol contain letters only
            if (!chemicalName.All(char.IsLetter) || !chemicalSymbol.All(char.IsLetter)) 
                return false;

            //Ensure that the both letters in the symbol exist in the name
            if (!chemicalName.ToLower().ToCharArray().Contains(chemicalSymbol.ToLower()[0]) ||
                !chemicalName.ToLower().ToCharArray().Contains(chemicalSymbol.ToLower()[1]))
                return false;

            // Ensure that the letters in the symbol appear in the order they appear in the name
            if (chemicalName.ToLower().IndexOf(chemicalSymbol.ToLower()[0]) >
                chemicalName.ToLower().LastIndexOf(chemicalSymbol.ToLower()[1]))
                return false;

            // Ensure that if the symbol has only one distinct letter, that this letter appears more than once in the name
            if (chemicalSymbol.ToLower().Distinct().Count() == 1 && 
                chemicalName.ToLower().Count(m => m == chemicalSymbol.ToLower()[0]) < 2)
                return false;

            // Ensure that only the first letter of the name and symbol are capital
            if (!IsOnlyFirstLetterCapital(chemicalName) || !IsOnlyFirstLetterCapital(chemicalSymbol))
                return false;

            return true;
        }

        private static bool IsOnlyFirstLetterCapital(string value)
        {
            // If all letters are upper or lower just return false
            if (value.All(char.IsUpper) || value.All(char.IsLower))
                return false;

            for (var i = 0; i < value.Length; i++)
            {
                if (char.IsUpper(value[i]))
                    if (i > 0)
                        return false;
            }

            return true;
        }

        #endregion
    }
}
