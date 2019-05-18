using System;
using BrewingModel;
using ProcessEquipmentParameters;


namespace BrewingModel.BrewingProcessEquipment
{
    public class MashFilterState
    {
        public MashFilterState()
        {
        }

        protected void SetProcessStepEndTime(string endTime, MashFilter mashFilter, Brew brew,
                                  MashFilterProcessParameters paramToCheck, 
                                  MashFilterProcessParameters paramToChange,
                                  IMashFilterState newState)
        {
            string brandName = brew.BrandName;
            string paramToCheckValue = brew.GetProcessParameterValue(ProcessEquipment.MashFilter,
                                              paramToCheck.ToString());

            //Complete process step
            if (brandName.Length > 0 &&
               paramToCheckValue.Length > 0)
            {
                Brew nBrew = mashFilter.Brew;
                string paramName = paramToChange.ToString();
                nBrew.SetProcessParameterValue(ProcessEquipment.MashFilter, paramName, endTime);

                //Set new state
                //string newStateString = 
                mashFilter.SetState(newState);
                //Console.WriteLine("New state is: " + newStateString);
            }
        }
    }
}
