using BrewingModel.Datasources;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;

namespace BrewReports.Reports
{
    class MsChartWeekReport : MsChartReport
    {
        string startDateString;
        string endDateString;
        DateTime startDate;
        DateTime endDate;
        int weekIndex;

        //internal ExcelWorksheet xlPeriodWorksheet;

        public MsChartWeekReport(XlPeriod period, int weekIndex) : base(period)
        {
            this.weekIndex = weekIndex;
        }

        internal override void CopyParametersFromPeriod()
        {
            startDateString = ReportHelper.GetStartDate(period, weekIndex);
            endDateString = ReportHelper.GetEndDate(period, weekIndex);

            startDate = DateHelper.ConvertStringToDateTime(startDateString);
            endDate = DateHelper.ConvertStringToDateTime(endDateString);

            DateTime columnDate;
            string columnDateString;

            using (xlPackage = new ExcelPackage(fileInfo))
            {
                XlPeriod xlPeriod = (BrewingModel.Datasources.XlPeriod)period;
                IDictionary<string, TimeSpan> chartSeriesDataSets = new Dictionary<string, TimeSpan>();

                for (int row = 6; row <= xlPeriod.XlBrewingFormWorksheet.Dimension.End.Row; row++)
                {
                    // Reset Series Data set
                    chartSeriesDataSets = new Dictionary<string, TimeSpan>();
                    for (int column = 3; column <= xlPeriod.XlBrewingFormWorksheet.Dimension.End.Column; column++)
                    {                        
                        if ((xlPeriod.XlBrewingFormWorksheet.Cells[row, column].Value != null && xlPeriod.XlBrewingFormWorksheet.Cells[row, column].Value.ToString() != "") && xlPeriod.XlBrewingFormWorksheet.Cells[2, column].Value != null)
                        {
                            columnDateString = xlPeriod.XlBrewingFormWorksheet.Cells[2, column].Value.ToString();
                            columnDate = DateHelper.ConvertStringToDateTime(columnDateString);

                            // Check if date is in week range before copying
                            if ((columnDate > startDate && columnDate < endDate) || (columnDate == startDate) || (columnDate == endDate))
                            {
                                // Load Series Data set
                                string brewNumberAndDate = xlPeriod.XlBrewingFormWorksheet.Cells[4, column].Value.ToString() +  " - "+ xlPeriod.XlBrewingFormWorksheet.Cells[2, column].Value.ToString();
                                TimeSpan value = xlPeriod.XlBrewingFormWorksheet.Cells[row, column].GetValue<TimeSpan>();
                                chartSeriesDataSets.Add(brewNumberAndDate, value);
                            }
                        }
                    }
                    // Add data set to chart dictionary with process parameter name as key
                    string processParameterName = xlPeriod.XlBrewingFormWorksheet.Cells[row, 2].Value.ToString();
                    chartDataSets.Add(processParameterName, chartSeriesDataSets);
                }
            }
        }
    }
}
