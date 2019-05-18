using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class WortCopperBoilingState : WortCopperState, IWortCopperState, IStateDescription
    {
        public WortCopperBoilingState()
        {
        }

        public string GetStateDescription()
        {
            return "WortCopperBoilingState";
        }

        public string GetShortState()
        {
            return "Boiling";
        }

        public void InitBrew(WortCopper holdingVessel, Brew brew)
        {

        }

        public void OnEntry(WortCopper holdingVessel, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, WortCopper holdingVessel, Brew brew)
        {
            if (paramText.Equals("Wort Boiling - Finish"))
            {
                WortCopperProcessParameters paramToCheck = WortCopperProcessParameters.HeatingEndTime;
                WortCopperProcessParameters paramToChange = WortCopperProcessParameters.BoilingEndTime;
                IWortCopperState newState = holdingVessel.ExtraBoilingState;
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
