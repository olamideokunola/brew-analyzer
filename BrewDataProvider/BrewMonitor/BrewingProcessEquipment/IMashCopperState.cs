using System;
using BrewingModel;

namespace BrewingModel.BrewingProcessEquipment
{
    public interface IMashCopperState
    {
        void InitBrew(MashCopper mashCopper, Brew brew);
        void StartMashingIn(string paramText, string startTime, MashCopper mashCopper, Brew brew);
        void SetEndTime(string paramText, string endTime, MashCopper mashCopper, Brew brew);
        void OnEntry(MashCopper mashCopper, Brew brew);
        void SetProteinRestTemperature(string temperature, MashCopper mashCopper, Brew brew);
        void SetHeatingUp1Temperature(string temperature, MashCopper mashCopper, Brew brew);
        void SetHeatingUp2Temperature(string temperature, MashCopper mashCopper, Brew brew);

    }
}
