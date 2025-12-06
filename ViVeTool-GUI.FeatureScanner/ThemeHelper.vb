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
Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
''' Helper class for theme-related operations
''' </summary>
Public NotInheritable Class ThemeHelper
    ' Dark theme colors - softer, less jarring
    Public Shared ReadOnly DarkBackColor As Color = Color.FromArgb(32, 32, 32)
    Public Shared ReadOnly DarkForeColor As Color = Color.FromArgb(200, 200, 200)  ' Softer white
    Private Shared ReadOnly DarkControlBackColor As Color = Color.FromArgb(50, 50, 50)
    Private Shared ReadOnly DarkTextBoxBackColor As Color = Color.FromArgb(45, 45, 45)
    Private Shared ReadOnly DarkButtonBackColor As Color = Color.FromArgb(55, 55, 55)
    Private Shared ReadOnly DarkBorderColor As Color = Color.FromArgb(70, 70, 70)  ' Subtle border
    Public Shared ReadOnly DarkTabBackColor As Color = Color.FromArgb(45, 45, 45)
    Public Shared ReadOnly DarkTabSelectedColor As Color = Color.FromArgb(60, 60, 60)

    ' Light theme colors
    Private Shared ReadOnly LightBackColor As Color = SystemColors.Control
    Private Shared ReadOnly LightForeColor As Color = SystemColors.ControlText
    Private Shared ReadOnly LightControlBackColor As Color = SystemColors.Control
    Private Shared ReadOnly LightTextBoxBackColor As Color = SystemColors.Window

    ' Track if dark mode is active (for tab drawing)
    Public Shared Property IsDarkMode As Boolean = False

    ' Track border cover panels for TabControls
    Private Shared _borderPanels As New Dictionary(Of TabControl, List(Of Panel))

    ''' <summary>
    ''' Applies dark theme to the application (stores preference)
    ''' </summary>
    ''' <param name="toggleButton">The toggle button to update</param>
    ''' <param name="darkModeImage">Image to use for dark mode</param>
    Public Shared Sub ApplyDarkTheme(toggleButton As CheckBox, darkModeImage As Image)
        toggleButton.Text = "  Dark Theme"
        toggleButton.Image = darkModeImage
        My.Settings.DarkMode = True
        My.Settings.Save()
    End Sub

    ''' <summary>
    ''' Applies light theme to the application (stores preference)
    ''' </summary>
    ''' <param name="toggleButton">The toggle button to update</param>
    ''' <param name="lightModeImage">Image to use for light mode</param>
    Public Shared Sub ApplyLightTheme(toggleButton As CheckBox, lightModeImage As Image)
        toggleButton.Text = "  Light Theme"
        toggleButton.Image = lightModeImage
        My.Settings.DarkMode = False
        My.Settings.Save()
    End Sub

    ''' <summary>
    ''' Applies system theme based on Windows settings
    ''' </summary>
    ''' <param name="toggleButton">The toggle button to update</param>
    ''' <param name="darkModeImage">Image to use for dark mode</param>
    ''' <param name="lightModeImage">Image to use for light mode</param>
    Public Shared Sub ApplySystemTheme(toggleButton As CheckBox, darkModeImage As Image, lightModeImage As Image)
        My.Settings.UseSystemTheme = True
        My.Settings.Save()
        Dim AppsUseLightTheme_CurrentUserDwordKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")
        If AppsUseLightTheme_CurrentUserDwordKey Is Nothing Then
            ' Registry key not found, default to light theme
            toggleButton.Checked = False
            toggleButton.Image = lightModeImage
            Return
        End If
        Dim AppsUseLightTheme_CurrentUserDwordValue As Object = AppsUseLightTheme_CurrentUserDwordKey.GetValue("AppsUseLightTheme")
        If AppsUseLightTheme_CurrentUserDwordValue IsNot Nothing AndAlso CInt(AppsUseLightTheme_CurrentUserDwordValue) = 0 Then
            toggleButton.Checked = True
            toggleButton.Image = darkModeImage
        Else
            toggleButton.Checked = False
            toggleButton.Image = lightModeImage
        End If
    End Sub

    ''' <summary>
    ''' Disables system theme usage
    ''' </summary>
    Public Shared Sub DisableSystemTheme()
        My.Settings.UseSystemTheme = False
        My.Settings.Save()
    End Sub

    ''' <summary>
    ''' Applies theme colors to a form and all its controls
    ''' </summary>
    ''' <param name="form">The form to apply the theme to</param>
    ''' <param name="isDarkMode">True for dark mode, False for light mode</param>
    Public Shared Sub ApplyThemeToForm(form As Form, isDarkMode As Boolean)
        ThemeHelper.IsDarkMode = isDarkMode
        If isDarkMode Then
            form.BackColor = DarkBackColor
            form.ForeColor = DarkForeColor
        Else
            form.BackColor = LightBackColor
            form.ForeColor = LightForeColor
        End If
        ApplyThemeToControls(form.Controls, isDarkMode)
        
        ' Update border cover panels visibility
        UpdateBorderPanels(isDarkMode)
    End Sub

    ''' <summary>
    ''' Updates visibility of border cover panels based on theme
    ''' </summary>
    Private Shared Sub UpdateBorderPanels(isDarkMode As Boolean)
        For Each kvp In _borderPanels
            For Each panel In kvp.Value
                panel.Visible = isDarkMode
                If isDarkMode Then
                    panel.BackColor = DarkBackColor
                    panel.BringToFront()
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' Recursively applies theme colors to a collection of controls
    ''' </summary>
    ''' <param name="controls">The control collection to apply the theme to</param>
    ''' <param name="isDarkMode">True for dark mode, False for light mode</param>
    Private Shared Sub ApplyThemeToControls(controls As Control.ControlCollection, isDarkMode As Boolean)
        For Each ctrl As Control In controls
            ApplyThemeToControl(ctrl, isDarkMode)
            If ctrl.HasChildren Then
                ApplyThemeToControls(ctrl.Controls, isDarkMode)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Applies theme colors to a single control based on its type
    ''' </summary>
    ''' <param name="ctrl">The control to apply the theme to</param>
    ''' <param name="isDarkMode">True for dark mode, False for light mode</param>
    Private Shared Sub ApplyThemeToControl(ctrl As Control, isDarkMode As Boolean)
        If isDarkMode Then
            ctrl.ForeColor = DarkForeColor

            Select Case True
                Case TypeOf ctrl Is TextBox
                    Dim tb = DirectCast(ctrl, TextBox)
                    tb.BackColor = DarkTextBoxBackColor
                    tb.ForeColor = DarkForeColor
                    tb.BorderStyle = BorderStyle.FixedSingle
                Case TypeOf ctrl Is TabControl
                    Dim tc = DirectCast(ctrl, TabControl)
                    tc.DrawMode = TabDrawMode.OwnerDrawFixed
                    tc.BackColor = DarkBackColor
                    tc.Invalidate()
                Case TypeOf ctrl Is TabPage
                    ctrl.BackColor = DarkBackColor
                Case TypeOf ctrl Is GroupBox
                    ctrl.BackColor = DarkBackColor
                    ctrl.ForeColor = Color.FromArgb(140, 140, 140)  ' Dimmer group box text
                Case TypeOf ctrl Is Button
                    ctrl.BackColor = DarkButtonBackColor
                    ctrl.ForeColor = DarkForeColor
                    DirectCast(ctrl, Button).FlatStyle = FlatStyle.Flat
                    DirectCast(ctrl, Button).FlatAppearance.BorderColor = DarkBorderColor
                    DirectCast(ctrl, Button).FlatAppearance.BorderSize = 1
                Case TypeOf ctrl Is CheckBox
                    Dim cb = DirectCast(ctrl, CheckBox)
                    If cb.Appearance = Appearance.Button Then
                        ctrl.BackColor = DarkButtonBackColor
                        cb.FlatStyle = FlatStyle.Flat
                        cb.FlatAppearance.BorderColor = DarkBorderColor
                        cb.FlatAppearance.BorderSize = 1
                    Else
                        ctrl.BackColor = DarkBackColor
                    End If
                Case TypeOf ctrl Is Label
                    ctrl.BackColor = Color.Transparent
                Case TypeOf ctrl Is LinkLabel
                    ctrl.BackColor = Color.Transparent
                    DirectCast(ctrl, LinkLabel).LinkColor = Color.FromArgb(100, 160, 220)  ' Softer blue
                    DirectCast(ctrl, LinkLabel).VisitedLinkColor = Color.FromArgb(150, 120, 200)
                Case TypeOf ctrl Is StatusStrip
                    ctrl.BackColor = DarkControlBackColor
                    ctrl.ForeColor = DarkForeColor
                Case TypeOf ctrl Is PictureBox
                    ctrl.BackColor = Color.Transparent
                Case TypeOf ctrl Is ProgressBar
                    ctrl.BackColor = DarkControlBackColor
                Case Else
                    ctrl.BackColor = DarkBackColor
            End Select
        Else
            ctrl.ForeColor = LightForeColor

            Select Case True
                Case TypeOf ctrl Is TextBox
                    Dim tb = DirectCast(ctrl, TextBox)
                    tb.BackColor = LightTextBoxBackColor
                    tb.BorderStyle = BorderStyle.Fixed3D
                Case TypeOf ctrl Is TabControl
                    Dim tc = DirectCast(ctrl, TabControl)
                    tc.DrawMode = TabDrawMode.Normal
                    tc.BackColor = LightBackColor
                    ctrl.Invalidate()
                Case TypeOf ctrl Is TabPage
                    DirectCast(ctrl, TabPage).UseVisualStyleBackColor = True
                Case TypeOf ctrl Is GroupBox
                    ctrl.BackColor = LightControlBackColor
                Case TypeOf ctrl Is Button
                    DirectCast(ctrl, Button).UseVisualStyleBackColor = True
                    DirectCast(ctrl, Button).FlatStyle = FlatStyle.Standard
                Case TypeOf ctrl Is CheckBox
                    DirectCast(ctrl, CheckBox).UseVisualStyleBackColor = True
                    DirectCast(ctrl, CheckBox).FlatStyle = FlatStyle.Standard
                Case TypeOf ctrl Is Label
                    ctrl.BackColor = Color.Transparent
                Case TypeOf ctrl Is LinkLabel
                    ctrl.BackColor = Color.Transparent
                    DirectCast(ctrl, LinkLabel).LinkColor = Color.Blue
                    DirectCast(ctrl, LinkLabel).VisitedLinkColor = Color.Purple
                Case TypeOf ctrl Is StatusStrip
                    ctrl.BackColor = LightControlBackColor
                Case TypeOf ctrl Is PictureBox
                    ctrl.BackColor = Color.Transparent
                Case Else
                    ctrl.BackColor = LightControlBackColor
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Handles custom drawing of tab headers for dark mode
    ''' Call this from TabControl.DrawItem event
    ''' </summary>
    ''' <param name="sender">The TabControl</param>
    ''' <param name="e">DrawItem event args</param>
    Public Shared Sub DrawTabItem(sender As Object, e As DrawItemEventArgs)
        Dim tc As TabControl = DirectCast(sender, TabControl)
        Dim tabPage As TabPage = tc.TabPages(e.Index)
        Dim tabBounds As Rectangle = tc.GetTabRect(e.Index)

        Dim backColor As Color
        Dim foreColor As Color

        If IsDarkMode Then
            If e.Index = tc.SelectedIndex Then
                backColor = DarkBackColor  ' Match the page background
            Else
                backColor = DarkTabBackColor
            End If
            foreColor = DarkForeColor
        Else
            ' Light mode - use default system colors
            If e.Index = tc.SelectedIndex Then
                backColor = SystemColors.Window
            Else
                backColor = SystemColors.Control
            End If
            foreColor = SystemColors.ControlText
        End If

        ' Fill background - expand slightly to cover borders
        Using brush As New SolidBrush(backColor)
            Dim expandedBounds As New Rectangle(tabBounds.X - 1, tabBounds.Y - 1, tabBounds.Width + 2, tabBounds.Height + 2)
            e.Graphics.FillRectangle(brush, expandedBounds)
        End Using

        ' Draw text centered
        Dim textFlags As TextFormatFlags = TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter
        TextRenderer.DrawText(e.Graphics, tabPage.Text, tc.Font, tabBounds, foreColor, textFlags)
    End Sub

    ''' <summary>
    ''' Sets up a TabControl for proper dark mode rendering
    ''' Call this once during form initialization
    ''' </summary>
    ''' <param name="tc">The TabControl to set up</param>
    Public Shared Sub SetupTabControlForDarkMode(tc As TabControl)
        ' Remove handlers first to avoid duplicates
        RemoveHandler tc.DrawItem, AddressOf DrawTabItem
        AddHandler tc.DrawItem, AddressOf DrawTabItem

        RemoveHandler tc.Paint, AddressOf PaintTabControlBackground
        AddHandler tc.Paint, AddressOf PaintTabControlBackground

        ' Create border cover panels if they don't exist
        If Not _borderPanels.ContainsKey(tc) Then
            CreateBorderCoverPanels(tc)
        End If

        ' Set up existing tab pages
        For Each tp As TabPage In tc.TabPages
            SetupTabPageForDarkMode(tp)
        Next

        ' Handle future tab pages
        RemoveHandler tc.ControlAdded, AddressOf TabControl_ControlAdded
        AddHandler tc.ControlAdded, AddressOf TabControl_ControlAdded
    End Sub

    ''' <summary>
    ''' Creates panels that cover the bright border lines around a TabControl
    ''' </summary>
    Private Shared Sub CreateBorderCoverPanels(tc As TabControl)
        Dim parent = tc.Parent
        If parent Is Nothing Then Return

        Dim panels As New List(Of Panel)
        Dim borderWidth As Integer = 3

        ' Calculate the tab content area (below the tab headers)
        Dim tabHeight As Integer = tc.ItemSize.Height + 4

        ' Left border panel
        Dim leftPanel As New Panel With {
            .BackColor = DarkBackColor,
            .Location = New Point(tc.Left - 1, tc.Top + tabHeight),
            .Size = New Size(borderWidth, tc.Height - tabHeight + 1),
            .Visible = False
        }
        parent.Controls.Add(leftPanel)
        leftPanel.BringToFront()
        panels.Add(leftPanel)

        ' Right border panel
        Dim rightPanel As New Panel With {
            .BackColor = DarkBackColor,
            .Location = New Point(tc.Right - borderWidth + 1, tc.Top + tabHeight),
            .Size = New Size(borderWidth, tc.Height - tabHeight + 1),
            .Visible = False
        }
        parent.Controls.Add(rightPanel)
        rightPanel.BringToFront()
        panels.Add(rightPanel)

        ' Bottom border panel
        Dim bottomPanel As New Panel With {
            .BackColor = DarkBackColor,
            .Location = New Point(tc.Left - 1, tc.Bottom - borderWidth + 1),
            .Size = New Size(tc.Width + 2, borderWidth),
            .Visible = False
        }
        parent.Controls.Add(bottomPanel)
        bottomPanel.BringToFront()
        panels.Add(bottomPanel)

        ' Top border panel (below tabs, covers the line between tabs and content)
        Dim topPanel As New Panel With {
            .BackColor = DarkBackColor,
            .Location = New Point(tc.Left - 1, tc.Top + tabHeight - 1),
            .Size = New Size(tc.Width + 2, borderWidth),
            .Visible = False
        }
        parent.Controls.Add(topPanel)
        topPanel.BringToFront()
        panels.Add(topPanel)

        _borderPanels(tc) = panels

        ' Handle resize to update panel positions
        AddHandler tc.Resize, Sub(s, e) RepositionBorderPanels(tc)
        AddHandler tc.LocationChanged, Sub(s, e) RepositionBorderPanels(tc)
    End Sub

    ''' <summary>
    ''' Repositions border cover panels when TabControl size/location changes
    ''' </summary>
    Private Shared Sub RepositionBorderPanels(tc As TabControl)
        If Not _borderPanels.ContainsKey(tc) Then Return

        Dim panels = _borderPanels(tc)
        If panels.Count < 4 Then Return

        Dim tabHeight As Integer = tc.ItemSize.Height + 4
        Dim borderWidth As Integer = 3

        ' Left
        panels(0).Location = New Point(tc.Left - 1, tc.Top + tabHeight)
        panels(0).Size = New Size(borderWidth, tc.Height - tabHeight + 1)

        ' Right
        panels(1).Location = New Point(tc.Right - borderWidth + 1, tc.Top + tabHeight)
        panels(1).Size = New Size(borderWidth, tc.Height - tabHeight + 1)

        ' Bottom
        panels(2).Location = New Point(tc.Left - 1, tc.Bottom - borderWidth + 1)
        panels(2).Size = New Size(tc.Width + 2, borderWidth)

        ' Top (below tabs)
        panels(3).Location = New Point(tc.Left - 1, tc.Top + tabHeight - 1)
        panels(3).Size = New Size(tc.Width + 2, borderWidth)

        For Each panel In panels
            If IsDarkMode Then panel.BringToFront()
        Next
    End Sub

    ''' <summary>
    ''' Handles new tab pages being added to set up their painting
    ''' </summary>
    Private Shared Sub TabControl_ControlAdded(sender As Object, e As ControlEventArgs)
        If TypeOf e.Control Is TabPage Then
            SetupTabPageForDarkMode(DirectCast(e.Control, TabPage))
        End If
    End Sub

    ''' <summary>
    ''' Sets up a TabPage for proper dark mode border removal
    ''' </summary>
    Private Shared Sub SetupTabPageForDarkMode(tp As TabPage)
        RemoveHandler tp.Paint, AddressOf PaintTabPageBackground
        AddHandler tp.Paint, AddressOf PaintTabPageBackground
    End Sub

    ''' <summary>
    ''' Paints the TabPage background to cover border lines
    ''' </summary>
    Private Shared Sub PaintTabPageBackground(sender As Object, e As PaintEventArgs)
        If Not IsDarkMode Then Return

        Dim tp As TabPage = DirectCast(sender, TabPage)

        ' Paint over all edges with a thicker coverage
        Dim borderWidth As Integer = 4
        Using brush As New SolidBrush(DarkBackColor)
            ' Left border
            e.Graphics.FillRectangle(brush, New Rectangle(0, 0, borderWidth, tp.Height))
            ' Right border
            e.Graphics.FillRectangle(brush, New Rectangle(tp.Width - borderWidth, 0, borderWidth, tp.Height))
            ' Bottom border
            e.Graphics.FillRectangle(brush, New Rectangle(0, tp.Height - borderWidth, tp.Width, borderWidth))
            ' Top border
            e.Graphics.FillRectangle(brush, New Rectangle(0, 0, tp.Width, borderWidth))
        End Using
    End Sub

    ''' <summary>
    ''' Paints the TabControl background to remove bright outlines
    ''' </summary>
    Private Shared Sub PaintTabControlBackground(sender As Object, e As PaintEventArgs)
        If Not IsDarkMode Then Return

        Dim tc As TabControl = DirectCast(sender, TabControl)

        ' Fill entire background first
        Using brush As New SolidBrush(DarkBackColor)
            e.Graphics.FillRectangle(brush, tc.ClientRectangle)
        End Using

        ' Fill the tab strip area
        Dim tabStripRect As New Rectangle(0, 0, tc.Width, tc.ItemSize.Height + 4)
        Using brush As New SolidBrush(DarkTabBackColor)
            e.Graphics.FillRectangle(brush, tabStripRect)
        End Using

        ' Redraw all tabs on top
        For i As Integer = 0 To tc.TabCount - 1
            Dim tabRect As Rectangle = tc.GetTabRect(i)
            Dim backColor As Color = If(i = tc.SelectedIndex, DarkBackColor, DarkTabBackColor)

            ' Expand the fill area to cover any border remnants
            Dim expandedRect As New Rectangle(tabRect.X - 1, tabRect.Y, tabRect.Width + 2, tabRect.Height + 2)
            Using brush As New SolidBrush(backColor)
                e.Graphics.FillRectangle(brush, expandedRect)
            End Using

            Dim textFlags As TextFormatFlags = TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter
            TextRenderer.DrawText(e.Graphics, tc.TabPages(i).Text, tc.Font, tabRect, DarkForeColor, textFlags)
        Next

        ' Paint over the border between tabs and content
        If tc.SelectedIndex >= 0 Then
            Dim selectedRect As Rectangle = tc.GetTabRect(tc.SelectedIndex)
            Using brush As New SolidBrush(DarkBackColor)
                ' Cover the line below the selected tab
                e.Graphics.FillRectangle(brush, New Rectangle(selectedRect.Left - 1, selectedRect.Bottom, selectedRect.Width + 2, 4))
            End Using
        End If

        ' Paint the outer edges of the tab area
        Using brush As New SolidBrush(DarkBackColor)
            ' Left edge of tab control
            e.Graphics.FillRectangle(brush, New Rectangle(0, tc.ItemSize.Height + 2, 3, tc.Height))
            ' Right edge
            e.Graphics.FillRectangle(brush, New Rectangle(tc.Width - 3, tc.ItemSize.Height + 2, 3, tc.Height))
            ' Bottom edge
            e.Graphics.FillRectangle(brush, New Rectangle(0, tc.Height - 3, tc.Width, 3))
        End Using
    End Sub
End Class
'''''''''''''
