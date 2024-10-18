using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace ResizingControlDemo.Controls;

public sealed class GroupBoxClipConverter : IMultiValueConverter
{
    public static readonly GroupBoxClipConverter Instance = new();

    public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is not [Rect bounds, Rect gap]
            || bounds == default
            || gap == default)
        {
            return new BindingNotification(
                new ArgumentException("Expecting two non-empty rectangles (type Avalonia.Rect)."), 
                BindingErrorType.Error);
        }

        gap = bounds.Intersect(gap);

        return new CombinedGeometry(
            GeometryCombineMode.Exclude,
            new RectangleGeometry { Rect = new Rect(bounds.Size) },
            new RectangleGeometry { Rect = new Rect(gap.Position - bounds.Position, gap.Size) });
    }
}
