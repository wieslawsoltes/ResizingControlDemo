using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using ResizingControlDemo.Controls;

namespace ResizingControlDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        AddHandler(KeyDownEvent, KeyDownHandler, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    private void KeyDownHandler(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete || e.Key == Key.Back)
        {
            var selectedResizingAdornerControl = ResizingHost.GetValue(ResizingHostControl.SelectedResizingAdornerControlProperty);
            if (selectedResizingAdornerControl is not null)
            {
                if (selectedResizingAdornerControl.AdornedElement is Control control)
                {
                    EditorCanvas.Children.Remove(control);
                }
            }
        }
    }
}
