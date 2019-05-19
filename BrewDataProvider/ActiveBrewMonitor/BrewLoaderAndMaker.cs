//using BrewDataProvider.BrewingModel;
using BrewDataProvider.BrewMonitor;
using BrewingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewDataProvider.ActiveBrewMonitor
{
    public class BrewLoaderAndMaker : BrewLoader
    {

        private BrewMaker brewMaker;

        public BrewLoaderAndMaker(IFileParser fileParser) : base(fileParser)
        {
            brewMaker = new BrewMaker();
        }

        public IBrew CreateBrew(string filePath, string brewNumber, string startDate)
        {
            return CreateBrew(GetBrewInProcess(filePath, brewNumber), startDate);
        }

        public IBrew CreateBrewOld(string filePath, string brewNumber, string startDate)
        {
            CreateBrewInProcess(filePath, brewNumber);
            return CreateBrew(GetBrewInProcessOld(filePath, brewNumber), startDate);
        }

        private IBrew CreateBrew(BrewInProcess brewInProcess, string startDate)
        {
            ActiveBrewInProcess activeBrewInProcess = brewMaker.CreateBrew(brewInProcess, startDate);
            return activeBrewInProcess.Brew;
        }

    }
}
