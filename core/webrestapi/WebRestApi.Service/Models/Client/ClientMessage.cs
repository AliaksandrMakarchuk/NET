namespace WebRestApi.Service.Models.Client
{
    public class ClientMessage
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public string Message { get; set; }
    }
}