using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace WebRestApi.WebApp
{
    public class Creds
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class NetworkManager : INetworkManager
    {
        private NetworkConfigurationOptions _networkConfiguration;

        public NetworkManager(IOptions<NetworkConfigurationOptions> networkConfiguration)
        {
            _networkConfiguration = networkConfiguration.Value;
        }

        public async Task Connect()
        {
            using(var client = new WebClient())
            {
                try
                {
                    var test = await client.DownloadDataTaskAsync($"{_networkConfiguration.Api}/test");
                }
                catch (WebException ex)
                {
                    var exc = ex.Message;
                }
            }
        }

        public async Task<bool> Login(string login, string password)
        {
            try
            {
                var body = $"login={login}&password={password}";
                // var requestBody = Encoding.UTF8.GetBytes(body);
                var jsonBody = JsonSerializer.Serialize(new Creds{Login=login,Password=password});
                var requestBody = Encoding.UTF8.GetBytes(jsonBody);

                var request = WebRequest.Create($"{_networkConfiguration.Api}/token");
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = requestBody.Length;

                using(var stream = await request.GetRequestStreamAsync())
                {
                    await stream.WriteAsync(requestBody, 0, requestBody.Length);
                }

                using(var response = await request.GetResponseAsync())
                {
                    using(var stream = response.GetResponseStream())
                    {
                        using(var reader = new StreamReader(stream))
                        {
                            var res = await reader.ReadToEndAsync();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                var res = ex.Message;
            }

            return false;
        }
    }
}