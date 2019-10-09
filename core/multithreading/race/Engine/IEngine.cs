using Multithreading.Race.Models;

namespace Multithreading.Race.Engine{
    public interface IEngine
    {
        void UpdateVelocity(GameObject gameObject);
        int GetUpdateInterval();
    }
}