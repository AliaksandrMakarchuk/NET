using System.Collections.Generic;
using Multithreading.Race.Models;

namespace Multithreading.Race.UserInterface{
    public interface IUserInterface
    {
        void UpdateRacerPosition(GameObject gameObject);
        void PrintInitialGameState(IList<GameObject> gameObjects);
        void PrintWinner(string mark);
    }
}