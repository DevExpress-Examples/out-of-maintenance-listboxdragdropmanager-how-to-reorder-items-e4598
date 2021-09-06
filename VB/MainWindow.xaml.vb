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

Imports System.Windows
Imports System.Collections.ObjectModel

Namespace DXEditorsSample
	Partial Public Class MainWindow
		Inherits Window

		Private Persons1 As ObservableCollection(Of Person)
		Private Persons2 As ObservableCollection(Of Person)
		Public Sub New()
			InitializeComponent()
			Persons1 = New ObservableCollection(Of Person)()
			Persons2 = New ObservableCollection(Of Person)()
			For i As Integer = 0 To 4
				Persons1.Add(New Person With {
					.Id = i,
					.Name = "Name1_" & i
				})
				Persons2.Add(New Person With {
					.Id = i,
					.Name = "Name2_" & i
				})
			Next i
			editor1.ItemsSource = Persons1
			editor2.ItemsSource = Persons2
		End Sub
	End Class
	Public Class Person
		Public Property Id() As Integer
		Public Property Name() As String
	End Class
End Namespace