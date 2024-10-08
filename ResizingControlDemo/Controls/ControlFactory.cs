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
            case "Rectangle":
            {
                return new Rectangle
                {
                    Fill = Brushes.Gray,
                    Width = 50,
                    Height = 50,
                };
            }
            case "Ellipse":
            {
                return new Ellipse
                {
                    Fill = Brushes.Gray,
                    Width = 50,
                    Height = 50,
                };
            }
        }

        return null;
    }
}
