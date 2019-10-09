using Multithreading.Race.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Multithreading.Race.UserInterface
{
    public class ConsoleUserInterface : IUserInterface
    {
        private readonly int racerCount;
        private readonly int fieldWidth;

        public ConsoleUserInterface(int racerCount, int fieldWidth)
        {
            this.racerCount = racerCount;
            this.fieldWidth = fieldWidth;
        }

        public void PrintInitialGameState(IList<GameObject> gameObjects)
        {
            ClearField();
            PrintRacers(gameObjects);
        }

        public void UpdateRacerPosition(GameObject gameObject)
        {
            Console.SetCursorPosition(0, gameObject.State.Order + 1);
            Console.Write(string.Join(string.Empty, Enumerable.Repeat('=', gameObject.State.X)));
            Console.Write(gameObject.Racer.Mark);
        }

        public void PrintWinner(string mark)
        {
            Console.SetCursorPosition(0, racerCount + 3);
            Console.WriteLine("Game over");
            Console.WriteLine($"Winner: {mark}");
        }

        private void ClearField()
        {
            Console.Clear();

            Console.Write((string.Join(string.Empty, Enumerable.Repeat('#', fieldWidth))));

            for (var row = 1; row < racerCount + 1; row++)
            {
                Console.SetCursorPosition(fieldWidth, row);
                Console.Write('|');
            }

            Console.WriteLine();
            Console.Write((string.Join(string.Empty, Enumerable.Repeat('#', fieldWidth))));
        }

        private void PrintRacers(IEnumerable<GameObject> gameObjects)
        {
            gameObjects.ToList().ForEach(gameObject =>
            {
                Console.SetCursorPosition(gameObject.State.X, gameObject.State.Order + 1);
                Console.Write(gameObject.Racer.Mark);
            });
        }
    }
}