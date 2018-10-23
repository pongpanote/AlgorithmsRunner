using AlgorithmsRunner.Common;
using AlgorithmsRunner.Common.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlgorithmsRunner.SumOfMultiple
{
    [Guid(Constants.SUM_OF_MULTIPLE_GUID)]
    public class Processor : BaseInputValidator, IAlgorithmItem
    {
        [Input(InputName = Constants.SUM_OF_MULTIPLE_INPUT_NAME, DisplayName = "Ceiling Number")]
        private BigInteger InputNumber { get; set; }

        private readonly int[] m_Samples = { 3, 5 };
        public Processor() : this(0)
        { }

        public Processor(BigInteger inputNumber)
        {
            InputNumber = inputNumber;
        }

        public JObject Process(JObject inputJObject)
        {
            if (inputJObject != null)
            {
                Validate<Processor>(inputJObject);
                ExtractInput(inputJObject);
            }

            return new JObject(new JProperty(Constants.RESULT, Execute()));
        }

        private void ExtractInput(JObject inputJObject)
        {
            var inputString = inputJObject.Property(Constants.SUM_OF_MULTIPLE_INPUT_NAME).Value.Value<string>();
            if (!IsNaturalNumber(inputString))
            {
                throw new FormatException("Input was not a natural number.");
            }

            InputNumber = BigInteger.Parse(inputString);
        }

        public string GetDisplayName()
        {
            return "SumOfMultiple";
        }

        public string GetDescription()
        {
            return "Find the sum of all natural numbers that are a multiple of 3 or 5 below a limit provided as input.\r\n" +
                   "Input: \"6\"\r\n" +
                   "Output: \"8\"";
        }

        private string Execute()
        {
            return Execute(InputNumber);
        }

        public string Execute(BigInteger input)
        {
            return SumOfMultiplicationAsync(input, m_Samples).Result.ToString();
        }

        private async Task<BigInteger> SumOfMultiplicationAsync(BigInteger inputNumber, int[] samples)
        {
            var taskResults = await Task.WhenAll(samples.Select(i => SumOfMultiplication(inputNumber, i)));
            return taskResults.Aggregate(BigInteger.Zero, (result, next) => result + next);
        }

        private Task<BigInteger> SumOfMultiplication(BigInteger inputNumber, int divisor)
        {
            var multiplication = CalculateMultiplicationNumber(inputNumber, divisor);
            return Task.Run(() => AccumulateOnMultiplication(divisor, multiplication));
        }

        public BigInteger AccumulateOnMultiplication(BigInteger seed, BigInteger multiplication)
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

        public BigInteger CalculateMultiplicationNumber(BigInteger input, int divisor)
        {
            return input % divisor == 0 ? input / divisor - 1 : input / divisor;
        }

    }
}
