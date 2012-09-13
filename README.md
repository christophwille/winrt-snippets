Enumerate Fonts in WinRT
======================
This [forum post](http://social.msdn.microsoft.com/Forums/en-GB/winappswithcsharp/thread/720419d2-9254-4b89-a32f-08345d5260c6) hints that 
enumerating / listing fonts in C#/XAML WinRT apps cannot be done without DirectWrite. Having done dyamic image generation using SharpDX already,
I searched the samples for font enumeration - and I found one that I converted into a Windows 8 app (I tested it with WACK).

This sample can be used as the basis for a font picker / chooser inside eg a flyout.

Screenshot

![In Action](https://raw.github.com/christophwille/winrt-snippets/master/listfonts.png)

Render Text to Bitmap
===========================

WinRT's WriteableBitmap class isn't much of a help when you want to render a text into a bitmap in a Windows 8 App. 
Thus this sample uses SharpDX to create a bitmap and render it to a MemoryStream, which is then bound to an 
XAML Image control (using a neat random access memory stream as the basis for the BitmapImage).

The solution is based on ASP.NET code from a StackOverflow question (see link in the codebase). It tries to deviate as 
little from the original as possible.

"Add New Item" item in GridView / ListView
=========================
![In Action](https://raw.github.com/christophwille/winrt-snippets/master/gridviewaddnewitem.png)

This sample shows a WinRT app that uses the standard items template (GridView, with ListView for snapped view).
It adds a "+" content item to the ItemsSource, which is used for the "Add New Item" action in the app.

Both the GridView and the ListView have been adapted via a DataTemplateSelector to look for the special item that is prepended to the list of actual items.

The code of interest is located in MainPage.xaml (Resources section and the controls) and MainPage.xaml.cs. AddNewItemTemplateSelector.cs hosts
the datatemplate selector, AddNewItemItem.cs is used in the GridView more or less like a marker, in the ListView however we do
show some info to help the user understand the item (adjust to your liking).