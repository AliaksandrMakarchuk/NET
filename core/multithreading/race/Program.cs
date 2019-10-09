using System;
using Multithreading.Race;
using Multithreading.Race.Engine;
using Multithreading.Race.UserInterface;

namespace race
{
    class Program
    {
        static void Main(string[] args)
        {
            var racerCount = Environment.ProcessorCount;
            var fieldWidth = 60;
            var game = new Game(racerCount, fieldWidth, new RandomEngine(), new ConsoleUserInterface(racerCount, fieldWidth));
            game.Init();
            game.Start();

            Console.ReadKey(true);
        }
    }
}
