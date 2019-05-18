using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class WortCopperExtraBoilingState : WortCopperState, IWortCopperState, IStateDescription
    {
        public WortCopperExtraBoilingState()
        {
        }

        public string GetStateDescription()
        {
            return "WortCopperExtraBoilingState";
        }

        public string GetShortState()
        {
            return "ExtraBoiling";
        }

        public void InitBrew(WortCopper holdingVessel, Brew brew)
        {

        }

        public void OnEntry(WortCopper holdingVessel, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, WortCopper wortCopper, Brew brew)
        {
           
        }

        public void StartCasting(string paramText, string startTime, WortCopper wortCopper, Brew brew)
        {
            if (paramText.Equals("Start Casting - Finish"))
            {
                WortCopperProcessParameters paramToCheck = WortCopperProcessParameters.BoilingEndTime;
                WortCopperProcessParameters paramToChange = WortCopperProcessParameters.CastingStartTime;
                IWortCopperState newState = wortCopper.CastingState;
                SetCastingStartTime(startTime, wortCopper, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartHeating(string paramText, string startTime, WortCopper holdingVessel, Brew brew)
        {

        }

        private void SetCastingStartTime(string endTime, WortCopper wortCopper, Brew brew,
                                  WortCopperProcessParameters paramToCheck,
                                  WortCopperProcessParameters paramToChange,
                                  IWortCopperState newState)
        {
            string brandName = brew.BrandName;
            string wortCopperParamToCheckValue = brew.GetProcessParameterValue(ProcessEquipment.WortCopper,
                                              paramToCheck.ToString());

            //Complete process step
            if (brandName.Length > 0 &&
               wortCopperParamToCheckValue.Length > 0)
            {
                Brew nBrew = wortCopper.Brew;
                string paramName = paramToChange.ToString();
                nBrew.SetProcessParameterValue(ProcessEquipment.WortCopper, paramName, endTime);

                //Set new state
                wortCopper.SetState(newState);
                //Console.WriteLine("New state is: " + newStateString);
            }
        }

    }
}
