using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace ResizingControlDemo.Controls;

public class ToolBoxItem : ListBoxItem
{
    private string? _typeName;
    private bool _isPressed;
    private Control? _control;
    private Point _start;

    protected override Type StyleKeyOverride => typeof(ListBoxItem);

    public ToolBoxItem()
    {
        AddHandler(PointerPressedEvent, ListBoxItemOnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        AddHandler(PointerReleasedEvent, ListBoxItemOnPointerReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        AddHandler(PointerMovedEvent, ListBoxItemOnPointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    private void ListBoxItemOnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Control { DataContext: string typeName })
        {
            _start = e.GetPosition(this);
            
            _typeName = typeName;
            _isPressed = true;
        }
    }
    
    private void ListBoxItemOnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (!_isPressed || _typeName is null)
        {
            return;
        }

        if (Parent is ToolBox { EditorCanvas: { } editorCanvas })
        {
            if (_control is not null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(editorCanvas);
                adornerLayer.Children.Remove(_control);

                var control = ControlFactory.CreateControl(_typeName);
                if (control is not null)
                {
                    var point = e.GetPosition(editorCanvas);

                    editorCanvas.Children.Add(control);

                    Canvas.SetLeft(control, point.X);
                    Canvas.SetTop(control, point.Y);
                }
            }
        }

        _control = null;
        _typeName = null;
        _isPressed = true;
    }

    private void ListBoxItemOnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_isPressed || _typeName is null)
        {
            return;
        }

        if (_control is null)
        {
            var move = e.GetPosition(this);
            var delta = _start - move;
            if (Math.Abs(delta.X) < 3 && Math.Abs(delta.Y) < 3)
            {
                return;
            }
        }

        if (Parent is ToolBox { EditorCanvas: { } editorCanvas })
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(editorCanvas);
            var point = e.GetPosition(adornerLayer);

            if (_control is null)
            {
                _control = ControlFactory.CreateControl(_typeName);
                adornerLayer.Children.Add(_control);
            }
            
            Canvas.SetLeft(_control, point.X);
            Canvas.SetTop(_control, point.Y);
        }
    }
}
