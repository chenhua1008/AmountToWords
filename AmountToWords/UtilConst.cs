using System;

namespace AmountProcess.Constants
{
    public class UtilConst
    {
        public static readonly string[] BelowTen = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        public static readonly string[] BelowTwenty = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        public static readonly string[] BelowHundred = { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public const string IllegalMsgEmpty = "Input is empty";
        public const string IllegalMsgOutOfRange = "Illegal input, amount must between 0 and 2147483647.99";
        public const string IllegalMsgCommon = "Illegal input, please input amount like '123', '123.0', '123.00'";
        public const string IllegalMsgDecimal = "Illegal decimal";
    }
}

