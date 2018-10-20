using AlgorithmsRunner.Common;
using System;
using System.Collections.Generic;

namespace AlgorithmsRunner.Runner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            var algorithmsList = new List<IAlgorithmItem>
                                    {
                                        new SumOfMultiple.Processor(),
                                        new SequenceAnalysis.Processor()
                                    };

            var userInterface = new ConsoleUserInterface();

            //create choices
            while (true)
            {
                var selectedAlgorithm = userInterface.GetAlgorithm(algorithmsList);

                if (selectedAlgorithm == null)
                {
                    break;
                }

                //get input for that algo
                var input = userInterface.GetInput(selectedAlgorithm);

                //Run
                var result = selectedAlgorithm.Run(input);

                //Displpay result
                userInterface.DisplayOutput(result);
            }

        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"Error: {e.ExceptionObject}");
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            Environment.Exit(1);
        }
    }
}
