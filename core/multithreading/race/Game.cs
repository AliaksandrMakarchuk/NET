using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Multithreading.Race.Engine;
using Multithreading.Race.Models;
using Multithreading.Race.UserInterface;

namespace Multithreading.Race
{
    public class Game
    {
        private readonly IUserInterface _userInterface;
        private readonly int _racerCount;
        private readonly IEngine _engine;
        private readonly int _fieldWidth;
        private readonly IList<GameObject> _gameObjects;
        private Task[] _tasks;
        private CancellationTokenSource _cts;
        private static object _locker = new object();

        public Game(int racerCount, int fieldWidth, IEngine engine, IUserInterface userInterface)
        {
            _racerCount = racerCount;
            _engine = engine;
            _fieldWidth = fieldWidth;
            _userInterface = userInterface;
            _gameObjects = new List<GameObject>();
        }

        public void Init()
        {
            for (var i = 0; i < _racerCount; i++)
            {
                _gameObjects.Add(new GameObject(new Racer(i), new GameRacerState(i)));
            }

            _tasks = new Task[_racerCount];

            _userInterface.PrintInitialGameState(_gameObjects);
        }

        public void Start()
        {
            _cts = new CancellationTokenSource();

            var taskFactory = new TaskFactory(_cts.Token);
            var i = 0;

            _gameObjects.ToList().ForEach(gameObject =>
            {
                _tasks[i++] = taskFactory.StartNew(() => UpdateRacerState(gameObject));
            });

            var continuationTask = new TaskFactory().ContinueWhenAny(_tasks, (t) =>
            {
                _userInterface.PrintWinner(GetWinner()?.Racer.Mark);
            });

            continuationTask.Wait();
        }

        private void UpdateRacerState(GameObject gameObject)
        {
            while (!_cts.IsCancellationRequested)
            {
                var interval = _engine.GetUpdateInterval();
                Thread.Sleep(interval);

                lock (_locker)
                {
                    if (_cts.IsCancellationRequested)
                    {
                        return;
                    }

                    _engine.UpdateVelocity(gameObject);
                    gameObject.State.X += gameObject.Racer.Velocity;
                    _userInterface.UpdateRacerPosition(gameObject);
                    CheckIfGameOver();
                }
            }
        }

        private void CheckIfGameOver()
        {
            _gameObjects.ToList().ForEach(gameObject =>
            {
                if (gameObject.State.X >= _fieldWidth)
                {
                    _cts.Cancel();
                }
            });
        }

        private GameObject GetWinner()
        {
            var maxX = _gameObjects.Max(x => x.State.X);
            return _gameObjects.First(x => x.State.X == maxX);
        }
    }
}