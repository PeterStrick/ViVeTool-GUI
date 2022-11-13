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