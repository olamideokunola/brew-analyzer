using BrewDataProvider;
using BrewingModel;
using ObserverSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public class TrendAnalyzer : Subject
    {
        private IDataProvider dataProvider;

        private string numberOfBrewsMessage;

        private IList<string> brewsStringList = new List<string>();

        public IList<string> BrewsStringList { get => brewsStringList; }

        public TrendAnalyzer(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public void RunAnalysis(string fileDestination)
        {
            throw new NotImplementedException();
        }

        public void SetDates(DateTime startDate, DateTime endDate)
        {
            //IList<IBrew> brews = dataProvider.SelectBrews(startDate, endDate);

            int NumberOfBrews = dataProvider.GetNumberOfBrews(startDate, endDate);
            brewsStringList = dataProvider.BrewsStringList;

            numberOfBrewsMessage = "";

            if (NumberOfBrews==0)   //brews.Count == 0)
            {
                numberOfBrewsMessage = "No brews!";
            }
            else if (NumberOfBrews == 1) //brews.Count == 1)
            {
                numberOfBrewsMessage = "Only one brew!";
            }
            Notify();
        }

        //public void StartTrendAnalysis()
        //{

        //    if (NoBrews() || OneBrew())
        //    {
        //        Notify();
        //    } else {
        //        // TODO
        //    }   
        //}

    

        public string NumberOfBrewsMessage
        {
            get { return numberOfBrewsMessage; }
        }

        
    }
}
