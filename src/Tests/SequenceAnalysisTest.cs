using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using AlgorithmsRunner.SequenceAnalysis;

namespace AlgorithmsRunner.Tests
{
    public class SequenceAnalysisTest
    {
        private Processor m_Processor;

        [OneTimeSetUp]
        public void SetUp()
        {
            m_Processor = new Processor();
        }

        [TestCase("", 0)]
        [TestCase(" ", 0)]
        [TestCase("a", 0)]
        [TestCase("Aa", 0)]
        [TestCase("A1", 0)]
        [TestCase("aA", 0)]
        [TestCase("A", 1)]
        [TestCase("A ", 1, "whitespace is allowed")]
        [TestCase(" A", 1)]
        [TestCase("A.", 1, "punctuation is allowed")]
        [TestCase(".A", 1)]
        [TestCase("A Test", 1)]
        [TestCase("A TEST", 2)]
        public void UppercaseWordsAreExtractedCorrectly(string input, int expectResult, string because = null)
        {
            var result = m_Processor.ExtractUppercaseWords(input);

            result.Count().Should().Be(expectResult, because);
        }

        [TestCase("A Test", "A")]
        [TestCase("A TEST", "AESTT")]
        [TestCase("This IS a STRING", "GIINRSST")]
        public void SequenceAnalysisOnText(string input, string expectResult, string because = null)
        {
            var result = m_Processor.Execute(input);

            result.Should().Be(expectResult, because);
        }
    }
}
