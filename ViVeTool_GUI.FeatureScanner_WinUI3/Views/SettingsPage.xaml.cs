using Microsoft.UI.Xaml.Controls;

using ViVeTool_GUI.FeatureScanner_WinUI3.ViewModels;

namespace ViVeTool_GUI.FeatureScanner_WinUI3.Views;

// TODO: Set the URL for your privacy policy by updating SettingsPage_PrivacyTermsLink.NavigateUri in Resources.resw.
public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
    }
}
