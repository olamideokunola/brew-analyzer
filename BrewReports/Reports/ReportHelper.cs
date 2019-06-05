using BrewingModel.Datasources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;

namespace BrewReports.Reports
{
    public static class ReportHelper
    {

        public static string GetEndDate(Period period, int weekIndex)
        {
            IDictionary<string, string> dates = GetWeekStartAndEndDates(period, weekIndex);

            string endDate = dates["EndDate"];
            return endDate;
        }

        public static IDictionary<string, string> GetWeekStartAndEndDates(Period period, int weekIndex)
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

        public static string GetStartDate(Period period, int weekIndex)
        {
            IDictionary<string, string> dates = GetWeekStartAndEndDates(period, weekIndex);

            string startDate = dates["StartDate"];
            return startDate;
        }
    }
}
