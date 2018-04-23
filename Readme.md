# ListBoxDragDropManager - How to reorder items


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


