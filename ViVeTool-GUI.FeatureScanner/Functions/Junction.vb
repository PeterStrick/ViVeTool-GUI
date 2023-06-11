'ViVeTool-GUI - Windows Feature Control GUI for ViVeTool
'Copyright (C) 2022  Peter Strick / Peters Software Solutions
'
'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.
'
'You should have received a copy of the GNU General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.
Option Strict On
Imports System.ComponentModel, System.IO, System.Runtime.InteropServices, Microsoft.Win32.SafeHandles

''' <summary>
''' Public Class for everything revolving around Junctions
''' </summary>
Public Class Junction
    ''' <summary>
    ''' Variable for the Folder Name used in the Junction Subs. Important: No leading Backslash
    ''' </summary>
    Private Shared ReadOnly FolderName As String = "C:\FeatureScanner"

    ''' <summary>
    ''' Win32FileAccess Enum
    ''' </summary>
    <Flags>
    Private Enum Win32FileAccess As UInteger
        GenericRead = &H80000000UI
        GenericWrite = &H40000000
        GenericExecute = &H20000000
        GenericAll = &H10000000
    End Enum

    ''' <summary>
    ''' Win32FileAttribute Enum
    ''' </summary>
    <Flags>
    Private Enum Win32FileAttribute As UInteger
        AttributeReadOnly = &H1
        AttributeHidden = &H2
        AttributeSystem = &H4
        AttributeDirectory = &H10
        AttributeArchive = &H20
        AttributeDevice = &H40
        AttributeNormal = &H80
        AttributeTemporary = &H100
        AttributeSparseFile = &H200
        AttributeReparsePoint = &H400
        AttributeCompressed = &H800
        AttributeOffline = &H1000
        AttributeNotContentIndexed = &H2000
        AttributeEncrypted = &H4000
        AttributeIntegrityStream = &H8000
        AttributeVirtual = &H10000
        AttributeNoScrubData = &H20000
        AttributeEA = &H40000
        AttributeRecallOnOpen = &H40000
        AttributePinned = &H80000
        AttributeUnpinned = &H100000
        AttributeRecallOnDataAccess = &H400000
        FlagOpenNoRecall = &H100000
        ''' <summary>
        ''' Normal re-parse point processing will not occur; CreateFile will attempt to open the re-parse point. When a file is opened, a file handle is returned,
        ''' whether or not the filter that controls the re-parse point is operational.
        ''' <br />This flag cannot be used with the <see cref="FileMode.Create"/> flag.
        ''' <br />If the file is not a re-parse point, then this flag is ignored.
        ''' </summary>
        FlagOpenReparsePoint = &H200000
        FlagSessionAware = &H800000
        FlagPosixSemantics = &H1000000
        ''' <summary>
        ''' You must set this flag to obtain a handle to a directory. A directory handle can be passed to some functions instead of a file handle.
        ''' </summary>
        FlagBackupSemantics = &H2000000
        FlagDeleteOnClose = &H4000000
        FlagSequentialScan = &H8000000
        FlagRandomAccess = &H10000000
        FlagNoBuffering = &H20000000
        FlagOverlapped = &H40000000
        FlagWriteThrough = &H80000000UI
    End Enum

    ''' <summary>
    ''' CreateFile API from kernel32.dll
    ''' </summary>
    ''' <param name="lpFileName"></param>
    ''' <param name="dwDesiredAccess"></param>
    ''' <param name="dwShareMode"></param>
    ''' <param name="lpSecurityAttributes"></param>
    ''' <param name="dwCreationDisposition"></param>
    ''' <param name="dwFlagsAndAttributes"></param>
    ''' <param name="hTemplateFile"></param>
    ''' <returns></returns>
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function CreateFile(lpFileName As String, dwDesiredAccess As Win32FileAccess, dwShareMode As FileShare, lpSecurityAttributes As IntPtr, dwCreationDisposition As FileMode, dwFlagsAndAttributes As Win32FileAttribute, hTemplateFile As IntPtr) As SafeFileHandle
    End Function

    ''' <summary>
    ''' Because the tag we're using is IO_REPARSE_TAG_MOUNT_POINT, we use the MountPointReparseBuffer struct in the DUMMYUNIONNAME union.
    ''' </summary>
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Private Structure ReparseDataBuffer
        ''' <summary>Re-parse point tag. Must be a Microsoft re-parse point tag.</summary>
        Public ReparseTag As UInteger
        ''' <summary>Size, in bytes, of the re-parse data in the buffer that <see cref="PathBuffer"/> points to.</summary>
        Public ReparseDataLength As UShort
        ''' <summary>Reserved; do not use.</summary>
        Private Reserved As UShort
        ''' <summary>Offset, in bytes, of the substitute name string in the <see cref="PathBuffer"/> array.</summary>
        Public SubstituteNameOffset As UShort
        ''' <summary>Length, in bytes, of the substitute name string. If this string is null-terminated, <see cref="SubstituteNameLength"/> does not include space for the null character.</summary>
        Public SubstituteNameLength As UShort
        ''' <summary>Offset, in bytes, of the print name string in the <see cref="PathBuffer"/> array.</summary>
        Public PrintNameOffset As UShort
        ''' <summary>Length, in bytes, of the print name string. If this string is null-terminated, <see cref="PrintNameLength"/> does not include space for the null character.</summary>
        Public PrintNameLength As UShort
        ''' <summary>
        ''' A buffer containing the Unicode-encoded path string. The path string contains the substitute name
        ''' string and print name string. The substitute name and print name strings can appear in any order.
        ''' </summary>
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=8184)>
        Friend PathBuffer As String
        ' with <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16368)> Public PathBuffer As Byte()
        ' 16368 is the amount of bytes. since a Unicode string uses 2 bytes per character, constrain to 16368/2 = 8184 characters.
    End Structure

    ''' <summary>
    ''' DeviceIoControl API from kernel32.dll
    ''' </summary>
    ''' <param name="hDevice"></param>
    ''' <param name="dwIoControlCode"></param>
    ''' <param name="lpInBuffer"></param>
    ''' <param name="nInBufferSize"></param>
    ''' <param name="lpOutBuffer"></param>
    ''' <param name="nOutBufferSize"></param>
    ''' <param name="lpBytesReturned"></param>
    ''' <param name="lpOverlapped"></param>
    ''' <returns></returns>
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function DeviceIoControl(hDevice As SafeFileHandle, dwIoControlCode As UInteger, <[In]> ByRef lpInBuffer As ReparseDataBuffer, nInBufferSize As UInteger, lpOutBuffer As IntPtr, nOutBufferSize As UInteger, <Out> ByRef lpBytesReturned As UInteger, lpOverlapped As IntPtr) As Boolean
    End Function

    ''' <summary>
    ''' Get's the File Name of a Full File Path
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns>The File Name of a Full File Path</returns>
    Private Shared Function GetFileName(path As String) As String
        Return IO.Path.GetFileName(path)
    End Function

    ''' <summary>
    ''' Creates junctions
    ''' </summary>
    ''' <param name="junctionPath">The Path where the Junction should be created. Ex: C:\Junction1</param>
    ''' <param name="targetDir">The Path where the Junction should point to. Ex: C:\Windows</param>
    ''' <param name="overwrite">Determines if the Junction should be overwritten or not. Defaults to false</param>
    Public Shared Sub Create(junctionPath As String, targetDir As String, Optional overwrite As Boolean = False)
        Const IO_REPARSE_TAG_MOUNT_POINT As UInteger = &HA0000003UI
        Const FSCTL_SET_REPARSE_POINT As UInteger = &H900A4

        ' This prefix indicates to NTFS that the path is to be treated as a non-interpreted path in the virtual file system.
        Const NonInterpretedPathPrefix As String = "\??\"

        If Path.GetFullPath(junctionPath).Equals(Path.GetFullPath(targetDir)) Then
            Throw New IOException("Junction Class: Junction point path and target dir can't be identical.")
        ElseIf Directory.Exists(junctionPath) Then
            If Not overwrite Then Throw New IOException("Junction Class: Directory already exists and overwrite parameter is false.")
        ElseIf Not Directory.Exists(targetDir) Then
            Throw New IOException("Junction Class: Target Directory doesn't exist.")
        Else
            Directory.CreateDirectory(junctionPath)
        End If

        targetDir = NonInterpretedPathPrefix & Path.GetFullPath(targetDir)

        Using reparsePointHandle As SafeFileHandle = CreateFile(junctionPath, Win32FileAccess.GenericWrite, FileShare.Read Or FileShare.Write Or FileShare.Delete, IntPtr.Zero, FileMode.Open, Win32FileAttribute.FlagBackupSemantics Or Win32FileAttribute.FlagOpenReparsePoint, IntPtr.Zero)

            If reparsePointHandle.IsInvalid OrElse Marshal.GetLastWin32Error() <> 0 Then
                Throw New IOException("Junction Class: Unable to open re-parse point.", New Win32Exception())
            End If

            ' Unicode string is 2 bytes per character, so *2 to get byte length
            Dim byteLength As UShort = CType(targetDir.Length * 2, UShort)
            Dim reparseDataBuffer As New ReparseDataBuffer With {
                .ReparseTag = IO_REPARSE_TAG_MOUNT_POINT,
                .ReparseDataLength = byteLength + 12US,
                .SubstituteNameOffset = 0,
                .SubstituteNameLength = byteLength,
                .PrintNameOffset = byteLength + 2US,
                .PrintNameLength = 0,
                .PathBuffer = targetDir
            }

            Dim result As Boolean = DeviceIoControl(reparsePointHandle, FSCTL_SET_REPARSE_POINT, reparseDataBuffer, byteLength + 20US, IntPtr.Zero, 0, 0, IntPtr.Zero)
            If Not result Then
                Throw New IOException("Junction Class: Unable to create junction point.", New Win32Exception())
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Deletes Junctions. Warning!; this uses Directory.Delete to remove Junctions as it treats Junctions as regular folders.
    ''' </summary>
    ''' <param name="junctionPath">The Full Path of the Junction to delete</param>
    Public Shared Sub Delete(junctionPath As String, Optional Recursive As Boolean = False)
        If Not Directory.Exists(junctionPath) Then
            Throw New IOException("Junction Class: The Junction Path doesn't exist.")
        End If

        Try
            Directory.Delete(junctionPath, Recursive)
        Catch io As IOException
            Throw New IOException("Junction Class: Unable to delete the junction or directory. Make sure that the provided path points to a junction or a directory.")
        End Try
    End Sub

    ''' <summary>
    ''' Creates Junctions used in Symbol Downloading and Scanning
    ''' </summary>
    Public Shared Sub FeatureScanner_CreateJunctions()
        ' Create a new Folder for the Junctions
        If Not Directory.Exists(FolderName) Then
            Directory.CreateDirectory(FolderName)
            Debug.WriteLine($"Directory {FolderName} created")
        End If

        ' Create Junctions
        SilentTryCatchHelper(Sub() Create($"{FolderName}\ImmersiveControlPanel", "C:\Windows\ImmersiveControlPanel"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\PrintDialog", "C:\Windows\PrintDialog"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\ShellComponents", "C:\Windows\ShellComponents"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\ShellExperiences", "C:\Windows\ShellExperiences"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\SystemApps", "C:\Windows\SystemApps"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\SystemResources", "C:\Windows\SystemResources"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\WinSxS", "C:\Windows\WinSxS"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\CommonFiles64", "C:\Program Files\Common Files"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\CommonFiles86", "C:\Program Files (x86)\Common Files"))


        ' Further Junctions
        ' Fix because mach2 tries to scan C:\Windows\System32\config, which it does not have access to, even as an Administrator.
        ' Instead of doing tricks and hacks to run the Application as TrustedInstaller, which could result in AV detections, 
        ' we just copy the .exe and .dll files inside the System32 and SysWOW64 and let mach2 scan these instead.
        ' Hard Links would be more convenient in this scenario, although are not applicable because almost all Files in C:\Windows
        ' would then need their Ownership changed, to create the Hard Link.

        ' Create a new Folder for the Junctions
        If Not Directory.Exists($"{FolderName}\System32") Then
            Directory.CreateDirectory($"{FolderName}\System32")
            Debug.WriteLine($"Directory {FolderName}\System32 created")
        End If

        SilentTryCatchHelper(Sub() Create($"{FolderName}\System32\diagsvcs", "C:\Windows\System32\diagsvcs"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\System32\Dism", "C:\Windows\System32\Dism"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\System32\downlevel", "C:\Windows\System32\downlevel"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\System32\dsc", "C:\Windows\System32\dsc"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\System32\F12", "C:\Windows\System32\F12"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\System32\HealthAttestationClient", "C:\Windows\System32\HealthAttestationClient"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\System32\InputMethod", "C:\Windows\System32\InputMethod"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\System32\migration", "C:\Windows\System32\migration"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\System32\migwiz", "C:\Windows\System32\migwiz"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\System32\oobe", "C:\Windows\System32\oobe"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\System32\setup", "C:\Windows\System32\setup"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\System32\ShellExperiences", "C:\Windows\System32\ShellExperiences"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\System32\UNP", "C:\Windows\System32\UNP"))

        ' Create a new Folder for the Junctions
        If Not Directory.Exists($"{FolderName}\SysWOW64") Then
            Directory.CreateDirectory($"{FolderName}\SysWOW64")
            Debug.WriteLine($"Directory {FolderName}\SysWOW64 created")
        End If

        SilentTryCatchHelper(Sub() Create($"{FolderName}\SysWOW64\diagsvcs", "C:\Windows\SysWOW64\diagsvcs"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\SysWOW64\Dism", "C:\Windows\SysWOW64\Dism"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\SysWOW64\downlevel", "C:\Windows\SysWOW64\downlevel"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\SysWOW64\F12", "C:\Windows\SysWOW64\F12"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\SysWOW64\InputMethod", "C:\Windows\SysWOW64\InputMethod"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\SysWOW64\migration", "C:\Windows\SysWOW64\migration"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\SysWOW64\migwiz", "C:\Windows\SysWOW64\migwiz"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\SysWOW64\oobe", "C:\Windows\SysWOW64\oobe"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\SysWOW64\setup", "C:\Windows\SysWOW64\setup"))
        SilentTryCatchHelper(Sub() Create($"{FolderName}\SysWOW64\ShellExperiences", "C:\Windows\SysWOW64\ShellExperiences"))

        ' Write the Full path of all *.exe and *.dll Files to an Array
        Dim AllFiles_System32 As New Collections.ArrayList()
        AllFiles_System32.AddRange(Directory.GetFiles("C:\Windows\System32", "*.EXE", SearchOption.TopDirectoryOnly))
        AllFiles_System32.AddRange(Directory.GetFiles("C:\Windows\System32", "*.DLL", SearchOption.TopDirectoryOnly))

        ' Create a Symlink for each File in the Array
        For Each File As String In AllFiles_System32
            SilentTryCatchHelper(Sub() IO.File.Copy(File, $"{FolderName}\System32\{GetFileName(File)}"))
        Next

        ' Write the Full path of all *.exe and *.dll Files to an Array
        Dim AllFiles_SysWOW64 As New Collections.ArrayList()
        AllFiles_SysWOW64.AddRange(Directory.GetFiles("C:\Windows\SysWOW64", "*.EXE", SearchOption.TopDirectoryOnly))
        AllFiles_SysWOW64.AddRange(Directory.GetFiles("C:\Windows\SysWOW64", "*.DLL", SearchOption.TopDirectoryOnly))

        ' Create a Symlink for each File in the Array
        For Each File As String In AllFiles_SysWOW64
            SilentTryCatchHelper(Sub() IO.File.Copy(File, $"{FolderName}\SysWOW64\{GetFileName(File)}"))
        Next

        Debug.WriteLine($"Junctions created at {FolderName}")
    End Sub

    ''' <summary>
    ''' Deletes Junctions that have been used in Symbol Downloading and Scanning
    ''' </summary>
    Public Shared Sub FeatureScanner_DeleteJunctions()
        ' Delete Junctions
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\ImmersiveControlPanel"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\PrintDialog"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\ShellComponents"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\ShellExperiences"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\SystemApps"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\SystemResources"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\WinSxS"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\CommonFiles64"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\CommonFiles86"))

        Debug.WriteLine($"Junctions in {FolderName} deleted")

        ' Delete Junctions inside System32
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32\diagsvcs"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32\Dism"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32\downlevel"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32\dsc"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32\F12"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32\HealthAttestationClient"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32\InputMethod"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32\migration"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32\migwiz"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32\oobe"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32\setup"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32\ShellExperiences"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32\UNP"))

        ' Delete the Feature Scanner's System32 Directory
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\System32", True))
        Debug.WriteLine($"Directory {FolderName}\System32 deleted")

        ' Delete Junctions inside SysWOW64
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\SysWOW64\diagsvcs"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\SysWOW64\Dism"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\SysWOW64\downlevel"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\SysWOW64\F12"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\SysWOW64\InputMethod"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\SysWOW64\migration"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\SysWOW64\migwiz"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\SysWOW64\oobe"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\SysWOW64\setup"))
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\SysWOW64\ShellExperiences"))

        ' Delete the Feature Scanner's SysWOW64 Directory
        SilentTryCatchHelper(Sub() Delete($"{FolderName}\SysWOW64", True))
        Debug.WriteLine($"Directory {FolderName}\SysWOW64 deleted")

        ' Delete the Junctions root Folder
        SilentTryCatchHelper(Sub() Delete(FolderName))
        Debug.WriteLine($"Directory {FolderName} deleted")
    End Sub

    ''' <summary>
    ''' A Try Catch Helper Sub. Used so that there aren't 43 Try Catch Blocks taking up space in the source code
    ''' </summary>
    ''' <param name="Action">Sub/Function to execute and silently catch. Used with: SilentTryCatchHelper(Sub() XYZ(Param1))</param>
    Private Shared Sub SilentTryCatchHelper(Action As Action)
        Try
            Action()
        Catch ex As Exception
            ' Do nothing
        End Try
    End Sub
End Class