using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace AlgorithmsRunner.Tests
{
    public class SequenceAnalysisTest
    {
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
            var instance = new SequenceAnalysis.Processor();
            var result = instance.ExtractUppercaseWords(input);

            result.Count().Should().Be(expectResult, because);
        }

    }
}
