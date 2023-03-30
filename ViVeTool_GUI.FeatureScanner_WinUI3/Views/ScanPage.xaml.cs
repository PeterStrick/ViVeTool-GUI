using Microsoft.UI.Xaml.Controls;

using ViVeTool_GUI.FeatureScanner_WinUI3.ViewModels;

namespace ViVeTool_GUI.FeatureScanner_WinUI3.Views;

public sealed partial class ScanPage : Page
{
    public ScanViewModel ViewModel
    {
        get;
    }

    public ScanPage()
    {
        ViewModel = App.GetService<ScanViewModel>();
        InitializeComponent();
    }
}
