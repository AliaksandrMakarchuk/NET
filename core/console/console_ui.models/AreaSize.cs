namespace console_ui.models
{
    /// <summary>
    /// 
    /// </summary>
    public class AreaSize
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int Width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public AreaSize(int width, int height)
        {
            Height = height;
            Width = width;
        }
    }
}