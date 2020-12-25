using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace concurrent_console_menu {
    class Program {
        private static object _locker = new object ();
        public static EventHandler<SelectionChangedEventArgs> OnSelectionChanged;

        static void Main (string[] args) {
            Console.Clear ();

            var tokenSource = new CancellationTokenSource ();
            var taskFactory = new TaskFactory ();

            var menuItems = new List<MenuItem> {
                new MenuItem { Name = "Run Command #1", Command = new PrintMessageCommand ("Command 1") },
                new MenuItem { Name = "Run Command #2", Command = new PrintMessageCommand ("Command #2") },
                new MenuItem { Name = "Play Beep", Command = new PlayBeepCommand () },
                new MenuItem { Name = "Exit", Command = new ExitCommand (tokenSource) }
            };
            var selectionManager = new MenuItemsManager (menuItems);
            OnSelectionChanged += selectionManager.OnSelectionChanged;

            taskFactory.StartNew (ConcurrentOutput, tokenSource.Token);
            taskFactory.StartNew (ConcurrentWindowsSize, tokenSource.Token);
            // taskFactory.StartNew (ConcurrentInput, tokenSource.Token);
            taskFactory.StartNew (PrintMenu, new MenuState {
                Token = tokenSource.Token,
                    StartPosition = new CursorPosition { Left = 0, Top = 2 },
                    MenuManager = selectionManager
            });

            lock (_locker) { }

            while (!tokenSource.IsCancellationRequested) {
                var key = Console.ReadKey (true);

                lock (_locker) {
                    (int, int) curentPosition = (Console.CursorLeft, Console.CursorTop);
                    (ConsoleColor, ConsoleColor) currentColors = (Console.BackgroundColor, Console.ForegroundColor);
                    var width = Console.WindowWidth;

                    // --- clear line ---
                    Console.SetCursorPosition (0, 10);
                    Console.Write (string.Join ("", Enumerable.Range (0, width).Select (x => " ")));
                    // ------------------

                    Console.SetCursorPosition (0, 10);
                    Console.Write ($"DEBUG. Key pressed {key.Key}");

                    Console.BackgroundColor = currentColors.Item1;
                    Console.ForegroundColor = currentColors.Item2;
                    Console.SetCursorPosition (curentPosition.Item1, curentPosition.Item2);
                }

                switch (key.Key) {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.Enter:
                        lock (_locker) {
                            OnSelectionChanged?.Invoke (null, new SelectionChangedEventArgs {
                                Key = key.Key
                            });
                        }
                        break;
                }

            }
        }

        private static void ConcurrentInput (object tokenObject) {
            lock (_locker) {
                (int, int) curentPosition = (Console.CursorLeft, Console.CursorTop);
                (ConsoleColor, ConsoleColor) currentColors = (Console.BackgroundColor, Console.ForegroundColor);
                var width = Console.WindowWidth;

                // --- clear line ---
                Console.SetCursorPosition (0, 13);
                Console.Write (string.Join ("", Enumerable.Range (0, width).Select (x => " ")));
                // ------------------

                Console.SetCursorPosition (0, 13);
                Console.Write ($"Concurrent Input: > ");
                var input = Console.ReadLine ();
                Console.SetCursorPosition (0, 14);
                Console.Write ($"Input: {input}");

                Console.BackgroundColor = currentColors.Item1;
                Console.ForegroundColor = currentColors.Item2;
                Console.SetCursorPosition (curentPosition.Item1, curentPosition.Item2);
            }
        }

        private static void ConcurrentWindowsSize (object tokenObject) {
            var token = (CancellationToken) tokenObject;

            while (!token.IsCancellationRequested) {
                lock (_locker) {
                    (int, int) curentPosition = (Console.CursorLeft, Console.CursorTop);
                    (ConsoleColor, ConsoleColor) currentColors = (Console.BackgroundColor, Console.ForegroundColor);

                    Console.SetCursorPosition (0, 1);

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine ($"Windows size: [{Console.WindowWidth}x{Console.WindowHeight}]");

                    Console.BackgroundColor = currentColors.Item1;
                    Console.ForegroundColor = currentColors.Item2;
                    Console.SetCursorPosition (curentPosition.Item1, curentPosition.Item2);
                }
            }

            Console.WriteLine ($"THREAD {Thread.CurrentThread.ManagedThreadId} has been stopped");
        }

        private static void PrintMenu (object menuState) {
            MenuState state = (MenuState) menuState;

            while (!state.Token.IsCancellationRequested) {
                lock (_locker) {
                    (int, int) curentPosition = (Console.CursorLeft, Console.CursorTop);
                    (ConsoleColor, ConsoleColor) currentColors = (Console.BackgroundColor, Console.ForegroundColor);

                    Console.SetCursorPosition (state.StartPosition.Left, state.StartPosition.Top);
                    Console.WriteLine ("[Menu]");

                    for (var i = 0; i < state.MenuManager.Items.Count; i++) {
                        DrawMenuItem (state.StartPosition.Left,
                            state.StartPosition.Top + i + 1,
                            state.MenuManager.Items[i],
                            i == state.MenuManager.SelectedIndex);
                    }

                    Console.BackgroundColor = currentColors.Item1;
                    Console.ForegroundColor = currentColors.Item2;
                    Console.SetCursorPosition (curentPosition.Item1, curentPosition.Item2);
                }
            }
        }

        private static void DrawMenuItem (int left, int top, string value, bool isSelected) {
            Console.SetCursorPosition (left, top);
            Console.BackgroundColor = isSelected ? ConsoleColor.DarkGray : ConsoleColor.Gray;
            Console.ForegroundColor = isSelected ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen;
            Console.Write (value);
        }

        private static async Task ConcurrentOutput (object tokenObject) {
            var token = (CancellationToken) tokenObject;
            var counter = 0;

            while (!token.IsCancellationRequested) {
                lock (_locker) {
                    (int, int) curentPosition = (Console.CursorLeft, Console.CursorTop);
                    (ConsoleColor, ConsoleColor) currentColors = (Console.BackgroundColor, Console.ForegroundColor);

                    Console.SetCursorPosition (0, 0);

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine ($"Output: {counter++}");

                    Console.BackgroundColor = currentColors.Item1;
                    Console.ForegroundColor = currentColors.Item2;
                    Console.SetCursorPosition (curentPosition.Item1, curentPosition.Item2);
                }

                await Task.Delay (150);
            }

            Console.WriteLine ($"THREAD {Thread.CurrentThread.ManagedThreadId} has been stopped");
        }
    }

    class MenuItemsManager {
        private IList<MenuItem> _items;
        private int _selectedIndex = 0;

        public IList<string> Items => _items.Select (x => x.Name).ToList ();
        public int SelectedIndex => Volatile.Read (ref _selectedIndex);

        public MenuItemsManager (IList<MenuItem> items) {
            _items = items;
        }

        public void OnSelectionChanged (object sender, SelectionChangedEventArgs args) {

            // (int, int) curentPosition = (Console.CursorLeft, Console.CursorTop);
            // (ConsoleColor, ConsoleColor) currentColors = (Console.BackgroundColor, Console.ForegroundColor);

            // Console.SetCursorPosition(0, 12);
            // Console.Write($"DEBUG. SelectionManager. Key pressed {args.Key}");

            // Console.BackgroundColor = currentColors.Item1;
            // Console.ForegroundColor = currentColors.Item2;
            // Console.SetCursorPosition(curentPosition.Item1, curentPosition.Item2);

            switch (args.Key) {
                case ConsoleKey.UpArrow:
                    if (SelectedIndex > 0) {
                        Volatile.Write (ref _selectedIndex, _selectedIndex - 1);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (SelectedIndex < _items.Count - 1) {
                        Volatile.Write (ref _selectedIndex, _selectedIndex + 1);
                    }
                    break;
                case ConsoleKey.Enter:
                    _items[SelectedIndex].Command.Execute ();
                    break;
            }
        }
    }

    class SelectionChangedEventArgs : EventArgs {
        public ConsoleKey Key { get; set; }
    }

    class MenuState {
        public CancellationToken Token { get; set; }
        public CursorPosition StartPosition { get; set; }
        public MenuItemsManager MenuManager { get; set; }
    }

    class CursorPosition {
        public int Left { get; set; }
        public int Top { get; set; }
    }

    class MenuItem {
        public string Name { get; set; }
        public ICommand Command { get; set; }
    }

    interface ICommand {
        void Execute ();
    }

    class ExitCommand : ICommand {
        private readonly CancellationTokenSource _tokenSource;

        public ExitCommand (CancellationTokenSource tokenSource) {
            _tokenSource = tokenSource;
        }

        public void Execute () {
            _tokenSource.Cancel ();
        }
    }

    class PrintMessageCommand : ICommand {
        private readonly string _message;

        public PrintMessageCommand (string message) {
            _message = message;
        }

        public void Execute () {
            (int, int) curentPosition = (Console.CursorLeft, Console.CursorTop);
            (ConsoleColor, ConsoleColor) currentColors = (Console.BackgroundColor, Console.ForegroundColor);

            var width = Console.WindowWidth;

            // --- clear line ---
            Console.SetCursorPosition (0, 12);
            Console.Write (string.Join ("", Enumerable.Range (0, width).Select (x => " ")));
            // ------------------

            Console.SetCursorPosition (0, 12);
            Console.Write ($"PrintMessageCommand: {_message}");

            Console.BackgroundColor = currentColors.Item1;
            Console.ForegroundColor = currentColors.Item2;
            Console.SetCursorPosition (curentPosition.Item1, curentPosition.Item2);
        }
    }

    class PlayBeepCommand : ICommand {
        public void Execute () {
            Console.Beep ();
        }
    }
}