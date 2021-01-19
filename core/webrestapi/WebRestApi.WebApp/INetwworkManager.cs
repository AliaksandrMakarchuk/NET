using System.Threading.Tasks;

namespace WebRestApi.WebApp {
    public interface INetworkManager {
        Task<bool> Login(string login, string password);
    }
}