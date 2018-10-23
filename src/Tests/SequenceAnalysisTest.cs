using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using AlgorithmsRunner.SequenceAnalysis;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

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
        public void NumberofUppercaseWordsAreCorrectlyExtracted(string input, int expectResult, string because = null)
        {
            var result = m_Processor.ExtractUppercaseWords(input);

            result.Count().Should().Be(expectResult, because);
        }

        [Test]
        public void UppercaseWordsAreCorrectlyExtracted()
        {
            var expectedResult = new List<string>
            {
                "IS",
                "STRING"
            };

            var result = m_Processor.ExtractUppercaseWords("This IS a STRING");

            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestCase("a Test", "", "no uppercase")]
        [TestCase("A Test", "A")]
        [TestCase("A TEST", "AESTT")]
        [TestCase("This IS a STRING", "GIINRSST")]
        public void ExecuteOnText(string input, string expectResult, string because = null)
        {
            var result = m_Processor.Execute(input);

            result.Should().Be(expectResult, because);
        }

        [TestCase("A Test", "{\r\n  \"Result\": \"A\"\r\n}")]
        [TestCase("A TEST", "{\r\n  \"Result\": \"AESTT\"\r\n}")]
        [TestCase("This IS a STRING", "{\r\n  \"Result\": \"GIINRSST\"\r\n}")]
        public void ProcessWithConstructor(string input, string expectedResult)
        {
            m_Processor = new Processor(input);
            m_Processor.Process(null).ToString().Should().BeEquivalentTo(expectedResult);
        }

        [TestCase("{\r\n  \"Text\": \"A Test\"\r\n}", "{\r\n  \"Result\": \"A\"\r\n}")]
        [TestCase("{\r\n  \"Text\": \"A TEST\"\r\n}", "{\r\n  \"Result\": \"AESTT\"\r\n}")]
        [TestCase("{\r\n  \"Text\": \"This IS a STRING\"\r\n}", "{\r\n  \"Result\": \"GIINRSST\"\r\n}")]
        public void ProcessWithJObject(string input, string expectedResult)
        {
            m_Processor.Process(JObject.Parse(input)).ToString().Should().BeEquivalentTo(expectedResult);
        }

        [TestCase("{\r\n  \"Input\": \"A\"\r\n}")]
        public void ProcessWithInvalidJObjectThrowException(string input)
        {
            Action act = () => m_Processor.Process(JObject.Parse(input));

            act.ShouldThrow<JSchemaValidationException>();
        }
    }
}
