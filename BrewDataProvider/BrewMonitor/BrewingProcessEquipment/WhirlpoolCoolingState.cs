using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class WhirlpoolCoolingState : WhirlpoolState, IWhirlpoolState, IStateDescription
    {
        public WhirlpoolCoolingState()
        {
        }

        public string GetStateDescription()
        {
            return "WhirlpoolCoolingState";
        }

        public string GetShortState()
        {
            return "Cooling";
        }

        public void InitBrew(Whirlpool whirlpool, Brew brew)
        {

        }

        public void OnEntry(Whirlpool whirlpool, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, Whirlpool whirlpool, Brew brew)
        {
            if (paramText.Equals("Wort Cooling - Finish"))
            {
                WhirlpoolProcessParameters paramToCheck = WhirlpoolProcessParameters.RestingEndTime;
                WhirlpoolProcessParameters paramToChange = WhirlpoolProcessParameters.CoolingEndTime;
                IWhirlpoolState newState = whirlpool.TrubRemovalState;
                SetProcessStepEndTime(endTime, whirlpool, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartCasting(string paramText, string startTime, Whirlpool wortCopper, Brew brew)
        {
            
        }
    }
}
