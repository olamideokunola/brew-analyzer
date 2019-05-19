using BrewDataProvider;
using BrewingModel;
using ObserverSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public class AnalysisFacade : IAnalyzer
    {

        private TrendAnalyzer trendAnalyzer;
        private IList<string> brewsStringList = new List<string>();
        public IList<string> BrewsStringList { get => brewsStringList; }

        private IList<IBrew> brewsList = new List<IBrew>();
        public IList<IBrew> BrewsList { get => brewsList; }

        public AnalysisFacade(IDataProvider dataProvider)
        {
            trendAnalyzer = new TrendAnalyzer(dataProvider);
        }

        public void RunAnalysis(string fileDestination, DateTime startDate, DateTime endDate)
        {
            trendAnalyzer.RunAnalysis(fileDestination, startDate, endDate);
            brewsList = trendAnalyzer.BrewsList;
        }

        public void SetDates(DateTime startDate, DateTime endDate)
        {
            trendAnalyzer.SetDates(startDate, endDate);
            brewsStringList = trendAnalyzer.BrewsStringList;
        }

        //public void StartTrendAnalysis()
        //{
        //    trendAnalyzer.StartTrendAnalysis();
        //}

        public void AttachTrendAnalyzerObserver(IObserver observer)
        {
            trendAnalyzer.Attach(observer);
        }
    }
}
