using System;
using BrewingModel.Datasources;
using BrewingModel.Settings;

namespace BrewingModel.Reports
{
	public class XlReportGenerator : ReportGenerator
    {
        public XlReportGenerator()
        {
            MyAppSettings appSettings = MyAppSettings.GetInstance();
            string connectionString = appSettings.ConnectionString;
            string templatePath = appSettings.PeriodTemplateFilePath;
            Datasource xlDatasource = new XlDatasource(connectionString, templatePath);
            datasourceHandler = DatasourceHandler.GetInstance(xlDatasource);
        }

        public override void CreateReport(string year, Month month, string reportName, string reportPath)
        {
            XlPeriod period = (XlPeriod)GetPeriod(year, month);
            report = new XlReport(period, reportName, reportPath);
        }
    }
}
