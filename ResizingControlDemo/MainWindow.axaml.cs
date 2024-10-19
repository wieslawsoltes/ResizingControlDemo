using Avalonia.Controls;

namespace ResizingControlDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        KeyBindingService = new KeyBindingService(this, ResizingHost);
    }

    public KeyBindingService KeyBindingService { get; }
}
