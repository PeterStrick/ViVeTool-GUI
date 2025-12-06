# ViVeTool-GUI WPF Migration Notes

This document describes the new WPF scaffold added to support migrating ViVeTool-GUI from WinForms to WPF on .NET 9.

## Project Structure

The new WPF project is located in `ViVeTool-GUI.Wpf/` and follows the MVVM pattern using CommunityToolkit.Mvvm.

### Directory Structure

```
ViVeTool-GUI.Wpf/
├── App.xaml / App.xaml.vb         # Application entry point with theme initialization
├── ViVeTool-GUI.Wpf.vbproj        # Project file (.NET 9, UseWPF)
├── Converters/
│   └── InverseBooleanConverter.vb # Value converter for boolean inversion
├── Models/
│   └── FeatureItem.vb             # Feature data model
├── Resources/
│   ├── icon.ico                   # Application icon
│   └── Icons/                     # Icons8 assets copied from WinForms project
├── Services/
│   ├── FeatureService.vb          # Feature management service (ViVe integration placeholder)
│   ├── GitHubService.vb           # GitHub API service for feature lists
│   └── ThemeService.vb            # Theme switching service (Light/Dark/System modes)
├── Themes/
│   ├── Light.xaml                 # Light theme resource dictionary
│   └── Dark.xaml                  # Dark theme resource dictionary
├── ViewModels/
│   └── MainViewModel.vb           # Main window view model with MVVM commands
└── Views/
    ├── MainWindow.xaml            # Main window XAML
    └── MainWindow.xaml.vb         # Main window code-behind
```

## How to Build

### Prerequisites
- .NET 9 SDK or later
- Windows (for running the application) or EnableWindowsTargeting=true for cross-platform build

### Building the WPF Project

```bash
cd ViVeTool-GUI.Wpf
dotnet restore
dotnet build
```

Note: The existing WinForms projects target .NET Framework 4.8 and require Windows with the .NET Framework SDK to build.

## .NET 9 Fluent Theme

The WPF project uses the new .NET 9 Fluent theme for a modern Windows 11-style appearance.

### Features

- **ThemeMode**: Supports `Light`, `Dark`, and `System` (follows OS setting) theme modes
- **Fluent Theme Dictionary**: Uses `PresentationFramework.Fluent` for modern control styling
- **Accent Colors**: UI elements use Windows system accent colors via `SystemColors.AccentColorBrushKey`
- **Fallback to Aero2**: Can disable Fluent theme via `ThemeService.UseFluentTheme = False` to revert to classic Aero2 theme

### ThemeMode Warning Suppression

The project file includes `<NoWarn>$(NoWarn);WPF0001</NoWarn>` to suppress the experimental ThemeMode API warning. This is expected as ThemeMode is an official .NET 9 feature.

### Theme Switching

Users can cycle through themes (Light → Dark → System) using the theme toggle button in the toolbar. The `ThemeService` manages:

1. Setting `Application.ThemeMode` for the Fluent theme system
2. Updating app-specific resource dictionaries for custom colors
3. Detecting system dark mode preference for "System" mode

## Architecture

### MVVM Pattern
- **Models**: Data classes (e.g., `FeatureItem`) that represent application data
- **ViewModels**: Classes implementing `ObservableObject` from CommunityToolkit.Mvvm with commands and properties
- **Views**: XAML windows/controls with data binding to ViewModels

### Theme System
- Light and Dark themes are defined in `Themes/Light.xaml` and `Themes/Dark.xaml`
- `ThemeService` manages theme switching at runtime with Light/Dark/System modes
- The Fluent theme provides modern control styling with accent color support
- Themes define colors, brushes, and styles for common controls (Window, Button, TextBox, ComboBox, DataGrid)

### Commands
- `ApplyFeatureCommand`: Enables a selected feature
- `RevertFeatureCommand`: Reverts a feature to default state
- `RefreshBuildsCommand`: Fetches available Windows builds from GitHub (async)
- `ToggleThemeCommand`: Cycles through Light/Dark/System themes
- `LoadFeaturesCommand`: Loads feature list (async)

## Next Steps for Migration

### High Priority
1. **Feature Grid Logic**: Port the feature listing and manipulation logic from `GUI.vb` to `FeatureService.vb`
2. **Albacore.ViVe Integration**: Implement actual calls to `RtlFeatureManager` in `FeatureService`
3. **Manual Feature Window**: Create `SetManualWindow.xaml` equivalent for manual feature ID entry

### Medium Priority
4. **Feature Scanner**: Port the feature scanning functionality
5. **Settings/About Window**: Create settings and about dialogs
6. **Localization**: Migrate .resx resources to WPF-compatible format (XAML resource dictionaries or .resw)

### Lower Priority
7. **Auto-Updater Integration**: Implement update checking via `GitHubService`
8. **Crash Reporter**: Port crash reporting functionality
9. **Theme Persistence**: Save and restore theme preference from settings
10. **Self-Contained Publishing**: Configure publishing for self-contained x64 deployment

## Self-Contained Deployment

To enable self-contained x64 deployment, uncomment the following in the `.vbproj`:

```xml
<RuntimeIdentifier>win-x64</RuntimeIdentifier>
<SelfContained>true</SelfContained>
<PublishSingleFile>true</PublishSingleFile>
<IncludeNativeLibrariesForSelfExtract>true</IncludeNativeLibrariesForSelfExtract>
```

Then publish with:
```bash
dotnet publish -c Release -r win-x64
```

## Dependencies

- **CommunityToolkit.Mvvm** (8.2.2): MVVM pattern support with source generators
- **Albacore.ViVe.dll**: Windows feature control library (from `lib/` folder)

## Important Notes

- The existing WinForms project remains untouched and functional
- Both projects can coexist in the solution
- The WPF project uses .NET 9 with the new Fluent theme and ThemeMode support
- No Telerik dependencies are used in the WPF project
