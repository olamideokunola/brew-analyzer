using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashFilterDrippingState : MashFilterState, IMashFilterState, IStateDescription
    {
        public MashFilterDrippingState()
        {
        }

        public string GetStateDescription()
        {
            return "MashFilterDrippingState";
        }

        public string GetShortState()
        {
            return "Dripping";
        }

        public void InitBrew(MashFilter mashFilter, Brew brew)
        {

        }

        public void OnEntry(MashFilter mashFilter, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashFilter mashFilter, Brew brew)
        {
            if (paramText.Equals("Dripping Time - Finish"))
            {
                MashFilterProcessParameters paramToCheck = MashFilterProcessParameters.ExtraSpargingEndTime;
                MashFilterProcessParameters paramToChange = MashFilterProcessParameters.DrippingEndTime;
                IMashFilterState newState = mashFilter.SpentGrainDischargeState;
                SetProcessStepEndTime(endTime, mashFilter, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartPrefilling(string paramText, string startTime, MashFilter mashFilter, Brew brew)
        {
           
        }
    }
}
