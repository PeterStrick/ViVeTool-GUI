using Microsoft.Windows.ApplicationModel.Resources;

namespace ViVeTool_GUI.FeatureScanner_WinUI3.Helpers;

public static class ResourceExtensions
{
    private static readonly ResourceLoader _resourceLoader = new();

    public static string GetLocalized(this string resourceKey) => _resourceLoader.GetString(resourceKey);
}
