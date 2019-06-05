using BrewDataProvider;
using BrewingModel;
using BrewingModel.Datasources;
using BrewingModel.Reports;
using BrewingModel.Settings;
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

        public void LoadBrews(DateTime startDate, DateTime endDate)
        {
            trendAnalyzer.LoadBrews(startDate, endDate);
            brewsList = trendAnalyzer.BrewsList;            
        }

        public void RunAnalysis(string fileDestination, DateTime startDate, DateTime endDate)
        {

        }

        public int GetNumberOfBrewsToAdd(Month month, int year)
        {
            // Create Datasource for report
            DatasourceHandler datasourceHandler = DatasourceHandler.GetInstance();

            MyAppSettings appSettings = MyAppSettings.GetInstance();

            string conStr = appSettings.ConnectionString;
            string tempPath = appSettings.PeriodTemplateFilePath;

            Datasource datasource = new XlDatasource(conStr, tempPath);
            datasourceHandler.Datasource = datasource;


            // Check for number of brews to add
            IList<IBrew> brews = brewsList;
            int numberOfBrewsToAdd;

            if (datasourceHandler.GetExistingBrewNumbers(month, year) != null)
            {
                IList<IDictionary<string, string>> existingBrewNumbers = datasourceHandler.GetExistingBrewNumbers(month, year);

                numberOfBrewsToAdd = brews.Count - existingBrewNumbers.Count;
            }
            else
            {
                numberOfBrewsToAdd = brews.Count;
            }

            return numberOfBrewsToAdd;
        }

        public void GenerateWeekReport(Month month, int year, string reportName, string destination, int week)
        {
            trendAnalyzer.GenerateWeekReport(month, year, reportName, destination, week);
        }

        public void GenerateWeekChartReport(Month month, int year, string reportName, string destination, int week)
        {
            trendAnalyzer.GenerateWeekChartReport(month, year, week);
        }

        public void SetDates(DateTime startDate, DateTime endDate)
        {
            trendAnalyzer.SetDates(startDate, endDate);
            brewsStringList = trendAnalyzer.BrewsStringList;
        }

        public void AttachTrendAnalyzerObserver(IObserver observer)
        {
            trendAnalyzer.Attach(observer);
        }

        public IDictionary<string, TimeSpan> GetFirstTrendChartSeriesDataSet()
        {
            return trendAnalyzer.GetFirstTrendChartSeriesDataSet();
        }

        public string GetFirstTrendChartProcessParameter()
        {
            return trendAnalyzer.GetFirstTrendChartProcessParameter();
        }

        public IDictionary<string, IDictionary<string, TimeSpan>> GetNextTrendChartSeriesDataSet()
        {
            return trendAnalyzer.GetNextTrendChartSeriesDataSet();
        }

        public IDictionary<string, IDictionary<string, TimeSpan>> GetPreviousTrendChartSeriesDataSet()
        {
            return trendAnalyzer.GetPreviousTrendChartSeriesDataSet();
        }

        public void GenerateWeekChartReport(Month month, int year, int week)
        {
            trendAnalyzer.GenerateWeekChartReport(month, year, week);
        }
    }
}
