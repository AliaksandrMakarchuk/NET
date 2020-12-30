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
            return CheckLeftIntersection(first, second) ||
                CheckLeftIntersection(second, first);
        }

        private bool CheckLeftIntersection(AbstractArea left, AbstractArea right)
        {
            return CheckTopLeftIntersection(left, right) ||
                CheckBottomLeftIntersection(left, right);
        }

        private bool CheckTopLeftIntersection(AbstractArea left, AbstractArea right)
        {
            return right.StartPosition.Left >= left.StartPosition.Left &&
                right.StartPosition.Left < left.StartPosition.Left + left.Size.Width &&
                right.StartPosition.Top >= left.StartPosition.Top &&
                right.StartPosition.Top < left.StartPosition.Top + left.Size.Height;
        }

        private bool CheckBottomLeftIntersection(AbstractArea left, AbstractArea right)
        {
            return right.StartPosition.Left > left.StartPosition.Left &&
                right.StartPosition.Left < left.StartPosition.Left + left.Size.Width &&
                right.StartPosition.Top + right.Size.Height > left.StartPosition.Top &&
                right.StartPosition.Top + right.Size.Height <= left.StartPosition.Top + left.Size.Height;
        }
    }
}