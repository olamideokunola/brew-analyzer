using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObserverSubject;

namespace brew_analyzer
{
    class TrendAnalysisGuiModel : Subject
    {
        IList<string> brewsList = new List<string>();
        string selectedBrew = "";

        // Constructor
        public TrendAnalysisGuiModel()
        {

        }

        // Getters and Setters
        public IList<string> BrewsList
        {
            get
            {
                return brewsList;
            }

            set
            { 
                brewsList = value;
                Notify();
            }
        }

        public string SelectedBrew
        {
            get
            {
                return selectedBrew;
            }

            set
            {
                selectedBrew = value;
                Notify();
            }
        }
    }
}
