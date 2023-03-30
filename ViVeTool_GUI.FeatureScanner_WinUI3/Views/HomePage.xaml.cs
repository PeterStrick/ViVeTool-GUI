using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using ViVeTool_GUI.FeatureScanner_WinUI3.ViewModels;

namespace ViVeTool_GUI.FeatureScanner_WinUI3.Views;

public sealed partial class HomePage : Page
{
    public HomeViewModel ViewModel
    {
        get;
    }

    public HomePage()
    {
        ViewModel = App.GetService<HomeViewModel>();
        InitializeComponent();
    }

    private async void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ContentDialog dialog = new ContentDialog();

        // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
        dialog.XamlRoot = this.XamlRoot;
        dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = "Dialog Title";
        dialog.PrimaryButtonText = "Primary Button";
        dialog.SecondaryButtonText = "Secondary Button";
        dialog.CloseButtonText = "Close Button";
        dialog.DefaultButton = ContentDialogButton.Primary;
        dialog.Content = "Dialog Content";

        var result = await dialog.ShowAsync();

    }
}
