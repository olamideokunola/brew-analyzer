using Analyzer;
using BrewingModel;
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
        private Subject guiModelSubject;
        private TrendAnalysisGuiModel guiModel;

        public TrendAnalysisController(IAnalyzer analyzerFacade, TrendAnalysisGuiModel guiModel)
        {
            this.analyzerFacade = analyzerFacade;
            this.guiModel = guiModel;
            this.guiModelSubject = guiModel;
        }

        //public void StartTrendAnalysis()
        //{
        //    analyzerFacade.StartTrendAnalysis();
        //}


        // Use case methods
        public void SetDates(DateTime startDate, DateTime endDate)
        {
            analyzerFacade.SetDates(startDate, endDate);
            IList<string> brewsStringList = analyzerFacade.BrewsStringList;
            guiModel.BrewsList = brewsStringList;
        }

        public void RunAnalysis(string fileDestination, DateTime startDate, DateTime endDate)
        {
            analyzerFacade.RunAnalysis(fileDestination, startDate, endDate);
        }

        public IList<IBrew> GetBrews()
        {
            return analyzerFacade.BrewsList;
        }

        public void DisplayNoBrewMessage(string message)
        {

        }

        public void AttachTrendAnalyzerObserver(IObserver observer)
        {
            analyzerFacade.AttachTrendAnalyzerObserver(observer);
            AttachGuiModelObserver(observer);
        }


        // GuiModel Mvc methods
        public void AttachGuiModelObserver(IObserver observer)
        {
            guiModel.Attach(observer);
        }

        public void DetachGuiModelObserver(IObserver observer)
        {
            guiModel.Detach(observer);
        }
    }
}
