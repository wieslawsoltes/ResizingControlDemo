using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Immutable;
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

        var toolboxControls = new List<string>
        {
            // "Pointer",
            "Border",
            "Button",
            "Calendar",
            "Canvas",
            "CheckBox",
            "ComboBox",
            "ContentControl",
            "DataGrid",
            "DatePicker",
            "DockPanel",
            // "DocumentViewer",
            "Ellipse",
            "Expander",
            // "Frame",
            "Grid",
            "GridSplitter",
            "GroupBox",
            "Image",
            "Label",
            "ListBox",
            // "ListView",
            // "MediaElement",
            "Menu",
            // "PasswordBox",
            "ProgressBar",
            "RadioButton",
            "Rectangle",
            // "RichTextBox",
            "ScrollBar",
            "ScrollViewer",
            "Separator",
            "Slider",
            "StackPanel",
            // "StatusBar",
            "TabControl",
            "TextBlock",
            "TextBox",
            // "ToolBar",
            // "ToolBarPanel",
            // "ToolBarTray",
            "TreeView",
            "Viewbox",
            // "WebBrowser",
            // "WindowsFormsHost",
            "WrapPanel"
        };

        ControlsToolBox.ItemsSource = toolboxControls;
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

    private static Color? GetColor(IBrush? brush)
    {
        switch (brush)
        {
            case null:
                break;
            case ISolidColorBrush solidColorBrush:
                return solidColorBrush.Color;
        }

        return null;
    }
    
    private void UpdateProperties(ResizingAdornerControl? resizingAdornerControl)
    {
        if (resizingAdornerControl is not null)
        {
            _selectedControl = resizingAdornerControl.AdornedElement as Control;

            // Foreground
            switch (_selectedControl)
            {
                case ContentControl contentControl:
                {
                    var color = GetColor(contentControl.Foreground);
                    if (color is not null)
                    {
                        ForegroundColorPicker.Color = color.Value;
                        ForegroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case TemplatedControl templatedControl:
                {
                    var color = GetColor(templatedControl.Foreground);
                    if (color is not null)
                    {
                        ForegroundColorPicker.Color = color.Value;
                        ForegroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case TextBlock textBlock:
                {
                    var color = GetColor(textBlock.Foreground);
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
                case Panel panel:
                {
                    var color = GetColor(panel.Background);
                    if (color is not null)
                    {
                        BackgroundColorPicker.Color = color.Value;
                        BackgroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case ContentControl contentControl:
                {
                    var color = GetColor(contentControl.Background);
                    if (color is not null)
                    {
                        BackgroundColorPicker.Color = color.Value;
                        BackgroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case TemplatedControl templatedControl:
                {
                    var color = GetColor(templatedControl.Background);
                    if (color is not null)
                    {
                        BackgroundColorPicker.Color = color.Value;
                        BackgroundColorPicker.IsVisible = true;
                    }
                    break;
                }
                case TextBlock textBlock:
                {
                    var color = GetColor(textBlock.Background);
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
            case ContentControl contentControl:
            {
                contentControl.Foreground = new SolidColorBrush(color);
                break;
            }
            case TemplatedControl templatedControl:
            {
                templatedControl.Foreground = new SolidColorBrush(color);
                break;
            }
            case TextBlock textBlock:
            {
                textBlock.Foreground = new SolidColorBrush(color);
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
            case Panel panel:
            {
                panel.Background = new SolidColorBrush(color);
                break;
            }
            case ContentControl contentControl:
            {
                contentControl.Background = new SolidColorBrush(color);
                break;
            }
            case TemplatedControl templatedControl:
            {
                templatedControl.Background = new SolidColorBrush(color);
                break;
            }
            case TextBlock textBlock:
            {
                textBlock.Background = new SolidColorBrush(color);
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
            // Panel.Children
            if (control.Parent is Panel panel)
            {
                panel.Children.Remove(control);

                ResizingHost.SetValue(ResizingHostControl.SelectedResizingAdornerControlProperty, null);
            }

            // ContentControl.Content
            if (control.Parent is ContentControl contentControl)
            {
                contentControl.Content = null;

                ResizingHost.SetValue(ResizingHostControl.SelectedResizingAdornerControlProperty, null);
            }

            // Decorator.Child
            if (control.Parent is Decorator decorator)
            {
                decorator.Child = null;

                ResizingHost.SetValue(ResizingHostControl.SelectedResizingAdornerControlProperty, null);
            }
        }
    }
}
