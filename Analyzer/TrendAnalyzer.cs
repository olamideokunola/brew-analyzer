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
            if (NoBrews())
            {
                // TODO
            }

            Notify();
        }

        private bool NoBrews()
        {
            if (dataProvider.GetNumberOfBrews() == 0)
            {
                return true;
            }
            return false;
        }

        public string DisplayNoBrewMessage()
        {
            return "No Brews!";
        }
    }
}
