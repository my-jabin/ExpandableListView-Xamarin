# Xamarin.Forms Expandable ListView

This demo explain how to create a expandable ListView with MVVM pattern using Xamarin.Forms. The ideal is from [here](http://www.compliancestudio.io/blog/xamarin-forms-expandable-listview). If you prepare to know some how to create a expandable ListView without MVVM pattern, please read the blog, and then go back to read this one. 
The following gif shows the result of this demo.

## Model
An easy example, a continent include many countries, so here we create two models, `Continent` and `Country`. `Continent` has a `Name` and a list stores countries. `Country` has three property, `Name`,`Code` and `Flag`.
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






> ContinentViewModel must be derived from a List or ObservableCollection class. Items of the List or ObservableCollection will be shown on the ListView


package:
MvvmHelpers from James




> ContinentViewModel must be derived from a List or ObservableCollection class. Items of the List or ObservableCollection will be shown on the ListView

Clone an object. https://forums.xamarin.com/discussion/55022/how-to-clone-a-list

package used:
MvvmHelpers

A continent has many countries, So it has an Collection object for storing its countries;




Reference : http://www.compliancestudio.io/blog/xamarin-forms-expandable-listview