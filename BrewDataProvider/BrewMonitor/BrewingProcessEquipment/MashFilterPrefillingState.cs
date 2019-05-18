using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashFilterPrefillingState : MashFilterState, IMashFilterState, IStateDescription
    {
        public MashFilterPrefillingState()
        {
        }

        public string GetStateDescription()
        {
            return "MashFilterPrefillingState";
        }

        public string GetShortState()
        {
            return "Prefilling";
        }

        public void InitBrew(MashFilter mashFilter, Brew brew)
        {

        }

        public void OnEntry(MashFilter mashFilter, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashFilter mashFilter, Brew brew)
        {
            if (paramText.Equals("Prefilling Time - Finish"))
            {
                MashFilterProcessParameters paramToCheck = MashFilterProcessParameters.PrefillingStartTime;
                MashFilterProcessParameters paramToChange = MashFilterProcessParameters.PrefillingEndTime;
                IMashFilterState newState = mashFilter.WeakWortTransferState;
                SetProcessStepEndTime(endTime, mashFilter, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartPrefilling(string paramText, string startTime, MashFilter mashFilter, Brew brew)
        {
           
        }
    }
}
