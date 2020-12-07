using System.Threading.Tasks;

namespace WebRestApi.WebApp {
    public interface INetworkManager {
        Task<bool> SendRequest(object request);
    }
}