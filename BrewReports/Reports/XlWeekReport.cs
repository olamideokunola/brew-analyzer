using BrewingModel.Datasources;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;
using System.IO;

namespace BrewingModel.Reports
{ 
    public class XlWeekReport : XlReport
    {
        string startDateString;
        string endDateString;
        DateTime startDate;
        DateTime endDate;
        int weekIndex;

        //internal ExcelWorksheet xlPeriodWorksheet;

        public XlWeekReport(XlPeriod period, string reportName, string reportPath, int weekIndex) : base(period, reportName, reportPath)
        {
            this.weekIndex = weekIndex;           
        }

        //internal override void CopyParametersFromPeriod()
        //{
        //    startDateString = GetStartDate(period, weekIndex);
        //    endDateString = GetEndDate(period, weekIndex);

        //    startDate = DateHelper.ConvertStringToDateTime(startDateString);
        //    endDate = DateHelper.ConvertStringToDateTime(endDateString);

        //    DateTime columnDate;
        //    string columnDateString;

        //    using (xlPackage = new ExcelPackage(fileInfo))
        //    {
        //        XlPeriod xlPeriod = (BrewingModel.Datasources.XlPeriod)period;
        //        xlReportWorksheet = xlPackage.Workbook.Worksheets[reportWorksheet];

        //        // data entry starts in 3rd column, so offset is needed
        //        int destinationColumn = 3;
        //        for (int column = 3; column <= xlPeriod.XlBrewingFormWorksheet.Dimension.End.Column; column++)
        //        {
        //            if(xlPeriod.XlBrewingFormWorksheet.Cells[2, column].Value != null)
        //            {
        //                columnDateString = xlPeriod.XlBrewingFormWorksheet.Cells[2, column].Value.ToString();
        //                columnDate = DateHelper.ConvertStringToDateTime(columnDateString);

        //                // Check if date is in week range before copying
        //                if ((columnDate > startDate && columnDate < endDate) || (columnDate == startDate) || (columnDate == endDate))
        //                {
        //                    for (int row = 1; row <= xlPeriod.XlBrewingFormWorksheet.Dimension.End.Row; row++)
        //                    {
        //                        xlReportWorksheet.Cells[row, destinationColumn].Value = xlPeriod.XlBrewingFormWorksheet.Cells[row, column].Value;
        //                    }
        //                    destinationColumn++;
        //                }
        //            }
                    
        //        }
        //        Byte[] bin = xlPackage.GetAsByteArray();
        //        File.WriteAllBytes(fileInfo.FullName, bin);
        //        //xlPackage.SaveAs(fileInfo);
        //    }
        //}

        internal override void CopyParametersFromPeriod()
        {

            //  Create new report dictionary element 
            SortedDictionary<string, IDictionary<string, object>> brewsInDateRange = GetBrewsInDateRange();

            using (xlPackage = new ExcelPackage(fileInfo))
            {
                XlPeriod xlPeriod = (BrewingModel.Datasources.XlPeriod)period;
                xlReportWorksheet = xlPackage.Workbook.Worksheets[reportWorksheet];

                // Get each column starting from column 3 of the report template
                int column = 3;
                foreach (KeyValuePair<string, IDictionary<string, object>> reportColumn in brewsInDateRange)
                {
                    IDictionary<string, object> columnData = reportColumn.Value;
                    int row = 2;
                    foreach (KeyValuePair<string, object> dataItem in columnData)
                    {
                        xlReportWorksheet.Cells[row, column].Value = dataItem.Value;
                        row++;
                    }
                    column++;
                }


                //write the file to the disk

                SaveWorkSheet(xlPackage);

                // Byte[] bin = xlPackage.GetAsByteArray();
                // File.WriteAllBytes(fileInfo.FullName, bin);

            }
        }

        private SortedDictionary<string, IDictionary<string, object>> GetBrewsInDateRange()
        {
            startDateString = GetStartDate(period, weekIndex);
            endDateString = GetEndDate(period, weekIndex);

            startDate = DateHelper.ConvertStringToDateTime(startDateString);
            endDate = DateHelper.ConvertStringToDateTime(endDateString);

            DateTime columnDate;
            string columnDateString;
            string columnBrewNumber;
            string columnKey;

            XlPeriod xlPeriod = (BrewingModel.Datasources.XlPeriod)period;

            SortedDictionary<string, IDictionary<string, object>> reportData = new SortedDictionary<string, IDictionary<string, object>>();

            // data entry starts in 3rd column, so offset is needed
            for (int column = 3; column <= xlPeriod.XlBrewingFormWorksheet.Dimension.End.Column; column++)
            {
                if (xlPeriod.XlBrewingFormWorksheet.Cells[2, column].Value != null)
                {
                    columnDateString = xlPeriod.XlBrewingFormWorksheet.Cells[2, column].Value.ToString();
                    columnDate = DateHelper.ConvertStringToDateTime(columnDateString);
                    columnBrewNumber = xlPeriod.XlBrewingFormWorksheet.Cells[4, column].Value.ToString();
                    columnKey = columnDateString + "-" + columnBrewNumber;

                    IDictionary<string, object> columnElements = new Dictionary<string, object>();

                    // Check if date is in week range before copying
                    if ((columnDate > startDate && columnDate < endDate) || (columnDate == startDate) || (columnDate == endDate))
                    {
                        columnElements = new Dictionary<string, object>();

                        for (int row = 2; row <= xlPeriod.XlBrewingFormWorksheet.Dimension.End.Row; row++)
                        {
                            if(xlPeriod.XlBrewingFormWorksheet.Cells[row, 2].Value != null)
                            {
                                string paramName;
                                object paramValue;

                                paramName = xlPeriod.XlBrewingFormWorksheet.Cells[row, 2].Value.ToString();
                                paramValue = xlPeriod.XlBrewingFormWorksheet.Cells[row, column].Value;
                                columnElements.Add(paramName, paramValue);
                            }
                            
                        }

                        reportData.Add(columnKey, columnElements);
                    }
                    
                }
            }
            return reportData;
        }


        private void SaveWorkSheet(ExcelPackage xlPackage)
        {
            using (xlPackage)
            {
                Byte[] bin = xlPackage.GetAsByteArray();

                FileInfo file = fileInfo;
                File.WriteAllBytes(file.FullName, bin);
                //xlPackage.SaveAs(file);
                //return file.FullName;
            }
        }

        private string GetEndDate(Period period, int weekIndex)
        {
            IDictionary<string, string> dates = GetWeekStartAndEndDates(period, weekIndex);

            string endDate = dates["EndDate"];
            return endDate;
        }

        private IDictionary<string, string> GetWeekStartAndEndDates(Period period, int weekIndex)
        {
            string monthStr = period.Month;
            //Util.Month mnth = (Util.Month) Enum.Parse(typeof(Util.Month), monthStr);

            //int monthInt = int.Parse(period.Month);
            int yearInt = int.Parse(period.Year);

            Month month = (Month)Enum.Parse(typeof(Month), monthStr);

            IDictionary<int, IDictionary<string, string>> weeksInMonth = DateHelper.GetWeeksInMonth(month, yearInt);
            IDictionary<string, string> dates = weeksInMonth[weekIndex];

            return dates;
        }

        private string GetStartDate(Period period, int weekIndex)
        {
            IDictionary<string, string> dates = GetWeekStartAndEndDates(period, weekIndex);

            string startDate = dates["StartDate"];
            return startDate;
        }



        //private int GetColumnWithHigherClosestDate(string dateString)
        //{
        //    int column = 0;

        //    using (xlPackage = new ExcelPackage(fileInfo))
        //    {
        //        XlPeriod xlPeriod = (BrewingModel.Datasources.XlPeriod)period;
        //        xlPeriodWorksheet = xlPeriod.XlBrewingFormWorksheet;

        //        // if date is found in column then return column number
        //        for (column = 1; column <= xlPeriodWorksheet.Dimension.End.Column; column++)
        //        {
        //            if (xlPeriodWorksheet.Cells[2, column].Value.ToString() == dateString)
        //            {
        //                return column;
        //            }
        //        }

        //        // if date is not found in column get closest higher date
        //        string currentCellStr;
        //        string previousCellStr;
        //        string nextCellStr;

        //        int currentDayInt;
        //        int previousDayInt;
        //        int nextDayInt;

        //        int subjectDayInt = int.Parse(dateString.Substring(1, 2)); ;

                
        //        for (column = 1; column < xlPeriodWorksheet.Dimension.End.Column; column++)
        //        {
                    

        //            if (xlPeriodWorksheet.Cells[2, column].Value.ToString() == dateString)
        //            {
        //                switch (column)
        //                {
        //                    case 1:
        //                        currentCellStr = xlPeriodWorksheet.Cells[2, column].Value.ToString();
        //                        nextCellStr = xlPeriodWorksheet.Cells[2, column + 1].Value.ToString();
        //                        break;
        //                    default:
        //                        currentCellStr = xlPeriodWorksheet.Cells[2, column].Value.ToString();
        //                        previousCellStr = xlPeriodWorksheet.Cells[2, column - 1].Value.ToString();
        //                        nextCellStr = xlPeriodWorksheet.Cells[2, column + 1].Value.ToString();

        //                        currentDayInt = int.Parse(currentCellStr.Substring(1,2));
        //                        previousDayInt = int.Parse(previousCellStr.Substring(1, 2));
        //                        nextDayInt = int.Parse(nextCellStr.Substring(1, 2));

        //                        if (currentDayInt > subjectDayInt && currentDayInt < nextDayInt)
        //                        {

        //                        }

        //                        break;
        //                }
                            
                        

        //                return column;
        //            }
        //        }
        //    }

        //    return column;
        //}
    }
}
