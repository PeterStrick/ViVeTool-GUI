using ViVeTool_GUI.FeatureScanner_WinUI3.Helpers;

namespace ViVeTool_GUI.FeatureScanner_WinUI3;

public sealed partial class MainWindow : WindowEx
{
    public MainWindow()
    {
        InitializeComponent();

        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/WindowIcon.ico"));
        Content = null;
        Title = "AppDisplayName".GetLocalized();
    }
}
