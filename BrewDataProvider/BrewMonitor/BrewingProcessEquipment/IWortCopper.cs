using System;
using BrewingModel;

namespace BrewingModel.BrewingProcessEquipment
{
    public interface IWortCopperState
    {
        void InitBrew(WortCopper wortCopper, Brew brew);
        void StartHeating(string paramText, string startTime, WortCopper wortCopper, Brew brew);
        void SetEndTime(string paramText, string endTime, WortCopper wortCopper, Brew brew);
        void StartCasting(string paramText, string startTime, WortCopper wortCopper, Brew brew);
        void OnEntry(WortCopper wortCopper, Brew brew);
        //void SetVolumeBeforeBoiling(string volume, WortCopper wortCopper, Brew brew);
        //void SetVolumeAfterBoiling(string volume, WortCopper wortCopper, Brew brew);
    }
}
