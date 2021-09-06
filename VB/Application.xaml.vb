' Developer Express Code Central Example:
' ListBoxDragDropManager - How to reorder items
' 
' The current ListBoxDragDropManager version does not provide the capability to
' reorder items. This example demonstrates how to implement this functionality
' manually.
' In this example, we have created a ListBoxDragDropManager class
' descendant and overridden its OnDragOver and OnDrop methods to add the
' capability to drop an item before or after another item.
' You can use this class
' like the original ListBoxDragDropManager in the following
' manner:
' 
' <dxe:ListBoxEdit x:Name="editor1"
' DisplayMember="Name">
' <i:Interaction.Behaviors>
' <local:MyListBoxDragDropManager
' x:Name="manager1"/>
' </i:Interaction.Behaviors>
' </dxe:ListBoxEdit>
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4598

Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Windows
Imports System.Windows.Threading
Imports DevExpress.Xpf.Core

Namespace DXEditorsSample
	''' <summary>
	''' Interaction logic for App.xaml
	''' </summary>
	Partial Public Class App
		Inherits Application

		Public Sub New()

		End Sub
	End Class
End Namespace
