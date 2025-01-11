using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace GroupBox.Avalonia;

public sealed class GroupBoxClipConverter : IMultiValueConverter
{
    public static readonly GroupBoxClipConverter Instance = new();

    public object Convert(IList<object?>? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is null
            || values.Count != 2
            || values[0] is not Rect bounds
            || values[1] is not Rect gap)
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
    
    public IMultiValueConverter ProvideValue(IServiceProvider serviceProvider) => Instance;
}
