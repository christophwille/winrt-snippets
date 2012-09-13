using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.IO;
using SharpDX.WIC;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Bitmap = SharpDX.WIC.Bitmap;
using D2DPixelFormat = SharpDX.Direct2D1.PixelFormat;
using WicPixelFormat = SharpDX.WIC.PixelFormat;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RenderTextToBitmap
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        // 
        // http://stackoverflow.com/questions/9151615/how-does-one-use-a-memory-stream-instead-of-files-when-rendering-direct2d-images
        // 
        // Identical to above SO question, except that we are rendering to MemoryStream because it was added to the API
        //
        private MemoryStream RenderStaticTextToBitmap()
        {
            var width = 400;
            var height = 100;
            var pixelFormat = WicPixelFormat.Format32bppBGR;

            var wicFactory = new ImagingFactory();
            var dddFactory = new SharpDX.Direct2D1.Factory();
            var dwFactory = new SharpDX.DirectWrite.Factory();

            var wicBitmap = new Bitmap(
                wicFactory,
                width,
                height,
                pixelFormat,
                BitmapCreateCacheOption.CacheOnLoad);

            var renderTargetProperties = new RenderTargetProperties(
                RenderTargetType.Default,
                new D2DPixelFormat(Format.Unknown, AlphaMode.Unknown),
                0,
                0,
                RenderTargetUsage.None,
                FeatureLevel.Level_DEFAULT);
            var renderTarget = new WicRenderTarget(
                dddFactory,
                wicBitmap,
                renderTargetProperties)
            {
                TextAntialiasMode = TextAntialiasMode.Cleartype
            };

            renderTarget.BeginDraw();

            var textFormat = new TextFormat(dwFactory, "Consolas", 48)
            {
                TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
                ParagraphAlignment = ParagraphAlignment.Center
            };
            var textBrush = new SharpDX.Direct2D1.SolidColorBrush(
                renderTarget,
                SharpDX.Colors.Blue);

            renderTarget.Clear(Colors.White);
            renderTarget.DrawText(
                "Hi, mom!",
                textFormat,
                new RectangleF(0, 0, width, height),
                textBrush);

            renderTarget.EndDraw();

            var ms = new MemoryStream();

            var stream = new WICStream(
                wicFactory,
                ms);

            var encoder = new PngBitmapEncoder(wicFactory);
            encoder.Initialize(stream);

            var frameEncoder = new BitmapFrameEncode(encoder);
            frameEncoder.Initialize();
            frameEncoder.SetSize(width, height);
            frameEncoder.PixelFormat = WicPixelFormat.FormatDontCare;
            frameEncoder.WriteSource(wicBitmap);
            frameEncoder.Commit();

            encoder.Commit();

            frameEncoder.Dispose();
            encoder.Dispose();
            stream.Dispose();

            ms.Position = 0;
            return ms;
        }

        private async void Render_OnClick(object sender, RoutedEventArgs e)
        {
            var ms = RenderStaticTextToBitmap();
            var msrandom = new MemoryRandomAccessStream(ms);

            BitmapImage image = new BitmapImage();
            await image.SetSourceAsync(msrandom);

            RenderedImage.Source = image;
        }
    }
}
