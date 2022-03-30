# Building ViVeTool GUI
### Build Requirements:
- ViVeTool GUI was created in Visual Studio 2022. Older Versions may not work.
- Windows 10 Version 2004 / Windows 11

### The first time you open up the solution, you´ll be met with Reference Issues.
To fix the Reference Issues for Albacore.ViVe, use the DLL from the `lib` Folder or [build it yourself](https://github.com/thebookisclosed/ViVe/tree/master/ViVe)

To fix the Reference Issues for AutoUpdater.NET, CrashReporter.Net and Newtonsft.JSON, open the Package Manager Console.
You will be asked if you want to restore the NuGet packages, do so by pressing on Restore.

To fix the Reference Issues for the Telerik libraries either use the DLLs from the `lib\RCWF\2021.3.1109.40` Folder or install the [Telerik UI for WinForms Suite (login required to download installer)](https://www.telerik.com/login/ui-for-winforms).

### You will not be able to access the designer if you do not have the Telerik UI for WinForms Suite installed. 

### However, you will be able to change the code and build ViVeTool GUI by referencing the Telerik libraries in the `lib\RCWF\2021.3.1109.40` folder.

# **It is also highly recommended to change CrashReporter.vb, see [Please change CrashReporter.vb](https://github.com/PeterStrick/ViVeTool-GUI/blob/master/vivetool-gui/C_Please_Change_CrashReporter.vb.md)**
