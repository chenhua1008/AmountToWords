using System;
using AmountProcess;

namespace AmountToWords
{
    class Program
    {
        static void Main(string[] args)
        {
            AmountProcess.AmountToWords _amountToWords = new AmountProcess.AmountToWords();
            Console.WriteLine(_amountToWords.ShowUsage());
            while (true)
            {
                try
                {
                    Console.WriteLine("Please input amount followed by ENTER, exit by Ctrl+C:");
                    string str = Console.ReadLine();
                    Console.WriteLine(_amountToWords.ConvertAmountToWords(str));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine();
            }
        }
    }
}
