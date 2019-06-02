using BrewingModel.Datasources;
using System;
using System.Collections.Generic;

namespace Util
{
    public class DateHelper
    {
        //Singleton
        private static DateHelper _uniqueInstance = null;

        //lazy construction of instance
        public static DateHelper GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new DateHelper();
            }

            return _uniqueInstance;
        }

        // Other attributes
        private static string dateTimePattern = "dd.MM.yyyy HH:mm:ss";
        private static string datePattern = "dd.MM.yyyy";

        //private static string timeSpanPattern = "HH:mm:ss";

        //public static string DateTimePattern
        //{
        //    get
        //    {
        //        return dateTimePattern;
        //    }
        //}

        //public static string DatePattern
        //{
        //    get
        //    {
        //        return datePattern;
        //    }
        //}

        public DateHelper()
        {
        }

        public static DateTime GetProcessParameterDateTime(string processParameterDateTimeString)
        {
            DateTime processParameterDateTime;

            processParameterDateTime = ConvertStringToDateTime(processParameterDateTimeString);

            return processParameterDateTime;
        }

        public static DateTime ConvertStringToDateTime(string dateStr)
        {
            DateTime convertedDate;

            if (dateStr.Length < 19)
            {
                convertedDate = DateTime.ParseExact(dateStr, datePattern, null);
            }
            else
            { 
                convertedDate = DateTime.ParseExact(dateStr, dateTimePattern, null);
            }
            
            return convertedDate;
        }

        public static string ConvertDateToString(DateTime date)
        {
            string dateStr = date.ToString(datePattern);
            return dateStr;
        }

        public static TimeSpan ConvertStringToTimeSpan(string timeSpanStr)
        {
            TimeSpan convertedTimeSpan;
            if (!string.IsNullOrEmpty(timeSpanStr))
            {
                //convertedTimeSpan = TimeSpan.ParseExact(timeSpanStr, timeSpanPattern, null);
                convertedTimeSpan = TimeSpan.Parse(timeSpanStr);
                //string[] formats = new string[2];
                //formats[0] = "";
                //formats[1] = timeSpanPattern;

                return convertedTimeSpan;
            }
            return TimeSpan.Zero;
        }

        public static IDictionary<int, IDictionary<string, string>> GetWeeksInMonth(Month month, int year)
        {
            IDictionary<int, IDictionary<string, string>> weeks = new Dictionary<int, IDictionary<string, string>>();

            IDictionary<string, string> dates = new Dictionary<string,string>();

            int monthInt = (int)month;

            DateTime monthStart = new DateTime(year, monthInt, 1);
            int daysInMonth = DateTime.DaysInMonth(year, monthInt);

            StringDateWorker stringDateWorker = StringDateWorker.GetInstance();
            string monthStr = stringDateWorker.GetMonthNumber(Enum.GetName(typeof(Month), month));

            // string startDateString = "01" + "." + monthStr + "." + year.ToString();

            int day = 1;
            int weekStart = 1;
            int weekEnd = 0;
            DateTime dayDate;
            DayOfWeek dow;
            string dayDateStr;            

            while (day <= daysInMonth)
            {
                // Every Sunday, reset weekstart and weekEnd and add previous week into week list
                dayDate = new DateTime(year, monthInt,day);
                // If day is sunday, load previous week if not first day and reset weekStart & weekEnd
                dow = dayDate.DayOfWeek;

                if ((dow == DayOfWeek.Sunday && day != 1) || day == daysInMonth)
                {
                    // Load previous week
                    LoadPreviousWeek(day, year, monthInt, weekStart, weeks);

                    // Reset weekStart & weekEnd
                    weekStart = day + 1;
                    weekEnd = 0;
                }

                day++;
            }

            return weeks;
        }

        private static void LoadPreviousWeek(int day, int year, int monthInt, int weekStart, IDictionary<int, IDictionary<string, string>> weeks)
        {
            int weekEnd = day;
            IDictionary<string, string> dates = new Dictionary<string, string>();
            IDictionary<int, IDictionary<string, string>> week = new Dictionary<int, IDictionary<string, string>>();

            string startDateString = ConvertDateToString(new DateTime(year, monthInt, weekStart));
            string endDateString = ConvertDateToString(new DateTime(year, monthInt, weekEnd));

            dates.Add("StartDate", startDateString);
            dates.Add("EndDate", endDateString);

            weeks.Add(weeks.Count +1,dates);
        }

    }
}
