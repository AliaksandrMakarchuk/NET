namespace console_ui.models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIntersectionChecker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        bool AreIntersect(AbstractArea first, AbstractArea second);
    }
}