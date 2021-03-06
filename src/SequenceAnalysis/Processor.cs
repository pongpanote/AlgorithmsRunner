﻿using AlgorithmsRunner.Common;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using AlgorithmsRunner.Common.Interfaces;

namespace AlgorithmsRunner.SequenceAnalysis
{
    [Guid(Constants.SEQUENCE_ANALYSIS_GUID)]
    public class Processor : BaseInputValidator, IAlgorithmItem
    {
        [Input(InputName = Constants.SEQUENCE_ANALYSIS_INPUT_NAME, DisplayName = "Input Text")]
        private string InputText { get; set; }

        public Processor() : this(string.Empty)
        { }

        public Processor(string inputText)
        {
            InputText = inputText;
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
            InputText = inputJObject.Property(Constants.SEQUENCE_ANALYSIS_INPUT_NAME).Value.Value<string>();
        }

        public string GetDisplayName()
        {
            return "SequenceAnalysis";
        }

        public string GetDescription()
        {
            return "Find the uppercase words in a string, provided as input, and order all characters in these words alphabetically.\r\n" +
                   "Input: \"This IS a STRING\"\r\n" +
                   "Output: \"GIINRSST\"";
        }

        private string Execute()
        {
            return Execute(InputText);
        }

        public string Execute(string inputString)
        {
            var uppercaseWordsCollection = ExtractUppercaseWords(inputString);
            var uppercaseString = uppercaseWordsCollection.Aggregate("", (result, next) => result + next);

            return uppercaseString.OrderBy(c => c).Aggregate("", (result, next) => result + next);
        }

        public IEnumerable<string> ExtractUppercaseWords(string inputString)
        {
            var uppercaseWordRegex = new Regex("\\b[A-Z]+\\b");

            return uppercaseWordRegex.Matches(inputString)
                .Cast<Match>()
                .Select(m => m.Value);
        }
    }
}
