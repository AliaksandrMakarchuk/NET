namespace console_ui.models {
    public interface IWindowBuilder {
        IWindowBuilder AddStartPosition (Position startPosition);
        IWindowBuilder AddWindowSize (WindowSize windowSize);
        IWindowBuilder AddWindowPrinter (IWindowPrinter printer);
        IWindowBuilder SetWindowConcurrency (bool IsConcurrent);
        IWindow Build ();
    }
}