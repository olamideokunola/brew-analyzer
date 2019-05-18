using System;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashTunSacRestState : MashTunState, IMashTunState, IStateDescription
    {
        public MashTunSacRestState()
        {
        }

        public string GetStateDescription()
        {
            return "MashTunSacRestState";
        }

        public string GetShortState()
        {
            return "SacRest";
        }

        public void InitBrew(MashTun mashTun, Brew brew)
        {
           
        }

        public void OnEntry(MashTun mashTun, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashTun mashTun, Brew brew)
        {
            if (paramText.Equals("Sacha. Rest time - Finish"))
            {
                MashTunProcessParameters paramToCheck = MashTunProcessParameters.ProteinRestEndTime;
                MashTunProcessParameters paramToChange = MashTunProcessParameters.SacharificationRestEndTime;
                IMashTunState newState = mashTun.HeatingUpState;
                SetProcessStepEndTime(endTime, mashTun, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void SetProteinRestTemperature(string temperature, MashTun mashTun, Brew brew)
        {

        }

        public void SetSacharificationRestTemperature(string temperature, MashTun mashTun, Brew brew)
        {
            brew.SetProcessParameterValue(ProcessEquipment.MashTun,
                                          MashTunProcessParameters.SacharificationRestTemperature.ToString(),
                                          temperature);
        }

        public void SetHeatingUpTemperature(string temperature, MashTun mashTun, Brew brew)
        {

        }

        public void StartMashingIn(string paramText, string startTime, MashTun mashTun, Brew brew)
        {
           
        }
    }
}
