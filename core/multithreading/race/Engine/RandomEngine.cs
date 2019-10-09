using System;
using Multithreading.Race.Models;

namespace Multithreading.Race.Engine
{
    public class RandomEngine : IEngine
    {
        public void UpdateVelocity(GameObject gameObject)
        {
            var random = new Random(DateTime.Now.Millisecond).Next(0, 100);
            var velocity = 1;

            if (random > 70)
            {
                velocity = new Random(DateTime.Now.Millisecond).Next(2, 3);
            }

            gameObject.Racer.Velocity = velocity;
        }

        public int GetUpdateInterval()
        {
            return new Random(DateTime.Now.Millisecond).Next(300, 800);
        }
    }
}