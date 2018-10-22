using System;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AlgorithmsRunner.Common;
using AlgorithmsRunner.Common.Interfaces;
using Newtonsoft.Json.Linq;

namespace AlgorithmsRunner.SumOfMultiple
{
    public class Processor : IAlgorithmItem
    {
        [Input(InputName = Constants.SUM_OF_MULTIPLE_INPUT_NAME, DisplayName = "Ceiling Number")]
        private string InputNumber { get; set; }

        private readonly int[] m_Samples = {3, 5};
        
        public JObject Process(JObject inputJObject)
        {
            ExtractInput(inputJObject);
            return new JObject(new JProperty(Constants.RESULT, Execute(InputNumber)));
        }

        private void ExtractInput(JObject inputJObject)
        {
            InputNumber = inputJObject.Property(Constants.SUM_OF_MULTIPLE_INPUT_NAME).Value.Value<string>();
            if (!IsNaturalNumber())
            {
                throw new Exception("Input was not a natural number.");
            }
        }

        public string GetDisplayName()
        {
            return "SumOfMultiple";
        }

        public string GetDescription()
        {
            return "Find the sum of all natural numbers that are a multiple of 3 or 5 below a limit provided as input." +
                   "Input: \"6\"\r\n" +
                   "Output: \"8\"";
        }

        public string Execute(string inputString)
        {
            return SumOfMultiplicationAsync(int.Parse(inputString), m_Samples).Result.ToString();
        }

        private async Task<BigInteger> SumOfMultiplicationAsync(int inputNumber, int[] samples)
        {
            var taskResults = await Task.WhenAll(samples.Select(i => SumOfMultiplication(inputNumber, i)));
            return taskResults.Aggregate(BigInteger.Zero, (result, next) => result + next);
        }

        private Task<BigInteger> SumOfMultiplication(int inputNumber, int divisor)
        {
            var multiplication = CalculateMultiplicationDivisibleNumber(inputNumber, divisor);
            return Task.Run(() => AccumulateOnMultiplications(divisor, multiplication));
        }

        public BigInteger AccumulateOnMultiplications(int seed, int multiplication)
        {
            double result = 0;
            for (var i = 1; i <= multiplication; i++)
            {
                result += seed * i;
            }

            return (BigInteger)result;
        }

        private bool IsNaturalNumber()
        {
            return IsNaturalNumber(InputNumber);
        }

        public bool IsNaturalNumber(string input)
        {
            var naturalNumberRegex = new Regex("^[0-9]+$");
            return naturalNumberRegex.IsMatch(input);
        }

        public int CalculateMultiplicationDivisibleNumber(int input, int divisor)
        {
            return input % divisor == 0 ? input / divisor - 1 : input / divisor;
        }

    }
}
