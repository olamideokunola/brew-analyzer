using System;
using ProcessEquipmentParameters;

namespace BrewingModel
{
    public class BrewIdleState : IBrewState
    {
        public BrewIdleState()
        {
        }

        public void CompleteWhirlpoolReady(string endTime, Brew brew)
        {

        }

        public void StartMashCopperMashingIn(string startTime, Brew brew)
        {
            string parameterName = MashTunProcessParameters.MashingInStartTime.ToString();
            string parameterValue = brew.GetProcessParameterValue(ProcessEquipment.MashTun, parameterName);
            if (parameterValue.Length == 0)
            {
                string paramToSetName = MashCopperProcessParameters.MashingInStartTime.ToString();
                brew.SetProcessParameterValue(ProcessEquipment.MashCopper, paramToSetName, startTime);
            }
        }

        public void StartMashTunMashingIn(string startTime, Brew brew)
        {
            string parameterName = MashCopperProcessParameters.MashingInStartTime.ToString();
            string parameterValue = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, parameterName);
            if (parameterValue.Length == 0)
            {
                string paramToSetName = MashTunProcessParameters.MashingInStartTime.ToString();
                brew.SetProcessParameterValue(ProcessEquipment.MashTun, paramToSetName, startTime);
            }
        }
    }
}
