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
Imports Albacore.ViVe, Albacore.ViVe.Exceptions, Albacore.ViVe.NativeStructs, Albacore.ViVe.NativeEnums
Imports Telerik.WinControls.UI

''' <summary>
''' Class that contains all Functions and Subs to work with the ViVe API
''' </summary>
Public Class Functions_ViVe
    ''' <summary>
    ''' Internal Function that set's Windows Feature Configuration accordingly with the specified ID, Variant, State and Priority
    ''' </summary>
    ''' <param name="ID">ViVeTool ID</param>
    ''' <param name="ID_Variant">ViVeTool ID Variant</param>
    ''' <param name="State">Feature State, either Enabled, Disabled, Default</param>
    ''' <param name="Priority">Feature Priority</param>
    ''' <param name="Operation">Feature Operation. Can be either VariantState and FeatureState for Enable/Disable, or alternatively ResetState for Reset</param>
    ''' <returns>An Integer corresponding to the Seccess Code of FeatureManager.SetFeatureConfigurations()</returns>
    Shared Function SetConfig(ID As UInteger, ID_Variant As UInteger, State As RTL_FEATURE_ENABLED_STATE, Priority As RTL_FEATURE_CONFIGURATION_PRIORITY, Operation As RTL_FEATURE_CONFIGURATION_OPERATION) As Integer
        Dim _configs(0) As RTL_FEATURE_CONFIGURATION_UPDATE

        _configs(0) = New RTL_FEATURE_CONFIGURATION_UPDATE() With {
           .FeatureId = ID,
           .EnabledState = State,
           .EnabledStateOptions = RTL_FEATURE_ENABLED_STATE_OPTIONS.WexpConfig,
           .[Variant] = ID_Variant,
           .VariantPayload = ID_Variant,
           .VariantPayloadKind = RTL_FEATURE_VARIANT_PAYLOAD_KIND.External,
           .Priority = Priority,
           .Operation = Operation
        }

        Return FeatureManager.SetFeatureConfigurations(_configs)
    End Function

    ''' <summary>
    ''' Enables ViVeTool Features using a specified ID and optionally a Variant
    ''' </summary>
    ''' <param name="ID">ViVeTool ID</param>
    ''' <param name="ID_Variant">ViVeTool ID Variant</param>
    Public Shared Sub Enable(ID As UInteger, Optional ID_Variant As UInteger = 0)
        Try
            Dim result = SetConfig(ID, ID_Variant, RTL_FEATURE_ENABLED_STATE.Enabled, RTL_FEATURE_CONFIGURATION_PRIORITY.Dynamic,
                                   RTL_FEATURE_CONFIGURATION_OPERATION.FeatureState Or RTL_FEATURE_CONFIGURATION_OPERATION.VariantState)
            If result = 0 Then
                RadTD.ShowDialog(My.Resources.SetConfig_Success,
                String.Format(My.Resources.SetConfig_SuccessfullySetFeatureID, ID, RTL_FEATURE_ENABLED_STATE.Enabled.ToString),
                Nothing, RadTaskDialogIcon.ShieldSuccessGreenBar)
            Else
                RadTD.ShowDialog(My.Resources.Error_Error,
                String.Format(My.Resources.Error_SetConfig, ID, RTL_FEATURE_ENABLED_STATE.Enabled.ToString),
                Nothing, RadTaskDialogIcon.Error, ExpandedText:=$"The Function returned {result}. Expected 0")
            End If
        Catch fpoe As FeaturePropertyOverflowException
            MsgBox(fpoe.Message)
        Catch ex As Exception
            RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnUnknownExceptionOccurred,
            Nothing, RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.ToString, ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Disables ViVeTool Features using a specified ID and optionally a Variant
    ''' </summary>
    ''' <param name="ID">ViVeTool ID</param>
    ''' <param name="ID_Variant">ViVeTool ID Variant</param>
    Public Shared Sub Disable(ID As UInteger, ID_Variant As UInteger)
        Try
            Dim result = SetConfig(ID, ID_Variant, RTL_FEATURE_ENABLED_STATE.Disabled, RTL_FEATURE_CONFIGURATION_PRIORITY.Dynamic,
                                   RTL_FEATURE_CONFIGURATION_OPERATION.FeatureState Or RTL_FEATURE_CONFIGURATION_OPERATION.VariantState)
            If result = 0 Then
                RadTD.ShowDialog(My.Resources.SetConfig_Success,
                String.Format(My.Resources.SetConfig_SuccessfullySetFeatureID, ID, RTL_FEATURE_ENABLED_STATE.Disabled.ToString),
                Nothing, RadTaskDialogIcon.ShieldSuccessGreenBar)
            Else
                RadTD.ShowDialog(My.Resources.Error_Error,
                String.Format(My.Resources.Error_SetConfig, ID, RTL_FEATURE_ENABLED_STATE.Disabled.ToString),
                Nothing, RadTaskDialogIcon.Error, ExpandedText:=$"The Function returned {result}. Expected 0")
            End If
        Catch fpoe As FeaturePropertyOverflowException
            MsgBox(fpoe.Message)
        Catch ex As Exception
            RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnUnknownExceptionOccurred,
            Nothing, RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.ToString, ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Resets ViVeTool Features using a specified ID
    ''' </summary>
    ''' <param name="ID">ViVeTool ID</param>
    Public Shared Sub Reset(ID As UInteger)
        Try
            Dim result = SetConfig(ID, 0, RTL_FEATURE_ENABLED_STATE.Default, RTL_FEATURE_CONFIGURATION_PRIORITY.Dynamic,
                                   RTL_FEATURE_CONFIGURATION_OPERATION.ResetState)
            If result = 0 Then
                RadTD.ShowDialog(My.Resources.SetConfig_Success,
                String.Format(My.Resources.SetConfig_SuccessfullySetFeatureID, ID, RTL_FEATURE_ENABLED_STATE.Default.ToString),
                Nothing, RadTaskDialogIcon.ShieldSuccessGreenBar)
            Else
                RadTD.ShowDialog(My.Resources.Error_Error,
                String.Format(My.Resources.Error_SetConfig, ID, RTL_FEATURE_ENABLED_STATE.Default.ToString),
                Nothing, RadTaskDialogIcon.Error, ExpandedText:=$"The Function returned {result}. Expected 0")
            End If
        Catch fpoe As FeaturePropertyOverflowException
            MsgBox(fpoe.Message)
        Catch ex As Exception
            RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnUnknownExceptionOccurred,
            Nothing, RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.ToString, ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Querys ViVeTool Features using a specified ID
    ''' </summary>
    ''' <param name="ID">ViVeTool ID</param>
    ''' <returns>ViVeTool ID Enabled State as String</returns>
    Public Shared Function Query(ID As UInteger) As String
        Return FeatureManager.QueryFeatureConfiguration(ID).GetValueOrDefault.ToString
    End Function
End Class
