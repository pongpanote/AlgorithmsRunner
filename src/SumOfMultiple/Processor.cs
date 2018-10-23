using AlgorithmsRunner.Common;
using AlgorithmsRunner.Common.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;

namespace AlgorithmsRunner.SumOfMultiple
{
    public class Processor : IAlgorithmItem
    {
        [Input(InputName = Constants.SUM_OF_MULTIPLE_INPUT_NAME, DisplayName = "Ceiling Number")]
        private int InputNumber { get; set; }

        private readonly int[] m_Samples = { 3, 5 };
        public Processor() : this(0)
        { }

        public Processor(int inputNumber)
        {
            InputNumber = inputNumber;
        }

        public JObject Process(JObject inputJObject)
        {
            if (inputJObject != null)
            {
                ExtractInput(inputJObject);
            }

            return new JObject(new JProperty(Constants.RESULT, Execute()));
        }

        private void ExtractInput(JObject inputJObject)
        {
            var schema = JSchema.Parse(Common.Properties.Resources.json_schema_SumOfMultiple);
            if (!inputJObject.IsValid(schema))
            {
                throw new JSchemaValidationException("Input format was invalid against JSON schema.");
            }

            var inputString = inputJObject.Property(Constants.SUM_OF_MULTIPLE_INPUT_NAME).Value.Value<string>();
            if (!IsNaturalNumber(inputString))
            {
                throw new FormatException("Input was not a natural number.");
            }

            InputNumber = int.Parse(inputString);
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

        private string Execute()
        {
            return Execute(InputNumber);
        }

        public string Execute(int input)
        {
            return SumOfMultiplicationAsync(input, m_Samples).Result.ToString();
        }

        private async Task<BigInteger> SumOfMultiplicationAsync(int inputNumber, int[] samples)
        {
            var taskResults = await Task.WhenAll(samples.Select(i => SumOfMultiplication(inputNumber, i)));
            return taskResults.Aggregate(BigInteger.Zero, (result, next) => result + next);
        }

        private Task<BigInteger> SumOfMultiplication(int inputNumber, int divisor)
        {
            var multiplication = CalculateMultiplicationNumber(inputNumber, divisor);
            return Task.Run(() => AccumulateOnMultiplications(divisor, multiplication));
        }

        public BigInteger AccumulateOnMultiplications(int seed, int multiplication)
        {
            BigInteger result = 0;
            for (var i = 1; i <= multiplication; i++)
            {
                result += seed * i;
            }

            return result;
        }

        public bool IsNaturalNumber(string input)
        {
            var naturalNumberRegex = new Regex("^[0-9]+$");
            return naturalNumberRegex.IsMatch(input);
        }

        public int CalculateMultiplicationNumber(int input, int divisor)
        {
            return input % divisor == 0 ? input / divisor - 1 : input / divisor;
        }

    }
}
