using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class WortCopperHeatingState : WortCopperState, IWortCopperState, IStateDescription
    {
        public WortCopperHeatingState()
        {
        }

        public string GetStateDescription()
        {
            return "WortCopperHeatingState";
        }

        public string GetShortState()
        {
            return "Heating";
        }

        public void InitBrew(WortCopper holdingVessel, Brew brew)
        {

        }

        public void OnEntry(WortCopper holdingVessel, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, WortCopper holdingVessel, Brew brew)
        {
            if (paramText.Equals("Heating Time (Boiling) - Finish"))
            {
                WortCopperProcessParameters paramToCheck = WortCopperProcessParameters.HeatingStartTime;
                WortCopperProcessParameters paramToChange = WortCopperProcessParameters.HeatingEndTime;
                IWortCopperState newState = holdingVessel.BoilingState;
                SetProcessStepEndTime(endTime, holdingVessel, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartCasting(string paramText, string startTime, WortCopper wortCopper, Brew brew)
        {
           
        }

        public void StartHeating(string paramText, string startTime, WortCopper holdingVessel, Brew brew)
        {

        }
    }
}
