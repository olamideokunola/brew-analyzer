using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashCopperHeatingUp1State : MashCopperState, IMashCopperState, IStateDescription
    {
        public MashCopperHeatingUp1State()
        {
        }

        public string GetStateDescription()
        {
            return "MashCopperHeatingUp1State";
        }

        public string GetShortState()
        {
            return "HeatingUp1";
        }

        public void InitBrew(MashCopper mashCopper, Brew brew)
        {

        }

        public void OnEntry(MashCopper mashCopper, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, MashCopper mashCopper, Brew brew)
        {
            if (paramText.Equals("Heating Time - Finish"))
            {
                MashCopperProcessParameters paramToCheck = MashCopperProcessParameters.ProteinRestEndTime;
                MashCopperProcessParameters paramToChange = MashCopperProcessParameters.HeatingUp1EndTime;
                IMashCopperState newState = mashCopper.Rest1State;
                SetProcessStepEndTime(endTime, mashCopper, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void SetProteinRestTemperature(string temperature, MashCopper mashCopper, Brew brew)
        {

        }

        public void SetHeatingUp1Temperature(string temperature, MashCopper mashCopper, Brew brew)
        {
            brew.SetProcessParameterValue(ProcessEquipment.MashCopper,
                                          MashCopperProcessParameters.HeatingUp1Temperature.ToString(),
                                          temperature);
        }

        public void SetHeatingUp2Temperature(string temperature, MashCopper mashCopper, Brew brew)
        {

        }

        public void StartMashingIn(string paramText, string startTime, MashCopper mashCopper, Brew brew)
        {

        }
    }
}
