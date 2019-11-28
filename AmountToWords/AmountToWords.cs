using System;
using System.Text.RegularExpressions;
using AmountProcess.Constants;

/// <summary>
/// Write a piece of code in C# to convert any amount to its English currency representation in words. 
/// Example:
///     Input - 1.15
/// Output - "One Dollar and Fifteen Cents"
/// </summary>
/// Author: Hua Chen
/// Time: 27.Nov.2019

namespace AmountProcess
{
    public class AmountToWords
    {
        /// <summary>
        /// Logic to convert amount to words
        /// 1. validate input
        /// 2. separate input number string to 2 parts by "."(if have): integer and decimal
        /// 3. convert to English words independently
        /// 4. combine the result
        /// </summary>
        /// <param name="strNum">the amount to be converted</param>
        /// return the converted result
        public string ConvertAmountToWords(string strNum)
        {
            if (string.IsNullOrEmpty(strNum))
            {
                throw new ArgumentException(UtilConst.IllegalMsgEmpty);
            }
            // validate the legal input
            if (!ValidateFormat(strNum))
            {
                throw new ArgumentException(UtilConst.IllegalMsgCommon);
            }

            // remove comma symbol from string
            strNum = strNum.Replace(",", string.Empty);

            int integerPart;
            int decimalPart;

            // separate input number string to 2 parts by "."(if have): integer and decimal
            try
            {
                int pos = strNum.IndexOf('.');

                if (pos == -1)
                {
                    integerPart = int.Parse(strNum);
                    decimalPart = 0;
                }
                else
                {
                    integerPart = int.Parse(strNum.Substring(0, pos));

                    string strDecimalPart = strNum.Substring(pos + 1);
                    if (strDecimalPart == "00")
                    {
                        strDecimalPart = "0";
                    }
                    if ((strDecimalPart.Length == 1)&&(strDecimalPart[0]!='0'))
                    {
                        strDecimalPart = strDecimalPart + '0';
                    }
                    decimalPart = int.Parse(strDecimalPart);
                }
            }
            catch (OverflowException)
            {
                throw new ArgumentException(UtilConst.IllegalMsgOutOfRange);
            }
            catch (FormatException)
            {
                throw new ArgumentException(UtilConst.IllegalMsgCommon);
            }

            // convert integer part to English words
            string strIntegerWords = ConvertIntegerPartToWords(integerPart);
            if (strIntegerWords.Length == 0)
            {
                strIntegerWords = "Zero";
            }

            // convert decimal part to English words
            string strDecimalWords = ConvertDecimalPartToWords(decimalPart);
            if (strDecimalWords.Length == 0)
            {
                strDecimalWords = "Zero";
            }

            // tidy and combine the result
            return TidyAndCombineWords(strIntegerWords, strDecimalWords);

        }

        /// <summary>
        /// Use regular expression to validate input
        /// positive number, up to 2 decimal
        /// </summary>
        /// <param name="input">the amount to be validate</param>
        /// return the validate result, true or false
        private bool ValidateFormat(string input)
        {
            Regex reg = new Regex("^[0-9,]+([.][0-9]{1,2})?$");
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            if (reg.Match(input).Success)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Logic to convert integer part [0,2147483647] to words
        /// recursive
        /// </summary>
        /// <param name="num">the number to be converted</param>
        /// return the convert result - string
        private string ConvertIntegerPartToWords(int num)
        {
            string result;
            if (num == 0) result = "";
            else if (num < 10) result = UtilConst.BelowTen[num];
            else if (num < 20) result = UtilConst.BelowTwenty[num - 10];
            else if (num < 100) result = UtilConst.BelowHundred[num / 10] + " " + ConvertIntegerPartToWords(num % 10);
            else if (num < 1000) result = ConvertIntegerPartToWords(num / 100) + " Hundred " + ConvertIntegerPartToWords(num % 100);
            else if (num < 1000000) result = ConvertIntegerPartToWords(num / 1000) + " Thousand " + ConvertIntegerPartToWords(num % 1000);
            else if (num < 1000000000) result = ConvertIntegerPartToWords(num / 1000000) + " Million " + ConvertIntegerPartToWords(num % 1000000);
            else result = ConvertIntegerPartToWords(num / 1000000000) + " Billion " + ConvertIntegerPartToWords(num % 1000000000);

            return result.Trim();
        }

        /// <summary>
        /// Logic to convert decimal part [0,99] to words
        /// recursive
        /// </summary>
        /// <param name="num">the number to be converted</param>
        /// return the convert result - string
        private string ConvertDecimalPartToWords(int num)
        {
            string result;
            if (num == 0) result = "";
            else if (num < 10) result = UtilConst.BelowTen[num];
            else if (num < 20) result = UtilConst.BelowTwenty[num - 10];
            else if (num < 100) result = UtilConst.BelowHundred[num / 10] + " " + ConvertDecimalPartToWords(num % 10);
            else throw new ArgumentException(UtilConst.IllegalMsgDecimal);

            return result.Trim();
        }

        /// <summary>
        /// tidy and combine the result string
        /// </summary>
        /// <param name="strIntegerWords">integer part words</param>
        /// <param name="strDecimalWords">decimal part words</param>
        /// return the combined result string
        private string TidyAndCombineWords(string strIntegerWords, string strDecimalWords)
        {
            if (string.IsNullOrEmpty(strIntegerWords) || string.IsNullOrEmpty(strDecimalWords) )
            {
                throw new ArgumentException(UtilConst.IllegalMsgEmpty);
            }

            string result;

            // deal with dollar&dollars, cent&cents
            string currancyDollar = " Dollars";
            if (strIntegerWords == "Zero" || strIntegerWords == "One")
            {
                currancyDollar = " Dollar";
            }

            string currancyCent = " Cents";
            if (strDecimalWords == "Zero" || strDecimalWords == "One")
            {
                currancyCent = " Cent";
            }

            // combine the result string
            if (strDecimalWords == "Zero")
            {
                result = strIntegerWords + currancyDollar;
            }
            else if (strIntegerWords == "Zero")
            {
                result = strDecimalWords + currancyCent;
            }
            else
            {
                result = strIntegerWords + currancyDollar + " and " + strDecimalWords + currancyCent;
            }

            return result.Trim();
        }
    }
}
