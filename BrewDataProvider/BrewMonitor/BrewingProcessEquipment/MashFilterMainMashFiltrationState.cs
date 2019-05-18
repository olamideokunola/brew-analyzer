using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashFilterMainMashFiltrationState : MashFilterState, IMashFilterState, IStateDescription
    {
        public MashFilterMainMashFiltrationState()
        {
        }

        public string GetStateDescription()
        {
            return "MashFilterMainMashFiltrationState";
        }

        public string GetShortState()
        {
            return "Filtration";
        }

        public void InitBrew(MashFilter mashFilter, Brew brew)
        {

        }

        public void OnEntry(MashFilter mashFilter, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashFilter mashFilter, Brew brew)
        {
            if (paramText.Equals("Main Mash Filtration Time - Finish"))
            {
                MashFilterProcessParameters paramToCheck = MashFilterProcessParameters.WeakWortTransferEndTime;
                MashFilterProcessParameters paramToChange = MashFilterProcessParameters.MainMashFiltrationEndTime;
                IMashFilterState newState = mashFilter.SpargingState;
                SetProcessStepEndTime(endTime, mashFilter, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartPrefilling(string paramText, string startTime, MashFilter mashFilter, Brew brew)
        {
           
        }
    }
}
