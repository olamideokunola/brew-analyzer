using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewDataProvider.ActiveBrewMonitor;
using BrewDataProvider.BrewMonitor;
using BrewingModel;

namespace BrewDataProvider
{
    public class BrewDataProvider : IDataProvider
        /// 
        /// Class that represents an abstraction that provides brew(s) from brew file storage  
        /// 
    {
        private BrewLoaderAndMaker brewLoader;
        private IFileParser fileParser;

        public BrewDataProvider ()
        {
            fileParser = new FileParser();
            brewLoader = new BrewLoaderAndMaker(fileParser);
        }

        // Methods
        private List<string> GetMonths(DateTime startDate, DateTime endDate)
        {
            return null; // TODO
        }

        private List<int> GetDays(string month)
        {
            return null; // TODO
        }

        private List<int> GetBrewNumbersOnDay(int day)
        {
            return null; // TODO
        }

        private IList<Brew> GetBrewIds(string month) 
        {
            // TODO
           return null;
        }


        // IDataProvider Interface Implementation
        public IBrew GetBrew(string brewId)
        {
            throw new NotImplementedException();
        }

        public IList<IBrew> SelectBrews(DateTime startDate, DateTime endDate)
        {
            // TODO
            return null;
        }

        public int GetNumberOfBrews()
        {
            return 0;
        }
    }
}
