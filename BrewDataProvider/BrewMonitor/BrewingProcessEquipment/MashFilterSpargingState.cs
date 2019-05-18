using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashFilterSpargingState : MashFilterState, IMashFilterState, IStateDescription
    {
        public MashFilterSpargingState()
        {
        }

        public string GetStateDescription()
        {
            return "MashFilterSpargingState";
        }

        public string GetShortState()
        {
            return "Sparging";
        }

        public void InitBrew(MashFilter mashFilter, Brew brew)
        {

        }

        public void OnEntry(MashFilter mashFilter, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashFilter mashFilter, Brew brew)
        {
            if (paramText.Equals("Sparging Time - Finish"))
            {
                MashFilterProcessParameters paramToCheck = MashFilterProcessParameters.MainMashFiltrationEndTime;
                MashFilterProcessParameters paramToChange = MashFilterProcessParameters.SpargingEndTime;
                IMashFilterState newState = mashFilter.SpargingToWwtState;
                SetProcessStepEndTime(endTime, mashFilter, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartPrefilling(string paramText, string startTime, MashFilter mashFilter, Brew brew)
        {
           
        }
    }
}
