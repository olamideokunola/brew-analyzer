using BrewDataProvider.ActiveBrewMonitor;
using BrewingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewDataProvider
{
    interface IBrewsGetter
    {
        IList<IBrew> GetBrewsInYearsOld(IList<string> years, int startYear, int endYear, int startDay, int endDay, BrewLoaderAndMaker brewLoaderAndMaker);
        // int GetBrewsInYears(IList<string> years, int startYear, int endYear, int startDay, int endDay, BrewLoaderAndMaker brewLoaderAndMaker);
        IList<string> GetBrewsInYears(IList<string> years, DateTime startDate, DateTime endDate);
        IList<IBrew> GetBrewsInPeriod(IList<string> years, DateTime startDate, DateTime endDate, BrewLoaderAndMaker brewLoaderAndMaker);
    }
}
