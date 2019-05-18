using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class WhirlpoolTrubRemovalState : WhirlpoolState, IWhirlpoolState, IStateDescription
    {
        public WhirlpoolTrubRemovalState()
        {
        }

        public string GetStateDescription()
        {
            return "WhirlpoolTrubRemovalState";
        }

        public string GetShortState()
        {
            return "TrubRemoval";
        }

        public void InitBrew(Whirlpool whirlpool, Brew brew)
        {

        }

        public void OnEntry(Whirlpool whirlpool, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, Whirlpool whirlpool, Brew brew)
        {
            if (paramText.Equals("Whirlpool Ready at - Finish"))
            {
                WhirlpoolProcessParameters paramToCheck = WhirlpoolProcessParameters.CoolingEndTime;
                WhirlpoolProcessParameters paramToChange = WhirlpoolProcessParameters.ReadyAtTime;
                IWhirlpoolState newState = whirlpool.IdleState;
                SetProcessStepEndTime(endTime, whirlpool, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void StartCasting(string paramText, string startTime, Whirlpool wortCopper, Brew brew)
        {
            
        }

    }
}
