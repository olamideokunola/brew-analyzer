using Analyzer;
using ObserverSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace brew_analyzer
{
    class TrendAnalysisController
    {
        private IAnalyzer analyzerFacade;

        public TrendAnalysisController(IAnalyzer analyzerFacade)
        {
            this.analyzerFacade = analyzerFacade;

        }

        //public void StartTrendAnalysis()
        //{
        //    analyzerFacade.StartTrendAnalysis();
        //}

        public void SetDates(DateTime startDate, DateTime endDate)
        {
            analyzerFacade.SetDates(startDate, endDate);
        }

        public void DisplayNoBrewMessage(string message)
        {

        }

        public void AttachTrendAnalyzerObserver(IObserver observer)
        {
            analyzerFacade.AttachTrendAnalyzerObserver(observer);
        }
    }
}
