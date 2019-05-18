using ProcessEquipmentParameters;
using BrewingModel;

namespace BrewingModel.BrewingProcessEquipment
{
    public class WhirlpoolState
    {
        public WhirlpoolState()
        {
        }

        protected void SetProcessStepEndTime(string endTime, Whirlpool wortCopper, Brew brew,
                                  WhirlpoolProcessParameters paramToCheck, 
                                  WhirlpoolProcessParameters paramToChange,
                                  IWhirlpoolState newState)
        {
            string brandName = brew.BrandName;
            string paramToCheckValue = brew.GetProcessParameterValue(ProcessEquipment.Whirlpool,
                                              paramToCheck.ToString());

            //Complete process step
            if (brandName.Length > 0 &&
               paramToCheckValue.Length > 0)
            {
                Brew nBrew = wortCopper.Brew;
                string paramName = paramToChange.ToString();
                nBrew.SetProcessParameterValue(ProcessEquipment.Whirlpool, paramName, endTime);

                //Set new state
                //string newStateString = 
                wortCopper.SetState(newState);
                //Console.WriteLine("New state is: " + newStateString);
            }
        }
    }
}
