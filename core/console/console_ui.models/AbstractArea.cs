using System;

namespace console_ui.models
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractArea
    {
        private IAreaPrinter _printer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="printer"></param>
        public AbstractArea(IAreaPrinter printer)
        {
            _printer = printer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Position StartPosition { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public AreaSize Size { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Name { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public bool IsActive { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public InputType? InputType { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public bool IsConcurrent { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isActive"></param>
        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Print()
        {
            _printer.Print(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Handle(ConsoleKey key)
        {
            if (InputType == null)
            {
                return;
            }

            HandleInput(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        protected abstract void HandleInput(ConsoleKey key);
    }
}