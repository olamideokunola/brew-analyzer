using System;
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
            convertedDate = DateTime.ParseExact(dateStr, dateTimePattern, null);

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
    }
}
