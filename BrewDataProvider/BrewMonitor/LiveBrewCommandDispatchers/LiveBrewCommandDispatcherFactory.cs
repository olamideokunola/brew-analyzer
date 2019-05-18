/*
 * LiveBrewManager creates the LiveBrewCommandDispatchers according to fieldSection
 */

using System;

namespace BrewingModel.BrewMonitor.LiveBrewCommandDispatchers
{
    public class LiveBrewCommandDispatcherFactory
    {
        private static LiveBrewCommandDispatcherFactory _uniqueInstance = null;
        LiveBrewCommandDispatcher liveBrewCommandDispatcher;

        //lazy construction of instance
        public static LiveBrewCommandDispatcherFactory GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new LiveBrewCommandDispatcherFactory();
            }

            return _uniqueInstance;
        }

        //hidden constructer to allow Singleton
        private LiveBrewCommandDispatcherFactory()
        {
        }

        public LiveBrewCommandDispatcher GetLiveBrewCommandDispatcher(string fieldSection)
        {
            switch (fieldSection)
            {
                case "Weigh bin Mash Copper":
                case "MASH COPPER":
                    liveBrewCommandDispatcher = MashCopperCommandDispatcher.GetInstance();
                    break;
                case "WEIGHBIN MASHTUN":
                case "MASH TUN":
                   liveBrewCommandDispatcher = MashTunCommandDispatcher.GetInstance();
                    break;
                case "MASH FILTER":
                    liveBrewCommandDispatcher = MashFilterCommandDispatcher.GetInstance();
                    break;
                case "HOLDING VESSEL":
                case "HOLDING VESSEL TO WORT COPPER":
                    liveBrewCommandDispatcher = HoldingVesselCommandDispatcher.GetInstance();
                    break;
                case "WORT COPPER":
                    liveBrewCommandDispatcher = WortCopperCommandDispatcher.GetInstance();
                    break;
                case "WHIRLPOOL":
                    liveBrewCommandDispatcher = WhirlpoolCommandDispatcher.GetInstance();
                    break;
            }

            return liveBrewCommandDispatcher;
        }
    }
}
