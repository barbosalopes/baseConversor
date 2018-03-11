﻿using System;

namespace Base_Conversor
{
    class MainClass
    {
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string ConvertBaseToDecimal(string input, int inputBase)
        {  
            double output = 0, digitValue;
            char[] num = input.ToCharArray();
            int commaPos = Reverse(input).IndexOf('.');
            int pos = commaPos == -1 ? input.Length-1 : input.Length - commaPos - 1;

            foreach(char digit in num)
            {
                digitValue = ConvertDigitToDecimal(digit);
                output += digitValue * Math.Pow(inputBase, pos);
                pos--;
            }


            return output.ToString();
        }

        public static double ConvertDigitToDecimal(char digit){
            char[] dictionary = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            double digitValue = Char.GetNumericValue(digit);
            return digitValue >= 0 && digitValue < 10 ? digitValue : Array.IndexOf(dictionary, digit) + 10;
        }

        // Sim, o nome ficou grande pra bosta, mas eu não sou criativo :V
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

        public static string ConvertDecimalTo(string input, int outputBase)
        {
            string output = "";
            char num;
            int diff;
            int dividend = int.Parse(input);

            while (dividend != 0)
            {
                diff = dividend % outputBase;
                num = ConvertDecimalDigitToBase(diff, outputBase);
                output += num;
                dividend = dividend / outputBase;
            }

            return Reverse(output);
        }

        public static string ConvertNumber(string input, int inputBase, int outputBase)
        {
            // Se for cnoverter de decimal para qualquer um chama a função, se não pega o valor
            //converte para a base decimal (gg  com posicional) e converte para decimal
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
