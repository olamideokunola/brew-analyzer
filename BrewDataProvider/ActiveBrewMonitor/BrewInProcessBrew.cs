using System;
using System.Collections.Generic;
using BrewDataProvider.BrewMonitor;
using BrewingModel;
using BrewingModel.BrewMonitor.LiveBrewCommandDispatchers;

namespace BrewDataProvider.ActiveBrewMonitor
{
    public class BrewInProcessBrew : BrewPlain
    {
        public BrewInProcessBrew()
        {

        }

        public BrewInProcessBrew(BrewInProcess brewInProcess, string startDate) : base(startDate, "", brewInProcess.BrewNumber)
        {
            foreach (KeyValuePair<string, IDictionary<string, string>> section in brewInProcess.AvailableFields)
            {
                IDictionary<string, string> sectionFields = new Dictionary<string, string>();
                sectionFields = section.Value;
                foreach (KeyValuePair<string, string> field in sectionFields)
                {
                    string fieldName = field.Key;
                    string fieldValue = field.Value;
                    string fieldSection = section.Key;

                    // Set Process Parameters field and values in Brew                                       
                    switch (fieldSection)
                    {
                        case "Weigh bin Mash Copper":
                        case "MASH COPPER":
                            SetProcessParameterValue(ProcessEquipment.MashCopper, fieldName, fieldValue);
                            break;
                        case "WEIGHBIN MASHTUN":                  
                        case "MASH TUN":
                            SetProcessParameterValue(ProcessEquipment.MashTun, fieldName, fieldValue);
                            break;
                        case "MASH FILTER":
                            SetProcessParameterValue(ProcessEquipment.MashFilter, fieldName, fieldValue);
                            break;
                        case "HOLDING VESSEL":                            
                        case "HOLDING VESSEL TO WORT COPPER":
                            SetProcessParameterValue(ProcessEquipment.HoldingVessel, fieldName, fieldValue);
                            break;
                        case "WORT COPPER":
                            SetProcessParameterValue(ProcessEquipment.WortCopper, fieldName, fieldValue);
                            break;
                        case "WHIRLPOOL":
                            SetProcessParameterValue(ProcessEquipment.Whirlpool, fieldName, fieldValue);
                            break;
                    }

                }
            }
        }


        //public BrewInProcessBrew(BrewInProcess brewInProcess, string startDate) : base(startDate, "",  brewInProcess.BrewNumber)
        //{
        //    foreach (KeyValuePair<string, IDictionary<string, string>> section in brewInProcess.AvailableFields)
        //    {
        //        IDictionary<string, string> sectionFields = new Dictionary<string, string>();
        //        sectionFields = section.Value;
        //        foreach (KeyValuePair<string, string> field in sectionFields)
        //        {
        //            string fieldName = field.Key;
        //            string fieldValue = field.Value;
        //            string fieldSection = section.Key;

        //            //Console.WriteLine("Has new field!");
        //            Console.WriteLine(fieldSection + ": " + fieldSection);
        //            Console.WriteLine(fieldName + ": " + fieldValue);

        //            //BrewingProcessHandler brewingProcessHandler = BrewingProcessHandler.GetInstance();
        //            //Brew brew = brewingProcessHandler.GetBrew(_brewInProcess.BrewNumber);
        //            Brew brew = this;

        //            LiveBrewCommandDispatcherFactory liveBrewCommandDispatcherFactory = LiveBrewCommandDispatcherFactory.GetInstance();
        //            LiveBrewCommandDispatcher liveBrewCommandDispatcher = liveBrewCommandDispatcherFactory.GetLiveBrewCommandDispatcher(fieldSection);

        //            liveBrewCommandDispatcher.CreateLiveBrewCommands(fieldName, fieldValue, brew, fieldSection);
        //            liveBrewCommandDispatcher.SendAllCommands(this);
        //        }
        //    }

        //}
    }
}