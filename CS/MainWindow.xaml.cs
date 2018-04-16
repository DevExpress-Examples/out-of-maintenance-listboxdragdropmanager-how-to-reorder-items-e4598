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