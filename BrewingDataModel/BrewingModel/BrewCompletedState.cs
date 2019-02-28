using System;
namespace BrewingModel
{
    public class BrewCompletedState : IBrewState
    {
        public BrewCompletedState()
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
