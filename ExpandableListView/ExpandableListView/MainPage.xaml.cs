using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpandableListView
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Continent> _world;
        private ObservableCollection<Continent> _expandedContinent;

        public MainPage()
        {
            InitializeComponent();
            _expandedContinent = new ObservableCollection<Continent>();
            InitDate();  
            UpdateList();
        }

        public void UpdateList() {
            _expandedContinent.Clear();
            foreach (Continent c in _world) {
                Continent newContinent = new Continent(c.Name, c.ShortName, c.Expanded);
                if (newContinent.Expanded) {
                    foreach (Country country in c) {
                        newContinent.Add(country);
                    }
                }
               _expandedContinent.Add(newContinent);
            }
            ContinentListView.ItemsSource = _expandedContinent;
        }

        private void GroupHeader_Clicked(object sender, EventArgs e)
        {
            //var layout = (RelativeLayout)sender;
            //Image stateImage = layout.FindByName<Image>("StateImage");
            //stateImage.PropertyChanged += StateImage_PropertyChanged;

            var tap = ((RelativeLayout)sender).GestureRecognizers.First() as TapGestureRecognizer;
            var index = _expandedContinent.IndexOf(tap.CommandParameter as Continent);
            _world[index].Expanded = !_world[index].Expanded;
            UpdateList();
        }

        private void InitDate(){
            _world = new ObservableCollection<Continent> {
                new Continent("Asia","Asia shortName"){
                    new Country{ Name = "China", ShortName="CHN", Icon="china"},
                    new Country{ Name = "Japn", ShortName="JPN", Icon="japan"},
                    new Country{ Name = "Korea", ShortName="KOR", Icon="korea"}
                }  ,
                new Continent("Europa","Europa shortName"){
                    new Country{ Name = "Germany", ShortName="GEU", Icon="germany"},
                    new Country{ Name = "France", ShortName="FRA", Icon="france"},
                    new Country{ Name = "United Kingdom", ShortName="GBR", Icon="england"}
                },
                new Continent("South American","SA"){
                    new Country{ Name = "Argentina", ShortName="ARG", Icon="argentina"},
                    new Country{ Name = "Brazil", ShortName="BRA", Icon="brazil"},
                    new Country{ Name = "Colombia", ShortName="COL", Icon="colombia"}
                }
            };
        }

      
    }
}
