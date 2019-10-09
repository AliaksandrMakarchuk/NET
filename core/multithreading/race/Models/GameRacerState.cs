using System;

namespace Multithreading.Race.Models
{
    public class GameRacerState
    {
        private int _order;

        public int X { get; set; }
        public int Order => _order;

        public GameRacerState(int order){
            _order = order;
        }
    }
}