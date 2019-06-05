using BrewingModel;
using BrewingModel.Datasources;
using ObserverSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public interface IAnalyzer
    {
        void SetDates(DateTime startDate, DateTime endDate);
        //void StartTrendAnalysis();
        void LoadBrews(DateTime startDate, DateTime endDate);
        void RunAnalysis(string fileDestination, DateTime startDate, DateTime endDate);
        void AttachTrendAnalyzerObserver(IObserver observer);
        IList<string> BrewsStringList { get; }
        IList<IBrew> BrewsList { get; }

        int GetNumberOfBrewsToAdd(Month month, int year);
        void GenerateWeekReport(Month month, int year, string reportName, string fileDestination, int week);
        void GenerateWeekChartReport(Month month, int year, int week);
        IDictionary<string, TimeSpan> GetFirstTrendChartSeriesDataSet();
        string GetFirstTrendChartProcessParameter();
        IDictionary<string, IDictionary<string, TimeSpan>> GetNextTrendChartSeriesDataSet();
        IDictionary<string, IDictionary<string, TimeSpan>> GetPreviousTrendChartSeriesDataSet();
    }
}
