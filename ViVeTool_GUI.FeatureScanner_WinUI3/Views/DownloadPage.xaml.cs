using Microsoft.UI.Xaml.Controls;

using ViVeTool_GUI.FeatureScanner_WinUI3.ViewModels;

namespace ViVeTool_GUI.FeatureScanner_WinUI3.Views;

public sealed partial class DownloadPage : Page
{
    public DownloadViewModel ViewModel
    {
        get;
    }

    public DownloadPage()
    {
        ViewModel = App.GetService<DownloadViewModel>();
        InitializeComponent();
    }
}
