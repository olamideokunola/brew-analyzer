using BrewDataProvider.BrewMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewDataProvider.ActiveBrewMonitor
{
    class BrewMaker
    {
        public BrewMaker()
        {

        }

        public ActiveBrewInProcess CreateBrew(BrewInProcess brewInProcess, string startDate)
        {
            ActiveBrewInProcess activeBrewInProcess = new ActiveBrewInProcess(brewInProcess, startDate);
            return activeBrewInProcess;
        }
    }
}
