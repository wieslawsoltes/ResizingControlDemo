using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using ResizingControlDemo.Controls;

namespace ResizingControlDemo;

public class KeyBindingService
{
    private readonly ResizingHostControl _resizingHostControl;

    public KeyBindingService(TopLevel topLevel, ResizingHostControl resizingHostControl)
    {
        _resizingHostControl = resizingHostControl;

        topLevel.AddHandler(InputElement.KeyDownEvent, KeyDownHandler, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    private void KeyDownHandler(object? sender, KeyEventArgs e)
    {
        if (e.Key is Key.Delete or Key.Back)
        {
            Delete();
        }
    }

    private void Delete()
    {
        var selectedResizingAdornerControl = _resizingHostControl.GetValue(ResizingHostControl.SelectedResizingAdornerControlProperty);
        if (selectedResizingAdornerControl is null)
        {
            return;
        }

        if (selectedResizingAdornerControl.AdornedElement is not Control control)
        {
            return;
        }

        // Panel.Children
        if (control.Parent is Panel panel)
        {
            panel.Children.Remove(control);

            _resizingHostControl.SetValue(ResizingHostControl.SelectedResizingAdornerControlProperty, null);
        }

        // ContentControl.Content
        if (control.Parent is ContentControl contentControl)
        {
            contentControl.Content = null;

            _resizingHostControl.SetValue(ResizingHostControl.SelectedResizingAdornerControlProperty, null);
        }

        // Decorator.Child
        if (control.Parent is Decorator decorator)
        {
            decorator.Child = null;

            _resizingHostControl.SetValue(ResizingHostControl.SelectedResizingAdornerControlProperty, null);
        }
    }
}
