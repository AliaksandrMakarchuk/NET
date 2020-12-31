using System;
using System.Collections.Generic;
using System.Linq;

namespace console_ui.models
{
    /// <summary>
    /// 
    /// </summary>
    public class Window
    {
        private IEnumerable<AbstractArea> _areas;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AbstractArea ActiveArea => _areas.SingleOrDefault(x => x.IsActive);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areas"></param>
        public Window(IEnumerable<AbstractArea> areas)
        {
            if (areas.Count(x => x.IsActive) > 1)
            {
                throw new ArgumentException();
            }

            _areas = areas;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Print()
        {
            foreach (var area in _areas)
            {
                // if concurrent - start task
                
                area.Print(); // default behavior
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyInfo"></param>
        public void HandleInput(ConsoleKeyInfo keyInfo)
        {
            var activeArea = _areas.SingleOrDefault(x => x.IsActive);
            if (activeArea == null)
            {
                return;
            }

            activeArea.Handle(keyInfo.Key);
        }
    }
}