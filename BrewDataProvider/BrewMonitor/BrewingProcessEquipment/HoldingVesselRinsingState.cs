using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class HoldingVesselRinsingState : HoldingVesselState, IHoldingVesselState, IStateDescription
    {
        public HoldingVesselRinsingState()
        {
        }

        public string GetStateDescription()
        {
            return "HoldingVesselRinsingState";
        }

        public string GetShortState()
        {
            return "Rinsing";
        }

        public void InitBrew(HoldingVessel holdingVessel, Brew brew)
        {

        }

        public void OnEntry(HoldingVessel holdingVessel, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, HoldingVessel holdingVessel, Brew brew)
        {
            if (paramText.Equals("Holding Vessle empty at - Finish"))
            {
                HoldingVesselProcessParameters paramToCheck = HoldingVesselProcessParameters.TransferToWcEndTime;
                HoldingVesselProcessParameters paramToChange = HoldingVesselProcessParameters.EmptyAtTime;
                IHoldingVesselState newState = holdingVessel.IdleState;
                SetProcessStepEndTime(endTime, holdingVessel, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartFilling(string paramText, string startTime, HoldingVessel holdingVessel, Brew brew)
        {
           
        }
    }
}
