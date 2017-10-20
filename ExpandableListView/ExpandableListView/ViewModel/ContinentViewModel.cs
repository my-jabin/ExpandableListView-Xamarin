using ExpandableListView.Model;
using Java.Lang;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandableListView.ViewModel
{
    public class ContinentViewModel : ObservableRangeCollection<CountryViewModel>, INotifyPropertyChanged  
    {
        private Continent _continent;

        // It's a backup variable for storing CountryViewModel objects
        private ObservableRangeCollection<CountryViewModel> Countries 
            = new ObservableRangeCollection<CountryViewModel>();
 
        public ContinentViewModel(Continent continent, bool expanded = true)
        {
            this._continent = continent;
            this._expanded = expanded;
            // Continent has many countries. Once we get it, init CountryViewModel and store it in a backup variable
            foreach (Country c in continent.Countries) {
                Countries.Add(new CountryViewModel(c));
            }
            // ContinentViewModel add a range with CountryViewModel
            this.AddRange(Countries);
        }

        public string Name { get { return _continent.Name; } }

        private bool _expanded;
        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Expanded"));
                    OnPropertyChanged(new PropertyChangedEventArgs("StateIcon"));
                    if (_expanded)
                    {
                        this.AddRange(Countries);
                    }
                    else {
                        this.Clear();
                    }
                }
            }
        }

        public string StateIcon
        {
            get { return Expanded ? "up" : "down"; }
        }
    }
}
