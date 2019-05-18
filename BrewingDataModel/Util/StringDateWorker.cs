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

        public string GetMonthNumber(string month)
        {
            int monthInt = (int)Enum.Parse(typeof(Month), month);
            string monthStr = monthInt.ToString();

            //if (monthInt < 10 )
            //{
            //    monthStr = 0 + monthStr;
            //}
            return GetTwoDigitNumber(monthStr);
        }

        public string GetTwoDigitNumber(string number)
        {
            int numberInt = int.Parse(number);
            string numberStr = numberInt.ToString();

            if (numberInt < 10)
            {
                numberStr = 0 + numberStr;
            }
            return numberStr;
        }
    }
}
