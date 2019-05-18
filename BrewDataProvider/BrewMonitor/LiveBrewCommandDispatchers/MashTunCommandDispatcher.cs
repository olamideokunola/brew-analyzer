using System;
using BrewingModel;
using BrewingModel.BrewMonitor.LiveBrewCommands;

namespace BrewingModel.BrewMonitor.LiveBrewCommandDispatchers
{
    public class MashTunCommandDispatcher : LiveBrewCommandDispatcher
    {
        private static MashTunCommandDispatcher _uniqueInstance = null;

        //lazy construction of instance
        public static MashTunCommandDispatcher GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new MashTunCommandDispatcher();
            }

            return _uniqueInstance;
        }

        //hidden constructer to allow Singleton
        private MashTunCommandDispatcher()
        {
        }

        public override void CreateLiveBrewCommands(string fieldName, string fieldValue, Brew brew, string fieldSection)
        {
            switch (fieldName)
            {
                case "Tranpsort Time - Finish":
                    liveBrewCommand = new StartMashTunMashingInCommand(brew.StartDate, brew.StartTime, brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    AddToLiveBrewCommands(liveBrewCommand);
                    break;
                case "Mash in Time - Finish":
                case "Protein Rest Time - Finish":
                case "Transfer Time from MC - Finish":
                case "Sacha. Rest time - Finish":
                case "Heating Time - Finish":
                case "Mash Tun ready at - Finish":
                    liveBrewCommand = new CompleteMashTunProcessStepCommand(brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    AddToLiveBrewCommands(liveBrewCommand);
                    break;
                    //case "Mash Tun ready at - Finish":
                    //liveBrewCommand = new StartMashTunMashingInCommand(brew.StartDate, brew.StartTime, brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    //break;
            }
        }
    }
}

