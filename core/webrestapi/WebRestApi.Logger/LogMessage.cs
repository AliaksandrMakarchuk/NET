using System;

namespace WebRestApi.Logger
{
    public class LogMessage
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Message { get; set; }
    }
}