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

            IUserInterface userInterface = new ConsoleUserInterface();

            while (true)
            {
                var selectedAlgorithm = userInterface.GetAlgorithm(algorithmsList);

                if (selectedAlgorithm == null)
                {
                    break;
                }

                try
                {
                    var input = userInterface.GetInput(selectedAlgorithm);

                    var result = selectedAlgorithm.Run(input);

                    userInterface.DisplayOutput(result);
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine($"Error: {ex.Message}\r\n {ex.StackTrace}");
#else
                    Console.WriteLine($"Error: {ex.Message}");
#endif
                    Console.WriteLine("\r\nPress any key to start over.");
                    Console.ReadLine();
                }
            }

            PauseBeforeExit();
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"Error: {e.ExceptionObject}");
            PauseBeforeExit();
            Environment.Exit(1);
        }

        private static void PauseBeforeExit()
        {
            Console.WriteLine("\r\nPress any key to exit.");
            Console.ReadLine();
        }
    }
}
