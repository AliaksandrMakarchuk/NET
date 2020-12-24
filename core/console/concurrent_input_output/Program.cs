using System;
using System.Threading;
using System.Threading.Tasks;

namespace concurrent_input_output
{
    class Program
    {
        private static object _locker = new object();

        static void Main(string[] args)
        {
            Console.Clear();

            var tokenSource = new CancellationTokenSource();

            new TaskFactory().StartNew(ConcurrentOutput, tokenSource.Token);

            lock(_locker)
            {
                Console.SetCursorPosition(0, 2);
                Console.Write("Input >");
            }

            var userInput = Console.ReadLine();

            tokenSource.Cancel();
            Console.WriteLine($"User input: {userInput}");
            Thread.Sleep(1000);
        }

        private static async Task ConcurrentOutput(object tokenObject)
        {
            var token = (CancellationToken) tokenObject;
            var counter = 0;

            while (!token.IsCancellationRequested)
            {
                lock(_locker)
                {
                    (int, int) curentPosition = (Console.CursorLeft, Console.CursorTop);
                    (ConsoleColor, ConsoleColor) currentColors = (Console.BackgroundColor, Console.ForegroundColor);

                    Console.SetCursorPosition(0, 0);
                    
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Output: {counter++}");
                    Console.BackgroundColor = currentColors.Item1;
                    Console.ForegroundColor = currentColors.Item2;

                    Console.SetCursorPosition(curentPosition.Item1, curentPosition.Item2);
                }

                await Task.Delay(150);
            }

            Console.WriteLine($"THREAD {Thread.CurrentThread.ManagedThreadId} has been stopped");
        }
    }
}