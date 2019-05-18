using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class WhirlpoolCastingState : WhirlpoolState, IWhirlpoolState, IStateDescription
    {
        public WhirlpoolCastingState()
        {
        }

        public string GetStateDescription()
        {
            return "WhirlpoolCastingState";
        }

        public string GetShortState()
        {
            return "Casting";
        }

        public void InitBrew(Whirlpool whirlpool, Brew brew)
        {

        }

        public void OnEntry(Whirlpool whirlpool, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, Whirlpool whirlpool, Brew brew)
        {
            if (paramText.Equals("Casting Time - Finish"))
            {
                WhirlpoolProcessParameters paramToCheck = WhirlpoolProcessParameters.CastingStartTime;
                WhirlpoolProcessParameters paramToChange = WhirlpoolProcessParameters.CastingEndTime;
                IWhirlpoolState newState = whirlpool.RestingState;
                SetProcessStepEndTime(endTime, whirlpool, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartCasting(string paramText, string startTime, Whirlpool whirlpool, Brew brew)
        {
            
        }

    }
}
