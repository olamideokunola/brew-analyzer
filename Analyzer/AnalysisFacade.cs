﻿using BrewDataProvider;
using ObserverSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public class AnalysisFacade : IAnalyzer
    {

        private TrendAnalyzer trendAnalyzer;
        private IList<string> brewsStringList = new List<string>();

        public IList<string> BrewsStringList { get => brewsStringList; }

        public AnalysisFacade(IDataProvider dataProvider)
        {
            trendAnalyzer = new TrendAnalyzer(dataProvider);
        }

        public void RunAnalysis(string fileDestination)
        {
            trendAnalyzer.RunAnalysis(fileDestination);
        }

        public void SetDates(DateTime startDate, DateTime endDate)
        {
            trendAnalyzer.SetDates(startDate, endDate);
            brewsStringList = trendAnalyzer.BrewsStringList;
        }

        //public void StartTrendAnalysis()
        //{
        //    trendAnalyzer.StartTrendAnalysis();
        //}

        public void AttachTrendAnalyzerObserver(IObserver observer)
        {
            trendAnalyzer.Attach(observer);
        }
    }
}
