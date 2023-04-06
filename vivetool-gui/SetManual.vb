'ViVeTool GUI - Windows Feature Control GUI for ViVeTool
'Copyright (C) 2023 Peter Strick
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

''' <summary>
''' ViVeTool GUI - Manual Feature ID UI
''' </summary>
Public Class SetManual
    ''' <summary>
    ''' Enable Feature Button, enables the currently selected Feature.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_ActivateF_Click(sender As Object, e As EventArgs) Handles RMI_ActivateF.Click
        RTB_FeatureID.Text = RTB_FeatureID.Text.Trim()
        RTB_VariantID.Text = RTB_VariantID.Text.Trim()

        If IsNumeric(RTB_VariantID.Text) AndAlso String.IsNullOrWhiteSpace(RTB_VariantID.Text) = False Then
            Functions_ViVe.Enable(CUInt(RTB_FeatureID.Text), CUInt(RTB_VariantID.Text))
        Else
            Functions_ViVe.Enable(CUInt(RTB_FeatureID.Text), 0)
        End If
    End Sub

    ''' <summary>
    ''' Disable Feature Button, disables the currently selected Feature.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_DeactivateF_Click(sender As Object, e As EventArgs) Handles RMI_DeactivateF.Click
        RTB_FeatureID.Text = RTB_FeatureID.Text.Trim()
        RTB_VariantID.Text = RTB_VariantID.Text.Trim()

        If IsNumeric(RTB_VariantID.Text) AndAlso String.IsNullOrWhiteSpace(RTB_VariantID.Text) = False Then
            Functions_ViVe.Disable(CUInt(RTB_FeatureID.Text), CUInt(RTB_VariantID.Text))
        Else
            Functions_ViVe.Disable(CUInt(RTB_FeatureID.Text), 0)
        End If
    End Sub

    ''' <summary>
    ''' Revert Feature Button, reverts the currently selected Feature back to default settings.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_RevertF_Click(sender As Object, e As EventArgs) Handles RMI_RevertF.Click
        RTB_FeatureID.Text = RTB_FeatureID.Text.Trim()
        Functions_ViVe.Reset(CUInt(RTB_FeatureID.Text))
    End Sub

    ''' <summary>
    ''' Used to help RTB_FeatureID_KeyPress by checking if the last Character in the Text Box is a Number. If yes the Drop Down Button get's enabled.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RTB_FeatureID_TextChanged(sender As Object, e As EventArgs) Handles RTB_FeatureID.TextChanged
        If IsNumeric(RTB_FeatureID.Text) Then
            ' Enable the Button if RTB_FeatureID.Text consists of numbers
            RDDB_PerformAction.Enabled = True
        Else
            ' Disable the Button if RTB_FeatureID.Text does not consist of numbers
            RDDB_PerformAction.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Used to help RTB_VariantID_KeyPress by checking if the last Character in the Text Box is a Number. If yes the Drop Down Button get's enabled.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RTB_VariantID_TextChanged(sender As Object, e As EventArgs) Handles RTB_VariantID.TextChanged
        If IsNumeric(RTB_VariantID.Text) AndAlso IsNumeric(RTB_FeatureID.Text) Then
            ' Enable the Button if RTB_VariantID.Text and RTB_FeatureID.Text consists of numbers
            RDDB_PerformAction.Enabled = True
        ElseIf IsNumeric(RTB_FeatureID.Text) AndAlso String.IsNullOrEmpty(RTB_VariantID.Text) Then
            ' Enable the Button if RTB_FeatureID.Text consists of numbers, while RTB_VariantID.Text contains Nothing
            ' (No Variant ID entered, Variant ID will be treated as 0)
            RDDB_PerformAction.Enabled = True
        Else
            ' Disable the Button if RTB_VariantID.Text does not consist of numbers
            RDDB_PerformAction.Enabled = False
        End If
    End Sub

    Private Sub __DBG_FixLKG_Click(sender As Object, e As EventArgs) Handles __DBG_FixLKG.Click
        Functions_ViVe.FixLastKnownGood()
    End Sub

    Private Sub _DBG_FixPriority_Click(sender As Object, e As EventArgs) Handles _DBG_FixPriority.Click
        Functions_ViVe.FixPriority()
    End Sub
End Class