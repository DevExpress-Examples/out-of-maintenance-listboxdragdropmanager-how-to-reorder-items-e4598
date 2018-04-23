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
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Xpf.Core;

namespace DXEditorsSample {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App: Application {
        public App() {
            
        }
    }
}
