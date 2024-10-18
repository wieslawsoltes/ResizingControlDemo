using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Reactive;
using ResizingControlDemo.Controls;

namespace ResizingControlDemo;

public partial class MainWindow : Window
{
    private Control? _selectedControl;

    public MainWindow()
    {
        InitializeComponent();

        AddHandler(KeyDownEvent, KeyDownHandler, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        InitializeProperties();

    }

    private void InitializeProperties()
    {
        ForegroundColorPicker.IsVisible = false;
        ForegroundColorPicker.ColorChanged += ForegroundColorPickerOnColorChanged;

        BackgroundColorPicker.IsVisible = false;
        BackgroundColorPicker.ColorChanged += BackgroundColorPickerOnColorChanged;

        ResizingHost
            .GetObservable(ResizingHostControl.SelectedResizingAdornerControlProperty)
            .Subscribe(new AnonymousObserver<ResizingAdornerControl?>(UpdateProperties));
    }

    private void UpdateProperties(ResizingAdornerControl? resizingAdornerControl)
    {
        if (resizingAdornerControl is not null)
        {
            _selectedControl = resizingAdornerControl.AdornedElement as Control;

            // Foreground
            switch (_selectedControl)
            {
                case TextBlock textBlock:
                {
                    var color = (textBlock.Foreground as SolidColorBrush)?.Color;
                    if (color is not null)
                    {
                        ForegroundColorPicker.Color = color.Value;
                        ForegroundColorPicker.IsVisible = true;
                    }

                    break;
                }
                case Label label:
                {
                    var color = (label.Foreground as SolidColorBrush)?.Color;
                    if (color is not null)
                    {
                        ForegroundColorPicker.Color = color.Value;
                        ForegroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case TextBox textBox:
                {
                    var color = (textBox.Foreground as SolidColorBrush)?.Color;
                    if (color is not null)
                    {
                        ForegroundColorPicker.Color = color.Value;
                        ForegroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case Button button:
                {
                    var color = (button.Foreground as SolidColorBrush)?.Color;
                    if (color is not null)
                    {
                        ForegroundColorPicker.Color = color.Value;
                        ForegroundColorPicker.IsVisible = true;
                    }
                    break;
                }
            }
                    
            // Background
            switch (_selectedControl)
            {
                case TextBlock textBlock:
                {
                    var color = (textBlock.Background as SolidColorBrush)?.Color;
                    if (color is not null)
                    {
                        BackgroundColorPicker.Color = color.Value;
                        BackgroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case Label label:
                {
                    var color = (label.Background as SolidColorBrush)?.Color;
                    if (color is not null)
                    {
                        BackgroundColorPicker.Color = color.Value;
                        BackgroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case TextBox textBox:
                {
                    var color = (textBox.Background as SolidColorBrush)?.Color;
                    if (color is not null)
                    {
                        BackgroundColorPicker.Color = color.Value;
                        BackgroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case Button button:
                {
                    var color = (button.Background as SolidColorBrush)?.Color;
                    if (color is not null)
                    {
                        BackgroundColorPicker.Color = color.Value;
                        BackgroundColorPicker.IsVisible = true;
                    }
                    break;
                }
            }
                    
        }
        else
        {
            ForegroundColorPicker.IsVisible = false;
            BackgroundColorPicker.IsVisible = false;

            _selectedControl = null; 
        }
    }

    private void ForegroundColorPickerOnColorChanged(object? sender, ColorChangedEventArgs e)
    {
        if (_selectedControl is null)
        {
            return;
        }

        var color = e.NewColor;

        switch (_selectedControl)
        {
            case TextBlock textBlock:
            {
                textBlock.Foreground = new SolidColorBrush(color);
                break;
            }
            case Label label:
            {
                label.Foreground = new SolidColorBrush(color);
                break;
            }
            case TextBox textBox:
            {
                textBox.Foreground = new SolidColorBrush(color);
                break;
            }
            case Button button:
            {
                button.Foreground = new SolidColorBrush(color);
                break;
            }
        }
    }

    private void BackgroundColorPickerOnColorChanged(object? sender, ColorChangedEventArgs e)
    {
        if (_selectedControl is null)
        {
            return;
        }

        var color = e.NewColor;

        switch (_selectedControl)
        {
            case TextBlock textBlock:
            {
                textBlock.Background = new SolidColorBrush(color);
                break;
            }
            case Label label:
            {
                label.Background = new SolidColorBrush(color);
                break;
            }
            case TextBox textBox:
            {
                textBox.Background = new SolidColorBrush(color);
                break;
            }
            case Button button:
            {
                button.Background = new SolidColorBrush(color);
                break;
            }
        }
    }

    private void KeyDownHandler(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete || e.Key == Key.Back)
        {
            Delete();
        }
    }

    private void Delete()
    {
        var selectedResizingAdornerControl = ResizingHost.GetValue(ResizingHostControl.SelectedResizingAdornerControlProperty);
        if (selectedResizingAdornerControl is null)
        {
            return;
        }

        if (selectedResizingAdornerControl.AdornedElement is Control control)
        {
            if (control.Parent is Panel panel)
            {
                panel.Children.Remove(control);

                ResizingHost.SetValue(ResizingHostControl.SelectedResizingAdornerControlProperty, null);
            }

            if (control.Parent is ContentControl contentControl)
            {
                contentControl.Content = null;

                ResizingHost.SetValue(ResizingHostControl.SelectedResizingAdornerControlProperty, null);
            }
        }
    }
}
