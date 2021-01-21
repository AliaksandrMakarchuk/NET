using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace WebRestApi.WebApp
{
    public class NetworkManager : INetworkManager
    {
        private NetworkConfigurationOptions _networkConfiguration;

        public NetworkManager(IOptions<NetworkConfigurationOptions> networkConfiguration)
        {
            _networkConfiguration = networkConfiguration.Value;
        }

        public async Task<bool> Login(string login, string password)
        {
            var tokenUri = $"{_networkConfiguration.Api}/token";
            var body = $"login={login}&password={password}";
            var jsonBody = JsonSerializer.Serialize(new Credentials { Login = login, Password = password });
            var requestBody = Encoding.UTF8.GetBytes(jsonBody);

            using(var client = new WebClient())
            {
                try
                {
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.Headers.Add(HttpRequestHeader.ContentLength, requestBody.Length.ToString());
                    var res = await client.UploadDataTaskAsync(tokenUri, "POST", requestBody);
                    var res1 = Encoding.UTF8.GetString(res);
                    return true;
                }
                catch (WebException ex)
                {
                    var res = ex.Message;
                    return false;
                }
            }
        }
    }
}