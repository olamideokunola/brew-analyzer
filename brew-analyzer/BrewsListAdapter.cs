using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace brew_analyzer
{
    class BrewsListAdapter : IBrewsListAdapter
    {

        private ListBox brewsListBox;
        private IList<string> brewsList = new List<string>();

        // Constructor
        public BrewsListAdapter(ListBox brewsListBox, IList<string> guiBrewsList)
        {
            this.brewsListBox = brewsListBox;
            this.brewsList = guiBrewsList;

            UpdateList(brewsList);
        }

        // IBrewsListAdapter interface implementation
        public void UpdateList(IList<string> newList)
        {
            brewsListBox.Items.Clear();

            foreach(string brewStr in newList)
            {
                brewsListBox.Items.Add(brewStr);
            }
        }
    }
}
