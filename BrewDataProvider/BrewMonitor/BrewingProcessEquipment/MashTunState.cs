using System;
using BrewingModel;
using ProcessEquipmentParameters;


namespace BrewingModel.BrewingProcessEquipment
{
    public class MashTunState
    {
        public MashTunState()
        {
        }

        protected void SetProcessStepEndTime(string endTime, MashTun mashTun, Brew brew,
                                  MashTunProcessParameters paramToCheck, 
                                  MashTunProcessParameters paramToChange,
                                  IMashTunState newState)
        {
            string brandName = brew.BrandName;
            string paramToCheckValue = brew.GetProcessParameterValue(ProcessEquipment.MashTun,
                                              paramToCheck.ToString());

            //Complete process step
            if (brandName.Length > 0 &&
               paramToCheckValue.Length > 0)
            {
                Brew nBrew = mashTun.Brew;
                string paramName = paramToChange.ToString();
                nBrew.SetProcessParameterValue(ProcessEquipment.MashTun, paramName, endTime);

                //Set new state
                //string newStateString = 
                mashTun.SetState(newState);
                //Console.WriteLine("New state is: " + newStateString);
            }
        }
    }
}
