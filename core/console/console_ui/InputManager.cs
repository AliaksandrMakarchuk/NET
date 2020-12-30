using System;
using System.Linq;
using console_ui.models;

namespace console_ui
{
    public class InputManager
    {
        private readonly WindowsManager _windowsManager;

        // private char[] _availableInputCharachtersLowerCase = "abcdefghijklmnopqrstuvwxyz".ToCharArray ();
        // private char[] _availableInputNumbers = "0123456789".ToCharArray ();
        // private char[] _availableInputSymbols = "".ToCharArray ();
        // public EventHandler<SelectionChangedEventArgs> OnMenuSelectionChanged;
        // public EventHandler<EventArgs> OnEnterPressed;
        // public EventHandler<InputCharachterEventArgs> OnCharachterInput;

        public InputManager(WindowsManager windowsManager)
        {
            _windowsManager = windowsManager ??
                throw new ArgumentNullException(nameof(windowsManager));
        }

        public void OnKeyPressed(ConsoleKeyInfo keyInfo)
        {
            var activeArea = _windowsManager.ActiveWindow?.ActiveArea;
            if (activeArea == null)
            {
                return;
            }

            if (activeArea.InputType != ResolveInputType(keyInfo) &&
                keyInfo.Key != ConsoleKey.Enter)
            {
                return;
            }

            activeArea.Handle(keyInfo.Key);
            // var availableCharachters = _availableInputCharachtersLowerCase
            //     .Union (_availableInputCharachtersLowerCase.SelectMany (c => c.ToString ().ToUpperInvariant ().ToCharArray ()))
            //     .Union (_availableInputNumbers)
            //     .Union (_availableInputSymbols);

            // if (availableCharachters.Contains (keyInfo.KeyChar)) {
            //     OnCharachterInput?.Invoke (null, new InputCharachterEventArgs { Charachter = keyInfo.KeyChar });
            //     return;
            // }

            // switch (keyInfo.Key) {
            //     case ConsoleKey.UpArrow:
            //     case ConsoleKey.DownArrow:
            //         OnMenuSelectionChanged?.Invoke (null, new SelectionChangedEventArgs { Key = keyInfo.Key });
            //         break;
            //     case ConsoleKey.Enter:
            //         OnEnterPressed?.Invoke (null, null);
            //         break;
            // }
        }

        private InputType ResolveInputType(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.UpArrow ||
                keyInfo.Key == ConsoleKey.DownArrow ||
                keyInfo.Key == ConsoleKey.LeftArrow ||
                keyInfo.Key == ConsoleKey.RightArrow ?
                InputType.ARROWS :
                InputType.TEXT;
        }
    }

    // public class SelectionChangedEventArgs : EventArgs {
    //     public ConsoleKey Key { get; set; }
    // }

    // public class InputCharachterEventArgs : EventArgs {
    //     public char Charachter { get; set; }
    // }
}