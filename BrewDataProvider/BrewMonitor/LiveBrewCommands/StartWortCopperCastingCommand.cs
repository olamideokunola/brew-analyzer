using System;
using BrewingModel;
namespace BrewingModel.BrewMonitor.LiveBrewCommands
{
    public class StartWortCopperCastingCommand : LiveBrewCommand
    {
        //string _startDate;
        string _startTime;
        string _brandName;
        string _brewNumber;
        string _fieldName;
        string _fieldValue;
        string _fieldSection;

        public StartWortCopperCastingCommand(string startTime, string brandName, string brewNumber, string fieldName, string fieldValue, string fieldSection)
        {
            //this._startDate = startDate;
            this._startTime = startTime;
            this._brandName = brandName;
            this._brewNumber = brewNumber;
            this._fieldName = fieldName;
            this._fieldValue = fieldValue;
            this._fieldSection = fieldSection;
            this.brewingProcessHandler = BrewingProcessHandler.GetInstance();
        }

        public override void Execute(IBrew brew)
        {
            this.brewingProcessHandler.StartWortCopperCasting(_brewNumber, _fieldName, _fieldValue);
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
