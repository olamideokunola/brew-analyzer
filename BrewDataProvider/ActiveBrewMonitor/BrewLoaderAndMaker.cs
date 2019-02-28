using BrewDataProvider.BrewingModel;
using BrewDataProvider.BrewMonitor;
using BrewingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewDataProvider.ActiveBrewMonitor
{
    class BrewLoaderAndMaker : BrewLoader
    {

        private BrewMaker brewMaker;

        public BrewLoaderAndMaker(IFileParser fileParser) : base(fileParser)
        {
            brewMaker = new BrewMaker();
        }

        public IBrew CreateBrew(BrewInProcess brewInProcess)
        {
            ActiveBrewInProcess activeBrewInProcess = brewMaker.CreateBrew(brewInProcess);
            return activeBrewInProcess.Brew;
        }

    }
}
