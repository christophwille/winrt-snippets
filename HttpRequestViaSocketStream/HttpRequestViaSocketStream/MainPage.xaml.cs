using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HttpRequestViaSocketStream
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void DoTheDemo_Click(object sender, RoutedEventArgs e)
        {
            string response = await GetUrlViaSocketStream(new Uri("http://www.orf.at/"));
            Output.Text = response;
        }

        private async Task<string> GetUrlViaSocketStream(Uri url)
        {
            try
            {
                using (var socket = new StreamSocket())
                {
                    var host = new HostName(url.Host);

                    await socket.ConnectAsync(host, "80").AsTask().ConfigureAwait(false);

                    const string request = "GET {0} HTTP/1.1\r\n" +
                                           "Host: {1}\r\n" +
                                           "\r\n";

                    var outStream = socket.OutputStream.AsStreamForWrite();
                    using (var sw = new StreamWriter(outStream))
                    {
                        var requestToSend = String.Format(request, url.AbsoluteUri, url.Host);
                        await sw.WriteAsync(requestToSend).ConfigureAwait(false);
                        await sw.FlushAsync().ConfigureAwait(false);
                    }

                    var inStream = socket.InputStream.AsStreamForRead();

                    using (var reader = new StreamReader(inStream))
                    {
                        var response = await reader.ReadToEndAsync().ConfigureAwait(false);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return null;
        }
    }
}
