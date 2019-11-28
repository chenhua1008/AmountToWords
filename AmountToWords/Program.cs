using System;
using System.Globalization;
using AmountProcess;

namespace AmountToWords
{
    class Program
    {
        static void Main(string[] args)
        {
            AmountProcess.AmountToWords _amountToWords = new AmountProcess.AmountToWords();
            Console.WriteLine(ShowUsage());
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
        /// <summary>
        /// Print help information
        /// </summary>
        static private string ShowUsage()
        {
            string strMsg;
            strMsg = "***********************************************************************\n"
                     + "This application is used to convert any amount to its English currency representation in words.\n"
                     + "Please input an amount, up to 2 decimal digit, like '123', '1234.5', '12345.67', and then press ENTER.\n"
                     + "Min:0   Max:2147483647.99\n"
                     + "***********************************************************************\n";
            return strMsg;
        }
    }
}
