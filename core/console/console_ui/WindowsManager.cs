using System;
using System.Collections.Generic;
using console_ui.models;

namespace console_ui
{
    public class WindowsManager
    {
        private IList<Window> _windows;
        private Window _activeWindow;

        public IList<Window> Windows => _windows;
        public Window ActiveWindow => _activeWindow;

        public WindowsManager(Window mainWindow)
        {
            _windows = new List<Window>();
            _windows.Add(mainWindow);

            _activeWindow = mainWindow;
        }

        public void AddWindow(Window window)
        {
            if (window == null)
            {
                return;
            }

            _windows.Add(window);

            if (_windows.Count == 1)
            {
                _activeWindow = window;
            }
        }

        public bool RemoveWindow(Window window)
        {
            if (_windows.Count == 1)
            {
                return false;
            }

            if (_activeWindow == window)
            {
                _activeWindow = _windows[0];
            }

            return _windows.Remove(window);
        }

        public bool SetActive(Window window)
        {
            var index = _windows.IndexOf(window);

            if (index > 0)
            {
                _activeWindow = window;
                return true;
            }

            return false;
        }
    }
}