namespace Multithreading.Race.Models
{
    public class GameObject
    {
        private Racer _racer;
        private GameRacerState _state;

        public Racer Racer => _racer;
        public GameRacerState State => _state;

        public GameObject(Racer racer, GameRacerState state)
        {
            _racer = racer;
            _state = state;
        }
    }
}