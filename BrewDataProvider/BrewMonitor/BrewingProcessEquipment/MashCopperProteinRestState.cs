using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashCopperProteinRestState : MashCopperState, IMashCopperState, IStateDescription
    {
        public MashCopperProteinRestState()
        {
        }

        public string GetStateDescription()
        {
            return "MashCopperProteinRest";
        }

        public string GetShortState()
        {
            return "ProteinRest";
        }

        public void InitBrew(MashCopper mashCopper, Brew brew)
        {

        }

        public void OnEntry(MashCopper mashCopper, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, MashCopper mashCopper, Brew brew)
        {
            if (paramText.Equals("Protein Rest Time - Finish"))
            {
                MashCopperProcessParameters paramToCheck = MashCopperProcessParameters.MashingInEndTime;
                MashCopperProcessParameters paramToChange = MashCopperProcessParameters.ProteinRestEndTime;
                IMashCopperState newState = mashCopper.HeatingUp1State;
                SetProcessStepEndTime(endTime, mashCopper, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void SetProteinRestTemperature(string temperature, MashCopper mashCopper, Brew brew)
        {
            brew.SetProcessParameterValue(ProcessEquipment.MashCopper,
                                          MashCopperProcessParameters.ProteinRestTemperature.ToString(),
                                          temperature);
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

