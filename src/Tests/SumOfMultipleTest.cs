using AlgorithmsRunner.SumOfMultiple;
using FluentAssertions;
using NUnit.Framework;

namespace AlgorithmsRunner.Tests
{
    public class SumOfMultipleTest
    {
        private Processor m_Processor;

        [OneTimeSetUp]
        public void Setup()
        {
            m_Processor = new Processor();
        }

        [TestCase(3, 3, 0)]
        [TestCase(5, 3, 1)]
        [TestCase(6, 3, 1)]
        [TestCase(9, 3, 2)]
        [TestCase(5, 5, 0)]
        [TestCase(10, 5, 1)]
        public void DivisibleNumberCalculation(int input, int divisor, int expectResult)
        {
            m_Processor.CalculateMultiplicationDivisibleNumber(input, divisor).Should().Be(expectResult);
        }

        [TestCase("0", true)]
        [TestCase("1", true)]
        [TestCase("-1", false)]
        [TestCase("1.0", false)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        public void IsNaturalNumber(string input, bool expectResult)
        {
            m_Processor.IsNaturalNumber(input).Should().Be(expectResult);
        }
        
        [TestCase(3, 1, 3)]
        [TestCase(3, 2, 9)]
        [TestCase(3, 3, 18)]
        [TestCase(5, 1, 5)]
        [TestCase(5, 2, 15)]
        [TestCase(5, 3, 30)]
        public void AccumulateOnMultiplications(int seed, int multiplication, int expectedResult)
        {
            m_Processor.AccumulateOnMultiplications(seed, multiplication).Should().Be(expectedResult);
        }

        [TestCase("4", "3")]
        [TestCase("5", "3")]
        [TestCase("6", "8")]
        [TestCase("7", "14")]
        [TestCase("8", "14")]
        [TestCase("9", "14")]
        [TestCase("10", "23")]
        [TestCase("11", "33")]
        public void SumOfMulipleOfThreeAndFiveOnText(string input, string expectedResult)
        {
            m_Processor.Execute(input).Should().Be(expectedResult);
        }
    }
}
