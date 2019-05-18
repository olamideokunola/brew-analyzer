using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class WhirlpoolIdleState : WhirlpoolState, IWhirlpoolState, IStateDescription
    {
        public WhirlpoolIdleState()
        {
        }

        public string GetStateDescription()
        {
            return "WhirlpoolIdleState";
        }

        public string GetShortState()
        {
            return "Idle";
        }

        public void InitBrew(Whirlpool whirlpool, Brew brew)
        {
            string brewNumber = whirlpool.Brew.BrewNumber;
            if(brew != null)
            {
                if(brewNumber.Length == 0)
                {
                    whirlpool.InitBrew(brew);
                }
            }
        }

        public void OnEntry(Whirlpool whirlpool, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, Whirlpool whirlpool, Brew brew)
        {

        }

        public void StartCasting(string paramText, string startTime, Whirlpool whirlpool, Brew brew)
        {
            string brewNumber = brew.BrewNumber;
            string brandName = brew.BrandName;
            string whirlpoolBrewNumber = whirlpool.Brew.BrewNumber;
            WhirlpoolProcessParameters param = WhirlpoolProcessParameters.CastingStartTime;
            string paramName = param.ToString();

            //Start Mashing In
            if (brandName.Length > 0 &&
               brewNumber.Length > 0 &&
               brewNumber == whirlpoolBrewNumber &&
               paramText.Equals("Start Casting - Finish"))
            {
                whirlpool.Brew.SetProcessParameterValue(ProcessEquipment.Whirlpool, paramName, startTime);

                //Set new state
                whirlpool.SetState(whirlpool.CastingState);
            }

        }
    }
}