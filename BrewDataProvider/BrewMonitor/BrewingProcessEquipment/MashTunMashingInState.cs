using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashTunMashingInState : MashTunState, IMashTunState, IStateDescription
    {
        public MashTunMashingInState()
        {
        }

        public string GetStateDescription()
        {
            return "MashTunMashingIn";
        }

        public string GetShortState()
        {
            return "MashingIn";
        }

        public void InitBrew(MashTun mashTun, Brew brew)
        {

        }

        public void OnEntry(MashTun mashTun, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashTun mashTun, Brew brew)
        {
            if(paramText.Equals("Mash in Time - Finish"))
            {
                MashTunProcessParameters paramToCheck = MashTunProcessParameters.MashingInStartTime;
                MashTunProcessParameters paramToChange = MashTunProcessParameters.MashingInEndTime;
                IMashTunState newState = mashTun.ProteinRestState;
                SetProcessStepEndTime(endTime, mashTun, brew, paramToCheck, paramToChange, newState);

                string mashingInEndTime = brew.GetProcessParameterValue(ProcessEquipment.MashTun,
                                                                             MashTunProcessParameters.MashingInEndTime.ToString());
                Console.WriteLine("Mash Tun mashingInEndTime: " + mashingInEndTime);
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
