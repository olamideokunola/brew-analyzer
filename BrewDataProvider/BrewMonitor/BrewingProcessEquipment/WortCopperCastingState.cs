using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class WortCopperCastingState : WortCopperState, IWortCopperState, IStateDescription
    {
        public WortCopperCastingState()
        {
        }

        public string GetStateDescription()
        {
            return "WortCopperCastingState";
        }

        public string GetShortState()
        {
            return "Casting";
        }

        public void InitBrew(WortCopper wortCopper, Brew brew)
        {

        }

        public void OnEntry(WortCopper wortCopper, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, WortCopper wortCopper, Brew brew)
        {
            if (paramText.Equals("Casting Time - Finish"))
            {
                WortCopperProcessParameters paramToCheck = WortCopperProcessParameters.CastingStartTime;
                WortCopperProcessParameters paramToChange = WortCopperProcessParameters.CastingEndTime;
                IWortCopperState newState = wortCopper.IdleState;
                SetProcessStepEndTime(endTime, wortCopper, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartCasting(string paramText, string startTime, WortCopper wortCopper, Brew brew)
        {
            
        }

        public void StartHeating(string paramText, string startTime, WortCopper wortCopper, Brew brew)
        {

        }
    }
}
