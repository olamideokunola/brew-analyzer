using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashFilterSpargingToWwtState : MashFilterState, IMashFilterState, IStateDescription
    {
        public MashFilterSpargingToWwtState()
        {
        }

        public string GetStateDescription()
        {
            return "MashFilterSpargingToWwtState";
        }

        public string GetShortState()
        {
            return "SpargingToWWT";
        }

        public void InitBrew(MashFilter mashFilter, Brew brew)
        {

        }

        public void OnEntry(MashFilter mashFilter, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashFilter mashFilter, Brew brew)
        {
            if (paramText.Equals("Sparging to WWT Time - Finish"))
            {
                MashFilterProcessParameters paramToCheck = MashFilterProcessParameters.SpargingEndTime;
                MashFilterProcessParameters paramToChange = MashFilterProcessParameters.SpargingToWWTEndTime;
                IMashFilterState newState = mashFilter.ExtraSpargingState;
                SetProcessStepEndTime(endTime, mashFilter, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartPrefilling(string paramText, string startTime, MashFilter mashFilter, Brew brew)
        {
           
        }
    }
}
