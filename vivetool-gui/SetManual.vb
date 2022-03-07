Imports Albacore.ViVe, Telerik.WinControls.UI

Public Class SetManual
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

    Private Sub RMI_ActivateF_Click(sender As Object, e As EventArgs) Handles RMI_ActivateF.Click
        SetConfig_Manual(FeatureEnabledState.Enabled)
    End Sub

    Private Sub RMI_DeactivateF_Click(sender As Object, e As EventArgs) Handles RMI_DeactivateF.Click
        SetConfig_Manual(FeatureEnabledState.Disabled)
    End Sub

    Private Sub RMI_RevertF_Click(sender As Object, e As EventArgs) Handles RMI_RevertF.Click
        SetConfig_Manual(FeatureEnabledState.Default)
    End Sub

    'Private Sub RTB_FeatureID_TextChanged(sender As Object, e As EventArgs) Handles RTB_FeatureID.TextChanged
    '    If RTB_FeatureID.Text = Nothing Then
    '        RDDB_PerformAction.Enabled = False
    '    Else
    '        RDDB_PerformAction.Enabled = True
    '    End If
    'End Sub

    Private Sub RTB_FeatureID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles RTB_FeatureID.KeyPress
        If Not Asc(e.KeyChar) = 8 Then
            Dim allowedChars As String = "0123456789"
            If Not allowedChars.Contains(e.KeyChar.ToString.ToLower) Then
                'e.KeyChar = ChrW(0)
                RDDB_PerformAction.Enabled = False
                'e.Handled = True
            Else
                RDDB_PerformAction.Enabled = True
            End If
        End If
    End Sub

    Private Sub RTB_FeatureID_TextChanged(sender As Object, e As EventArgs) Handles RTB_FeatureID.TextChanged
        Dim allowedChars As String = "0123456789"
        Try
            If Not allowedChars.Contains(RTB_FeatureID.Text.Last) Then
                RDDB_PerformAction.Enabled = False
            Else
                RDDB_PerformAction.Enabled = True
            End If
        Catch ex As Exception
            RDDB_PerformAction.Enabled = False
        End Try
    End Sub
End Class
