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
        }

        private static async Task ConcurrentOutput(object tokenObject)
        {
            var token = (CancellationToken) tokenObject;
            var counter = 0;

            while (true)
            {
                lock(_locker)
                {
                    Console.
                    var curentPosition = Console.GetCursorPosition();

                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine($"Output: {counter++}");
                    Console.SetCursorPosition(curentPosition.Left, curentPosition.Top);
                }

                await Task.Delay(150);
            }
        }
    }
}