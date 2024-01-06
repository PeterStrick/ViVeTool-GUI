'ViVeTool-GUI - Windows Feature Control GUI for ViVeTool
'Copyright (C) 2024 Peter Strick
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
Imports Telerik.WinControls.UI.Localization

''' <summary>
''' Telerik Localization Provider Class. Used to house Translations for Telerik Components, as well as a Method to set them
''' </summary>
Public Class TelerikLocalization
    ''' <summary>
    ''' Set the Localization Providers to the translated ones
    ''' </summary>
    Public Shared Sub SetProviders()
        RadTaskDialogLocalizationProvider.CurrentProvider = New CustomRadTaskDialogLocalizationProvider()
    End Sub
End Class

Public Class CustomRadTaskDialogLocalizationProvider
    Inherits RadTaskDialogLocalizationProvider
    Public Overrides Function GetLocalizedString(ByVal id As String) As String
        Select Case id
            Case RadTaskDialogStringId.ExpanderCollapsedButtonText
                Return My.Resources.Telerik_SeeDetails
            Case RadTaskDialogStringId.ExpanderExpandedButtonText
                Return My.Resources.Telerik_HideDetails
            Case RadTaskDialogStringId.ContinueButtonText
                Return My.Resources.Telerik_Continue
            Case RadTaskDialogStringId.TryAgainButtonText
                Return My.Resources.Telerik_TryAgain
            Case RadTaskDialogStringId.HelpButtonText
                Return My.Resources.Telerik_Help
            Case RadTaskDialogStringId.CloseButtonText
                Return My.Resources.Generic_Close
            Case RadTaskDialogStringId.NoButtonText
                Return My.Resources.Telerik_No
            Case RadTaskDialogStringId.YesButtonText
                Return My.Resources.Telerik_Yes
            Case RadTaskDialogStringId.RetryButtonText
                Return My.Resources.Telerik_Retry
            Case RadTaskDialogStringId.AbortButtonText
                Return My.Resources.Telerik_Abort
            Case RadTaskDialogStringId.IgnoreButtonText
                Return My.Resources.Telerik_Ignore
            Case RadTaskDialogStringId.OKButtonText
                Return My.Resources.Telerik_OK
            Case RadTaskDialogStringId.CancelButtonText
                Return My.Resources.Telerik_Cancel
            Case Else
                'Return MyBase.GetLocalizedString(id)
                Return String.Format(My.Resources.TelerikLocalization_MissingID, id)
        End Select
    End Function
End Class