using AlgorithmsRunner.SumOfMultiple;
using FluentAssertions;
using NUnit.Framework;

namespace AlgorithmsRunner.Tests
{
    public class SumOfMultipleTest
    {
        private Processor m_Instance;

        [OneTimeSetUp]
        public void Setup()
        {
            m_Instance = new Processor();
        }

        [TestCase(3, 3, 0)]
        [TestCase(5, 3, 1)]
        [TestCase(6, 3, 1)]
        [TestCase(9, 3, 2)]
        [TestCase(5, 5, 0)]
        [TestCase(10, 5, 1)]
        public void TimesCalculation(int input, int divisor, int expectResult)
        {
            m_Instance.TimesItCanBreakDown(input, divisor).Should().Be(expectResult);
        }

        [TestCase("0", true)]
        [TestCase("1", true)]
        [TestCase("-1", false)]
        [TestCase("1.0", false)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        public void IsNaturalNumber(string input, bool expectResult)
        {
            m_Instance.IsNaturalNumber(input).Should().Be(expectResult);
        }

        [TestCase(3, 2, 12)]
        public void CalculateItsPower(int seed, int power, int expectedResult)
        {
            m_Instance.AccumulateAllPower(seed, power).Should().Be(expectedResult);
        }
    }
}
