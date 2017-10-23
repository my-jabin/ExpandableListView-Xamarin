using ExpandableListView.Model;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpandableListView.ViewModel
{
    public class ExamplePageViewModel
    {
        public ObservableRangeCollection<ContinentViewModel> List { get; private set; }
            = new ObservableRangeCollection<ContinentViewModel>();
      
        public ICommand LoadDataCommand { get; private set; }
        public ICommand HeaderClickCommand { get; private set; }                 

        public ExamplePageViewModel()
        {
            this.LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());
            this.HeaderClickCommand = new Command<ContinentViewModel>((item) => ExecuteHeaderClickCommand(item));
        }

        private async Task ExecuteLoadDataCommand()
        {
            Country China = new Country("China", "CHN", "china");
            Country Japan = new Country("Japn", "JPN", "japan");
            Country Korea = new Country("Korea", "KOR", "korea");

            var Germany = new Country("Germany", "GEU", "germany");
            var France = new Country("France", "FRA", "france");
            var UK = new Country("United Kingdom", "GBR", "england");
            var Italia = new Country("Italia", "ITA", "italia");

            var Arg = new Country("Argentina", "ARG", "argentina");
            var Brazil = new Country("Brazil", "BRA", "brazil");
            var Col = new Country("Colombia", "COL", "colombia");

            var Egypt = new Country("Egypt", "EGY", "egypt");
            var Morocco = new Country("Morocco", "MAR", "morocco");

            Continent Asia = new Continent() { Name = "Asia" };
            Asia.Countries.Add(China);
            Asia.Countries.Add(Japan);
            Asia.Countries.Add(Korea);

            Continent Europa = new Continent() { Name = "Europa" };
            Europa.Countries.Add(Germany);
            Europa.Countries.Add(France);
            Europa.Countries.Add(UK);
            Europa.Countries.Add(Italia);

            Continent SA = new Continent() { Name = "South American" };
            SA.Countries.Add(Arg);
            SA.Countries.Add(Brazil);
            SA.Countries.Add(Col);

            Continent Africa = new Continent() { Name = "Africa" };
            Africa.Countries.Add(Morocco);
            Africa.Countries.Add(Egypt);

            var AsiaViewModel = new ContinentViewModel(Asia);
            var EuropaViewModel = new ContinentViewModel(Europa);
            var SAViewModel = new ContinentViewModel(SA);
            var AfricaViewModel = new ContinentViewModel(Africa);

            List.Add(AsiaViewModel);
            List.Add(EuropaViewModel);
            List.Add(SAViewModel);
            List.Add(AfricaViewModel);

        }

        private void ExecuteHeaderClickCommand(ContinentViewModel item) {
            item.Expanded = !item.Expanded;
        }
    }
}
