# WinUI for WPF developers

Notes I'm making while learning WinUI. They should be useful for WPF developers, who are starting their first WinUI project.

- [WinUI for WPF developers](#winui-for-wpf-developers)
  - [1.1. Some useful links on start](#11-some-useful-links-on-start)
  - [1.2. Common errors](#12-common-errors)
  - [1.3. x:Bind](#13-xbind)
    - [1.3.1. Main differences between **Bind** and **x:Bind**](#131-main-differences-between-bind-and-xbind)
    - [1.3.2. **x:Bind** in depth](#132-xbind-in-depth)
    - [1.3.4. Example application](#134-example-application)
  - [1.4. ResourceDictionary](#14-resourcedictionary)
  - [1.5. Visibility and **x:Load**](#15-visibility-and-xload)
  - [1.6. Styles](#16-styles)

## 1.1. Some useful links on start

- [Windows App SDK download](https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/downloads)

- [Windows Template Studio](https://marketplace.visualstudio.com/items?itemName=TemplateStudio.TemplateStudioForWinUICs) - a sophisticated wizard for creating new WinUI projects

- [WinUI 3 Gallery](https://apps.microsoft.com/store/detail/winui-3-gallery/9P3JFPWWDZRC) - a demo app, that shows all WinUI 3 controls with Xaml examples and links to documentation. Also some Microsoft docs can open an example in this application, if you have it installed
  
- [WinUI 2 Gallery](https://apps.microsoft.com/store/detail/winui-2-gallery/9MSVH128X2ZT?hl=en-us&gl=us) - also useful, because some Microsoft docs, for older controls, link to WinUI 2 Gallery app

- [Windows Community Toolkit sample app](https://apps.microsoft.com/store/detail/windows-community-toolkit-sample-app/9NBLGGH4TLCQ) - a demo app for [Windows Community Toolkit](https://github.com/CommunityToolkit/WindowsCommunityToolkit)

- [Windows Composition Samples](https://apps.microsoft.com/store/detail/windows-composition-samples/9N1H8CZHBPXB) - a collection of samples demonstrating how to build WinUI applications with advanced effects and animations, according to Fluent Design [on github](https://github.com/microsoft/WindowsCompositionSamples)

- [**ResourceDictionary with Code-behind** Visual Studio Item Template](https://marketplace.visualstudio.com/items?itemName=FonsSonnemans.RDwCB) - see [ResourceDictionary](#14-resourcedictionary) section

## 1.2. Common errors

- In case of this compilations error:_`Microsoft.ui.xaml.dll is unable to load_ compilation` - edit project file, and add: `<WindowsAppSDKSelfContained>true</WindowsAppSDKSelfContained>`
  
- `XamlCompiler error WMC0615: Type 'TemplateBinding' used after '{' must be a Markup Extension. Error code 0x80004005.` - this is a parser error - template binding can only be used in **ControlTemplates**

## 1.3. x:Bind

### 1.3.1. Main differences between **Bind** and **x:Bind**

- **x:Mode** is by default **OneTime**! (An exception: [x:DefaultBindMode](https://learn.microsoft.com/en-us/windows/uwp/xaml-platform/x-defaultbindmode-attribute))
  
- Child controls don't inherit **DataContext** property, you need to set **DataContext** explicitly in Xaml if you want to use it.
  
- **x:Binding** default source is not the **DataContext** but the code-behind

- Binding is not supported in styles. There is a [workaround](https://stackoverflow.com/questions/33573929/uwp-binding-in-style-setter-not-working), but it's not a generic solution.

- Binding in **ResourceDictionary** - see: [ResourceDictionary section](#14-resourcedictionary)

- **RelativeSource Mode="FindAncestor"** is not supported, a workaround is [on this blog post](https://blog.magnusmontin.net/2022/01/20/bind-to-a-parent-element-in-winui-3/)

- Check [UWP vs WPF](https://github.com/robloo/PublicDocs/blob/master/UWPvsWPF.md#markup-extensions--directives) document. It seems still relevant for WinUI.

### 1.3.2. **x:Bind** in depth

- [Dictionary item](https://learn.microsoft.com/en-us/windows/uwp/xaml-platform/x-bind-markup-extension#collections) binding is possible. Example: `<TextBlock Text="{x:Bind Players['John Smith']}" />.`
This works for types that implement `IDictionary<string, T>` only.

- [Attached Properies](https://learn.microsoft.com/en-us/windows/uwp/xaml-platform/x-bind-markup-extension#attached-properties). Example: `<TextBlock Text="{x:Bind Button22.(Grid.Row)}"/>`

- [Pathless casting](https://learn.microsoft.com/en-us/windows/uwp/xaml-platform/x-bind-markup-extension#pathless-casting) - `{x:Bind MethodName((namespace:TypeOfThis))}` is a valid way to perform what is conceptually equivalent to `{x:Bind MethodName(this)}`.

- [Casting](https://learn.microsoft.com/en-us/windows/uwp/xaml-platform/x-bind-markup-extension#casting). In short: C# style casting is needed (because **x:Bind** is strongly typed)  and possible, in C# manner. [Example, see: **casting** tab](<https://github.com/MaciekSwiszczowski/WinUI-for-WPF-devs/blob/main/src/Examples/Binding/MainWindow.xaml#:~:text=%3CComboBox%20x%3AName,SelectedItem)%2C%20Mode%3DOneWay%7D%22%20/%3E>)

- [Event binding](https://learn.microsoft.com/en-us/windows/uwp/xaml-platform/x-bind-markup-extension#event-binding) - commands are not needed to handle events in view model! [Example](https://github.com/MaciekSwiszczowski/WinUI-for-WPF-devs/blob/main/src/Examples/Binding/MainWindow.xaml#:~:text=counter%3A%22%20/%3E-,%3CButton%20Click%3D%22%7Bx%3ABind%20TestViewModel.IncreaseCountVersion1%7D%22,%3D%22%7Bx%3ABind%20TestViewModel.ClickCount%2C%20Mode%3DOneWay%7D%22%20/%3E,-%3C/StackPanel%3E)

- [Function binding](https://learn.microsoft.com/en-us/windows/uwp/data-binding/function-bindings) - functions can have multiple arguments, so they may not only replace converters, but also multibinding. [Supported function argument types.](https://learn.microsoft.com/en-us/windows/uwp/data-binding/function-bindings#function-arguments)

- Extension methods are supported in **function binding** too! [Example.](https://github.com/MaciekSwiszczowski/WinUI-for-WPF-devs/blob/main/src/Examples/Binding/MainWindow.xaml#:~:text=%3CTextBlock%20Text%3D%22%7Bx%3ABind%20local%3AExtensionMethods,Debug(ComboBox.SelectedItem)%2C%20Mode%3DOneWay%7D%22%20/%3E)

- [BindBack](https://learn.microsoft.com/en-us/windows/uwp/data-binding/function-bindings#two-way-function-bindings) - Specifies a function to use for the reverse direction of a two-way binding. [Example - BindBack tab]([/src/Examples/Binding/MainWindow.xaml](https://github.com/MaciekSwiszczowski/WinUI-for-WPF-devs/blob/main/src/Examples/Binding/MainWindow.xaml#:~:text=%3CTextBox,PropertyChanged%7D%22%20/%3E))

- Template binding - the [TemplateBinding](https://learn.microsoft.com/en-us/windows/uwp/xaml-platform/templatebinding-markup-extension#remarks) markup extension has many limitations. It doesn't allow for converters or binding mode, and it doesn't provide any error feedback in case of a mis-typed target property name. This means that the view will simply fail to load. Use `Binding RelativeSource={RelativeSource Mode=TemplatedParent}, Path=<property>` instead. [Example - **Template binding** tab](https://github.com/MaciekSwiszczowski/WinUI-for-WPF-devs/blob/main/src/Examples/Binding/MainWindow.xaml#:~:text=%3C!%2D%2D%20%20Attempt%20to,%3C/TextBox%3E).

### 1.3.4. Example application

A simple app presenting a few usages of **x:Bind** which are not obvious for WPF devs [is in src folder of this repo](/src/Examples/)

## 1.4. ResourceDictionary

To have **x:Bind** working in a **ResourceDictionary** it is [required to have code-behind](https://learn.microsoft.com/en-us/windows/uwp/data-binding/data-binding-in-depth#resource-dictionaries-with-xbind).
Here is a useful [**ResourceDictionary with Code-behind**](https://marketplace.visualstudio.com/items?itemName=FonsSonnemans.RDwCB) Visual Studio extension.

## 1.5. Visibility and **x:Load**

In contrast to WPF, setting the **Visibility** property of a UI element to **Collapsed** does not unload it; it simply prevents it from being displayed, making it equivalent to **Visibility.Hidden** in WPF. This can result in performance problems, especially when using **Collapsed** in item templates for lists, etc. To effectively unload a portion of the visual tree, use the [**x:Load**](https://learn.microsoft.com/en-us/windows/uwp/xaml-platform/x-load-attribute) attribute. Note that **x:DeferLoadStrategy**, which does a similar job, is now obsolete and should not be used.

## 1.6. Styles

Binding is style setters is not supported (except for **TemplateBinding**), as well as **BasedOn** with default styles.

Binding in style [workaround](https://stackoverflow.com/a/33582406/275330) - it requires to write some code for every dependency property you'd like to use, so it's not a generic solution.
