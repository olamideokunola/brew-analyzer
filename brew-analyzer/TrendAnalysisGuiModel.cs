using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObserverSubject;

namespace brew_analyzer
{
    public class TrendAnalysisGuiModel : Subject
    {
        IList<string> brewsList = new List<string>();
        string selectedBrew = "";
        int numberOfBrews;

        DateTime startDate;
        DateTime endDate;
        string fileDestination;


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

        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public string FileDestination { get => fileDestination; set => fileDestination = value; }

        public int NumberOfBrews
        {
            get
            {
                return brewsList.Count;
            }
        }
    }
}
