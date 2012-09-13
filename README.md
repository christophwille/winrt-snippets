Enumerate Fonts in WinRT
======================
This [forum post](http://social.msdn.microsoft.com/Forums/en-GB/winappswithcsharp/thread/720419d2-9254-4b89-a32f-08345d5260c6) hints that 
enumerating / listing fonts in C#/XAML WinRT apps cannot be done without DirectWrite. Having done dyamic image generation using SharpDX already,
I searched the samples for font enumeration - and I found one that I converted into a Windows 8 app (I tested it with WACK).

This sample can be used as the basis for a font picker / chooser inside eg a flyout.

Screenshot

![In Action](https://raw.github.com/christophwille/winrt-snippets/master/listfonts.png)