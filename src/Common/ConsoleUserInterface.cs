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
            
            string choice;
            do
            {
                choice = GetUserChoice();

                switch (choice)
                {
                    case null:
                        continue;
                    case "?":
                        DisplayDescription();
                        break;
                }

                if (m_AlgorithmsDictionary.TryGetValue(choice, out var algorithmSelected))
                {
                    return algorithmSelected;
                }

            } while (choice != "0");

            return null;
        }

        private void DisplayDescription()
        {
            foreach (var algorithm in m_AlgorithmsDictionary)
            {
                Console.WriteLine($"[{algorithm.Value.GetDisplayName()}]");
                Console.WriteLine($"{algorithm.Value.GetDescription()}");
                Console.WriteLine();
            }
        }

        private string GetUserChoice()
        {
            foreach (var algorithm in m_AlgorithmsDictionary)
            {
                Console.WriteLine($"{algorithm.Key}. {algorithm.Value.GetDisplayName()}");
            }
            Console.WriteLine("Please select algorithm to run (? for descriptions, 0 to exit)");
            Console.Write(">");

            return Console.ReadLine();
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
