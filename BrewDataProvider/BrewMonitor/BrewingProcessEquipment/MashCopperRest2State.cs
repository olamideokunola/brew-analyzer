using System;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashCopperRest2State : MashCopperState, IMashCopperState, IStateDescription
    {
        public MashCopperRest2State()
        {
        }

        public string GetStateDescription()
        {
            return "MashCopperRest2State";
        }

        public string GetShortState()
        {
            return "Rest2";
        }

        public void InitBrew(MashCopper mashCopper, Brew brew)
        {
           
        }

        public void OnEntry(MashCopper mashCopper, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, MashCopper mashCopper, Brew brew)
        {
            if (paramText.Equals("Rest Time - Finish 2"))
            {
                MashCopperProcessParameters paramToCheck = MashCopperProcessParameters.HeatingUp2EndTime;
                MashCopperProcessParameters paramToChange = MashCopperProcessParameters.Rest2EndTime;
                IMashCopperState newState = mashCopper.MashTransferToMashTunState;
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
