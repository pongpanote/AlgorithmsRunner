using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsRunner.Common
{
    public class ConsoleUserInterface : IUserInterface
    {
        private Dictionary<string, IAlgorithmItem> m_AlgorithmsDictionary = new Dictionary<string, IAlgorithmItem>();
        public IAlgorithmItem GetAlgorithm(IEnumerable<IAlgorithmItem> algorithms)
        {
            if (!m_AlgorithmsDictionary.Any())
            {
                m_AlgorithmsDictionary = AggregateAlgorithm(algorithms);
            }


            return null;

        }

        private Dictionary<string, IAlgorithmItem> AggregateAlgorithm(IEnumerable<IAlgorithmItem> algorithms)
        {
            var algorithmsDictionary = new Dictionary<string, IAlgorithmItem>();
            var index = 1;

            foreach (var algorithm in algorithms)
            {
                algorithmsDictionary.Add(index.ToString(), algorithm);
                index++;
            }

            return algorithmsDictionary;
        }

        public JObject GetInput(IAlgorithmItem selectedAlgorithm)
        {
            throw new NotImplementedException();
        }

        public void DisplayOutput(JObject jObject)
        {
            throw new NotImplementedException();
        }
    }
}
