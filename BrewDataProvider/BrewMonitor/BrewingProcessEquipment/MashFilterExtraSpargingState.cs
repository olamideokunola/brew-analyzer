using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashFilterExtraSpargingState : MashFilterState, IMashFilterState, IStateDescription
    {
        public MashFilterExtraSpargingState()
        {
        }

        public string GetStateDescription()
        {
            return "MashFilterExtraSpargingState";
        }

        public string GetShortState()
        {
            return "ExtraSparging";
        }

        public void InitBrew(MashFilter mashFilter, Brew brew)
        {

        }

        public void OnEntry(MashFilter mashFilter, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashFilter mashFilter, Brew brew)
        {
            if (paramText.Equals("Extra Sparging Time - Finish"))
            {
                MashFilterProcessParameters paramToCheck = MashFilterProcessParameters.SpargingToWWTEndTime;
                MashFilterProcessParameters paramToChange = MashFilterProcessParameters.ExtraSpargingEndTime;
                IMashFilterState newState = mashFilter.DrippingState;
                SetProcessStepEndTime(endTime, mashFilter, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartPrefilling(string paramText, string startTime, MashFilter mashFilter, Brew brew)
        {
           
        }
    }
}
