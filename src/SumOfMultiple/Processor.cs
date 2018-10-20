using AlgorithmsRunner.Common;
using System;
using System.Numerics;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace AlgorithmsRunner.SumOfMultiple
{
    public class Processor : IAlgorithmItem
    {
        [PropertyUsage(PropertyName = "Input")]
        private string Input { get; set; }
        
        public JObject Run(JObject inputJObject)
        {
            throw new NotImplementedException();
        }

        public string GetDisplayName()
        {
            return "SumOfMultiple";
        }

        public string GetHelp()
        {
            return "Find the sum of all natural numbers that are a multiple of 3 or 5 below a limit provided as input." +
                   "Input: \"6\"\r\n" +
                   "Output: \"8\"";
        }

        public string Execute(string inputString)
        {

            if (!IsNaturalNumber(inputString))
            {
                // says it was not a nutural number
            }

            var inputNumber = int.Parse(inputString);
            var powerOfThree = AccumulatePowerOfThree(inputNumber, 3);
            var powerOfFive = TimesItCanBreakDown(inputNumber, 5);
            return "";
        }

        private BigInteger AccumulatePowerOfThree(int inputNumber, int divisor)
        {
            var timesItCanBreakDown = TimesItCanBreakDown(inputNumber, divisor);
            return AccumulateAllPower(divisor, timesItCanBreakDown);
        }

        public BigInteger AccumulateAllPower(int seed, int power)
        {
            double result = 0;
            for (var i = 1; i <= power; i++)
            {
                result += Math.Pow(seed, i);
            }

            return (BigInteger)result;
        }

        public bool IsNaturalNumber(string input)
        {
            var naturalNumberRegex = new Regex("^[0-9]+$");
            return naturalNumberRegex.IsMatch(input);
        }

        public int TimesItCanBreakDown(int input, int divisor)
        {
            return input % divisor == 0 ? input / divisor - 1 : input / divisor;
        }

    }
}
