using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashTunHeatingUpState : MashTunState, IMashTunState, IStateDescription
    {
        public MashTunHeatingUpState()
        {
        }

        public string GetStateDescription()
        {
            return "MashTunHeatingUpState";
        }

        public string GetShortState()
        {
            return "HeatingUp";
        }

        public void InitBrew(MashTun mashTun, Brew brew)
        {

        }

        public void OnEntry(MashTun mashTun, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, MashTun mashTun, Brew brew)
        {
            if (paramText.Equals("Heating Time - Finish"))
            {
                MashTunProcessParameters paramToCheck = MashTunProcessParameters.SacharificationRestEndTime;
                MashTunProcessParameters paramToChange = MashTunProcessParameters.HeatingUpEndTime;
                IMashTunState newState = mashTun.MashTransferToMashFilterState;
                SetProcessStepEndTime(endTime, mashTun, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void SetProteinRestTemperature(string temperature, MashTun mashTun, Brew brew)
        {

        }

        public void SetSacharificationRestTemperature(string temperature, MashTun mashTun, Brew brew)
        {

        }

        public void SetHeatingUpTemperature(string temperature, MashTun mashTun, Brew brew)
        {
            brew.SetProcessParameterValue(ProcessEquipment.MashTun,
                                          MashTunProcessParameters.HeatingUpTemperature.ToString(),
                                          temperature);
        }
        public void StartMashingIn(string paramText, string startTime, MashTun mashTun, Brew brew)
        {

        }
    }
}
