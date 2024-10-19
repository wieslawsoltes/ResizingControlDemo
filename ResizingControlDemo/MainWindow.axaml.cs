using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ResizingControlDemo;

public partial class MainWindow : Window
{
    public KeyBindingService KeyBindingService { get; private set; }

    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        KeyBindingService = new KeyBindingService(this, EditorView.ResizingHostControl);
    }
}
