using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewingModel;

namespace BrewDataProvider
{
    public interface IDataProvider
    {
        IBrew GetBrew(string brewId);
        IList<IBrew> SelectBrewsOld(DateTime startDate, DateTime endDate);
        int GetNumberOfBrews();
        int GetNumberOfBrews(DateTime startDate, DateTime endDate);
        IList<string> BrewsStringList { get; }
        IList<IBrew> GetBrews(DateTime startDate, DateTime endDate);
    }
}
