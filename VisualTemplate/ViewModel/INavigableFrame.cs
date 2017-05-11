using Windows.UI.Xaml.Controls;

namespace VisualTemplate.ViewModel
{
    interface INavigableFrame
    {
        Frame NavigableFrame { get; }
        void NavigateDefault(object parameter);
    }
}
