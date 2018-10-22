using AlgorithmsRunner.Common;
using System;
using System.Collections.Generic;
using AlgorithmsRunner.Common.Interfaces;

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

                    var result = selectedAlgorithm.Process(input);

                    userInterface.DisplayOutput(result);
                }
                catch (Exception ex)
                {
                    ExceptionHandler(ex);
                }
            }

            PauseBeforeExit();
        }

        private static void ExceptionHandler(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
#if DEBUG
            Console.WriteLine($"{ex.StackTrace}");
#endif
            Console.WriteLine("\r\nPress any key to start over.");
            Console.ReadLine();
        }

        private static void PauseBeforeExit()
        {
            Console.WriteLine("\r\nPress any key to exit.");
            Console.ReadLine();
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"Error: {e.ExceptionObject}");
            PauseBeforeExit();
            Environment.Exit(1);
        }
    }
}
