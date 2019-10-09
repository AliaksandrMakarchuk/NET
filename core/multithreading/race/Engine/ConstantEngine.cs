using Multithreading.Race.Models;

namespace Multithreading.Race.Engine
{
    public class ConstantEngine : IEngine
    {
        public int GetUpdateInterval()
        {
            return 500;
        }

        public void UpdateVelocity(GameObject gameObject)
        {
            gameObject.Racer.Velocity = 1;
        }
    }
}