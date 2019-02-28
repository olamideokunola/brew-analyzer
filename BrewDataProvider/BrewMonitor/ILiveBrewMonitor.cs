using System;

namespace BrewDataProvider.BrewMonitor
{
    public interface ILiveBrewMonitor
    {
        void StartMonitoring(string filePath, string brandName, string brewNumber);
    }
}
