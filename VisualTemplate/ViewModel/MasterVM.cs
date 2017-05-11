using VisualTemplate.Model;
using VisualTemplate.Model.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace VisualTemplate.ViewModel
{
    public class MasterVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        void RaiseProperty([CallerMemberName]string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<string> ElementCompleteListData;

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

        private IncrementalLoadingCollection<MasterModel> _MasterList;
        public IncrementalLoadingCollection<MasterModel> MasterList
        {
            get { return _MasterList; }
            set
            {
                if (value != _MasterList)
                {
                    _MasterList = value;
                    RaiseProperty();
                }
            }
        }
        public IEnumerable<string> DataList { get { return ElementCompleteListData != null? ElementCompleteListData.Select(p => p) : null; } }

        public MasterVM()
        { }
        
        public async Task GetElementList()
        {
            IsBusy = true;
            try
            {
                ElementCompleteListData = new ObservableCollection<string>();
                var numberOfElements = ElementCompleteListData.Count();
                MasterList = new IncrementalLoadingCollection<MasterModel>(numberOfElements, GetElementsList, LoadIndividualItem);
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
        private IEnumerable<MasterModel> GetElementsList(long start, long end)
        {
            Func<string, MasterModel> selectionFunction = p =>
            {
                var splitedString = p.Split(' ');
                var name = splitedString.ElementAt(0);
                return new MasterModel() { Name = name, URL = name };
            };
            Func<string, int, bool> whereFunction = (p, i) =>
            {
                var splitedString = p.Split(' ');
                int index = int.Parse(splitedString.ElementAt(0));
                bool result = i >= start;
                result &= i < end;
                return result;
            };
            var pokemonDataEnumerable = ElementCompleteListData.Where(whereFunction).Select(selectionFunction);
            return pokemonDataEnumerable;
        }
        private async Task<MasterModel> LoadIndividualItem(MasterModel element)
        {
            return element;
        }
    }
}
