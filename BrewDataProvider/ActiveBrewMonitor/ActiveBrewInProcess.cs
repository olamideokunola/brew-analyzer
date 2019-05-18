using BrewDataProvider.BrewMonitor;
using BrewingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewDataProvider.ActiveBrewMonitor
{
    class ActiveBrewInProcess
    {
        private IBrew brew;
        private BrewInProcess brewInProcess;

        public IBrew Brew { get => brew; }

        public ActiveBrewInProcess(BrewInProcess brewInProcess, string startDate)
        {
            this.brewInProcess = brewInProcess;
            CreateBrewFromBrewInProcess(startDate);
        }

        private void CreateBrewFromBrewInProcess(string startDate)
        {         
            brew = new BrewInProcessBrew(brewInProcess, startDate);
        }
    }
}
