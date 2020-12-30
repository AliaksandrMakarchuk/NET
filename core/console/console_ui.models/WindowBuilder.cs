using System;
using System.Collections.Generic;
using System.Linq;

namespace console_ui.models
{
    /// <summary>
    /// 
    /// </summary>
    public class WindowBuilder
    {
        private IList<AbstractArea> _areas;
        private IIntersectionChecker _intersectionChecker;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intersectionChecker"></param>
        public WindowBuilder(IIntersectionChecker intersectionChecker)
        {
            _intersectionChecker = intersectionChecker;
            _areas = new List<AbstractArea>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public WindowBuilder AddArea(AbstractArea area)
        {
            // if there is no Pop-Up area
            if (_areas.Any(x => _intersectionChecker.AreIntersect(x, area)))
            {
                throw new ArgumentException();
            }

            _areas.Add(area);

            return this;
        }

        /// <summary>
        /// Activate the most recently added area
        /// and drop activation on previously added
        /// </summary>
        /// <returns>Current instance of <see cref="WindowBuilder"/></returns>
        public WindowBuilder ActivateLastArea()
        {
            if (_areas.Any())
            {
                foreach (var area in _areas)
                {
                    area.SetIsActive(false);
                }
                _areas.Last().SetIsActive(true);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Window Build()
        {
            if (_areas.All(x => !x.IsActive))
            {
                throw new Exception();
            }

            return new Window(_areas);
        }
    }
}