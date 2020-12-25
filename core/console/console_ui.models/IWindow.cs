namespace console_ui.models {
    public interface IWindow {
        bool IsConcurrent { get; }
        Position StartPosition { get; }
        WindowSize Size { get; }
        IWindowPrinter WindowPrinter { get; }
    }
}