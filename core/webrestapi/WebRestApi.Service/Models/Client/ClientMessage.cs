namespace WebRestApi.Service.Models.Client
{
    public class ClientMessage
    {
        public int Id { get; set; } = -1;
        public ClientUser From { get; set; }
        public ClientUser To { get; set; }
        public string Message { get; set; }
    }
}