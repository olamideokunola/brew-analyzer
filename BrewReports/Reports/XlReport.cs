using System;
using System.IO;
using BrewingModel.Datasources;
using BrewingModel.Settings;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace BrewingModel.Reports
{
    public class XlReport : Report
    {
        /// <summary>
        ///  Represents a concreate class for an excel based report
        /// </summary>

        ExcelPackage xlPackage;
        ExcelWorksheet xlReportWorksheet;
        ExcelWorksheet xlBrewingFormWorksheet;

        private FileInfo template;
        private FileInfo fileInfo;
        //private DirectoryInfo directoryInfo;

        private const string reportWorksheet = "Brewing forms";
        private const string brewingFormSheetName = "Brewing forms";
        MyAppSettings appSettings = MyAppSettings.GetInstance();

        //private string fileServerPath;
        //private string folderSeparator;

        private string templatePath;

        public XlReport(XlPeriod period, string reportName, string reportPath)
        {
            this.period = period;
            this.reportName = reportName;
            this.fileInfo = new FileInfo(reportPath + Path.DirectorySeparatorChar + reportName + ".xlsx");

            xlBrewingFormWorksheet = period.XlBrewingFormWorksheet;

            templatePath = appSettings.ReportTemplateFilePath;
            this.template = new FileInfo(templatePath);

            CreateReportWorkSheet();
            CopyParametersFromPeriod();
        }

        public XlReport()
        {
        }

        private void CreateReportWorkSheet()
        {
            using (xlPackage = new ExcelPackage(template, true))
            {
                xlPackage.SaveAs(fileInfo);
                xlReportWorksheet = xlPackage.Workbook.Worksheets[reportWorksheet];
            }
        }

        private void CopyParametersFromPeriod()
        {
            using (xlPackage = new ExcelPackage(fileInfo))
            {
                XlPeriod xlPeriod = (BrewingModel.Datasources.XlPeriod)period;
                xlReportWorksheet = xlPackage.Workbook.Worksheets[reportWorksheet];

                //for (int column = 1; column <= xlPeriod.XlBrewingFormWorksheet.Dimension.Columns; column++)
                for (int column = 1; column <= xlPeriod.XlBrewingFormWorksheet.Dimension.End.Column; column++)

                    {
                        for (int row = 1; row <= xlPeriod.XlBrewingFormWorksheet.Dimension.End.Row; row++)
                    {
                        xlReportWorksheet.Cells[row, column].Value = xlPeriod.XlBrewingFormWorksheet.Cells[row, column].Value;
                    }
                }

                xlPackage.Save();
            }
        }

          
        public string ReportWorksheet
        {
            get
            {
                return reportWorksheet;
            }
        }

        public string BrewingFormSheetName
        {
            get
            {
                return brewingFormSheetName;
            }
        }

        public FileInfo FileInfo
        {
            get
            {
                return fileInfo;
            }
        }

        public ExcelWorksheet XlBrewingFormWorksheet
        {
            get
            {
                return xlBrewingFormWorksheet;
            }
        }

        public ExcelWorksheet XlReportWorksheet
        {
            get
            {
                return xlReportWorksheet;
            }
        }


    }
}
