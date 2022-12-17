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
Module IconExtensions
    <Runtime.CompilerServices.Extension>
    Public Function ToIcon(img As Bitmap, makeTransparent As Boolean, colorToMakeTransparent As Color) As Icon
        If makeTransparent Then
            img.MakeTransparent(colorToMakeTransparent)
        End If
        Dim iconHandle = img.GetHicon()
        Return Icon.FromHandle(iconHandle)
    End Function
End Module