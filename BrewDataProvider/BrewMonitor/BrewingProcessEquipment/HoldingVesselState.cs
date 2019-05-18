using System;
using BrewingModel;
using ProcessEquipmentParameters;


namespace BrewingModel.BrewingProcessEquipment
{
    public class HoldingVesselState
    {
        public HoldingVesselState()
        {
        }

        protected void SetProcessStepEndTime(string endTime, HoldingVessel holdingVessel, Brew brew,
                                  HoldingVesselProcessParameters paramToCheck, 
                                  HoldingVesselProcessParameters paramToChange,
                                  IHoldingVesselState newState)
        {
            string brandName = brew.BrandName;
            string paramToCheckValue = brew.GetProcessParameterValue(ProcessEquipment.HoldingVessel,
                                              paramToCheck.ToString());

            //Complete process step
            if (brandName.Length > 0 &&
               paramToCheckValue.Length > 0)
            {
                Brew nBrew = holdingVessel.Brew;
                string paramName = paramToChange.ToString();
                nBrew.SetProcessParameterValue(ProcessEquipment.HoldingVessel, paramName, endTime);

                //Set new state
                //string newStateString = 
                holdingVessel.SetState(newState);
                //Console.WriteLine("New state is: " + newStateString);
            }
        }
    }
}
