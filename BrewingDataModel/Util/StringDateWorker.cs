using System;
namespace Util
{
    public class StringDateWorker
    {
        //Singleton
        private static StringDateWorker _uniqueInstance = null;

        //lazy construction of instance
        public static StringDateWorker GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new StringDateWorker();
            }

            return _uniqueInstance;
        }

        public StringDateWorker()
        {
        }

        public string GetYear(string startDate)
        {
            string year = "";
            if (startDate.Length > 0)
            {
                year = startDate.Substring(6, 4);
            }
            return year;
        }

        public string GetMonth(string startDate)
        {
            string month = "";
            int monthInt = 0;

            if (startDate.Length > 0)
            {
                month = startDate.Substring(3, 2);
                monthInt = int.Parse(month);
            }
            return Enum.GetName(typeof(Month), monthInt);
        }

        public string GetDay(string startDate)
        {
            string day = "";
            int dayInt = 0;

            if (startDate.Length > 0)
            {
                day = startDate.Substring(0, 2);
                dayInt = int.Parse(day);
            }
            return dayInt.ToString();
        }
    }
}
