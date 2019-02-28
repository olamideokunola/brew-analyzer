using BrewDataProvider.BrewMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewDataProvider.BrewingModel
{
    class BrewMaker
    {
        public BrewMaker()
        {

        }

        public ActiveBrewInProcess CreateBrew(BrewInProcess brewInProcess)
        {
            ActiveBrewInProcess activeBrewInProcess = new ActiveBrewInProcess(brewInProcess);
            return activeBrewInProcess;
        }
    }
}
