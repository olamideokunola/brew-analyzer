using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class HoldingVesselFillingState : HoldingVesselState, IHoldingVesselState, IStateDescription
    {
        public HoldingVesselFillingState()
        {
        }

        public string GetStateDescription()
        {
            return "HoldingVesselFillingState";
        }

        public string GetShortState()
        {
            return "Filling";
        }

        public void InitBrew(HoldingVessel holdingVessel, Brew brew)
        {

        }

        public void OnEntry(HoldingVessel holdingVessel, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, HoldingVessel holdingVessel, Brew brew)
        {
            if (paramText.Equals("Transfer Time (WC) - Finish"))
            {
                HoldingVesselProcessParameters paramToCheck = HoldingVesselProcessParameters.FillingStartTime;
                HoldingVesselProcessParameters paramToChange = HoldingVesselProcessParameters.TransferToWcEndTime;
                IHoldingVesselState newState = holdingVessel.RinsingState;
                SetProcessStepEndTime(endTime, holdingVessel, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartFilling(string paramText, string startTime, HoldingVessel holdingVessel, Brew brew)
        {
           
        }
    }
}
