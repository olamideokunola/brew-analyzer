using System;
using System.Collections.Generic;

namespace BrewingModel.Datasources
{
    public abstract class Period
    {
        protected string periodName;
        protected string year;
        protected  string month;

        protected IDictionary<string, IBrew> _brews = new Dictionary<string, IBrew>();

        public abstract void AddBrew(IBrew brew);
        public abstract void RemoveBrew(IBrew brew);
        public abstract void UpdateBrew(IBrew brew);
        public abstract IBrew GetBrew(string brewNumber);
        public abstract IBrew GetBrewWithProcessParameters(IBrew brew);

        public abstract void LoadBrews();

        public string PeriodName
        {
            get
            {
                return periodName;
            }
        }

        public string Year
        {
            get
            {
                return year;
            }
        }

        public string Month
        {
            get
            {
                return month;
            }
        }

        public IDictionary<string, IBrew> Brews
        {
            get
            {
                return _brews;
            }
        }
    }
}
