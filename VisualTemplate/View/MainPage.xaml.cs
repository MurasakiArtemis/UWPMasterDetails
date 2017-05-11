using VisualTemplate.Model;
using VisualTemplate.Model.Wrappers;
using VisualTemplate.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace VisualTemplate.View
{
    public sealed partial class MainPage : Page
    {
        private List<PaneIconDescription> PaneObjectsList;
        private IEnumerable<string> dataList;
        public MainPage()
        {
            this.InitializeComponent();
            PaneObjectsList = new List<PaneIconDescription>();
            PaneObjectsList.Add(new PaneIconDescription() { Type = ResourceType.Tipo1, Description = "Tipo1", Icon = new BitmapImage(new Uri("ms-appx:///Assets/Square44x44Logo.scale-100.png")) });
            PaneObjectsList.Add(new PaneIconDescription() { Type = ResourceType.Tipo2, Description = "Tipo2", Icon = new BitmapImage(new Uri("ms-appx:///Assets/Square44x44Logo.scale-100.png")) });
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            NavMenu.IsPaneOpen = !NavMenu.IsPaneOpen;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ContentFrame.Navigate(typeof(MasterView));
            Frame rootFrame = Window.Current.Content as Frame;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = rootFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (ContentFrame.CanGoBack && !e.Handled)
            {
                e.Handled = true;
                ContentFrame.GoBack();
            }
        }
        private void NavListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            PaneIconDescription clickedItem = (PaneIconDescription)e.ClickedItem;
            switch (clickedItem.Type)
            {
            case ResourceType.Tipo1:
                ContentFrame.Navigate(typeof(MasterView));
                break;
            case ResourceType.Tipo2:
            case ResourceType.Tipo3:
            case ResourceType.Tipo4:
                //Navigate to another Master
                break;
            default:
                break;
            }
        }
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (dataList == null)
                PopulateQueryList();
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (!string.IsNullOrEmpty(sender.Text))
                {
                    var filteredItems = dataList.Where(p => p.ToLowerInvariant().StartsWith(sender.Text.ToLowerInvariant())).GroupBy(p => p).Select(p => p.First());
                    if (filteredItems.Count() != 0)
                        sender.ItemsSource = filteredItems;
                    else
                        sender.ItemsSource = new string[] { "No results" };
                }
            }
        }
        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (dataList == null)
                PopulateQueryList();
            if (!string.IsNullOrEmpty(args.QueryText))
            {
                var query = args.QueryText.ToLowerInvariant();
                query = char.ToUpper(query[0]) + query.Substring(1);
                if (ContentFrame.Content is INavigableFrame)
                {
                    var frame = ContentFrame.Content as INavigableFrame;
                    if (dataList.Contains(query))
                        frame.NavigateDefault(query);
                }
                else if (ContentFrame.Content is DetailView)
                {
                    if (dataList.Contains(query))
                        ContentFrame.Navigate(typeof(DetailView), query);
                }
            }
        }
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            PopulateQueryList();
        }
        private void PopulateQueryList()
        {
            if (ContentFrame.Content is IContentList)
            {
                var frame = ContentFrame.Content as IContentList;
                dataList = frame.ContentList;
            }
        }
    }
}
