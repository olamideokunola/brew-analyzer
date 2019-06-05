using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewingModel.Datasources;
using ObserverSubject;
using Util;
using Month = BrewingModel.Datasources.Month;

namespace brew_analyzer
{
    public class TrendAnalysisGuiModel : Subject
    {
        private string reportName;
        private Month month;
        private int year;
        IList<string> brewsList = new List<string>();
        string selectedBrew = "";
        int numberOfBrews;

        int selectedWeek;

        IList<string> weeksInMonth = new List<string>();

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

        public int Year { get => year; set => year = value; }
        public IList<string> WeeksInMonth { get => weeksInMonth; }
        public int SelectedWeek { get => selectedWeek; set => selectedWeek = value; }
        internal Month Month { get => month; set => month = value; }
        internal string ReportName { get => reportName; set => reportName = value; }

        internal void SetWeeksInMonth(Month month, int year)
        {
            weeksInMonth.Clear();

            foreach (int weekNumber in DateHelper.GetWeeksInMonth(month, year).Keys)
            {
                weeksInMonth.Add("Week " + weekNumber.ToString());
            }
        }
    }
}
