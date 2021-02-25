using System;

namespace WebRestApi.Logger
{
    public class BatchingLoggerOptions : FileLoggerOptions
    {
        public TimeSpan FlushPeriod { get; set; }
    }
}