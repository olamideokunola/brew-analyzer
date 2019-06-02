using Analyzer;
using BrewingModel;
using BrewingModel.Datasources;
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

        internal void SetMonth(Month month)
        {
            guiModel.Month = month;
        }

        internal Month GetMonth()
        {
            return guiModel.Month;
        }

        internal void SetYear(int year)
        {
            guiModel.Year = year;
        }

        internal int GetYear()
        {
            return guiModel.Year;
        }

        internal void SetWeeksInMonth(Month month, int year)
        {
            guiModel.SetWeeksInMonth(month, year);
        }

        internal IList<string> GetWeeksInMonth()
        {
            return guiModel.WeeksInMonth;
        }

        internal void SetSelectedWeek(int week)
        {
            guiModel.SelectedWeek = week;
        }

        internal int GetSelectedWeek()
        {
            return guiModel.SelectedWeek;
        }

        internal int GetNumberOfBrewsToAdd()
        {
            return analyzerFacade.GetNumberOfBrewsToAdd(guiModel.Month, guiModel.Year);
        }

        internal void GenerateWeekReport()
        {
            Month month = guiModel.Month;
            int year = guiModel.Year;
            string reportName = guiModel.ReportName;
            string fileDestination = guiModel.FileDestination;
            int week = guiModel.SelectedWeek;

            analyzerFacade.GenerateWeekReport(month, year, reportName, fileDestination, week);
        }

        internal void SetReportName(string reportName)
        {
            guiModel.ReportName = reportName;
        }

        internal void LoadBrews()
        {
            analyzerFacade.LoadBrews(guiModel.StartDate, guiModel.EndDate);
        }

        internal string GetReportName()
        {
            return guiModel.ReportName;
        }
    }
}
