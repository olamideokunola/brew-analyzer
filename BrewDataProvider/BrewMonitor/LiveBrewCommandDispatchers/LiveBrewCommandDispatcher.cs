using System;
//using System.Collections.Concurrent;
using System.Collections.Generic;
using BrewingModel;
using BrewingModel.BrewMonitor;
using BrewingModel.BrewMonitor.LiveBrewCommands;

namespace BrewingModel.BrewMonitor.LiveBrewCommandDispatchers
{
    public abstract class LiveBrewCommandDispatcher
    {
        protected LiveBrewCommand liveBrewCommand;

        protected IList<LiveBrewCommand> liveBrewCommands = new List<LiveBrewCommand>();

        //protected BlockingCollection<LiveBrewCommand> liveBrewCommands = new BlockingCollection<LiveBrewCommand>();
        //public void SendLiveBrewCommand(string fieldName, string fieldValue, Brew brew, string fieldSection)
        //{
        //liveBrewCommand = CreateLiveBrewCommand(fieldName, fieldValue, brew, fieldSection);
        //liveBrewCommand.Execute();
        //BrewingProcessHandler brewingProcessHandler = BrewingProcessHandler.GetInstance();
        //brewingProcessHandler.PrintCurrentState();
        //}

        public void AddToLiveBrewCommands(LiveBrewCommand liveBrewCommand)
        {
            liveBrewCommands.Add(liveBrewCommand);
        }

        public void SendAllCommands(IBrew brew)
        {

            foreach(LiveBrewCommand command in liveBrewCommands)
            {
                command.Execute(brew);
            }
        }

        //public abstract LiveBrewCommand CreateLiveBrewCommand(String fieldName, string fieldValue, Brew brew, string fieldSection);

        public abstract void CreateLiveBrewCommands(String fieldName, string fieldValue, Brew brew, string fieldSection);
    }
}
