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

        public async Task<bool> Login(string login, string password)
        {
            try
            {
                var tokenUri = $"{_networkConfiguration.Api}/token";
                var body = $"login={login}&password={password}";
                var jsonBody = JsonSerializer.Serialize(new Creds { Login = login, Password = password });
                var requestBody = Encoding.UTF8.GetBytes(jsonBody);

                using(var client = new WebClient())
                {
                    try
                    {
                        client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                        client.Headers.Add(HttpRequestHeader.ContentLength, requestBody.Length.ToString());
                        var res = await client.UploadDataTaskAsync(tokenUri, "POST", requestBody);
                        var res1 = Encoding.UTF8.GetString(res);
                    }
                    catch (WebException ex)
                    {
                        var res = ex.Message;
                    }
                }

                // var request = WebRequest.Create(tokenUri);
                // request.Method = "POST";
                // request.ContentType = "application/json";
                // request.ContentLength = requestBody.Length;

                // using(var stream = await request.GetRequestStreamAsync())
                // {
                //     await stream.WriteAsync(requestBody, 0, requestBody.Length);
                // }

                // using(var response = await request.GetResponseAsync())
                // {
                //     using(var stream = response.GetResponseStream())
                //     {
                //         using(var reader = new StreamReader(stream))
                //         {
                //             var res = await reader.ReadToEndAsync();
                //         }
                //     }
                // }
            }
            catch (WebException ex)
            {
                var res = ex.Message;
            }

            return false;
        }
    }
}