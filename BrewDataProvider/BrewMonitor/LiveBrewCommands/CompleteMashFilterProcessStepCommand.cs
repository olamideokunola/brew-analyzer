using System;
using BrewingModel;

namespace BrewingModel.BrewMonitor.LiveBrewCommands
{
    public class CompleteMashFilterProcessStepCommand : LiveBrewCommand
    {
        string _brandName;
        string _brewNumber;
        string _fieldName;
        string _fieldValue;
        string _fieldSection;

        public CompleteMashFilterProcessStepCommand(string brandName, string brewNumber, string fieldName, string fieldValue, string fieldSection)
        {
            this._brandName = brandName;
            this._brewNumber = brewNumber;
            this._fieldName = fieldName;
            this._fieldValue = fieldValue;
            this._fieldSection = fieldSection;
            this.brewingProcessHandler = BrewingProcessHandler.GetInstance();
        }

        public override void Execute(IBrew brew)
        {
            this.brewingProcessHandler.CompleteMashFilterProcessStep(_brewNumber, _fieldName, _fieldValue, brew);
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