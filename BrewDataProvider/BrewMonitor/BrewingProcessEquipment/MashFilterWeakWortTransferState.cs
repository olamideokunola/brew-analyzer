using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashFilterWeakWortTransferState : MashFilterState, IMashFilterState, IStateDescription
    {
        public MashFilterWeakWortTransferState()
        {
        }

        public string GetStateDescription()
        {
            return "MashFilterWeakWortTransferState";
        }

        public string GetShortState()
        {
            return "TransferToWWT";
        }

        public void InitBrew(MashFilter mashFilter, Brew brew)
        {

        }

        public void OnEntry(MashFilter mashFilter, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashFilter mashFilter, Brew brew)
        {
            if (paramText.Equals("WeakWort Transfer to WWT - Finish"))
            {
                MashFilterProcessParameters paramToCheck = MashFilterProcessParameters.PrefillingEndTime;
                MashFilterProcessParameters paramToChange = MashFilterProcessParameters.WeakWortTransferEndTime;
                IMashFilterState newState = mashFilter.MainMashFiltrationState;
                SetProcessStepEndTime(endTime, mashFilter, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartPrefilling(string paramText, string startTime, MashFilter mashFilter, Brew brew)
        {
           
        }
    }
}
