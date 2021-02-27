using System;

namespace WebRestApi.Logger
{
    public class BatchingLoggerOptions
    {
        public TimeSpan FlushPeriod { get; set; }
    }
}