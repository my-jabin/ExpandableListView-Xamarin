# Xamarin.Forms Expandable ListView

This demo explain how to create a expandable ListView with MVVM pattern using Xamarin.Forms. The ideal is from [this blog](http://www.compliancestudio.io/blog/xamarin-forms-expandable-listview). If you prefer first to know how to create a expandable ListView without MVVM pattern, please read the blog, and then go back to read this one.
The following gif shows the result of this demo.

## Model
An easy example, a continent include many countries, so here we create two models, `Continent` and `Country`. `Continent` has a `Name` and a list with several countries. `Country` has three property, `Name`,`Code` and `Flag`.
``` Csharp
public class Continent  
{
    public string Name { get; set; }
    public List<Country> Countries { get; set; } = new List<Country>();
}


public class Country
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Flag { get; set; }
}
```
## View
The view is the page, layout, UI controls of what a user sees on the screen. In this example is the `ExamplePage`.

## ViewModel
For exposing public properties of a model and some other properties that are useful for presenting , we need a `ViewModel` for each `Model`. `Country` model corresponds to a `CountryViewModel` and `Continent` corresponds to a `ContinentViewModel`.
``` Csharp
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
```
`ContinentViewModel` has a `Expanded` property which keeps track on whether the continent is expanded or collapsed. `StateIcon` will be an expaned icon when the list is expanded and collapsed when it's not.

`ContinentViewModel` implements `INotifyPropertyChanged` interfacce so that any changes on properties will be reflected in our list view. For example, when property `Expanded` changes, the `StateIcon` should be changed too, so we need to notify our `ListView` that the both properties are changed by calling `OnPropertyChanged` methods.
``` Csharp
OnPropertyChanged(new PropertyChangedEventArgs("Expanded"));
OnPropertyChanged(new PropertyChangedEventArgs("StateIcon"));
```

Notice that `ContinentViewModel` must be subclass of `ObservableCollection` with generic type of `CountryViewModel`. Items of the `ObservableCollection` will be shown on the `ListView`. Here I use a light-weight MVVM helper classes named [MVVMHelpers](https://github.com/jamesmontemagno/mvvm-helpers) of James, it has a awesome `ObservableRangeCollection` which is a subclass of `ObservableCollection` and can add, remove or replace a range of object. More detail on this [video](https://channel9.msdn.com/Shows/XamarinShow/The-Xamarin-Show-12-MVVM-Helpers).

Constuctor of `ContinentViewModel` takes a `Continent` object which contains a country list and set `Expanded` by default to be true. In the constuctor method, a backup variable for storing `CountryViewModel` obejcts is populated with the countries of `Continent`. This backup variable is added to the `ContinentViewModel` if `Expanded` property is `true`.
``` Csharp
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
        if(expanded)
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
```

Beside the above two ViewModels, another ViewModel is also required---`ExamplePageViewModel` corresponds to `ExamplePage`. Most of logic to `ExamplePage` should be handled in `ExamplePageViewModel` but not in `ExamplePage.xaml.cs`. `ExamplePage` sets the `BindingContext` to the `ExamplePageViewModel`.
Once the `ExamplePage` is appearing, it calls `OnAppearing` methods, this is the apporiate place to load data from server. This job belongs to our `ExamplePageViewModel`, and it simply execute its command to load data. There is another method is used for handling property changed event. If the `StateIcon` has changed and new icon is inflated, it performs fade animation. Now nothing more code should be here, and logic handling should be in `ExamplePageViewModel`.

``` Csharp
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
```

`ExamplePageViewModel` has a `List` of type `ObservableRangeCollection` and two commands. `List` is the data source for `ListView`. `LoadDataCommand` is for loading data from server and `HeaderClickCommand` is for `Continent` click event handling.
``` Csharp
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
    	...
    	...
    }

    private void ExecuteHeaderClickCommand(ContinentViewModel item) {
        item.Expanded = !item.Expanded;
    }
}
```

## Data Binding
`ExamplePage` contains only a `ListView` displayed with customized group header and item. Group header template is a view of `RelativeLayout`, it shows a `Lable` and an `Image`. The `TapGestureRecognizer` of the `RelativeLayout` allows to click the header and expand or collapse the `ListView`. Once user click the group header, `HeaderClickCommand` will be executed and the `Expanded` property of `ContinentViewModel` will be updated. Since we have written the `Set` method of property `Expanded`, it changes the content of `ContinentViewModel`---add a range or clear itself.

The bindable property `ItemSource` of `ListView` is bound to `List` which is a public property of `ExamplePageViewModel`.
The `GroupHeaderTemplate` of `ListView` is bound to each item of `List` object, which is a `ContinentViewModel` object.
The `ItemTemplate` of `ListView` is bound to each item of the `ContinentViewModel` object, which is a `CountryViewModel` object.

----------------------------
Source Code is [here](https://github.com/my-jabin/ExpandableListView-Xamarin)


package used in this example:
- [MvvmHelpers](https://github.com/jamesmontemagno/mvvm-helpers) from James
- [CircleImage](https://github.com/jamesmontemagno/ImageCirclePlugin) from James

Reference : http://www.compliancestudio.io/blog/xamarin-forms-expandable-listview
