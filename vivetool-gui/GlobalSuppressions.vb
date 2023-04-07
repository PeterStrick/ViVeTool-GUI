' This file is used by Code Analysis to maintain SuppressMessage
' attributes that are applied to this project.
' Project-level suppressions either have no target or are given
' a specific target and scoped to a namespace, type, member, etc.

Imports System.Diagnostics.CodeAnalysis

<Assembly: SuppressMessage("Major Code Smell", "S3385:""Exit"" statements should not be used", Justification:="Easy Exit of a Try Catch statement", Scope:="member", Target:="~M:ViVeTool_GUI.GUI.LoadCommentsFromDB(System.String)")>
<Assembly: SuppressMessage("Style", "IDE1006:Benennungsstile", Justification:="Naming convention for Debug Subs and Functions", Scope:="member", Target:="~M:ViVeTool_GUI.GUI.__DBG_SetRDDL_Build_Text_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Style", "IDE1006:Benennungsstile", Justification:="Naming convention for Debug Subs and Functions", Scope:="member", Target:="~M:ViVeTool_GUI.GUI.__DBG_ChnageLanguage_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Style", "IDE1006:Benennungsstile", Justification:="Naming convention for Debug Subs and Functions", Scope:="member", Target:="~M:ViVeTool_GUI.GUI.__DBG_DisableCommentLoadingFromManualFL_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Style", "IDE1006:Benennungsstile", Justification:="Naming convention for Debug Subs and Functions", Scope:="member", Target:="~M:ViVeTool_GUI.GUI.__DBG_EnableCommentLoadingFromManualFL_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Style", "IDE1006:Benennungsstile", Justification:="Naming convention for Debug Subs and Functions", Scope:="member", Target:="~M:ViVeTool_GUI.GUI.__DBG_GetComments_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Style", "IDE1006:Benennungsstile", Justification:="Naming convention for Debug Subs and Functions", Scope:="member", Target:="~M:ViVeTool_GUI.GUI.__DBG_SeeCommentsData_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Style", "IDE1006:Benennungsstile", Justification:="Naming convention for Debug Subs and Functions", Scope:="member", Target:="~M:ViVeTool_GUI.GUI.__DBG_SetRDDL_Build_Text_ToNothing_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Style", "IDE1006:Benennungsstile", Justification:="Naming convention for Debug Subs and Functions", Scope:="member", Target:="~M:ViVeTool_GUI.SetManual.__DBG_FixLKG_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Style", "IDE1006:Benennungsstile", Justification:="Naming convention for Debug Subs and Functions", Scope:="member", Target:="~M:ViVeTool_GUI.SetManual._DBG_FixPriority_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification:="GitHub API URIs don't change much", Scope:="member", Target:="~M:ViVeTool_GUI.GUI.PopulateBuildComboBox")>
<Assembly: SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification:="The Comments Server URI shouldn't change much", Scope:="member", Target:="~M:ViVeTool_GUI.CommentsClient.SendComment_Thread")>
