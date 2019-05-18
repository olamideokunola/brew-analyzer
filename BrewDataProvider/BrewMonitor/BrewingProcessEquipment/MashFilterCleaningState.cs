using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashFilterCleaningState : MashFilterState, IMashFilterState, IStateDescription
    {
        public MashFilterCleaningState()
        {
        }

        public string GetStateDescription()
        {
            return "MashFilterCleaningState";
        }

        public string GetShortState()
        {
            return "Cleaning";
        }

        public void InitBrew(MashFilter mashFilter, Brew brew)
        {

        }

        public void OnEntry(MashFilter mashFilter, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashFilter mashFilter, Brew brew)
        {
            if (paramText.Equals("Mash Filter Ready at - Finish"))
            {
                MashFilterProcessParameters paramToCheck = MashFilterProcessParameters.SpentGrainDischargeEndTime;
                MashFilterProcessParameters paramToChange = MashFilterProcessParameters.ReadyAtTime;
                IMashFilterState newState = mashFilter.IdleState;
                SetProcessStepEndTime(endTime, mashFilter, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartPrefilling(string paramText, string startTime, MashFilter mashFilter, Brew brew)
        {
           
        }
    }
}
