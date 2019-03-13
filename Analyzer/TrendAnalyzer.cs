using BrewDataProvider;
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
            throw new NotImplementedException();
        }

        public void StartTrendAnalysis()
        {

            if (NoBrews() || OneBrew())
            {
                Notify();
            } else {
                // TODO
            }   
        }

        private bool NoBrews()
        {
            if (dataProvider.GetNumberOfBrews() == 0)
            {
                numberOfBrewsMessage = "No brews!";
                return true;
            }
            return false;
        }

        private bool OneBrew()
        {
            if (dataProvider.GetNumberOfBrews() > 0)
            {
                numberOfBrewsMessage = "Only one brew!";
                return true;
            }
            return false;
        }

        public string NumberOfBrewsMessage
        {
            get { return numberOfBrewsMessage; }
        }

    }
}
