using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using ResizingControlDemo.Controls;

namespace ResizingControlDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        AddHandler(KeyDownEvent, KeyDownHandler, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        ShowTitleBar();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        ShowTitleBar();
    }

    private void ShowTitleBar()
    {
        var titleBar = this.GetVisualDescendants().OfType<TitleBar>().FirstOrDefault();
        if (titleBar is not null)
        {
            titleBar.SetCurrentValue(TitleBar.IsVisibleProperty, true);
            // titleBar.IsVisible = true;
        }
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
