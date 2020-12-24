using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace concurrent_console_menu
{
    class Program
    {
        private static object _locker = new object();
        public static EventHandler<SelectionChangedEventArgs> OnSelectionChanged;

        static void Main(string[] args)
        {
            Console.Clear();

            var tokenSource = new CancellationTokenSource();
            var taskFactory = new TaskFactory();

            var selectionManager = new SelectionManager(3);
            OnSelectionChanged += selectionManager.OnSelectionChanged;

            taskFactory.StartNew(ConcurrentOutput, tokenSource.Token);
            taskFactory.StartNew(ConcurrentMenu, new MenuState
            {
                Token = tokenSource.Token,
                StartPosition = new CursorPosition { Left = 0, Top = 2 },
                SelectionManager = selectionManager
            });

            lock (_locker) { }

            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                        lock (_locker)
                        {
                            // (int, int) curentPosition = (Console.CursorLeft, Console.CursorTop);
                            // (ConsoleColor, ConsoleColor) currentColors = (Console.BackgroundColor, Console.ForegroundColor);

                            // Console.SetCursorPosition(0, 10);
                            // Console.Write($"DEBUG. Key pressed {key.Key}");

                            // Console.BackgroundColor = currentColors.Item1;
                            // Console.ForegroundColor = currentColors.Item2;
                            // Console.SetCursorPosition(curentPosition.Item1, curentPosition.Item2);

                            OnSelectionChanged?.Invoke(null, new SelectionChangedEventArgs
                            {
                                Key = key.Key
                            });
                        }
                        break;
                }

            }
        }

        private static async Task ConcurrentMenu(object menuState)
        {
            MenuState state = (MenuState)menuState;
            // (int, int) startPosition = (0, 2);
            var menu = new List<string> { "List item #1", "List item #2", "List item #3" };

            while (!state.Token.IsCancellationRequested)
            {
                lock (_locker)
                {
                    (int, int) curentPosition = (Console.CursorLeft, Console.CursorTop);
                    (ConsoleColor, ConsoleColor) currentColors = (Console.BackgroundColor, Console.ForegroundColor);

                    Console.SetCursorPosition(state.StartPosition.Left, state.StartPosition.Top);
                    Console.WriteLine("[Menu]");

                    for (var i = 0; i < menu.Count; i++)
                    {
                        DrawMenuItem(state.StartPosition.Left,
                        state.StartPosition.Top + i + 1,
                        menu[i],
                        i == state.SelectionManager.SelectedIndex);
                    }

                    Console.BackgroundColor = currentColors.Item1;
                    Console.ForegroundColor = currentColors.Item2;
                    Console.SetCursorPosition(curentPosition.Item1, curentPosition.Item2);
                }
            }
        }

        private static void DrawMenuItem(int left, int top, string value, bool isSelected)
        {
            Console.SetCursorPosition(left, top);
            Console.BackgroundColor = isSelected ? ConsoleColor.DarkGray : ConsoleColor.Gray;
            Console.ForegroundColor = isSelected ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen;
            Console.Write(value);
        }

        private static async Task ConcurrentOutput(object tokenObject)
        {
            var token = (CancellationToken)tokenObject;
            var counter = 0;

            while (!token.IsCancellationRequested)
            {
                lock (_locker)
                {
                    (int, int) curentPosition = (Console.CursorLeft, Console.CursorTop);
                    (ConsoleColor, ConsoleColor) currentColors = (Console.BackgroundColor, Console.ForegroundColor);

                    Console.SetCursorPosition(0, 0);

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Output: {counter++}");

                    Console.BackgroundColor = currentColors.Item1;
                    Console.ForegroundColor = currentColors.Item2;
                    Console.SetCursorPosition(curentPosition.Item1, curentPosition.Item2);
                }

                await Task.Delay(150);
            }

            Console.WriteLine($"THREAD {Thread.CurrentThread.ManagedThreadId} has been stopped");
        }
    }

    class SelectionManager
    {
        private int _count;
        private int _selectedIndex = 0;
        public int SelectedIndex => Volatile.Read(ref _selectedIndex);

        public SelectionManager(int count)
        {
            _count = count;
        }

        public void OnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {

            // (int, int) curentPosition = (Console.CursorLeft, Console.CursorTop);
            // (ConsoleColor, ConsoleColor) currentColors = (Console.BackgroundColor, Console.ForegroundColor);

            // Console.SetCursorPosition(0, 12);
            // Console.Write($"DEBUG. SelectionManager. Key pressed {args.Key}");

            // Console.BackgroundColor = currentColors.Item1;
            // Console.ForegroundColor = currentColors.Item2;
            // Console.SetCursorPosition(curentPosition.Item1, curentPosition.Item2);

            switch (args.Key)
            {
                case ConsoleKey.UpArrow:
                    if (SelectedIndex > 0)
                    {
                        Volatile.Write(ref _selectedIndex, _selectedIndex - 1);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (SelectedIndex < _count - 1)
                    {
                        Volatile.Write(ref _selectedIndex, _selectedIndex + 1);
                    }
                    break;
            }
        }
    }

    class SelectionChangedEventArgs : EventArgs
    {
        public ConsoleKey Key { get; set; }
    }

    class MenuState
    {
        public CancellationToken Token { get; set; }
        public CursorPosition StartPosition { get; set; }
        public SelectionManager SelectionManager { get; set; }
    }

    class CursorPosition
    {
        public int Left { get; set; }
        public int Top { get; set; }
    }
}
