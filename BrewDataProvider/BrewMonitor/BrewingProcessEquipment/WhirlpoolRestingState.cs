using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class WhirlpoolRestingState : WhirlpoolState, IWhirlpoolState, IStateDescription
    {
        public WhirlpoolRestingState()
        {
        }

        public string GetStateDescription()
        {
            return "WhirlpoolRestingState";
        }

        public string GetShortState()
        {
            return "Resting";
        }

        public void InitBrew(Whirlpool whirlpool, Brew brew)
        {

        }

        public void OnEntry(Whirlpool whirlpool, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, Whirlpool whirlpool, Brew brew)
        {
            if (paramText.Equals("Rest Time - Finish"))
            {
                WhirlpoolProcessParameters paramToCheck = WhirlpoolProcessParameters.CastingEndTime;
                WhirlpoolProcessParameters paramToChange = WhirlpoolProcessParameters.RestingEndTime;
                IWhirlpoolState newState = whirlpool.CoolingState;
                SetProcessStepEndTime(endTime, whirlpool, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartCasting(string paramText, string startTime, Whirlpool whirlpool, Brew brew)
        {
            
        }

    }
}
