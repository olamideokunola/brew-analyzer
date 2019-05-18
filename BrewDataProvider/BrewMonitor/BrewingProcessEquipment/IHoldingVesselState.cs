using System;
using BrewingModel;

namespace BrewingModel.BrewingProcessEquipment
{
    public interface IHoldingVesselState
    {
        void InitBrew(HoldingVessel holdingVessel, Brew brew);
        void StartFilling(string paramText, string startTime, HoldingVessel holdingVessel, Brew brew);
        void SetEndTime(string paramText, string endTime, HoldingVessel holdingVessel, Brew brew);
        void OnEntry(HoldingVessel holdingVessel, Brew brew);
    }
}
