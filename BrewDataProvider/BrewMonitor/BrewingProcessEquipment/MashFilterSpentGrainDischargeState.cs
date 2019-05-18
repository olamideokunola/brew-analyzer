using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashFilterSpentGrainDischargeState : MashFilterState, IMashFilterState, IStateDescription
    {
        public MashFilterSpentGrainDischargeState()
        {
        }

        public string GetStateDescription()
        {
            return "MashFilterSpentGrainDischargeState";
        }

        public string GetShortState()
        {
            return "SG Discharge";
        }

        public void InitBrew(MashFilter mashFilter, Brew brew)
        {

        }

        public void OnEntry(MashFilter mashFilter, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashFilter mashFilter, Brew brew)
        {
            if (paramText.Equals("Spent Grain Discharge - Finish"))
            {
                MashFilterProcessParameters paramToCheck = MashFilterProcessParameters.DrippingEndTime;
                MashFilterProcessParameters paramToChange = MashFilterProcessParameters.SpentGrainDischargeEndTime;
                IMashFilterState newState = mashFilter.CleaningState;
                SetProcessStepEndTime(endTime, mashFilter, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartPrefilling(string paramText, string startTime, MashFilter mashFilter, Brew brew)
        {
           
        }
    }
}
