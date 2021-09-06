﻿' Developer Express Code Central Example:
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

Imports System.Reflection
Imports System.Resources
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Windows

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.
<Assembly: AssemblyTitle("Q388999")>
<Assembly: AssemblyDescription("")>
<Assembly: AssemblyConfiguration("")>
<Assembly: AssemblyCompany("")>
<Assembly: AssemblyProduct("Q388999")>
<Assembly: AssemblyCopyright("Copyright ©  2012")>
<Assembly: AssemblyTrademark("")>
<Assembly: AssemblyCulture("")>

' Setting ComVisible to false makes the types in this assembly not visible 
' to COM components.  If you need to access a type in this assembly from 
' COM, set the ComVisible attribute to true on that type.
<Assembly: ComVisible(False)>

'In order to begin building localizable applications, set 
'<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
'inside a <PropertyGroup>.  For example, if you are using US english
'in your source files, set the <UICulture> to en-US.  Then uncomment
'the NeutralResourceLanguage attribute below.  Update the "en-US" in
'the line below to match the UICulture setting in the project file.

'[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


<Assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)>


' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' [assembly: AssemblyVersion("1.0.*")]
<Assembly: AssemblyVersion("1.0.0.0")>
<Assembly: AssemblyFileVersion("1.0.0.0")>
