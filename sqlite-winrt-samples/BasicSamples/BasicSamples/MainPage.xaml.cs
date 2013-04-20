using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BasicSamples.Data;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BasicSamples
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
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Arrange
            var ds = new DataSource();
            await ds.CleanDatabaseAsync();
            await ds.InitializeDbAsync();

            // CRUD Operations Demos
            await CrudOperationsDemo(ds);

            // Tx Demos
            var speakers = await TxInsertDemo(ds);
            await TxDeleteDemo(ds, speakers.First().Id);

            // Reset the database
            await ds.CleanDatabaseAsync();
            await ds.InitializeDbAsync();

            // Querying Demos
            await TxInsertDemo(ds); // re-add data to the database
            await QueryingDemos(ds);

            // Some advanced scenarios
            await AdvancedDemos(ds);
        }

        private async Task CrudOperationsDemo(DataSource ds)
        {
            var speaker = new Speaker()
            {
                Name = "Christoph Wille"
            };

            await ds.InsertSpeakerAsync(speaker);   // Id column will be populated

            speaker.Name = "Chris Wille";

            await ds.UpdateSpeakerAsync(speaker);

            //await ds.DeleteSpeakerAsync(speaker);
            //await ds.DeleteSpeakerByIdAsync(speaker.Id);
            await ds.DeleteSpeakerByCommandAsync(speaker.Id);
        }

        private async Task<List<Speaker>> TxInsertDemo(DataSource ds)
        {
            var speakers = new List<Speaker>()
                    {
                        new Speaker()
                        {
                            Name = "Christoph Wille",
                            Sessions = new List<Session>()
                            {
                                new Session()
                                {
                                    Title = "SQLite in Windows 8 und Windows Phone 8"
                                },
                                new Session()
                                {
                                    Title = "Single Sign On (SSO) mit Windows Azure Active Directory"
                                },
                            }
                        },
                        new Speaker()
                        {
                            Name = "Alexander Zeitler",
                            Sessions = new List<Session>()
                            {
                                new Session()
                                {
                                    Title = "Single Page Applications - jenseits von ToDo"
                                },
                                new Session()
                                {
                                    Title = "Hypermedia mit ASP.NET Web API"
                                },
                            }
                        }
                    };

            await ds.InsertSpeakerGraphAsync(speakers);

            return speakers;
        }

        private async Task TxDeleteDemo(DataSource ds, int speakerId)
        {
            try
            {
                await ds.DeleteSpeakerAsync(speakerId, forceTxFail: true);
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
            }

            await ds.DeleteSpeakerAsync(speakerId, forceTxFail: false);
        }

        private async Task AdvancedDemos(DataSource ds)
        {
            // Foreign key support
            await ds.FkInsertAsync();

            // List all tables in database
            var tables = await ds.AdvGetTablesAsync();
        }

        private async Task QueryingDemos(DataSource ds)
        {
            // via ExecuteScalar
            int numOfSessions = await ds.GetSessionCountAsync();

            // via .CountAsync()
            numOfSessions = await ds.GetSessionCount2Async();

            var speaker = await ds.GetSpeakerAsync("Christoph Wille");
            var sessions = await ds.GetSessionsStartingWithSAsync();

            var speakersGraph = await ds.GetSpeakersGraphAsync();

            var speakersessions = await ds.QueryWithJoinAsync();
        }
    }
}
