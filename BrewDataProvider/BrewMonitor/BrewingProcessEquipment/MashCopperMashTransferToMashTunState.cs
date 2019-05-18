using System;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashCopperMashTransferToMashTunState : MashCopperState, IMashCopperState, IStateDescription
    {
        public MashCopperMashTransferToMashTunState()
        {
        }

        public string GetStateDescription()
        {
            return "MashCopperMashTransferToMashTunState";
        }

        public string GetShortState()
        {
            return "MashTransfer";
        }

        public void InitBrew(MashCopper mashCopper, Brew brew)
        {

        }

        public void OnEntry(MashCopper mashCopper, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, MashCopper mashCopper, Brew brew)
        {
            if (paramText.Equals("Mash Transfer to MT - Finish"))
            {
                MashCopperProcessParameters paramToCheck = MashCopperProcessParameters.Rest2EndTime;
                MashCopperProcessParameters paramToChange = MashCopperProcessParameters.TransferToMtEndTime;
                IMashCopperState newState = mashCopper.IdleState;
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
