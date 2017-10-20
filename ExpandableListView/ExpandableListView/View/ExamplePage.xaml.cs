using ExpandableListView.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpandableListView.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExamplePage : ContentPage
    {
        private ExamplePageViewModel ViewModel {
            get { return (ExamplePageViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        public ExamplePage(ExamplePageViewModel viewModel)
        {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.LoadDataCommand.Execute(null);
        }

        private void StateImage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Source")) {
                var image = sender as Image;
                image.Opacity = 0;
                image.FadeTo(1, 1000);
            }      
        }
    }
}