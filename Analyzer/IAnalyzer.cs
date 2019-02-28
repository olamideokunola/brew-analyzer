using ObserverSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public interface IAnalyzer
    {
        void SetDates(DateTime startDate, DateTime endDate);
        void StartTrendAnalysis();
        void RunAnalysis(string fileDestination);
        void AttachTrendAnalyzerObserver(IObserver observer);
    }
}
