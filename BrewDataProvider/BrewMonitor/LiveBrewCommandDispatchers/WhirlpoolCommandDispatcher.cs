using System;
using System.Collections.Generic;
//using System.Collections.Concurrent;
using BrewingModel;
using BrewingModel.BrewMonitor.LiveBrewCommands;

namespace BrewingModel.BrewMonitor.LiveBrewCommandDispatchers
{
    public class WhirlpoolCommandDispatcher : LiveBrewCommandDispatcher
    {
        private static WhirlpoolCommandDispatcher _uniqueInstance = null;

        //lazy construction of instance
        public static WhirlpoolCommandDispatcher GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new WhirlpoolCommandDispatcher();
            }

            return _uniqueInstance;
        }

        //hidden constructor to allow Singleton
        private WhirlpoolCommandDispatcher()
        {
        }

        public override void CreateLiveBrewCommands(string fieldName, string fieldValue, Brew brew, string fieldSection)
        {
            //this.liveBrewCommands = new BlockingCollection<LiveBrewCommand>();
            this.liveBrewCommands = new List<LiveBrewCommand>();
            switch (fieldName)
            {
                case "Start Casting - Finish":
                    liveBrewCommand = new StartWortCopperCastingCommand(brew.StartTime, brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    AddToLiveBrewCommands(liveBrewCommand);
                    liveBrewCommand = new StartWhirlpoolCastingCommand(brew.StartTime, brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    AddToLiveBrewCommands(liveBrewCommand);
                    break;
                case "Casting Time - Finish":
                    liveBrewCommand = new CompleteWortCopperProcessStepCommand(brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    AddToLiveBrewCommands(liveBrewCommand);
                    liveBrewCommand = new CompleteWhirlpoolProcessStepCommand(brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    AddToLiveBrewCommands(liveBrewCommand);
                    break;
                case "Rest Time - Finish":
                case "Wort Cooling - Finish":
                case "Trub Removal - Finish":
                case "Whirlpool Ready at - Finish":
                    liveBrewCommand = new CompleteWhirlpoolProcessStepCommand(brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    AddToLiveBrewCommands(liveBrewCommand);
                    break;
            }
        }
    }
}