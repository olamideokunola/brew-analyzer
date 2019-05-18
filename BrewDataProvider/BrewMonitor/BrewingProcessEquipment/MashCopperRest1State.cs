using System;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashCopperRest1State : MashCopperState, IMashCopperState, IStateDescription
    {
        public MashCopperRest1State()
        {
        }

        public string GetStateDescription()
        {
            return "MashCopperRest1State";
        }

        public string GetShortState()
        {
            return "Rest1";
        }

        public void InitBrew(MashCopper mashCopper, Brew brew)
        {
           
        }

        public void OnEntry(MashCopper mashCopper, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashCopper mashCopper, Brew brew)
        {
            if (paramText.Equals("Rest time - Finish"))
            {
                MashCopperProcessParameters paramToCheck = MashCopperProcessParameters.HeatingUp1EndTime;
                MashCopperProcessParameters paramToChange = MashCopperProcessParameters.Rest1EndTime;
                IMashCopperState newState = mashCopper.HeatingUp2State;
                SetProcessStepEndTime(endTime, mashCopper, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void SetProteinRestTemperature(string temperature, MashCopper mashCopper, Brew brew)
        {

        }

        public void SetHeatingUp1Temperature(string temperature, MashCopper mashCopper, Brew brew)
        {

        }

        public void SetHeatingUp2Temperature(string temperature, MashCopper mashCopper, Brew brew)
        {

        }

        public void StartMashingIn(string paramText, string startTime, MashCopper mashCopper, Brew brew)
        {
           
        }
    }
}
