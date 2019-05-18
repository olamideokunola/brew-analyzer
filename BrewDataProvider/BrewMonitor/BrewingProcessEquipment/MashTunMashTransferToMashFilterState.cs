using System;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashTunMashTransferToMashFilterState : MashTunState, IMashTunState, IStateDescription
    {
        public MashTunMashTransferToMashFilterState()
        {
        }

        public string GetStateDescription()
        {
            return "MashTransferToMashTunState";
        }

        public string GetShortState()
        {
            return "MashTransfer";
        }

        public void InitBrew(MashTun mashTun, Brew brew)
        {

        }

        public void OnEntry(MashTun mashTun, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, MashTun mashTun, Brew brew)
        {
            if (paramText.Equals("Mash Tun ready at - Finish"))
            {
                MashTunProcessParameters paramToCheck = MashTunProcessParameters.HeatingUpEndTime;
                MashTunProcessParameters paramToChange = MashTunProcessParameters.MashTunReadyAt;
                IMashTunState newState = mashTun.IdleState;
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

        }
        public void StartMashingIn(string paramText, string startTime, MashTun mashTun, Brew brew)
        {

        }
    }
}
