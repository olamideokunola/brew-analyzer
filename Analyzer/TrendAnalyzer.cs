using BrewDataProvider;
using BrewingModel;
using BrewingModel.Datasources;
using BrewingModel.Reports;
using BrewReports.Reports;
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

        private IList<IBrew> brewsList = new List<IBrew>();
        public IList<IBrew> BrewsList { get => brewsList; }

        private ReportGenerator reportGenerator;

        public TrendAnalyzer(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public void LoadBrews(DateTime startDate, DateTime endDate)
        {
            brewsList = dataProvider.GetBrews(startDate, endDate);
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

        internal void GenerateWeekReport(Month month, int year, string reportName, string fileDestination, int week)
        {
            // Generate Report
            reportGenerator = new XlReportGenerator();

            string yearStr = year.ToString();

            reportGenerator.CreateWeekReport(yearStr, month, reportName, fileDestination, week);


        }

        internal void GenerateWeekChartReport(Month month, int year, int week)
        {
            // Generate Report
            reportGenerator = new MsChartReportGenerator();

            string yearStr = year.ToString();

            reportGenerator.CreateWeekReport(yearStr, month, "", "", week);

        }

        internal IDictionary<string, TimeSpan> GetFirstTrendChartSeriesDataSet()
        {
            MsChartReport msChartReport = (MsChartReport)reportGenerator.Report;
            return msChartReport.GetFirstTrendChartSeriesDataSet();
        }
        internal string GetFirstTrendChartProcessParameter()
        {
            MsChartReport msChartReport = (MsChartReport)reportGenerator.Report;
            return msChartReport.GetFirstTrendChartProcessParameter();
        }
        internal IDictionary<string, IDictionary<string, TimeSpan>> GetNextTrendChartSeriesDataSet()
        {
            MsChartReport msChartReport = (MsChartReport)reportGenerator.Report;
            return msChartReport.GetNextTrendChartSeriesDataSet();
        }

        internal IDictionary<string, IDictionary<string, TimeSpan>> GetPreviousTrendChartSeriesDataSet()
        {
            MsChartReport msChartReport = (MsChartReport)reportGenerator.Report;
            return msChartReport.GetPreviousTrendChartSeriesDataSet();
        }
    }
}
