using Analyzer;
using BrewingModel;
using ObserverSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace brew_analyzer
{
    public class TrendAnalysisController
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
        public void SetDates()
        {
            analyzerFacade.SetDates(guiModel.StartDate, guiModel.EndDate);
            IList<string> brewsStringList = analyzerFacade.BrewsStringList;
            guiModel.BrewsList = brewsStringList;
        }

        public void RunAnalysis()
        {
            analyzerFacade.RunAnalysis(guiModel.FileDestination, guiModel.StartDate, guiModel.EndDate);
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

        public void SetStartDate(DateTime startDate)
        {
            guiModel.StartDate = startDate;
        }

        public void SetEndDate(DateTime endDate)
        {
            guiModel.EndDate = endDate;

        }
        public DateTime GetStartDate()
        {
            return guiModel.StartDate;
        }

        public DateTime GetEndDate()
        {
            return guiModel.EndDate;
        }

        public void SetFileDestination(string fileDestination)
        {
            guiModel.FileDestination = fileDestination;
        }

        public string GetFileDestination()
        {
            return guiModel.FileDestination;
        }

        public int GetNumberOfBrews()
        {
            return guiModel.NumberOfBrews;
        }
    }
}
