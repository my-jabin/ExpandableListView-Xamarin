using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandableListView.Model
{
    public class Continent  
    {
        public string Name { get; set; }
        public List<Country> Countries { get; set; } = new List<Country>();
    }
}
