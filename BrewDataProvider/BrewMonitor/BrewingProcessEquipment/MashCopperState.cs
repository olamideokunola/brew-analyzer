using System;
using BrewingModel;
using ProcessEquipmentParameters;


namespace BrewingModel.BrewingProcessEquipment
{
    public class MashCopperState
    {
        public MashCopperState()
        {
        }

        protected void SetProcessStepEndTime(string endTime, MashCopper mashCopper, Brew brew,
                                  MashCopperProcessParameters paramToCheck, 
                                  MashCopperProcessParameters paramToChange,
                                  IMashCopperState newState)
        {
            string brandName = brew.BrandName;
            string paramToCheckValue = brew.GetProcessParameterValue(ProcessEquipment.MashCopper,
                                              paramToCheck.ToString());

            //Complete process step
            if (brandName.Length > 0 &&
               paramToCheckValue.Length > 0)
            {
                Brew nBrew = mashCopper.Brew;
                string paramName = paramToChange.ToString();
                nBrew.SetProcessParameterValue(ProcessEquipment.MashCopper, paramName, endTime);

                //Set new state
                //string newStateString = 
                mashCopper.SetState(newState);
                //Console.WriteLine("New state is: " + newStateString);
            }
        }
    }
}
