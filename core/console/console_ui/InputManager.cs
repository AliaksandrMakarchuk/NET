using System;
using System.Linq;

namespace console_ui
{
    public class InputManager
    {
        private char[] _availableInputCharachtersLowerCase = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private char[] _availableInputNumbers = "0123456789".ToCharArray();
        private char[] _availableInputSymbols = "".ToCharArray();
        public EventHandler<SelectionChangedEventArgs> OnMenuSelectionChanged;
        public EventHandler<EventArgs> OnEnterPressed;
        public EventHandler<InputCharachterEventArgs> OnCharachterInput;

        public void OnKeyPressed(ConsoleKeyInfo keyInfo)
        {

            var availableCharachters = _availableInputCharachtersLowerCase
                .Union(_availableInputCharachtersLowerCase.SelectMany(c => c.ToString().ToUpperInvariant().ToCharArray()))
                .Union(_availableInputNumbers)
                .Union(_availableInputSymbols);

            if (availableCharachters.Contains(keyInfo.KeyChar))
            {
                OnCharachterInput?.Invoke(null, new InputCharachterEventArgs { Charachter = keyInfo.KeyChar });
                return;
            }

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                    OnMenuSelectionChanged?.Invoke(null, new SelectionChangedEventArgs { Key = keyInfo.Key });
                    break;
                case ConsoleKey.Enter:
                    OnEnterPressed?.Invoke(null, null);
                    break;
            }
        }
    }

    public class SelectionChangedEventArgs : EventArgs
    {
        public ConsoleKey Key { get; set; }
    }

    public class InputCharachterEventArgs : EventArgs
    {
        public char Charachter { get; set; }
    }
}