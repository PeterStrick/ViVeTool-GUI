﻿'ViVeTool-GUI - Windows Feature Control GUI for ViVeTool
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
Imports Albacore.ViVe, Telerik.WinControls.UI

''' <summary>
''' ViVeTool GUI - Manual Feature ID UI
''' </summary>
Public Class SetManual
    ''' <summary>
    ''' Set's the Feature Configuration. Uses the FeatureEnabledState parameter to set the EnabledState of the Feature
    ''' </summary>
    ''' <param name="FeatureEnabledState">Specifies what Enabled State the Feature should be in. Can be either Enabled, Disabled or Default</param>
    Private Sub SetConfig_Manual(FeatureEnabledState As FeatureEnabledState)
        Try
            'Initialize Variables
            Dim _enabledStateOptions, _variant, _variantPayloadKind, _variantPayload, _group As Integer
            _enabledStateOptions = 1
            _group = 4

            'FeatureConfiguration Variable
            Dim _configs As New List(Of FeatureConfiguration) From {
                New FeatureConfiguration() With {
                    .FeatureId = CUInt(RTB_FeatureID.Text),
                    .EnabledState = FeatureEnabledState,
                    .EnabledStateOptions = _enabledStateOptions,
                    .Group = _group,
                    .[Variant] = _variant,
                    .VariantPayload = _variantPayload,
                    .VariantPayloadKind = _variantPayloadKind,
                    .Action = FeatureConfigurationAction.UpdateEnabledState
                }
            }

            'Set's the selected Feature to it's specified EnabledState. If anything goes wrong, display a Error Message in the Status Label.
            'On Successful Operations; 
            'RtlFeatureManager.SetBootFeatureConfigurations(_configs) returns True
            'and RtlFeatureManager.SetLiveFeatureConfigurations(_configs, FeatureConfigurationSection.Runtime) returns 0
            If Not RtlFeatureManager.SetBootFeatureConfigurations(_configs) OrElse RtlFeatureManager.SetLiveFeatureConfigurations(_configs, FeatureConfigurationSection.Runtime) >= 1 Then
                Dim RTD As New RadTaskDialogPage With {
                    .Caption = " An Error occurred",
                    .Heading = "An Error occurred while trying to set Feature ID " & RTB_FeatureID.Text & " to " & FeatureEnabledState.ToString,
                    .Icon = RadTaskDialogIcon.Error
                }
                RTD.CommandAreaButtons.Add(RadTaskDialogButton.Close)
                RadTaskDialog.ShowDialog(RTD)
            Else
                Dim RTD As New RadTaskDialogPage With {
                    .Caption = " Success",
                    .Heading = "Successfully set Feature ID " & RTB_FeatureID.Text & " to " & FeatureEnabledState.ToString,
                    .Icon = RadTaskDialogIcon.ShieldSuccessGreenBar
                }
                RTD.CommandAreaButtons.Add(RadTaskDialogButton.Close)
                RadTaskDialog.ShowDialog(RTD)
            End If
        Catch ex As Exception
            'Catch Any Exception that may occur

            Dim CopyExAndClose As New RadTaskDialogButton With {
                .Text = "Copy Exception and Close"
            }
            AddHandler CopyExAndClose.Click, New EventHandler(Sub() My.Computer.Clipboard.SetText(ex.ToString))

            Dim RTD As New RadTaskDialogPage With {
                    .Caption = " An Exception occurred",
                    .Heading = "An Exception occurred while trying to set Feature ID " & RTB_FeatureID.Text & " to " & FeatureEnabledState.ToString,
                    .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                }
            RTD.Expander.Text = ex.ToString
            RTD.Expander.ExpandedButtonText = "Collapse Exception"
            RTD.Expander.CollapsedButtonText = "Show Exception"
            RTD.CommandAreaButtons.Add(CopyExAndClose)

            RadTaskDialog.ShowDialog(RTD)
        End Try
    End Sub

    ''' <summary>
    ''' Enable Feature Button, enables the currently selected Feature.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_ActivateF_Click(sender As Object, e As EventArgs) Handles RMI_ActivateF.Click
        RTB_FeatureID.Text = RTB_FeatureID.Text.Trim()
        SetConfig_Manual(FeatureEnabledState.Enabled)
    End Sub

    ''' <summary>
    ''' Disable Feature Button, disables the currently selected Feature.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_DeactivateF_Click(sender As Object, e As EventArgs) Handles RMI_DeactivateF.Click
        RTB_FeatureID.Text = RTB_FeatureID.Text.Trim()
        SetConfig_Manual(FeatureEnabledState.Disabled)
    End Sub

    ''' <summary>
    ''' Revert Feature Button, reverts the currently selected Feature back to default settings.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_RevertF_Click(sender As Object, e As EventArgs) Handles RMI_RevertF.Click
        RTB_FeatureID.Text = RTB_FeatureID.Text.Trim()
        SetConfig_Manual(FeatureEnabledState.Default)
    End Sub

    ''' <summary>
    ''' Used to help RTB_FeatureID_KeyPress by checking if the last Character in the Text Box is a Number. If yes the Drop Down Button get's enabled.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RTB_FeatureID_TextChanged(sender As Object, e As EventArgs) Handles RTB_FeatureID.TextChanged
        If IsNumeric(RTB_FeatureID.Text) Then
            RDDB_PerformAction.Enabled = True
        Else
            RDDB_PerformAction.Enabled = False
        End If
    End Sub
End Class