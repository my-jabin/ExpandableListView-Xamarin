using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandableListView
{
    public class Continent: ObservableCollection<Country>, INotifyPropertyChanged
    {
        private bool _expanded;
        public bool Expanded {
            get { return _expanded; }
            set {
                if (_expanded != value) {
                    _expanded = value;
                    OnPropertyChanged("Expanded");
                    OnPropertyChanged("StateIcon");
                }  
            }
        }

        public string Name { get; set; }
        public string ShortName { get; set; }
        
        public string StateIcon {
            get { return Expanded ? "up" : "down"; }
        }

        public Continent(string name, string shortName, bool expanded = true)
        {
            Name = name;
            ShortName = shortName;
            Expanded = expanded;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
