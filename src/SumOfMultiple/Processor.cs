using AlgorithmsRunner.Common;
using System;
using System.Numerics;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace AlgorithmsRunner.SumOfMultiple
{
    public class Processor : IAlgorithmItem
    {
        [Input(InputName = Constants.SUM_OF_MULTIPLE_INPUT_NAME, DisplayName = "Ceiling Number")]
        private string InputNumber { get; set; }
        
        public JObject Run(JObject inputJObject)
        {
            ExtractInput(inputJObject);
            return new JObject(new JProperty(Constants.RESULT, Execute(InputNumber)));
        }

        private void ExtractInput(JObject inputJObject)
        {
            InputNumber = inputJObject.Property(Constants.SUM_OF_MULTIPLE_INPUT_NAME).Value.Value<string>();
            if (!IsNaturalNumber())
            {
                throw new Exception("Input was not a nutural number.");
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
            return SumOfMultiplication(int.Parse(inputString)).ToString();
        }

        private BigInteger SumOfMultiplication(int inputNumber)
        {
            //TODO : token is not used
            //var tokenSource = new CancellationTokenSource();
            //var token = tokenSource.Token;

            //var taskOfThree = new Task<BigInteger>(() => SumOfMultiplication(inputNumber, 3), token, TaskCreationOptions.LongRunning);
            //var taskOfFive = new Task<BigInteger>(() => SumOfMultiplication(inputNumber, 5), token, TaskCreationOptions.LongRunning);

            //var taskResults = Task.WhenAll(taskOfThree, taskOfFive).Result;

            //return taskResults.Aggregate(BigInteger.Zero, (result, next) => result + next);

            var taskOfThree = SumOfMultiplication(inputNumber, 3);
            var taskOfFive = SumOfMultiplication(inputNumber, 5);

            return taskOfThree + taskOfFive;
        }

        private BigInteger SumOfMultiplication(int inputNumber, int divisor)
        {
            var divisibleNumber = CalculateDivisibleNumber(inputNumber, divisor);
            return AccumulateOnMultiplications(divisor, divisibleNumber);
        }

        public BigInteger AccumulateOnMultiplications(int seed, int power)
        {
            double result = 0;
            for (var i = 1; i <= power; i++)
            {
                result += Math.Pow(seed, i);
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

        public int CalculateDivisibleNumber(int input, int divisor)
        {
            return input % divisor == 0 ? input / divisor - 1 : input / divisor;
        }

    }
}
