using System;
using System.Collections.Generic;
using BrewDataProvider.BrewMonitor;
using BrewingDataModel.ProcessEquipmentParameters;
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

                    string processParameterName = "";

                    // Set Process Parameters field and values in Brew                                       
                    switch (fieldSection)
                    {
                        case "Weigh bin Mash Copper":
                        case "MASH COPPER":
                            processParameterName = GetProcessParameterName(fieldName + " - MASH COPPER");
                            SetProcessParameterValue(ProcessEquipment.MashCopper, processParameterName, fieldValue);
                            break;
                        case "WEIGHBIN MASHTUN":                
                        case "MASH TUN":
                            processParameterName = GetProcessParameterName(fieldName + " - MASH TUN");
                            SetProcessParameterValue(ProcessEquipment.MashTun, processParameterName, fieldValue);
                            break;
                        case "MASH FILTER":
                            processParameterName = GetProcessParameterName(fieldName + " - MASH FILTER");
                            SetProcessParameterValue(ProcessEquipment.MashFilter, processParameterName, fieldValue);
                            break;
                        case "HOLDING VESSEL":                   
                        case "HOLDING VESSEL TO WORT COPPER":
                            processParameterName = GetProcessParameterName(fieldName + " - MASH VESSEL");
                            SetProcessParameterValue(ProcessEquipment.HoldingVessel, processParameterName, fieldValue);
                            break;
                        case "WORT COPPER":
                            processParameterName = GetProcessParameterName(fieldName + " - WORT COPPER");
                            SetProcessParameterValue(ProcessEquipment.WortCopper, processParameterName, fieldValue);
                            break;
                        case "WHIRLPOOL":
                            processParameterName = GetProcessParameterName(fieldName + " - WHIRLPOOL");
                            SetProcessParameterValue(ProcessEquipment.Whirlpool, processParameterName, fieldValue);
                            break;
                    }

                }
            }
        }



        private string GetProcessParameterName(string fieldName)
        {
            ProcessParameterMapper processParameterMapper = ProcessParameterMapper.GetInstance();
            return processParameterMapper.GetProcessParameterName(fieldName);
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