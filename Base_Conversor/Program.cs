using System;

namespace Base_Conversor
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int inputBase, outputBase;
            string output;
            string input;
            ConsoleKeyInfo control;
            
            do
            {
				try{
                    Console.Clear();
                    Console.WriteLine("Type the number:");
                    input = Console.ReadLine();
                    Console.WriteLine("Type it's base:");
                    inputBase = int.Parse(Console.ReadLine());
                    Console.WriteLine("Type the desired base:");
                    outputBase = int.Parse(Console.ReadLine());
                    output = BaseConversor.ConvertNumber(input, inputBase, outputBase);
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
