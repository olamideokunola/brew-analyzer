using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashTunIdleState : MashTunState, IMashTunState, IStateDescription
    {
        public MashTunIdleState()
        {
        }

        public string GetStateDescription()
        {
            return "MashTunIdle";
        }

        public string GetShortState()
        {
            return "Idle";
        }

        public void InitBrew(MashTun mashTun, Brew brew)
        {
            string brewNumber = mashTun.Brew.BrewNumber;
            if(brew != null)
            {
                if(brewNumber.Length == 0)
                {
                    mashTun.InitBrew(brew);
                }
            }
        }

        public void OnEntry(MashTun mashTun, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, MashTun mashTun, Brew brew)
        {

        }

        public void SetProteinRestTemperature(string temperature, MashTun mashTun, Brew brew)
        {
           
        }

        public void StartMashingIn(string paramText, string startTime, MashTun mashTun, Brew brew)
        {
            string brewNumber = brew.BrewNumber;
            string brandName = brew.BrandName;
            string mashTunBrewNumber = mashTun.Brew.BrewNumber;
            MashTunProcessParameters param = MashTunProcessParameters.MashingInStartTime;
            string paramName = param.ToString();

            //Start Mashing In9
            if (brandName.Length > 0 && 
               brewNumber.Length > 0 &&
               brewNumber == mashTunBrewNumber &&
               paramText.Equals("Tranpsort Time - Finish"))
            {
                mashTun.Brew.SetProcessParameterValue(ProcessEquipment.MashTun, paramName, startTime);

                string mashingInTime = brew.GetProcessParameterValue(ProcessEquipment.MashTun,
                                                                     MashTunProcessParameters.MashingInStartTime.ToString());
                Console.WriteLine("Mash Tun mashingInTime: " + mashingInTime);

                //Set new state
                mashTun.SetState(mashTun.MashingInState);
            }

        }

        public void SetSacharificationRestTemperature(string temperature, MashTun mashTun, Brew brew)
        {

        }

        public void SetHeatingUpTemperature(string temperature, MashTun mashTun, Brew brew)
        {

        }
    }
}