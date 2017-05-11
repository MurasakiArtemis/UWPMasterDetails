using VisualTemplate.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;
using VisualTemplate.View.Converters;
using System;
using VisualTemplate.Model.Wrappers;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VisualTemplate.ViewModel
{
    public class DetailVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        void RaiseProperty([CallerMemberName]string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            private set
            {
                if (value != _isBusy)
                {
                    _isBusy = value;
                    RaiseProperty();
                }
            }
        }

        private bool _isError;
        public bool IsError
        {
            get { return _isError; }
            set
            {
                if (value != _isError)
                {
                    _isError = value;
                    RaiseProperty();
                }
            }
        }

        private DetailModel _currentElement;
        public DetailModel CurrentElement
        {
            get { return _currentElement; }
            set
            {
                if (value != _currentElement)
                {
                    _currentElement = value;
                    RaiseProperty();
                }
            }
        }
        public DetailVM()
        { }
        public async Task GetElement(string element)
        {
            IsBusy = true;
            try
            {
            }
            catch
            {
                IsError = true;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
