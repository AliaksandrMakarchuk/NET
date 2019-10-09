using System.Collections.Generic;
using Multithreading.Race.Models;

namespace Multithreading.Race.UserInterface{
    public class EmptyUserInterface : IUserInterface
    {
        public void PrintInitialGameState(IList<GameObject> gameObjects)
        {
            // Do nothing
        }

        public void PrintWinner(string mark)
        {
            // Do nothing
        }

        public void UpdateRacerPosition(GameObject gameObject)
        {
            // Do noting
        }
    }
}