using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashCopperHeatingUp2State : MashCopperState, IMashCopperState, IStateDescription
    {
        public MashCopperHeatingUp2State()
        {

        }

        public string GetStateDescription()
        {
            return "MashCopperHeatingUp2State";
        }

        public string GetShortState()
        {
            return "HeatingUp2";
        }

        public void InitBrew(MashCopper mashCopper, Brew brew)
        {

        }

        public void OnEntry(MashCopper mashCopper, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, MashCopper mashCopper, Brew brew)
        {
            if (paramText.Equals("Heating Time - Finish 2"))
            {
                MashCopperProcessParameters paramToCheck = MashCopperProcessParameters.Rest1EndTime;
                MashCopperProcessParameters paramToChange = MashCopperProcessParameters.HeatingUp2EndTime;
                IMashCopperState newState = mashCopper.Rest2State;
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
            brew.SetProcessParameterValue(ProcessEquipment.MashCopper,
                                          MashCopperProcessParameters.HeatingUp2Temperature.ToString(),
                                          temperature);
        }

        public void StartMashingIn(string paramText, string startTime, MashCopper mashCopper, Brew brew)
        {

        }
    }
}
