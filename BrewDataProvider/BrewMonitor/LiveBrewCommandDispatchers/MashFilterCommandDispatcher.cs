using System;
using BrewingModel;
using BrewingModel.BrewMonitor.LiveBrewCommands;

namespace BrewingModel.BrewMonitor.LiveBrewCommandDispatchers
{
    public class MashFilterCommandDispatcher : LiveBrewCommandDispatcher
    {
        private static MashFilterCommandDispatcher _uniqueInstance = null;

        //lazy construction of instance
        public static MashFilterCommandDispatcher GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new MashFilterCommandDispatcher();
            }

            return _uniqueInstance;
        }

        //hidden constructer to allow Singleton
        private MashFilterCommandDispatcher()
        {
        }

        public override void CreateLiveBrewCommands(string fieldName, string fieldValue, Brew brew, string fieldSection)
        {
            switch (fieldName)
            {
                case "Start Prefilling - Finish":
                    liveBrewCommand = new StartMashFilterPrefillingCommand(brew.StartDate, brew.StartTime, brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    AddToLiveBrewCommands(liveBrewCommand);
                    break;
                case "Prefilling Time - Finish":
                case "WeakWort Transfer to WWT - Finish":
                case "Main Mash Filtration Time - Finish":
                case "Sparging Time - Finish":
                case "Sparging to WWT Time - Finish":
                case "Extra Sparging Time - Finish":
                case "Dripping Time - Finish":
                case "Spent Grain Discharge - Finish":
                case "Cleaning MF - Finish":
                case "Mash Filter Ready at - Finish":
                    liveBrewCommand = new CompleteMashFilterProcessStepCommand(brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    AddToLiveBrewCommands(liveBrewCommand);
                    break;
                    //case "Mash Filter ready at - Finish":
                    //liveBrewCommand = new StartMashFilterMashingInCommand(brew.StartDate, brew.StartTime, brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    //break;
            }
        }
    }
}

