using System;
using System.Collections.Generic;
using console_ui.models;

namespace console_ui {
    public class WindowsManager {
        private IList<IWindow> _windows;
        private IWindow _activeWindow;

        public IList<IWindow> Windows => _windows;
        public IWindow ActiveWindow => _activeWindow;

        public WindowsManager (IWindow mainWindow) {
            _windows = new List<IWindow> ();
            _windows.Add (mainWindow);

            _activeWindow = mainWindow;
        }

        public void AddWindow (IWindow window) {
            _windows.Add (window);
        }

        public bool RemoveWindow (IWindow window) {
            if (_windows.Count == 1) {
                return false;
            }

            if (_activeWindow == window) {
                _activeWindow = _windows[0];
            }

            return _windows.Remove (window);
        }

        public bool SetActive (IWindow window) {
            var index = _windows.IndexOf (window);

            if (index > 0) {
                _activeWindow = window;
                return true;
            }

            return false;
        }
    }
}