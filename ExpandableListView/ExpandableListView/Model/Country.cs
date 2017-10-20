using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandableListView.Model
{
    public class Country
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Flag { get; set; }


        public Country(string name, string code, string flag)
        {
            this.Name = name;
            this.Code = code;
            this.Flag = flag;
        }
    }
}
