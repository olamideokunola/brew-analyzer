using System;
namespace BrewingModel
{
    public interface IBrewState
    {
        void StartMashTunMashingIn(string startTime, Brew brew);
        void StartMashCopperMashingIn(string startTime, Brew brew);
        void CompleteWhirlpoolReady(string endTime, Brew brew);
    }
}
