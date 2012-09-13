using GridViewAddNewItemSelector.Data;
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
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace GridViewAddNewItemSelector
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class MainPage : GridViewAddNewItemSelector.Common.LayoutAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // This is a sample hack - I didn't want to modify too many files in the standard project
            // In real life, you would have a base collection / vector doing just that
            var allitems = new List<object>();
            allitems.Add(new AddNewItemItem()
                             {
                                 InstructionTitle = "Add",
                                 InstructionSubtitle = "Please have your ID ready"
                             });
            allitems.AddRange(SampleDataSource.GetGroup("Group-1").Items);

            this.DefaultViewModel["Items"] = allitems;
        }

        private void Common_OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is AddNewItemItem)
            {
                // Add your add new item logic
            }
            else
            {
                // Perform single item click logic
            }
        }

        //
        // This code work for Single and Multiple; Extended has not been tested!
        //
        private void Common_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var gridView = (ListViewBase)sender;

            if (gridView.SelectionMode == ListViewSelectionMode.Multiple)
            {
                var markerItemSelected = gridView.SelectedItems.OfType<AddNewItemItem>().FirstOrDefault();
                if (null != markerItemSelected)
                {
                    gridView.SelectedItems.Remove(markerItemSelected);
                }
            }
            else if (gridView.SelectionMode == ListViewSelectionMode.Single)
            {
                if (gridView.SelectedItem is AddNewItemItem)
                    gridView.SelectedItem = null;
            }
        }
    }
}
