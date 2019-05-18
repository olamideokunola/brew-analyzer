using System;
using BrewingModel;

namespace BrewingModel.BrewingProcessEquipment
{
    public interface IWhirlpoolState
    {
        void InitBrew(Whirlpool wortCopper, Brew brew);
        void SetEndTime(string paramText, string endTime, Whirlpool wortCopper, Brew brew);
        void StartCasting(string paramText, string startTime, Whirlpool wortCopper, Brew brew);
        void OnEntry(Whirlpool wortCopper, Brew brew);
    }
}
