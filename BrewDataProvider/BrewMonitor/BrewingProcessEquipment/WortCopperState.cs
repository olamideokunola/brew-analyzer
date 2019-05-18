using System;
using BrewingModel;
using ProcessEquipmentParameters;


namespace BrewingModel.BrewingProcessEquipment
{
    public class WortCopperState
    {
        public WortCopperState()
        {
        }

        protected void SetProcessStepEndTime(string endTime, WortCopper wortCopper, Brew brew,
                                  WortCopperProcessParameters paramToCheck, 
                                  WortCopperProcessParameters paramToChange,
                                  IWortCopperState newState)
        {
            string brandName = brew.BrandName;
            string paramToCheckValue = brew.GetProcessParameterValue(ProcessEquipment.WortCopper,
                                              paramToCheck.ToString());

            //Complete process step
            if (brandName.Length > 0 &&
               paramToCheckValue.Length > 0)
            {
                Brew nBrew = wortCopper.Brew;
                string paramName = paramToChange.ToString();
                nBrew.SetProcessParameterValue(ProcessEquipment.WortCopper, paramName, endTime);

                //Set new state
                //string newStateString = 
                wortCopper.SetState(newState);
                //Console.WriteLine("New state is: " + newStateString);
            }
        }
    }
}
