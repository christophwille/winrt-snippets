using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DeploymentSample.Data;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DeploymentSample.WP8.Resources;

namespace DeploymentSample.WP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var ds = new SampleDataSource();
            var speakers = await ds.GetSpeakerAsync();

            SpeakerListBox.ItemsSource = speakers;
        }
    }
}