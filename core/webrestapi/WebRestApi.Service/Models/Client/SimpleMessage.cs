namespace WebRestApi.Service.Models.Client
{
    public class SimpleMessage
    {
        public int? FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Message { get; set; }
    }
}