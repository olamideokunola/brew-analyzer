using BrewingModel.Datasources;
using BrewingModel.Reports;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BrewReports.Reports
{
    public class MsChartReport : Report
    {

        /// <summary>
        /// Represents a MS Chart Report
        /// Uses a XlPeriod
        /// </summary>
        /// 
        internal ExcelPackage xlPackage;
        ExcelWorksheet xlBrewingFormWorksheet;
        internal FileInfo fileInfo;

        internal IDictionary<string, IDictionary<string, TimeSpan>> chartDataSets = new Dictionary<string, IDictionary<string, TimeSpan>>();

        IDictionary<string, TimeSpan> firstTrendChartSeriesDataSet = new Dictionary<string, TimeSpan>();
        //string firstTrendChartProcessParameter;
        int nextIndex;

        // Getters & Setters


        public MsChartReport(XlPeriod period)
        {
            this.period = period;
            xlBrewingFormWorksheet = period.XlBrewingFormWorksheet;
        }

        public MsChartReport()
        {
        }

        public override void GenerateReport()
        {
            CopyParametersFromPeriod();
        }

        internal virtual void CopyParametersFromPeriod()
        {
            IDictionary<string, TimeSpan> chartSeriesDataSets = new Dictionary<string, TimeSpan>();
            using (xlPackage = new ExcelPackage(fileInfo))
            {
                XlPeriod xlPeriod = (BrewingModel.Datasources.XlPeriod)period;

                for (int row = 6; row <= xlPeriod.XlBrewingFormWorksheet.Dimension.End.Row; row++)                    
                {
                    for (int column = 3; column <= xlPeriod.XlBrewingFormWorksheet.Dimension.End.Column; column++)
                    {
                        // Reset Series Data set
                        chartSeriesDataSets = new Dictionary<string, TimeSpan>();
                        if (xlPeriod.XlBrewingFormWorksheet.Cells[row, column].Value != null)
                        {
                            // Load Series Data set
                            string brewNumberAndDate = xlPeriod.XlBrewingFormWorksheet.Cells[4, column].Value.ToString() + xlPeriod.XlBrewingFormWorksheet.Cells[2, column].Value.ToString();
                            TimeSpan value = (TimeSpan) xlPeriod.XlBrewingFormWorksheet.Cells[row, column].Value;
                            chartSeriesDataSets.Add(brewNumberAndDate, value); 
                        }                        
                    }
                    // Add data set to chart dictionary with process parameter name as key
                    string processParameterName = xlPeriod.XlBrewingFormWorksheet.Cells[row, 2].Value.ToString();
                    chartDataSets.Add(processParameterName, chartSeriesDataSets);
                }
            }
        }

        public IDictionary<string, TimeSpan> GetFirstTrendChartSeriesDataSet()
        {
            return chartDataSets.ElementAt(0).Value;   
        }

        public string GetFirstTrendChartProcessParameter()
        {
            return chartDataSets.ElementAt(0).Key;
        }

        public IDictionary<string, IDictionary<string, TimeSpan>> GetNextTrendChartSeriesDataSet()
        {
            string nextKey = "";
            IDictionary<string, TimeSpan> nextSeriesData = new Dictionary<string, TimeSpan>();

            if (nextIndex < chartDataSets.Count-1)
            { 
                nextIndex++;
                nextKey = chartDataSets.ElementAt(nextIndex).Key;
                nextSeriesData = chartDataSets.ElementAt(nextIndex).Value;                
            }
            else if (nextIndex == chartDataSets.Count - 1)
            {
                nextKey = chartDataSets.ElementAt(nextIndex).Key;
                nextSeriesData = chartDataSets.ElementAt(nextIndex).Value;
            }

            // Create result
            IDictionary<string, IDictionary<string, TimeSpan>> result = new Dictionary<string, IDictionary<string, TimeSpan>>
            {
                { nextKey, nextSeriesData }
            };
            return result;   
        }

        public IDictionary<string, IDictionary<string, TimeSpan>> GetPreviousTrendChartSeriesDataSet()
        {
            string nextKey = "";
            IDictionary<string, TimeSpan> nextSeriesData = new Dictionary<string, TimeSpan>();

            if (nextIndex > 0)
            {
                nextIndex--;
                nextKey = chartDataSets.ElementAt(nextIndex).Key;
                nextSeriesData = chartDataSets.ElementAt(nextIndex).Value;
            }
            else if(nextIndex == 0)
            {
                nextKey = chartDataSets.ElementAt(nextIndex).Key;
                nextSeriesData = chartDataSets.ElementAt(nextIndex).Value;
            }

            // Create result
            IDictionary<string, IDictionary<string, TimeSpan>> result = new Dictionary<string, IDictionary<string, TimeSpan>>
            {
                { nextKey, nextSeriesData }
            };
            return result;
        }
    }
}
