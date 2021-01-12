using System.Net;

namespace WebRestApi.WebApp
{
    public class NetworkManager
    {
        public void Connect()
        {
            using(var client = new WebClient())
            {
                var test = client.DownloadDataTaskAsync("http://localhost:3010/api/test");
            }
        }
    }
}