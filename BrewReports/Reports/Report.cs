using System;
using System.Collections.Generic;
using BrewingModel.Datasources;

namespace BrewingModel.Reports
{
    public class Report
    /// Abstract Class representing a report
    /// wraps a period
    {
        // Fields
        protected string reportName;
        protected string year;
        protected string month;
        protected Period period;
        protected IDictionary<string, IBrew> _brews = new Dictionary<string, IBrew>();

        // Constructor
        public Report()
        {
        }

        // Getters & Setters
        public string ReportName
        {
            get
            {
                return reportName;
            }
        }

        public string Year
        {
            get
            {
                return period.Year;
            }
        }

        public string Month
        {
            get
            {
                return period.Month;
            }
        }

        public Period Period
        {
            get
            {
                return period;
            }
        }

        public IDictionary<string, IBrew> Brews
        {
            get
            {
                return period.Brews;
            }
        }

    }
}
