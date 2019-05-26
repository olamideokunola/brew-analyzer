using System;
using System.Collections.Generic;
using System.IO;
using BrewingModel;
//using OfficeOpenXml;

namespace BrewingModel.Datasources
{
    public abstract class Datasource
    {
        /// <summary>
        /// The Datasource allows access to the data store of processed brews
        /// It is made up of periods, which represent each month of production
        /// It is represented on the system as a folder structure
        /// </summary>

        protected string connectionString;
        protected IDictionary<string, Period> periods = new Dictionary<string, Period>();

        protected string fileName;
        protected FileInfo template;

        public IDictionary<string, Period> Periods
        {
            get
            {
                return periods;
            }
        }

        public string ConnectionString { get => connectionString; set => connectionString = value; }


        // Methods
        public abstract Period CreatePeriod(IBrew brew);
        public abstract Period CreatePeriod(string year, Month month);
        public abstract Period CreatePeriod(string year, string month);

        public abstract void AddPeriod(Period period);

        public abstract void UpdatePeriod(Period period);

        public abstract void DeletePeriod(Period period);

        public abstract Period GetPeriod(string year, Month month);
        public abstract Period GetPeriod(IBrew brew);

        public abstract Period LoadPeriod(string year, string month);
        public abstract void LoadPeriods();

        public abstract IBrew GetBrewWithProcessParameters(IBrew brew);

        public abstract string SaveBrew(IBrew brew);

    }
}
