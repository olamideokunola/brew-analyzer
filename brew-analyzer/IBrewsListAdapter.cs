using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace brew_analyzer
{
    interface IBrewsListAdapter
    {
        void UpdateList(IList<string> newList);
    }
}
