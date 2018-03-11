using System;

namespace Base_Conversor
{
    class MainClass
    {
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
            int commaPos = Reverse(input).IndexOf('.');
            input = input.Remove(input.IndexOf('.'), 1);
			char[] num = input.ToCharArray();
            int pos = commaPos == -1 ? input.Length-1 : input.Length - commaPos - 1;

            foreach(char digit in num)
            {
                digitValue = ConvertDigitToDecimal(digit);
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
        public static double ConvertDigitToDecimal(char digit){
            char[] dictionary = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            double digitValue = Char.GetNumericValue(digit);
            return digitValue >= 0 && digitValue < 10 ? digitValue : Array.IndexOf(dictionary, digit) + 10;
        }

        /// <summary>
        /// Coverts the decimal digit to the given base.
        /// </summary>
        /// <returns>The decimal digit in the given base.</returns>
        /// <param name="value">Digit to be converted.</param>
        /// <param name="outputBase">Digit base.</param>
        public static char ConvertDecimalDigitToBase(int value, int outputBase)
        {
            char[] dictionary = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            if (value >= outputBase)
            {//Ta cagado, lança erro pq o numero estora a base
            }

            if (value < 10)
                //Conversão na tabela ASCII ;-;
                return (char)(value + '0');
            else
                return dictionary[value-10];
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
                Console.WriteLine("Digite o número:");
                input = Console.ReadLine();
                Console.WriteLine("Digite a base do número:");
                inputBase = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite a base desejada:");
                outputBase = int.Parse(Console.ReadLine());

                output = ConvertNumber(input, inputBase, outputBase);

                Console.WriteLine(input + "(base " + inputBase + ") na base " + outputBase + " é " + output);
                Console.WriteLine("Para converter outro numero digite qualquer tecla. (ESC para sair)");
                control = Console.ReadKey();
            } while (control.Key != ConsoleKey.Escape);
        }
    }
}
