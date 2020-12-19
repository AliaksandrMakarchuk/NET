using System.Threading.Tasks;

namespace WebRestApi.WebApp
{
    public class NetworkManager : INetworkManager
    {
        public async Task<bool> SendRequest(object request)
        {
            await Task.Delay(1500);

            return true;
        }
    }
}