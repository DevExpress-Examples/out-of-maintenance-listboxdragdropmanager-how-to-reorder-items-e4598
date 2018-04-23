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

using System.Windows;
using System.Collections.ObjectModel;

namespace DXEditorsSample {
	public partial class MainWindow: Window {
		ObservableCollection<Person> Persons1;
		ObservableCollection<Person> Persons2;
		public MainWindow() {
			InitializeComponent();
			Persons1 = new ObservableCollection<Person>();
			Persons2 = new ObservableCollection<Person>();
			for (int i = 0; i < 5; i++) {
				Persons1.Add(new Person { Id = i, Name = "Name1_" + i });
				Persons2.Add(new Person { Id = i, Name = "Name2_" + i });
			}
			editor1.ItemsSource = Persons1;
			editor2.ItemsSource = Persons2;
		}
	}
	public class Person {
		public int Id { get; set; }
		public string Name { get; set; }
	}
}