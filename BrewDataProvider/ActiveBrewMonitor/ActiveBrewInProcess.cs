using BrewDataProvider.BrewMonitor;
using BrewingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewDataProvider.BrewingModel
{
    class ActiveBrewInProcess
    {
        private IBrew brew;
        private BrewInProcess brewInProcess;

        public IBrew Brew { get => brew; }

        public ActiveBrewInProcess(BrewInProcess brewInProcess)
        {
            this.brewInProcess = brewInProcess;
            CreateBrewFromBrewInProcess();
        }

        private void CreateBrewFromBrewInProcess()
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
