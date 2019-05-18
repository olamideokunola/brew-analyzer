using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class HoldingVesselIdleState : HoldingVesselState, IHoldingVesselState, IStateDescription
    {
        public HoldingVesselIdleState()
        {
        }

        public string GetStateDescription()
        {
            return "HoldingVesselIdleState";
        }

        public string GetShortState()
        {
            return "Idle";
        }

        public void InitBrew(HoldingVessel holdingVessel, Brew brew)
        {
            string brewNumber = holdingVessel.Brew.BrewNumber;
            if(brew != null)
            {
                if(brewNumber.Length == 0)
                {
                    holdingVessel.InitBrew(brew);
                }
            }
        }

        public void OnEntry(HoldingVessel holdingVessel, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, HoldingVessel holdingVessel, Brew brew)
        {

        }

        public void StartFilling(string paramText, string startTime, HoldingVessel holdingVessel, Brew brew)
        {
            string brewNumber = brew.BrewNumber;
            string brandName = brew.BrandName;
            string holdingVesselBrewNumber = holdingVessel.Brew.BrewNumber;
            HoldingVesselProcessParameters param = HoldingVesselProcessParameters.FillingStartTime;
            string paramName = param.ToString();

            //Start Mashing In
            if (brandName.Length > 0 &&
               brewNumber.Length > 0 &&
               brewNumber == holdingVesselBrewNumber &&
               paramText.Equals("Start Filling - Finish"))
            {
                holdingVessel.Brew.SetProcessParameterValue(ProcessEquipment.HoldingVessel, paramName, startTime);

                //Set new state
                holdingVessel.SetState(holdingVessel.FillingState);
            }

        }
    }
}