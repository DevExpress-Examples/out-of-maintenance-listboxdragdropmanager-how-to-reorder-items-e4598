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
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Core.Native
Imports System.Windows.Media
Imports DevExpress.Xpf.Grid.DragDrop
Imports System.Windows
Imports System.Windows.Input
Imports System.Collections
Imports System.ComponentModel

Namespace DXEditorsSample

    Public Class MyListBoxDragDropManager
        Inherits ListBoxDragDropManager

        Private Class DragDropHitTestResult

            Private _Element As ListBoxEditItem

            Public Property Element As ListBoxEditItem
                Get
                    Return _Element
                End Get

                Private Set(ByVal value As ListBoxEditItem)
                    _Element = value
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

        Public Overrides Sub OnDragOver(ByVal sourceManager As DragDropManagerBase, ByVal source As UIElement, ByVal pt As Point)
            dragOverSourceManager = sourceManager
            Dim e As ListBoxDragOverEventArgs = RaiseDragOverEvent(sourceManager, pt, DropTargetType.None)
            DropEventIsLocked = If(e.Handled, Not e.AllowDrop, Not AllowDrop OrElse sourceManager.DraggingRows Is Nothing OrElse sourceManager.DraggingRows.Count < 1)
            If Not DropEventIsLocked Then
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
            Dim result As DragDropHitTestResult = New DragDropHitTestResult()
            Call VisualTreeHelper.HitTest(ListBox, Nothing, New HitTestResultCallback(AddressOf result.CallBack), New PointHitTestParameters(pt))
            Return result.Element
        End Function

        Protected Overrides Sub OnDrop(ByVal sourceManager As DragDropManagerBase, ByVal source As UIElement, ByVal pt As Point)
            If DropEventIsLocked Then Return
            Dim e As ListBoxDropEventArgs = RaiseDropEvent(sourceManager)
            If Not e.Handled Then
                If sourceManager.DraggingRows.Count > 0 AndAlso AllowDrop Then
                    ProcessTargetItem(e.SourceManager, pt)
                    Dim currentSource As IList = ItemsSource
                    Dim draggedFromSource As IList = e.SourceManager.GetSource(Nothing)
                    For Each obj As Object In e.DraggedRows
                        If TargetItem IsNot Nothing AndAlso obj Is TargetItem.Content Then Continue For
                        draggedFromSource.Remove(obj)
                        Select Case e.SourceManager.ViewInfo.DropTargetType
                            Case DropTargetType.DataArea
                                currentSource.Add(obj)
                            Case DropTargetType.InsertRowsAfter
                                currentSource.Insert(currentSource.IndexOf(TargetItem.Content) + 1, obj)
                            Case DropTargetType.InsertRowsBefore
                                currentSource.Insert(Math.Max(currentSource.IndexOf(TargetItem.Content), 0), obj)
                        End Select
                    Next

                    RaiseDroppedEvent(sourceManager, e.DraggedRows)
                End If
            End If

            HideDropMarker()
        End Sub

        Protected Overrides ReadOnly Property ItemsSource As IList
            Get
                If TypeOf ListBox.ItemsSource Is ICollectionView Then
                    Return TryCast(CType(ListBox.ItemsSource, ICollectionView).SourceCollection, IList)
                End If

                Return TryCast(ListBox.ItemsSource, IList)
            End Get
        End Property
    End Class
End Namespace
