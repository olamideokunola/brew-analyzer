using System;
using BrewingModel;
using BrewingModel.BrewMonitor.LiveBrewCommands;

namespace BrewingModel.BrewMonitor.LiveBrewCommandDispatchers
{
    public class WortCopperCommandDispatcher : LiveBrewCommandDispatcher
    {
        private static WortCopperCommandDispatcher _uniqueInstance = null;

        //lazy construction of instance
        public static WortCopperCommandDispatcher GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new WortCopperCommandDispatcher();
            }

            return _uniqueInstance;
        }

        //hidden constructor to allow Singleton
        private WortCopperCommandDispatcher()
        {
        }

        public override void CreateLiveBrewCommands(string fieldName, string fieldValue, Brew brew, string fieldSection)
        {
            switch (fieldName)
            {
                case "Start Heating(Boiling) - Finish":
                    liveBrewCommand = new StartWortCopperHeatingCommand(brew.StartDate, brew.StartTime, brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    AddToLiveBrewCommands(liveBrewCommand);
                    break;
                // from whirlpool
                case "Start Casting - Finish":
                    liveBrewCommand = new StartWortCopperCastingCommand(brew.StartTime, brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    AddToLiveBrewCommands(liveBrewCommand);
                    break;
                case "Heating Time (Boiling) - Finish":
                case "Wort Boiling - Finish":
                case "Extra Boiling Time - Finish":
                case "Casting to Whirpool - Finish":
                case "Wort Copper Finish(No cip) - Finish":
                case "Daily Cleaning - Finish":
                case "Wort Copper Finish(with cip) - Finish":
                    liveBrewCommand = new CompleteWortCopperProcessStepCommand(brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    AddToLiveBrewCommands(liveBrewCommand);
                    break;
            }
        }
    }
}

