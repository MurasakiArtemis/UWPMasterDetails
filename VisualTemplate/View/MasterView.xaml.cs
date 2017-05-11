using VisualTemplate.Model;
using VisualTemplate.ViewModel;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace VisualTemplate.View
{
    public sealed partial class MasterView : Page, INavigableFrame, IContentList
    {
        public IEnumerable<string> ContentList { get { return MasterVM.DataList; } }
        public Frame NavigableFrame { get { return VisualStates.CurrentState == StackedLayout ? Frame : DetailsFrame; } }
        public MasterView()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadInformation();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }
        private async void LoadInformation()
        {
            await MasterVM.GetElementList();
            Bindings.Update();
        }
        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack && !e.Handled)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var element = e.ClickedItem as MasterModel;
            NavigateDefault(element.Name);
        }
        public void NavigateDefault(object parameter)
        {
            NavigableFrame.Navigate(typeof(DetailView), parameter);
        }
    }
}
