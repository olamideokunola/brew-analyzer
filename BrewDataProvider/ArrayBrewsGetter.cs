using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BrewDataProvider.ActiveBrewMonitor;
using BrewingModel;
using BrewingModel.Settings;
using Util;

namespace BrewDataProvider
{
    public class ArrayBrewsGetter : IBrewsGetter
    {
        // private MyAppSettings myAppSettings = MyAppSettings.GetInstance();

        public IList<IBrew> GetBrewsInYears(IList<string> years, int startYear, int endYear, int startDay, int endDay, BrewLoaderAndMaker brewLoaderAndMaker)
        {
            // Get BrewsFileNamesArray from Years
            String[] brewFileNames = GetBrewFileNames(years);

            // for each brew file, create brew and add to brewslist
            IList<IBrew> brews = GetBrewWithFileNames(brewFileNames);

            return brews;
        }

        private IList<IBrew> GetBrewWithFileNames(string[] brewFileNames)
        {
            throw new NotImplementedException();
        }

        public string[] GetBrewFileNames(IList<string> years)
        {
            MyAppSettings myAppSettings = MyAppSettings.GetInstance();
            string fileServerPath = myAppSettings.FileServerPath;


            IList<string> brewFileNamesList = new List<string>();

            foreach (string year in years)
            {
                // if year exists as a folder in server
                if (FolderWorker.IsSubDirectory(fileServerPath, year))
                {
                    brewFileNamesList.Add(year);
                }
            }

            return brewFileNamesList.ToArray<string>();
        }

        public IList<IBrew> GetBrewsInYearsOld(IList<string> years, int startYear, int endYear, int startDay, int endDay, BrewLoaderAndMaker brewLoaderAndMaker)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetBrewsInYears(IList<string> years, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
