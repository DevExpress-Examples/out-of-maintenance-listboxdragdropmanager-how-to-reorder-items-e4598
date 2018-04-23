// Developer Express Code Central Example:
// ListBoxDragDropManager - How to reorder items
// 
// The current ListBoxDragDropManager version does not provide the capability to
// reorder items. This example demonstrates how to implement this functionality
// manually.
// In this example, we have created a ListBoxDragDropManager class
// descendant and overridden its OnDragOver and OnDrop methods to add the
// capability to drop an item before or after another item.
// You can use this class
// like the original ListBoxDragDropManager in the following
// manner:
// 
// <dxe:ListBoxEdit x:Name="editor1"
// DisplayMember="Name">
// <i:Interaction.Behaviors>
// <local:MyListBoxDragDropManager
// x:Name="manager1"/>
// </i:Interaction.Behaviors>
// </dxe:ListBoxEdit>
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E4598

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core.Native;
using System.Windows.Media;
using System.Reflection;
using DevExpress.Xpf.Grid.DragDrop;
using System.Windows;
using System.Windows.Input;
using System.Collections;
using System.ComponentModel;

namespace DXEditorsSample {
	public class MyListBoxDragDropManager: ListBoxDragDropManager {
        class DragDropHitTestResult {
            public ListBoxEditItem Element { get; private set; }
            public HitTestResultBehavior CallBack(HitTestResult result) {
                ListBoxEditItem item = LayoutHelper.FindParentObject<ListBoxEditItem>(result.VisualHit);
                if (item != null) {
                    Element = item;
                    return HitTestResultBehavior.Stop;
                }
                return HitTestResultBehavior.Continue;
            }
        }
        ListBoxEditItem TargetItem;
        public override void OnDragOver(DragDropManagerBase sourceManager, UIElement source, Point pt) {
            ListBoxDragOverEventArgs e = RaiseDragOverEvent(sourceManager, pt, DropTargetType.None);
            DropEventIsLocked = e.Handled ? !e.AllowDrop : !AllowDrop || sourceManager.DraggingRows == null || sourceManager.DraggingRows.Count < 1;
            if (!DropEventIsLocked) {
                ProcessTargetItem(sourceManager, pt);
                return;
            }
        }
        protected virtual void ProcessTargetItem(DragDropManagerBase sourceManager, Point pt) {
            TargetItem = GetVisibleHitTestElement(pt);
            if (TargetItem == null) {
                sourceManager.ViewInfo.DropTargetRow = null;
                sourceManager.SetDropTargetType(DropTargetType.DataArea);
                ShowDropMarker(ListBox, TableDragIndicatorPosition.None);
                return;
            }
            var position = Mouse.GetPosition(TargetItem);
            double height = TargetItem.ActualHeight;
            if (position.Y < height / 2) {
                sourceManager.SetDropTargetType(DropTargetType.InsertRowsBefore);
                ShowDropMarker(TargetItem, TableDragIndicatorPosition.Top);
            }
            else {
                sourceManager.SetDropTargetType(DropTargetType.InsertRowsAfter);
                ShowDropMarker(TargetItem, TableDragIndicatorPosition.Bottom);
            }
            sourceManager.ViewInfo.DropTargetRow = TargetItem.Content;
        }
        protected virtual ListBoxEditItem GetVisibleHitTestElement(Point pt) {
            DragDropHitTestResult result = new DragDropHitTestResult();
            VisualTreeHelper.HitTest(ListBox, null, new HitTestResultCallback(result.CallBack), new PointHitTestParameters(pt));
            return result.Element;
        }
        protected override void OnDrop(DragDropManagerBase sourceManager, UIElement source, Point pt) {
            if (DropEventIsLocked) return;
            ListBoxDropEventArgs e = RaiseDropEvent(sourceManager);
            if (!e.Handled) {
                if (sourceManager.DraggingRows.Count > 0 && AllowDrop) {
                    ProcessTargetItem(e.SourceManager, pt);
                    IList currentSource = ItemsSource;
                    IList draggedFromSource = e.SourceManager.GetSource(null);
                    foreach (object obj in e.DraggedRows) {
                        if (TargetItem != null && obj == TargetItem.Content)
                            continue;
                        draggedFromSource.Remove(obj);
                        switch (e.SourceManager.ViewInfo.DropTargetType) {
                            case DropTargetType.DataArea:
                                currentSource.Add(obj);
                                break;
                            case DropTargetType.InsertRowsAfter:
                                currentSource.Insert(currentSource.IndexOf(TargetItem.Content) + 1, obj);
                                break;
                            case DropTargetType.InsertRowsBefore:
                                currentSource.Insert(Math.Max(currentSource.IndexOf(TargetItem.Content), 0), obj);
                                break;
                        }
                    }
                    RaiseDroppedEvent(sourceManager, e.DraggedRows);
                }
            }
            HideDropMarker();
        }
        protected override IList ItemsSource {
            get {
                if (ListBox.ItemsSource is ICollectionView) {
                    return ((ICollectionView)ListBox.ItemsSource).SourceCollection as IList;
                }
                return ListBox.ItemsSource as IList;
            }
        }
	}
}