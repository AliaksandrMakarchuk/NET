namespace WebRestApi.Logger
{
    public class FileLoggerOptions : BatchingLoggerOptions
    {
        public string LogDirectory { get; set; }
        public string FileName { get; set; }
        public int? FileSizeLimit { get; set; }
        public int? RetainedFileCountLimit { get; set; }
    }
}