namespace console_ui.models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAreaBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="printer"></param>
        /// <returns></returns>
        IAreaBuilder SetPrinter(IAreaPrinter printer);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IAreaBuilder SetName(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPosition"></param>
        /// <returns></returns>
        IAreaBuilder SetStartPosition(Position startPosition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaSize"></param>
        /// <returns></returns>
        IAreaBuilder SetAreaSize(AreaSize areaSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsConcurrent"></param>
        /// <returns></returns>
        IAreaBuilder SetConcurrency(bool IsConcurrent);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        AbstractArea Build();
    }
}