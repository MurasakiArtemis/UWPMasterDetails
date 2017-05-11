using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using VisualTemplate.Model.Wrappers;

namespace VisualTemplate.Model
{
    public class DetailModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate
        {
        };
        void RaiseProperty([CallerMemberName]string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public DetailModel()
        { }

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
