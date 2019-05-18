using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashCopperIdleState : MashCopperState, IMashCopperState, IStateDescription
    {
        public MashCopperIdleState()
        {
        }

        public string GetStateDescription()
        {
            return "MashCopperIdle";
        }

        public string GetShortState()
        {
            return "Idle";
        }

        public void InitBrew(MashCopper mashCopper, Brew brew)
        {
            string brewNumber = mashCopper.Brew.BrewNumber;
            if(brew != null)
            {
                if(brewNumber.Length == 0)
                {
                    mashCopper.InitBrew(brew);
                }
            }
        }

        public void OnEntry(MashCopper mashCopper, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, MashCopper mashCopper, Brew brew)
        {

        }

        public void SetHeatingUp1Temperature(string temperature, MashCopper mashCopper, Brew brew)
        {
            
        }

        public void SetHeatingUp2Temperature(string temperature, MashCopper mashCopper, Brew brew)
        {

        }

        public void SetProteinRestTemperature(string temperature, MashCopper mashCopper, Brew brew)
        {
           
        }

        public void StartMashingIn(string paramText, string startTime, MashCopper mashCopper, Brew brew)
        {
            mashCopper.InitBrew(brew);
            string brewNumber = brew.BrewNumber;
            string brandName = brew.BrandName;
            string mashCopperBrewNumber = mashCopper.Brew.BrewNumber;

            //Start Mashing In
            if (//brandName.Length > 0 && 
               brewNumber.Length > 0 &&
               brewNumber == mashCopperBrewNumber &&
                paramText.Equals("Transport Time RAW Sorguum to WB MC - Finish"))
            {
                string paramName = MashCopperProcessParameters.MashingInStartTime.ToString();
                mashCopper.Brew.SetProcessParameterValue(ProcessEquipment.MashCopper, paramName, startTime);

                //Set new state
                brew.SetState(new BrewInProcessState());
                mashCopper.SetState(mashCopper.MashingInState);
            }

        }
    }
}
