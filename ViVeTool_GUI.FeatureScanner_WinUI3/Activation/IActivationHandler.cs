namespace ViVeTool_GUI.FeatureScanner_WinUI3.Activation;

public interface IActivationHandler
{
    bool CanHandle(object args);

    Task HandleAsync(object args);
}
