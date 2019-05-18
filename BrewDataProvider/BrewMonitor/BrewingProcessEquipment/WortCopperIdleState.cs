using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class WortCopperIdleState : WortCopperState, IWortCopperState, IStateDescription
    {
        public WortCopperIdleState()
        {
        }

        public string GetStateDescription()
        {
            return "WortCopperIdleState";
        }

        public string GetShortState()
        {
            return "Idle";
        }

        public void InitBrew(WortCopper wortCopper, Brew brew)
        {
            string brewNumber = wortCopper.Brew.BrewNumber;
            if(brew != null)
            {
                if(brewNumber.Length == 0)
                {
                    wortCopper.InitBrew(brew);
                }
            }
        }

        public void OnEntry(WortCopper wortCopper, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, WortCopper wortCopper, Brew brew)
        {

        }

        public void StartCasting(string paramText, string startTime, WortCopper wortCopper, Brew brew)
        {
           
        }

        public void StartHeating(string paramText, string startTime, WortCopper wortCopper, Brew brew)
        {
            string brewNumber = brew.BrewNumber;
            string brandName = brew.BrandName;
            string wortCopperBrewNumber = wortCopper.Brew.BrewNumber;
            WortCopperProcessParameters param = WortCopperProcessParameters.HeatingStartTime;
            string paramName = param.ToString();

            //Start Mashing In
            if (brandName.Length > 0 &&
               brewNumber.Length > 0 &&
               brewNumber == wortCopperBrewNumber &&
               paramText.Equals("Start Heating(Boiling) - Finish"))
            {
                wortCopper.Brew.SetProcessParameterValue(ProcessEquipment.WortCopper, paramName, startTime);

                //Set new state
                wortCopper.SetState(wortCopper.HeatingState);
            }

        }
    }
}