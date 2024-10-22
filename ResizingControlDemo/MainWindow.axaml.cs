using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.VisualTree;
using Dock.Model.Avalonia.Controls;

namespace ResizingControlDemo;

public partial class MainWindow : Window
{
    public static readonly StyledProperty<EditorView?> EditorViewProperty = 
        AvaloniaProperty.Register<PropertiesView, EditorView?>(nameof(EditorView));

    [ResolveByName]
    public EditorView? EditorView
    {
        get => GetValue(EditorViewProperty);
        set => SetValue(EditorViewProperty, value);
    }

    public KeyBindingService? KeyBindingService { get; private set; }

    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        if (Dock?.Factory is null)
        {
            return;
        }

        Dock.Factory.SetActiveDockable(UserControl1);
        Dock.Factory.SetFocusedDockable(DocumentsPane, UserControl1);

        // TODO:
        EditorView = GetEditorView("UserControl1");
        
        if (EditorView is not null)
        {
            KeyBindingService = new KeyBindingService(this);
            KeyBindingService.ResizingHostControl = EditorView.ResizingHostControl;
        }

        Dock.Factory.ActiveDockableChanged += (_, args) =>
        {
            if (args.Dockable is Document document)
            {
                // NOTE: Call UpdateLayout() so template is applied to the document.
                UpdateLayout();

                EditorView = GetEditorView(document.Id);
            }
        };
    }

    private EditorView? GetEditorView(string documentId)
    {
        return Dock
            .GetVisualDescendants()
            .OfType<EditorView>()
            .FirstOrDefault(x => (x.DataContext as Document)?.Id == documentId);
    }
}
