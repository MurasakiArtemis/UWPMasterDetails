using VisualTemplate.Model.Wrappers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media.Imaging;

namespace VisualTemplate.Model
{
    public class MasterModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        void RaiseProperty([CallerMemberName]string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    RaiseProperty();
                }
            }
        }

        private string url;
        public string URL
        {
            get { return url; }
            set
            {
                if (value != url)
                {
                    url = value;
                    RaiseProperty();
                }
            }
        }

    }
}
