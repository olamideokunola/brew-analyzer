using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using BrewDataProvider.ActiveBrewMonitor;
using BrewingModel;
using BrewingModel.Settings;
using Util;

namespace BrewDataProvider
{
    class NestedForBrewsGetter : IBrewsGetter
    {
        IList<string> brewsStringList = new List<string>();

        public IList<IBrew> GetBrewsInPeriod(IList<string> years, DateTime startDate, DateTime endDate, BrewLoaderAndMaker brewLoaderAndMaker)
        {
            MyAppSettings myAppSettings = MyAppSettings.GetInstance();
            string fileServerPath = myAppSettings.FileServerPath;

            //for each year folder, get brews
            IList<IBrew> brews = new List<IBrew>();
            foreach (string year in years)
            {
                if (FolderWorker.IsSubDirectory(fileServerPath, year))
                {
                    DirectoryInfo yearFolder = new DirectoryInfo(fileServerPath + myAppSettings.FolderSeparator + year);
                    string[] months = Enum.GetNames(typeof(Month));

                    // for each month in year folder, get brews
                    foreach (DirectoryInfo monthFolder in yearFolder.GetDirectories())
                    {
                        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                        // Convert monthName to title case
                        string monthName = textInfo.ToTitleCase(monthFolder.Name).ToString();

                        // If month folder exists then 
                        if (months.Contains(monthName))
                        {
                            // for each day folder in month, get days
                            foreach (DirectoryInfo dayFolder in monthFolder.GetDirectories())
                            {
                                int yearInt = int.Parse(year);
                                Month currMonth = (Month)Enum.Parse(typeof(Month), monthName);
                                int monthInt = (int)currMonth;
                                int dayInt = int.Parse(dayFolder.Name);

                                DateTime folderDate = new DateTime(yearInt, monthInt, dayInt);

                                if (folderDate.Date >= startDate.Date && folderDate.Date <= endDate.Date)
                                {
                                    // for each brew file in day, get file name and add to list
                                    foreach (FileInfo brewFile in dayFolder.GetFiles())
                                    {
                                        // string filePath = appSettings.FileServerPath + GetFilePath(brewId);
                                        // filePath is for the folder containing the brewFile that is why it is renamed folderPath

                                        string folderPath = dayFolder.FullName + myAppSettings.FolderSeparator;

                                        string[] brewFileName = null;

                                        // Split file name to remove extension
                                        brewFileName = brewFile.Name.Split('.');
                                        string brewNumber = brewFileName[0];

                                        StringDateWorker stringDateWorker = StringDateWorker.GetInstance();
                                        string monthId = stringDateWorker.GetMonthNumber(monthName);
                                        string dayId = stringDateWorker.GetTwoDigitNumber(dayFolder.Name);

                                        string startDateStr = dayId + '.' + monthId + '.' + year;

                                        brews.Add(brewLoaderAndMaker.CreateBrew(folderPath, brewNumber, startDateStr));
                                    }

                                    //NumberOfBrews = NumberOfBrews + dayFolder.GetFiles().Length;
                                }

                            }

                        }
                    }
                }
            }
            return brews;
        }

        // Include startMonth for accuracy, method is not correct when startmonth and endmonth are same
        public IList<string> GetBrewsInYears(IList<string> years, DateTime startDate, DateTime endDate)
        {
            MyAppSettings myAppSettings = MyAppSettings.GetInstance();
            string fileServerPath = myAppSettings.FileServerPath;

            int startYear = startDate.Year;
            int endYear = endDate.Year;

            int NumberOfBrews = 0;

            //for each year folder, get brews
            IList<IBrew> brews = new List<IBrew>();
            foreach (string year in years)
            {
                if (FolderWorker.IsSubDirectory(fileServerPath, year))
                {
                    DirectoryInfo yearFolder = new DirectoryInfo(fileServerPath + myAppSettings.FolderSeparator + year);
                    string[] months = Enum.GetNames(typeof(Month));

                    // for each month in year folder, get brews
                    foreach (DirectoryInfo monthFolder in yearFolder.GetDirectories())
                    {
                        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                        // Convert monthName to title case
                        string monthName = textInfo.ToTitleCase(monthFolder.Name).ToString();

                        // If month folder exists then 
                        if (months.Contains(monthName))
                        {
                            // for each day folder in month, get days
                            foreach (DirectoryInfo dayFolder in monthFolder.GetDirectories())
                            {
                                //// Current year is start year and current month is in range
                                //if (int.Parse(yearFolder.Name) == startYear && int.Parse(dayFolder.Name) >= startDay)
                                //{
                                //    NumberOfBrews = NumberOfBrews + dayFolder.GetFiles().Length;
                                //}

                                //// Current year is in query range
                                //else if (int.Parse(yearFolder.Name) < endYear)
                                //{
                                //    NumberOfBrews = NumberOfBrews + dayFolder.GetFiles().Length;
                                //}

                                //// Current year is end year and month is in range
                                //else if (int.Parse(yearFolder.Name) == endYear && int.Parse(dayFolder.Name) <= endDay)
                                //{
                                //    NumberOfBrews = NumberOfBrews + dayFolder.GetFiles().Length;
                                //}

                                int yearInt = int.Parse(year);
                                Month currMonth = (Month) Enum.Parse(typeof(Month), monthName);
                                int monthInt = (int)currMonth;
                                int dayInt = int.Parse(dayFolder.Name);

                                DateTime folderDate = new DateTime(yearInt, monthInt, dayInt);

                                if(folderDate.Date >= startDate.Date && folderDate.Date <= endDate.Date)
                                {
                                    // for each brew file in day, get file name and add to list
                                    foreach (FileInfo brewFile in dayFolder.GetFiles())
                                    {
                                        brewsStringList.Add(brewFile.Name);
                                    }
                                        
                                    //NumberOfBrews = NumberOfBrews + dayFolder.GetFiles().Length;
                                }

                            }

                        }
                    }
                }
            }
            return brewsStringList;
        }

        public IList<IBrew> GetBrewsInYearsOld(IList<string> years, int startYear, int endYear, int startDay, int endDay, BrewLoaderAndMaker brewLoaderAndMaker)
        {
            MyAppSettings myAppSettings = MyAppSettings.GetInstance();
            string fileServerPath = myAppSettings.FileServerPath;

            //for each year folder, get brews
            IList<IBrew> brews = new List<IBrew>();
            foreach (string year in years)
            {
                if (FolderWorker.IsSubDirectory(fileServerPath, year))
                {
                    DirectoryInfo yearFolder = new DirectoryInfo(fileServerPath + myAppSettings.FolderSeparator + year);
                    string[] months = Enum.GetNames(typeof(Month));

                    // for each month in year folder, get brews
                    foreach (DirectoryInfo monthFolder in yearFolder.GetDirectories())
                    {
                        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                        // Convert monthName to title case
                        string monthName = textInfo.ToTitleCase(monthFolder.Name).ToString();

                        // If month folder exists then 
                        if (months.Contains(monthName))
                        {
                            // for each day folder in month, get days
                            foreach (DirectoryInfo dayFolder in monthFolder.GetDirectories())
                            {


                                // for each brew file in day, get brew number
                                foreach (FileInfo brewFile in dayFolder.GetFiles())
                                {
                                    // string filePath = appSettings.FileServerPath + GetFilePath(brewId);
                                    // filePath is for the folder containing the brewFile that is why it is renamed folderPath

                                    string folderPath = dayFolder.FullName + myAppSettings.FolderSeparator;

                                    string[] brewFileName = null;

                                    // Split file name to remove extension
                                    brewFileName = brewFile.Name.Split('.');
                                    string brewNumber = brewFileName[0];

                                    StringDateWorker stringDateWorker = StringDateWorker.GetInstance();
                                    string monthId = stringDateWorker.GetMonthNumber(monthName);
                                    string dayId = stringDateWorker.GetTwoDigitNumber(dayFolder.Name);

                                    string startDate = dayId + '.' + monthId + '.' + year;

                                    //BrewingProcessHandler brewingProcessHandler = BrewingProcessHandler.GetInstance();

                                    if (int.Parse(yearFolder.Name) == startYear && int.Parse(dayFolder.Name) >= startDay)
                                    {
                                        brews.Add(brewLoaderAndMaker.CreateBrew(folderPath, brewNumber, startDate));
                                        //brewingProcessHandler.StartNewBrew(startDate,"", brewNumber);
                                    }
                                    else if (int.Parse(yearFolder.Name) < endYear)
                                    {
                                        brews.Add(brewLoaderAndMaker.CreateBrew(folderPath, brewNumber, startDate));
                                    }
                                    else if (int.Parse(yearFolder.Name) == endYear && int.Parse(dayFolder.Name) <= endDay)
                                    {
                                        brews.Add(brewLoaderAndMaker.CreateBrew(folderPath, brewNumber, startDate));
                                    }


                                }

                            }

                        }
                    }
                }
            }
            return brews;
        }
    }

   
}
