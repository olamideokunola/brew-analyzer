using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using BrewDataProvider.ActiveBrewMonitor;
using BrewDataProvider.BrewMonitor;
using BrewingModel;
using BrewingModel.Settings;
using Util;

namespace BrewDataProvider
{
    public class BrewDataProvider : IDataProvider
        /// 
        /// Class that represents an abstraction that provides brew(s) from brew file storage  
        /// 
    {
        private BrewLoaderAndMaker brewLoaderAndMaker;
        private IFileParser fileParser;

        private IList<string> brewsStringList = new List<string>();

        public IList<string> BrewsStringList { get => brewsStringList; }

        public BrewDataProvider ()
        {
            fileParser = new FileParser();
            brewLoaderAndMaker = new BrewLoaderAndMaker(fileParser);
        }

        /// 
        /// Methods
        /// 
       
            
        //private List<string> GetMonths(DateTime startDate, DateTime endDate)
        //{
        //    return null; // TODO
        //}



        //private List<int> GetDays(string month)
        //{
        //    return null; // TODO
        //}

        //private List<string> GetBrewNumbersOnDay(int day)
        //{
        //    return null; // TODO
        //}

        //private List<string> GetBrewIds(IList<string> months) 
        //{
        //    // TODO
        //    List<string> brewIds = new List<string>();

        //    foreach (string month in months)
        //    {
        //        IList<int> days = GetDays(month);
        //        IList<string> brewNumbersOnDay = new List<string>();

        //        foreach (int day in days)
        //        {
        //            brewNumbersOnDay = GetBrewNumbersOnDay(day);
        //        }

        //        brewIds.AddRange(brewNumbersOnDay);
        //    }

        //    return brewIds;
        //}


        // IDataProvider Interface Implementation
        public IBrew GetBrew(string brewId)
        {
            throw new NotImplementedException();
        }



        public IList<IBrew> SelectBrewsOld (DateTime startDate, DateTime endDate)
        {
            IList<IBrew> brews = new List<IBrew>();
            IList<string> years = new List<string>();

            int startYear = startDate.Year;
            int endYear = endDate.Year;

            // Get years in list
            years = GetYearList(startYear, endYear);

            // Get brews from years
            int startDay = startDate.Day;
            int endDay = endDate.Day;
            return GetBrewsInYearsOld(years, startYear, endYear, startDay, endDay);
        }

        private IList<IBrew> GetBrewsInYearsOld(IList<string> years, int startYear, int endYear, int startDay, int endDay)
        {
            IBrewsGetter brewsGetter = new NestedForBrewsGetter();
            return brewsGetter.GetBrewsInYearsOld(years, startYear, endYear, startDay, endDay, brewLoaderAndMaker);
        }

        private IList<string> GetBrewsInYears(IList<string> years, DateTime startDate, DateTime endDate)
        {
            IBrewsGetter brewsGetter = new NestedForBrewsGetter();
            brewsStringList = brewsGetter.GetBrewsInYears(years, startDate, endDate);
            return brewsStringList;
            // return brewsGetter.GetBrewsInYears(years, startYear, endYear, startDay, endDay, brewLoaderAndMaker);
        }

        private IList<string> GetYearList(int startYear, int endYear)
        { 
            IList<string> years = new List<string>();

            if (endYear > startYear)
            {
                for (int i = 0; i <= endYear - startYear; i++)
                {
                    int year = startYear + i;
                    years.Add(year.ToString());
                }
            }
            else if (startYear == endYear)
            {
                years.Add(startYear.ToString());
            }

            return years;
        }

        //public IList<IBrew> SelectBrews(DateTime startDate, DateTime endDate)
        //{
        //    IList<IBrew> brews = new List<IBrew>();
        //    IList<string> months = new List<string>();

        //    months = GetMonths(startDate, endDate);
        //    //format of months: 'month_year'

        //    List<string> brewIds = new List<string>();
        //    brewIds = GetBrewIds(months);
        //    //brewId: 'brewnumber_dd.mm.yyyy'

        //    foreach (string brewId in brewIds)
        //    {
        //        MyAppSettings appSettings = MyAppSettings.GetInstance();

        //        string filePath = appSettings.FileServerPath + GetFilePath(brewId);
        //        string brewNumber = GetBrewNumber(brewId);
        //        brewLoaderAndMaker.CreateBrew(filePath, brewNumber);
        //    }

        //    return brews;
        //}

        //private string GetFilePath (string brewId)
        //{
        //    // TODO
        //    return "";
        //}

        //private string GetBrewNumber(string brewId)
        //{
        //    // TODO
        //    return "";
        //}

        public int GetNumberOfBrews()
        {
            return 0;
        }

        public int GetNumberOfBrews(DateTime startDate, DateTime endDate)
        {
            IList<IBrew> brews = new List<IBrew>();
            IList<string> years = new List<string>();

            int startYear = startDate.Year;
            int endYear = endDate.Year;

            // Get years in list
            years = GetYearList(startYear, endYear);

            // Get brews from years
            int startDay = startDate.Day;
            int endDay = endDate.Day;
            return GetBrewsInYears(years, startDate, endDate).Count;
        }
    }
}