using Microsoft.UI.Xaml.Controls;

using ViVeTool_GUI.FeatureScanner_WinUI3.ViewModels;

namespace ViVeTool_GUI.FeatureScanner_WinUI3.Views;

public sealed partial class ResultsPage : Page
{
    public ResultsViewModel ViewModel
    {
        get;
    }

    public ResultsPage()
    {
        ViewModel = App.GetService<ResultsViewModel>();
        InitializeComponent();
    }
}
