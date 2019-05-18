using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewMonitor.LiveBrewCommands
{
    public class StartMashCopperMashingInCommand : LiveBrewCommand
    {
        string _startDate;
        string _startTime;
        string _brandName;
        string _brewNumber;
        string _fieldName;
        string _fieldValue;
        string _fieldSection;

        public StartMashCopperMashingInCommand(string startDate, string startTime, string brandName, string brewNumber, string fieldName, string fieldValue, string fieldSection)
        {
            _startDate = startDate;
            _startTime = startTime;
            _brandName = brandName;
            _brewNumber = brewNumber;
            _fieldName = fieldName;
            _fieldValue = fieldValue;
            _fieldSection = fieldSection;
            brewingProcessHandler = BrewingProcessHandler.GetInstance();
        }

        public override void Execute(IBrew brew)
        {
            brewingProcessHandler.StartMashCopperMashingIn(_startTime, _brewNumber, _fieldName, _fieldValue, brew);
            //Brew brew = brewingProcessHandler.GetBrew(_brewNumber);
            string mashingInTime = brew.GetProcessParameterValue(ProcessEquipment.MashCopper,
                                                                 MashCopperProcessParameters.MashingInStartTime.ToString());
            Console.WriteLine("Mash Copper Mashing In time: "+ mashingInTime);
        }

        public override bool IsReversible()
        {
            return false;
        }

        public override void UnExecute()
        {

        }
    }
}
