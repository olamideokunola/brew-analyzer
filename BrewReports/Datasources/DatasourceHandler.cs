using System;
using System.Collections.Generic;

namespace BrewingModel.Datasources
{
    public class DatasourceHandler
    {
        //Singleton
        private static DatasourceHandler _uniqueInstance = null;

        //lazy construction of instance
        public static DatasourceHandler GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new DatasourceHandler();
            }

            return _uniqueInstance;
        }

        public static DatasourceHandler GetInstance(Datasource datasource)
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new DatasourceHandler(datasource);
            }
            _datasource = datasource;
            _datasource.LoadPeriods();
            return _uniqueInstance;
        }

        private IDictionary<string, Period> _periods = new Dictionary<string, Period>();
        private static Datasource _datasource;

        public Datasource Datasource
        {
            get { return _datasource; }
            set
            {
                _datasource = value;
                _datasource.LoadPeriods();
            }
        }

        private DatasourceHandler(Datasource datasource)
        {
            Datasource = datasource;

        }

        private DatasourceHandler()
        {
        }

        public void SaveBrew(IBrew brew)
        {
            _datasource.SaveBrew(brew);
        }

        public Brew GetBrewWithProcessParameters(IBrew brew)
        {
            return (BrewingModel.Brew)_datasource.GetBrewWithProcessParameters(brew);
        }

        public Period LoadPeriod(string year, Month month)
        {
            return _datasource.GetPeriod(year, month);
        }

        public void LoadPeriods()
        {
            _datasource.LoadPeriods();
        }
    }
}
