namespace console_ui.models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAreaPrinter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <typeparam name="TArea"></typeparam>
        void Print<TArea>(TArea area)
        where TArea : AbstractArea;
    }
}