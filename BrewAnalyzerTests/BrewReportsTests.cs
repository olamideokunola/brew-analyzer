using NUnit.Framework;
using BrewDataProvider;
using System.Collections.Generic;
using BrewDataProvider.ActiveBrewMonitor;
using BrewDataProvider.BrewMonitor;
using System;
using BrewingModel.Reports;
using BrewingModel.Datasources;
using BrewingModel.Settings;

namespace Tests
{
    public class BrewReportsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestXlWeekReport()
        {
            MyAppSettings myAppSettings = MyAppSettings.GetInstance();
            XlPeriod period = new XlPeriod(year: "2018", month: Month.January, connectionString: "C:\\Users\\Olamide Okunola\\source\\repos\\ProductionLog\\BrewingModel\\bin\\Debug\\datasource\\");
            Report wkReport = new XlWeekReport(period, reportName: "testWeekReport", reportPath: "C:\\Users\\Olamide Okunola\\Documents", weekIndex: 1);

            Assert.Equals(wkReport.ReportName, "testWeekReport");
            //Assert.Pass();
        }


    }
}