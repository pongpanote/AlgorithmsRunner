using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AlgorithmsRunner.Common.Interfaces;

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
            var jObject = new JObject();
            var properties = GetInputPropertiesList(selectedAlgorithm);
            foreach (var input in properties)
            {
                Console.Write($"{input.DisplayName}: ");
                jObject.Add(new JProperty(input.InputName, Console.ReadLine()));
            }

            return jObject;
        }

        private static List<InputAttribute> GetInputPropertiesList(IAlgorithmItem selectedAlgorithm)
        {
            var propertiesList = new List<InputAttribute>();
            var properties = selectedAlgorithm.GetType().GetProperties(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance).Where(x => x.GetAttributeOfType<InputAttribute>() != null).ToList();

            foreach (var property in properties)
            {
                propertiesList.Add(property.GetAttributeOfType<InputAttribute>());
            }

            return propertiesList;
        }

        public void DisplayOutput(JObject jObject)
        {
            Console.WriteLine($"Result: {jObject.Property(Constants.RESULT).Value.Value<string>()}");
            Console.WriteLine();
        }
    }
}
