using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;

namespace ResizingControlDemo.Controls;

public class ControlFactory
{
    public static Control? CreateControl(string typeName)
    {
        switch (typeName)
        {
            case "TextBlock":
            {
                return new TextBlock
                {
                    Text = "TextBlock",
                    Width = 120,
                    Height = 24,
                };
            }
            case "Label":
            {
                return new Label
                {
                    Content = "Label",
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Width = 120,
                    Height = 32,
                };
            }
            case "TextBox":
            {
                return new TextBox
                {
                    Text = "TextBox",
                    Width = 120,
                    Height = 32,
                };
            }
            case "Button":
            {
                return new Button
                {
                    Content = "Button",
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Width = 120,
                    Height = 32,
                };
            }
            case "RadioButton":
            {
                return new RadioButton
                {
                    Content = "RadioButton",
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 120,
                    Height = 32,
                };
            }
            case "CheckBox":
            {
                return new CheckBox
                {
                    Content = "CheckBox",
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 120,
                    Height = 32,
                };
            }
            case "GroupBox":
            {
                return new GroupBox
                {
                    Header = "GroupBox",
                    Width = 140,
                    Height = 92,
                    Content = new StackPanel
                    {
                        Children =
                        {
                            new RadioButton
                            {
                                Content = "RadioButton1",
                            },
                            new RadioButton
                            {
                                Content = "RadioButton2",
                            },
                        }
                    }
                };
            }
            case "Rectangle":
            {
                return new Rectangle
                {
                    Fill = Brushes.Gray,
                    Width = 48,
                    Height = 48,
                };
            }
            case "Ellipse":
            {
                return new Ellipse
                {
                    Fill = Brushes.Gray,
                    Width = 48,
                    Height = 48,
                };
            }
            case "StackPanel":
            {
                return new StackPanel
                {
                    Background = Brushes.LightGray,
                    Width = 120,
                    Height = 160,
                };
            }
            case "WrapPanel":
            {
                return new WrapPanel
                {
                    Background = Brushes.LightGray,
                    Width = 120,
                    Height = 160,
                };
            }
            case "DockPanel":
            {
                return new DockPanel
                {
                    Background = Brushes.LightGray,
                    Width = 120,
                    Height = 160,
                };
            }
            case "Canvas":
            {
                return new Canvas
                {
                    Background = Brushes.LightGray,
                    Width = 240,
                    Height = 240,
                };
            }
        }

        return null;
    }
}
