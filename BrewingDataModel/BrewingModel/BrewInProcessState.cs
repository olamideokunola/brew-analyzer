using System;
namespace BrewingModel
{
    public class BrewInProcessState : IBrewState
    {
        public BrewInProcessState()
        {
        }

        public void CompleteWhirlpoolReady(string endTime, Brew brew)
        {

        }

        public void StartMashCopperMashingIn(string startTime, Brew brew)
        {
            throw new NotImplementedException();
        }

        public void StartMashTunMashingIn(string startTime, Brew brew)
        {
            throw new NotImplementedException();
        }
    }
}
