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
        IList<IBrew> SelectBrews(DateTime startDate, DateTime endDate);
        int GetNumberOfBrews();
    }
}
