namespace console_ui.models
{
    /// <summary>
    /// 
    /// </summary>
    public class Position
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int Left { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int Top { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <param name="left"></param>
        public Position(int top, int left)
        {
            Left = left;
            Top = top;
        }
    }
}