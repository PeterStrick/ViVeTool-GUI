# Building ViVeTool-GUI
### Build Requirements:
- ViVeTool-GUI was created in Visual Sttudio 2022. Older Versions may work
- Windows 10 Version 2004 / Windows 11

### The first time you open up the solution, you´ll be met with Reference Issues.
To fix the Reference Issues for Albacore.ViVe, use the DLL from the `lib` Folder or [build it yourself](https://github.com/thebookisclosed/ViVe/tree/master/ViVe)

To fix the Reference Issues for AutoUpdater.NET or Newtonsft.JSON, open the Package Manager Console.
You will be asked if you want to restore the NuGet packages, do so by pressing on Restore.

To fix the Reference Issues for the Telerik librarys either use the DLLs from the `lib\RCWF\2021.3.1109.40` Folder or install the [Telerik UI for WinForms](https://www.telerik.com/login/ui-for-winforms) application.

### You will not be able to access the designer if you do not have Telerik UI for WinForms installed. 

### However, you will be able to change code and build the solution by referencing the Telerik librarys in the `lib\RCWF\2021.3.1109.40` folder.
