namespace console_ui.models
{
    /// <summary>
    /// 
    /// </summary>
    public class IntersectionChecker : IIntersectionChecker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public bool AreIntersect(AbstractArea first, AbstractArea second)
        {
            return CheckIntersection(first, second) || CheckIntersection(second, first);
        }

        private bool CheckIntersection(AbstractArea first, AbstractArea second)
        {
            return second.StartPosition.Left >= first.StartPosition.Left &&
                second.StartPosition.Left < first.StartPosition.Left + first.Size.Width &&
                second.StartPosition.Top >= first.StartPosition.Top &&
                second.StartPosition.Top < first.StartPosition.Top + first.Size.Height;
        }
    }
}