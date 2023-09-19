<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128645151/22.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4598)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* **[MainWindow.xaml](./CS/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MainWindow.xaml))**
* [MainWindow.xaml.cs](./CS/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/MainWindow.xaml.vb))
* [MyListBoxDragDropManager.cs](./CS/MyListBoxDragDropManager.cs) (VB: [MyListBoxDragDropManager.vb](./VB/MyListBoxDragDropManager.vb))
<!-- default file list end -->
# [Obsolete] ListBoxDragDropManager - How to reorder items


<p>The current ListBoxDragDropManager version does not provide the capability to reorder items. This example demonstrates how to implement this functionality manually.</p>
<p>In this example, we have created a ListBoxDragDropManager class descendant and overridden its OnDragOver and OnDrop methods to add the capability to drop an item before or after another item.</p>
<p>You can use this class like the original ListBoxDragDropManager in the following manner:</p>


```xaml
<dxe:ListBoxEdit x:Name="editor1" DisplayMember="Name">
	<dxmvvm:Interaction.Behaviors>
		<local:MyListBoxDragDropManager x:Name="manager1"/>
	</dxmvvm:Interaction.Behaviors>
</dxe:ListBoxEdit>
```



<br/>


