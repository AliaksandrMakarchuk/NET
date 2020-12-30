using System;

namespace console_ui
{
    public class ConsoleUIManager
    {
        private InputManager _inputManager;
        private WindowsManager _windowsManager;

        public void Initialize(InputManager inputManager, WindowsManager windowsManager)
        {
            _inputManager = inputManager;
            _windowsManager = windowsManager;
        }

        public void Start()
        {
            if (_windowsManager.ActiveWindow == null)
            {
                return;
            }
            _windowsManager.ActiveWindow.Print();
        }
    }
}