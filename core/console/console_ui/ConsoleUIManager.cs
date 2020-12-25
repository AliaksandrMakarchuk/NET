using System.Linq;
using console_ui.models;

namespace console_ui {
    public static class ConsoleUIManager {
        private static InputManager _inputManager;
        private static WindowsManager _windowsManager;

        public static void Initialize (InputManager inputManager, WindowsManager windowsManager) {
            _inputManager = inputManager;
            _windowsManager = windowsManager;
        }

        public static void Start () {
            _windowsManager.Windows[0].WindowPrinter.Draw (_windowsManager.Windows[0]);
            if (_windowsManager.ActiveWindow != _windowsManager.Windows[0]) {
                _windowsManager.ActiveWindow.WindowPrinter.Draw (_windowsManager.ActiveWindow);
            }
        }
    }
}