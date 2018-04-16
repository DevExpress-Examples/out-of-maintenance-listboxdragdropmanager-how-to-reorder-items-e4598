Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Core.Native
Imports System.Windows.Media
Imports System.Reflection
Imports DevExpress.Xpf.Grid.DragDrop
Imports System.Windows
Imports System.Windows.Input
Imports System.Collections
Imports System.ComponentModel

Namespace DXEditorsSample
	Public Class MyListBoxDragDropManager
		Inherits ListBoxDragDropManager
		Private Class DragDropHitTestResult
			Private privateElement As ListBoxEditItem
			Public Property Element() As ListBoxEditItem
				Get
					Return privateElement
				End Get
				Private Set(ByVal value As ListBoxEditItem)
					privateElement = value
				End Set
			End Property
			Public Function CallBack(ByVal result As HitTestResult) As HitTestResultBehavior
				Dim item As ListBoxEditItem = LayoutHelper.FindParentObject(Of ListBoxEditItem)(result.VisualHit)
				If item IsNot Nothing Then
					Element = item
					Return HitTestResultBehavior.Stop
				End If
				Return HitTestResultBehavior.Continue
			End Function
		End Class
		Private TargetItem As ListBoxEditItem
		Private _Info As MethodInfo
		Private ReadOnly Property Info() As MethodInfo
			Get
				If _Info Is Nothing Then
					_Info = GetType(DragDropManagerBase).GetMethod("GetSource", BindingFlags.Instance Or BindingFlags.NonPublic)
				End If
				Return _Info
			End Get
		End Property
		Public Overrides Sub OnDragOver(ByVal sourceManager As DragDropManagerBase, ByVal source As UIElement, ByVal pt As Point)
			Dim e As ListBoxDragOverEventArgs = RaiseDragOverEvent(sourceManager, pt)
			DropEventIsLocked = If(e.Handled, (Not e.AllowDrop), (Not AllowDrop) OrElse sourceManager.DraggingRows Is Nothing OrElse sourceManager.DraggingRows.Count < 1)
			If (Not DropEventIsLocked) Then
				ProcessTargetItem(sourceManager, pt)
				Return
			End If
		End Sub
		Protected Overridable Sub ProcessTargetItem(ByVal sourceManager As DragDropManagerBase, ByVal pt As Point)
			TargetItem = GetVisibleHitTestElement(pt)
			If TargetItem Is Nothing Then
				sourceManager.ViewInfo.DropTargetRow = Nothing
				sourceManager.SetDropTargetType(DropTargetType.DataArea)
				ShowDropMarker(ListBox, TableDragIndicatorPosition.None)
				Return
			End If
			Dim position = Mouse.GetPosition(TargetItem)
			Dim height As Double = TargetItem.ActualHeight
			If position.Y < height / 2 Then
				sourceManager.SetDropTargetType(DropTargetType.InsertRowsBefore)
				ShowDropMarker(TargetItem, TableDragIndicatorPosition.Top)
			Else
				sourceManager.SetDropTargetType(DropTargetType.InsertRowsAfter)
				ShowDropMarker(TargetItem, TableDragIndicatorPosition.Bottom)
			End If
			sourceManager.ViewInfo.DropTargetRow = TargetItem.Content
		End Sub
		Protected Overridable Function GetVisibleHitTestElement(ByVal pt As Point) As ListBoxEditItem
			Dim result As New DragDropHitTestResult()
			VisualTreeHelper.HitTest(ListBox, Nothing, New HitTestResultCallback(AddressOf result.CallBack), New PointHitTestParameters(pt))
			Return result.Element
		End Function
		Protected Overrides Sub OnDrop(ByVal sourceManager As DragDropManagerBase, ByVal source As UIElement, ByVal pt As Point)
			If DropEventIsLocked Then
				Return
			End If
			Dim e As ListBoxDropEventArgs = RaiseDropEvent(sourceManager)
			If (Not e.Handled) Then
				If sourceManager.DraggingRows.Count > 0 AndAlso AllowDrop Then
					For Each obj As Object In sourceManager.DraggingRows
						ProcessTargetItem(sourceManager, pt)
						Dim sourceCollection As IList = TryCast(Info.Invoke(sourceManager, New Object() { obj }), IList)
						Dim currentCollection As IList = GetSource(Nothing)
						If sourceManager.ViewInfo.DropTargetRow IsNot Nothing Then
							Dim dropIndex As Integer = currentCollection.IndexOf(TargetItem.Content)
							sourceCollection.Remove(obj)
							Dim newDropIndex As Integer = currentCollection.IndexOf(TargetItem.Content)
							If newDropIndex <> -1 Then
								dropIndex = newDropIndex
							End If
							If sourceManager.ViewInfo.DropTargetType = DropTargetType.InsertRowsBefore Then
								currentCollection.Insert(dropIndex, obj)
							End If
							If sourceManager.ViewInfo.DropTargetType = DropTargetType.InsertRowsAfter Then
								If dropIndex < currentCollection.Count - 1 Then
									currentCollection.Insert(dropIndex + 1, obj)
								Else
									currentCollection.Add(obj)
								End If
							End If
						ElseIf sourceManager.ViewInfo.DropTargetType = DropTargetType.DataArea Then
							currentCollection.Add(obj)
						End If
						RaiseDroppedEvent(sourceManager, e.DraggedRows)
					Next obj
				End If
			End If
			HideDropMarker()
		End Sub
		Protected Overrides ReadOnly Property ItemsSource() As IList
			Get
				If TypeOf ListBox.ItemsSource Is ICollectionView Then
					Return TryCast((CType(ListBox.ItemsSource, ICollectionView)).SourceCollection, IList)
				End If
				Return TryCast(ListBox.ItemsSource, IList)
			End Get
		End Property
	End Class
End Namespace