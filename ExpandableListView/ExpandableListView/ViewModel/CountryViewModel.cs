using ExpandableListView.Model;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandableListView.ViewModel
{
    public class CountryViewModel
    {
        private Country _country;

        public CountryViewModel(Country country)
        {
            this._country = country;
        }

        public string Name { get { return _country.Name; } }
        public string Code { get { return _country.Code; } }
        public string Flag { get { return _country.Flag; } }
    }
}
