using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashTunProteinRestState : MashTunState, IMashTunState, IStateDescription
    {
        public MashTunProteinRestState()
        {
        }

        public string GetStateDescription()
        {
            return "MashTunProteinRest";
        }

        public string GetShortState()
        {
            return "ProteinRest";
        }

        public void InitBrew(MashTun mashTun, Brew brew)
        {

        }

        public void OnEntry(MashTun mashTun, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, MashTun mashTun, Brew brew)
        {
            if (paramText.Equals("Protein Rest Time - Finish"))
            {
                MashTunProcessParameters paramToCheck = MashTunProcessParameters.MashingInEndTime;
                MashTunProcessParameters paramToChange = MashTunProcessParameters.ProteinRestEndTime;
                IMashTunState newState = mashTun.SacRestState;
                SetProcessStepEndTime(endTime, mashTun, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void SetProteinRestTemperature(string temperature, MashTun mashTun, Brew brew)
        {
            brew.SetProcessParameterValue(ProcessEquipment.MashTun,
                                          MashTunProcessParameters.ProteinRestTemperature.ToString(),
                                          temperature);
        }

        public void SetSacharificationRestTemperature(string temperature, MashTun mashTun, Brew brew)
        {

        }

        public void SetHeatingUpTemperature(string temperature, MashTun mashTun, Brew brew)
        {

        }

        public void StartMashingIn(string paramText, string startTime, MashTun mashTun, Brew brew)
        {

        }
    }
}

