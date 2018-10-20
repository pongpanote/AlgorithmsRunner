using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AlgorithmsRunner.Common;
using Newtonsoft.Json.Linq;

namespace AlgorithmsRunner.SequenceAnalysis
{
    public class Processor : IAlgorithmItem
    {
        public JObject Run(JObject inputJObject)
        {
            throw new NotImplementedException();
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
