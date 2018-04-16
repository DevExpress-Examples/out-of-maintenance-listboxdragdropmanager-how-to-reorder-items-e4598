Imports Microsoft.VisualBasic
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
				Persons1.Add(New Person With {.Id = i, .Name = "Name1_" & i})
				Persons2.Add(New Person With {.Id = i, .Name = "Name2_" & i})
			Next i
			editor1.ItemsSource = Persons1
			editor2.ItemsSource = Persons2
		End Sub
	End Class
	Public Class Person
		Private privateId As Integer
		Public Property Id() As Integer
			Get
				Return privateId
			End Get
			Set(ByVal value As Integer)
				privateId = value
			End Set
		End Property
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
	End Class
End Namespace