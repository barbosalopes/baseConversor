using System;

namespace Base_Conversor
{
    class MainClass
    {
        public static readonly char[] DIGIT_DICTIONARY = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// Reverse the specified string.
        /// </summary>
        /// <returns>The reversed string.</returns>
		/// <param name="s">String to be reverted.</param>
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Converts the number on the given base to decimal.
        /// </summary>
        /// <returns>The number in decimal base.</returns>
        /// <param name="input">Number to be converted.</param>
        /// <param name="inputBase">Number's base.</param>
        public static string ConvertBaseToDecimal(string input, int inputBase)
        {  
            double output = 0, digitValue;
            int aux = Reverse(input).IndexOf(',');
            int commaPos = input.IndexOf(',');
            if(commaPos >= 0)
                input = input.Remove(commaPos, 1);
			char[] num = input.ToCharArray();
            int pos = aux == -1 ? input.Length-1 : input.Length - aux - 1;

            foreach(char digit in num)
            {
                digitValue = ConvertDigitToDecimal(digit, inputBase);
                output += digitValue * Math.Pow(inputBase, pos);
                pos--;
            }


            return output.ToString();
        }

        /// <summary>
        /// Converts the digit to decimal.
        /// </summary>
        /// <returns>The decimal value of the digit.</returns>
        /// <param name="digit">Digit.</param>
        public static double ConvertDigitToDecimal(char digit, int digitBase)
        {
            double digitValue = Array.IndexOf(DIGIT_DICTIONARY, digit);
            if (digitValue >= digitBase)
            {
                throw new System.ArgumentException("Digit " + digit + " is not part of the base " + digitBase);
            }

            return digitValue;
        }

        /// <summary>
        /// Coverts the decimal digit to the given base.
        /// </summary>
        /// <returns>The decimal digit in the given base.</returns>
        /// <param name="number">Digit to be converted.</param>
        /// <param name="numberBase">Digit base.</param>
        public static char ConvertDecimalDigitToBase(int number, int numberBase)
        {
            char digit = DIGIT_DICTIONARY[number];
            if(number >= numberBase){
                throw new System.ArgumentException("Digit " + digit + " is not part of the base " + numberBase);
            }

            return digit;
        }

        /// <summary>
        /// Converts the decimal number to the given base.
        /// </summary>
        /// <returns>The number converted to the given base.</returns>
        /// <param name="input">Decimal numbel.</param>
        /// <param name="outputBase">Base to convert the number.</param>
        public static string ConvertDecimalTo(string input, int outputBase)
        {
            if (outputBase == 10) return input;
            string output = "";
            char digit;
            int diff, integerPart;
			double number = double.Parse(input), result;
            int dividend = (int)Math.Round(number);
            double coefficient = number - dividend;

            do
            {
                diff = dividend % outputBase;
                digit = ConvertDecimalDigitToBase(diff, outputBase);
                output += digit;
                dividend = dividend / outputBase;
            } while (dividend != 0);

            output = Reverse(output);

            if(coefficient != 0){
                output += ".";
                int precision = 5;
                while (precision != 0 && coefficient != 0){
                    result = coefficient * outputBase;
                    integerPart = (int)Math.Truncate(result);
                    coefficient = result - integerPart;
                    digit = ConvertDecimalDigitToBase(integerPart, outputBase);
                    output += digit;
                    precision--;
                }
            }

            return output;
        }

        /// <summary>
        /// Converts the number to the given base.
        /// </summary>
        /// <returns>The number.</returns>
        /// <param name="input">Number to be converted.</param>
        /// <param name="inputBase">Number's base.</param>
        /// <param name="outputBase">Base to be converted.</param>
        public static string ConvertNumber(string input, int inputBase, int outputBase)
        {
            if (inputBase == 10)
                return ConvertDecimalTo(input, outputBase);
            else
                return ConvertDecimalTo(ConvertBaseToDecimal(input, inputBase), outputBase);
        }

        public static void Main(string[] args)
        {

            int inputBase, outputBase;
            string output;
            string input;
            ConsoleKeyInfo control;

            do
            {
                Console.Clear();
                Console.WriteLine("Type the number:");
                input = Console.ReadLine();
                Console.WriteLine("Type it's base:");
                inputBase = int.Parse(Console.ReadLine());
                Console.WriteLine("Type the desired base:");
                outputBase = int.Parse(Console.ReadLine());
				try{
                    output = ConvertNumber(input, inputBase, outputBase);
                    Console.WriteLine(input + "(base " + inputBase + ") = " + output + "(base " + outputBase + ")");
					Console.WriteLine("To convert another number type any key. (ESC to exit)");
                }
                catch(Exception e){
                    Console.WriteLine(e.Message);
                    Console.WriteLine("To try again type any key. (ESC to exit)");
                }

                control = Console.ReadKey();
            } while (control.Key != ConsoleKey.Escape);
        }
    }
}
