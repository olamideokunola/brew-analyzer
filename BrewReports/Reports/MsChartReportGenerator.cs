using BrewingModel.Datasources;
using BrewingModel.Reports;
using BrewingModel.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewReports.Reports
{
    public class MsChartReportGenerator : ReportGenerator
    {
        public MsChartReportGenerator()
        {
            MyAppSettings appSettings = MyAppSettings.GetInstance();
            string connectionString = appSettings.ConnectionString;
            string templatePath = appSettings.PeriodTemplateFilePath;
            Datasource xlDatasource = new XlDatasource(connectionString, templatePath);
            datasourceHandler = DatasourceHandler.GetInstance(xlDatasource);
        }

        public override void CreateReport(string year, Month month, string reportName, string reportPath)
        {
            
        }

        public override void CreateWeekReport(string year, Month month, string reportName, string reportPath, int weekIndex)
        {
            XlPeriod period = (XlPeriod)GetPeriod(year, month);
            report = new MsChartWeekReport(period, weekIndex);
            report.GenerateReport();
        }
    }
}
