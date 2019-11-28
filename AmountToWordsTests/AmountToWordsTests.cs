using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AmountProcess;
using AmountProcess.Constants;
using AmountToWords = AmountProcess.AmountToWords;

namespace AmountToWordsTests
{
    [TestFixture]
    public class AmountToWordsTests
    {
        private AmountProcess.AmountToWords _amountToWords;

        [SetUp]
        public void InitializeTests()
        {
            _amountToWords = new AmountProcess.AmountToWords();
        }

        private static IEnumerable<TestCaseData> InputCases()
        {
            // Cases: border
            yield return new TestCaseData("0", "Zero Dollar");
            yield return new TestCaseData("0.0", "Zero Dollar");
            yield return new TestCaseData("0.00", "Zero Dollar");
            yield return new TestCaseData("00.00", "Zero Dollar");
            yield return new TestCaseData("000.00", "Zero Dollar");
            yield return new TestCaseData("2147483647.99", "Two Billion One Hundred Forty Seven Million Four Hundred Eighty Three Thousand Six Hundred Forty Seven Dollars and Ninety Nine Cents");

            // Cases: ten power
            yield return new TestCaseData("1", "One Dollar");
            yield return new TestCaseData("10", "Ten Dollars");
            yield return new TestCaseData("100", "One Hundred Dollars");
            yield return new TestCaseData("1000", "One Thousand Dollars");
            yield return new TestCaseData("10000", "Ten Thousand Dollars");
            yield return new TestCaseData("100000", "One Hundred Thousand Dollars");
            yield return new TestCaseData("1000000", "One Million Dollars");
            yield return new TestCaseData("10000000", "Ten Million Dollars");
            yield return new TestCaseData("100000000", "One Hundred Million Dollars");
            yield return new TestCaseData("1000000000", "One Billion Dollars");

            // Cases: specific 1-20,30,40,50,60,70,80,90,hundred,thousand,million,billion
            yield return new TestCaseData("1.01", "One Dollar and One Cent");
            yield return new TestCaseData("11.02", "Eleven Dollars and Two Cents");
            yield return new TestCaseData("112.03", "One Hundred Twelve Dollars and Three Cents");
            yield return new TestCaseData("1113.04", "One Thousand One Hundred Thirteen Dollars and Four Cents");
            yield return new TestCaseData("20014.05", "Twenty Thousand Fourteen Dollars and Five Cents");
            yield return new TestCaseData("130115.06", "One Hundred Thirty Thousand One Hundred Fifteen Dollars and Six Cents");
            yield return new TestCaseData("1140216.07", "One Million One Hundred Forty Thousand Two Hundred Sixteen Dollars and Seven Cents");
            yield return new TestCaseData("80250317.08", "Eighty Million Two Hundred Fifty Thousand Three Hundred Seventeen Dollars and Eight Cents");
            yield return new TestCaseData("190360418.09", "One Hundred Ninety Million Three Hundred Sixty Thousand Four Hundred Eighteen Dollars and Nine Cents");
            yield return new TestCaseData("1021470519.10", "One Billion Twenty One Million Four Hundred Seventy Thousand Five Hundred Nineteen Dollars and Ten Cents");

            // Cases: others
            yield return new TestCaseData("0.01", "One Cent");
            yield return new TestCaseData("0.1", "Ten Cents");
            yield return new TestCaseData("0.10", "Ten Cents");
            yield return new TestCaseData("1.0", "One Dollar");
            yield return new TestCaseData("1.00", "One Dollar");
            yield return new TestCaseData("01", "One Dollar");
            yield return new TestCaseData("01.0", "One Dollar");
            yield return new TestCaseData("1.01", "One Dollar and One Cent");
            yield return new TestCaseData("1.1", "One Dollar and Ten Cents");
            yield return new TestCaseData("1.10", "One Dollar and Ten Cents");
            yield return new TestCaseData("102", "One Hundred Two Dollars");
            yield return new TestCaseData("120", "One Hundred Twenty Dollars");
            yield return new TestCaseData("0123456", "One Hundred Twenty Three Thousand Four Hundred Fifty Six Dollars");
            yield return new TestCaseData("123,456", "One Hundred Twenty Three Thousand Four Hundred Fifty Six Dollars");
            yield return new TestCaseData("123,456.00", "One Hundred Twenty Three Thousand Four Hundred Fifty Six Dollars");
            yield return new TestCaseData("01234,56", "One Hundred Twenty Three Thousand Four Hundred Fifty Six Dollars");
            yield return new TestCaseData(",123,456", "One Hundred Twenty Three Thousand Four Hundred Fifty Six Dollars");

        }

        /// <summary>
        /// Test function ConvertAmountToWords()
        /// </summary>
        [Test, TestCaseSource("InputCases")]
        public void TestConvertAmountToWords(string inputAmount, string expectedWords)
        {
            Assert.AreEqual(expectedWords, _amountToWords.ConvertAmountToWords(inputAmount));
        }

        private static IEnumerable<TestCaseData> InputIllegalCases()
        {
            // Case: null
            yield return new TestCaseData(null, UtilConst.IllegalMsgEmpty);

            // Cases: numbers, but out of range [0,2147483647.99]  
            yield return new TestCaseData("-1", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("-1.00", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("-0.01", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("2147483647.991", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("2147483648", UtilConst.IllegalMsgOutOfRange);
            yield return new TestCaseData("3147483647", UtilConst.IllegalMsgOutOfRange);

            // Cases: other illegal
            yield return new TestCaseData(" ", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData(".", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData(",", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("-0", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("a", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("a.0", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("0b", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("0.c", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("0.000", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("1.", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("1.000", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("1.0,", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("0.", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("10.", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("10.000", UtilConst.IllegalMsgCommon);
            yield return new TestCaseData("2147483647.990", UtilConst.IllegalMsgCommon);
        }

        /// <summary>
        /// Test function ConvertAmountToWords()
        /// Input Illegal
        /// </summary>
        [Test, TestCaseSource("InputIllegalCases")]
        public void TestConvertAmountToWords_InputIllegal(string inputAmount, string expectedMsg)
        {
            Assert.Throws<ArgumentException>(() => _amountToWords.ConvertAmountToWords(inputAmount), expectedMsg);
        }

    }
}
