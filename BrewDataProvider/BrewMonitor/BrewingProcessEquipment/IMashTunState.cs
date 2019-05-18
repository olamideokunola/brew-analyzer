using System;
using BrewingModel;

namespace BrewingModel.BrewingProcessEquipment
{
    public interface IMashTunState
    {
        void InitBrew(MashTun mashTun, Brew brew);
        void StartMashingIn(string paramText, string startTime, MashTun mashTun, Brew brew);
        void SetEndTime(string paramText, string endTime, MashTun mashTun, Brew brew);
        void OnEntry(MashTun mashTun, Brew brew);
        void SetProteinRestTemperature(string temperature, MashTun mashTun, Brew brew);
        void SetSacharificationRestTemperature(string temperature, MashTun mashTun, Brew brew);
        void SetHeatingUpTemperature(string temperature, MashTun mashTun, Brew brew);
    }
}
