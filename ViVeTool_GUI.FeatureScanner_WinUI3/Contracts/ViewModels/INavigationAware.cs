namespace ViVeTool_GUI.FeatureScanner_WinUI3.Contracts.ViewModels;

public interface INavigationAware
{
    void OnNavigatedTo(object parameter);

    void OnNavigatedFrom();
}
