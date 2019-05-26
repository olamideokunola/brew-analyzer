using System;
using System.Collections.Generic;
using BrewingModel.Datasources;

namespace BrewingModel.Reports
{
    public abstract class ReportGenerator
    {
        private IDictionary<string, Period> availablePeriods = new Dictionary<string, Period>();
        protected Report report;
        internal DatasourceHandler datasourceHandler;

        public Report Report
        {
            get
            {
                return report;
            }
        }

        public abstract void CreateReport(string year, Month month, string reportName, string reportPath);

        public Period GetPeriod(string year, Month month)
        {
            return datasourceHandler.LoadPeriod(year, month);
        }

        public void LoadPeriods()
        {
            datasourceHandler.LoadPeriods();
            Datasource datasource = datasourceHandler.Datasource;
            availablePeriods = datasource.Periods;
        }

    }
}
