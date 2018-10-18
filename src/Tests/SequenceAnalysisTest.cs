using AlgorithmsRunner.SequenceAnalysis;
using NUnit.Framework;

namespace AlgorithmsRunner.Tests
{
    public class SequenceAnalysisTest
    {
        [Test]
        public void ValidateInput()
        {
            new Processor().Execute("");
        }

    }
}
