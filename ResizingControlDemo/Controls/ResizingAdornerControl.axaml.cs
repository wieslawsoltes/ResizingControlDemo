using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace ResizingControlDemo.Controls;

public class ResizingAdornerControl : TemplatedControl
{
    private double _left;
    private double _top;
    private double _width;
    private double _height;
    private Canvas? AdornerCanvas;
    private Thumb? MoveThumb;
    private Thumb? TopLeftThumb;
    private Thumb? TopRightThumb;
    private Thumb? BottomLeftThumb;
    private Thumb? BottomRightThumb;

    public static readonly StyledProperty<Visual?> AdornedElementProperty =
        AvaloniaProperty.Register<ResizingAdornerControl, Visual?>(nameof(AdornedElement));

    public Visual? AdornedElement
    {
        get => GetValue(AdornedElementProperty);
        set => SetValue(AdornedElementProperty, value);
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        AdornerCanvas = e.NameScope.Find<Canvas>("AdornerCanvas");
        MoveThumb = e.NameScope.Find<Thumb>("MoveThumb");
        TopLeftThumb = e.NameScope.Find<Thumb>("TopLeftThumb");
        TopRightThumb = e.NameScope.Find<Thumb>("TopRightThumb");
        BottomLeftThumb = e.NameScope.Find<Thumb>("BottomLeftThumb");
        BottomRightThumb = e.NameScope.Find<Thumb>("BottomRightThumb");

        MoveThumb.DragStarted += MoveThumb_OnDragStarted;
        MoveThumb.DragDelta += MoveThumb_OnDragDelta;
        MoveThumb.DragCompleted += MoveThumb_OnDragCompleted;

        TopLeftThumb.DragStarted += TopLeftThumb_OnDragStarted;
        TopLeftThumb.DragDelta += TopLeftThumb_OnDragDelta;
        TopLeftThumb.DragCompleted += TopLeftThumb_OnDragCompleted;

        TopRightThumb.DragStarted += TopRightThumb_OnDragStarted;
        TopRightThumb.DragDelta += TopRightThumb_OnDragDelta;
        TopRightThumb.DragCompleted += TopRightThumb_OnDragCompleted;

        BottomLeftThumb.DragStarted += BottomLeftThumb_OnDragStarted;
        BottomLeftThumb.DragDelta += BottomLeftThumb_OnDragDelta;
        BottomLeftThumb.DragCompleted += BottomLeftThumb_OnDragCompleted;

        BottomRightThumb.DragStarted += BottomRightThumb_OnDragStarted;
        BottomRightThumb.DragDelta += BottomRightThumb_OnDragDelta;
        BottomRightThumb.DragCompleted += BottomRightThumb_OnDragCompleted;
    }

    private enum DragDirection
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        Move,
    }

    private void DragStarted(object? sender, DragDirection direction)
    {
        var thumb = sender as Thumb;
        var adornedElement = AdornedElement as Control;

        _left = Canvas.GetLeft(adornedElement);
        _top = Canvas.GetTop(adornedElement);
        _width = adornedElement.Width;
        _height = adornedElement.Height;

        // Console.WriteLine($"DragStarted={direction} {adornedElement.GetType().Name} left={_left} top={_top} width={_width} height={_height}");
    }

    private void DragDelta(object? sender, VectorEventArgs e, DragDirection direction)
    {
        var thumb = sender as Thumb;
        var adornedElement = AdornedElement as Control;

        var left = direction switch
        {
            DragDirection.TopLeft => _left + e.Vector.X,
            DragDirection.TopRight => _left,
            DragDirection.BottomLeft => _left + e.Vector.X,
            DragDirection.BottomRight => _left,
            DragDirection.Move => _left + e.Vector.X,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        var top = direction switch
        {
            DragDirection.TopLeft => _top + e.Vector.Y,
            DragDirection.TopRight => _top + e.Vector.Y,
            DragDirection.BottomLeft => _top,
            DragDirection.BottomRight => _top,
            DragDirection.Move => _top + e.Vector.Y,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        var width = direction switch
        {
            DragDirection.TopLeft => _width - e.Vector.X,
            DragDirection.TopRight => _width + e.Vector.X,
            DragDirection.BottomLeft => _width - e.Vector.X,
            DragDirection.BottomRight => _width + e.Vector.X,
            DragDirection.Move => _width,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        var height = direction switch
        {
            DragDirection.TopLeft => _height - e.Vector.Y,
            DragDirection.TopRight => _height - e.Vector.Y,
            DragDirection.BottomLeft => _height + e.Vector.Y,
            DragDirection.BottomRight => _height + e.Vector.Y,
            DragDirection.Move => _height,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        if (width < 0.0)
        {
            switch (direction)
            {
                case DragDirection.TopLeft:
                case DragDirection.TopRight:
                case DragDirection.BottomLeft:
                case DragDirection.BottomRight:
                    width = Math.Abs(width);
                    left -= width;
                    break;
                case DragDirection.Move:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        if (height < 0.0)
        {
            switch (direction)
            {
                case DragDirection.TopLeft:
                case DragDirection.TopRight:
                case DragDirection.BottomLeft:
                case DragDirection.BottomRight:
                    height = Math.Abs(height);
                    top -= height;
                    break;
                case DragDirection.Move:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        switch (direction)
        {
            case DragDirection.TopLeft:
                break;
            case DragDirection.TopRight:
                _width = width;
                break;
            case DragDirection.BottomLeft:
                _height = height;
                break;
            case DragDirection.BottomRight:
                _width = width;
                _height = height;
                break;
            case DragDirection.Move:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
        
        Canvas.SetLeft(adornedElement, left);
        Canvas.SetTop(adornedElement, top);
        adornedElement.Width = width;
        adornedElement.Height = height;

        // Console.WriteLine($"DragDelta={direction} {adornedElement.GetType().Name} Vector={e.Vector} width={width} height={height}");
    }

    private void DragCompleted(object? sender, DragDirection direction)
    {
        var thumb = sender as Thumb;
        var adornedElement = AdornedElement as Control;

        // Console.WriteLine($"DragCompleted={direction} {adornedElement.GetType().Name}");
    }

    // TopLeft
    
    private void TopLeftThumb_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, DragDirection.TopLeft);
    }

    private void TopLeftThumb_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDelta(sender, e, DragDirection.TopLeft);
    }

    private void TopLeftThumb_OnDragCompleted(object? sender, VectorEventArgs e)
    {
        DragCompleted(sender, DragDirection.TopLeft);
    }

    // TopRight
    
    private void TopRightThumb_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, DragDirection.TopRight);
    }

    private void TopRightThumb_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDelta(sender, e, DragDirection.TopRight);
    }

    private void TopRightThumb_OnDragCompleted(object? sender, VectorEventArgs e)
    {
        DragCompleted(sender, DragDirection.TopRight);
    }

    // BottomLeft
    
    private void BottomLeftThumb_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, DragDirection.BottomLeft);
    }

    private void BottomLeftThumb_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDelta(sender, e, DragDirection.BottomLeft);
    }

    private void BottomLeftThumb_OnDragCompleted(object? sender, VectorEventArgs e)
    {
        DragCompleted(sender, DragDirection.BottomLeft);
    }

    // BottomRight

    private void BottomRightThumb_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, DragDirection.BottomRight);
    }

    private void BottomRightThumb_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDelta(sender, e, DragDirection.BottomRight);
    }

    private void BottomRightThumb_OnDragCompleted(object? sender, VectorEventArgs e)
    {
        DragCompleted(sender, DragDirection.BottomRight);
    }

    // MoveThumb

    private void MoveThumb_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, DragDirection.Move);
    }

    private void MoveThumb_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDelta(sender, e, DragDirection.Move);
    }

    private void MoveThumb_OnDragCompleted(object? sender, VectorEventArgs e)
    {
        DragCompleted(sender, DragDirection.Move);
    }
}
