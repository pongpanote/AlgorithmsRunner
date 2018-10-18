using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AlgorithmsRunner.Common;

namespace AlgorithmsRunner.SequenceAnalysis
{
    public class Processor : IAlgorithmItem
    {
        public void Run()
        {
            throw new NotImplementedException();
        }

        public string GetHelp()
        {
            return "Find the uppercase words in a string, provided as input, and order all characters in these words alphabetically.\r\n" +
                   "Input: \"This IS a STRING\"\r\n" + 
                   "Output: \"GIINRSST\"";
        }

        public string Execute(string input)
        {
            var uppercaseWordsCollection = RetrieveUppercaseWords(input);
            var uppercaseString = uppercaseWordsCollection.Aggregate("", (result, next) => result + next);

            return uppercaseString.OrderBy(c => c).Aggregate("", (result, next) => result + next);
        }

        private IEnumerable<string> RetrieveUppercaseWords(string input)
        {
            var uppercaseWordRegex = new Regex("\\b[A-Z]+\\b");

            return uppercaseWordRegex.Matches(input)
                .Cast<Match>()
                .Select(m => m.Value);
        }

    }
}
