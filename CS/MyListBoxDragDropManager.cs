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
		MethodInfo _Info;
		MethodInfo Info {
			get {
				if (_Info == null)
					_Info = typeof(DragDropManagerBase).GetMethod("GetSource", BindingFlags.Instance | BindingFlags.NonPublic);
				return _Info;
			}
		}
		protected override void OnDragOver(DragDropManagerBase sourceManager, UIElement source, Point pt) {
			ListBoxDragOverEventArgs e = RaiseDragOverEvent(sourceManager, pt);
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
					foreach (object obj in sourceManager.DraggingRows) {
						ProcessTargetItem(sourceManager, pt);
						IList sourceCollection = Info.Invoke(sourceManager, new object[] { obj }) as IList;
						IList currentCollection = GetSource(null);
						if (sourceManager.ViewInfo.DropTargetRow != null) {
							int dropIndex = currentCollection.IndexOf(TargetItem.Content);
							sourceCollection.Remove(obj);
							int newDropIndex = currentCollection.IndexOf(TargetItem.Content);
							if (newDropIndex != -1)
								dropIndex = newDropIndex;
							if (sourceManager.ViewInfo.DropTargetType == DropTargetType.InsertRowsBefore)
								currentCollection.Insert(dropIndex, obj);
							if (sourceManager.ViewInfo.DropTargetType == DropTargetType.InsertRowsAfter)
								if (dropIndex < currentCollection.Count - 1)
									currentCollection.Insert(dropIndex + 1, obj);
								else
									currentCollection.Add(obj);
						}
						else if (sourceManager.ViewInfo.DropTargetType == DropTargetType.DataArea)
							currentCollection.Add(obj);
						RaiseDroppedEvent(sourceManager, e.DraggedRows);
					}
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