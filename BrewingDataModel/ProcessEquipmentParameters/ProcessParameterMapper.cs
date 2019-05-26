using ProcessEquipmentParameters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewingDataModel.ProcessEquipmentParameters
{
    public class ProcessParameterMapper
    {
        private IDictionary<string, string> processParameterMap = new Dictionary<string, string>();

        //Singleton
        private static ProcessParameterMapper _uniqueInstance = null;

        //lazy construction of instance
        public static ProcessParameterMapper GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new ProcessParameterMapper();
            }
            return _uniqueInstance;
        }

        private ProcessParameterMapper()
        {
            SetupProcessParameterMap();
        }

        private void SetupProcessParameterMap()
        {
            SetupMashCopperProcessParameterMap();
            SetupMashTunProcessParameterMap();
            SetupMashFilterProcessParameterMap();
            SetupHoldingVesselProcessParameterMap();
            SetupWortCopperProcessParameterMap();
            SetupWhirlpoolProcessParameterMap();
        }

        private void SetupWhirlpoolProcessParameterMap()
        {
            processParameterMap.Add("Start Casting - Finish" + " - WHIRLPOOL", Enum.GetName(typeof(WhirlpoolProcessParameters), WhirlpoolProcessParameters.CastingStartTime));
            processParameterMap.Add("Casting Time - Finish" + " - WHIRLPOOL", Enum.GetName(typeof(WhirlpoolProcessParameters), WhirlpoolProcessParameters.CastingEndTime));
            processParameterMap.Add("Rest Time - Finish" + " - WHIRLPOOL", Enum.GetName(typeof(WhirlpoolProcessParameters), WhirlpoolProcessParameters.RestingEndTime));
            processParameterMap.Add("Wort Cooling - Finish" + " - WHIRLPOOL", Enum.GetName(typeof(WhirlpoolProcessParameters), WhirlpoolProcessParameters.CoolingEndTime));
            processParameterMap.Add("Whirlpool Ready at - Finish" + " - WHIRLPOOL", Enum.GetName(typeof(WhirlpoolProcessParameters), WhirlpoolProcessParameters.ReadyAtTime));
        }

        private void SetupWortCopperProcessParameterMap()
        {
            processParameterMap.Add("Start Heating(Boiling) - Finish" + " - WORT COPPER", Enum.GetName(typeof(WortCopperProcessParameters), WortCopperProcessParameters.HeatingStartTime));
            processParameterMap.Add("Heating Time (Boiling) - Finish" + " - WORT COPPER", Enum.GetName(typeof(WortCopperProcessParameters), WortCopperProcessParameters.HeatingEndTime));
            processParameterMap.Add("Wort Boiling - Finish" + " - WORT COPPER", Enum.GetName(typeof(WortCopperProcessParameters), WortCopperProcessParameters.BoilingEndTime));
            processParameterMap.Add("Extra Boiling Time - Finish" + " - WORT COPPER", Enum.GetName(typeof(WortCopperProcessParameters), WortCopperProcessParameters.ExtraBoilingEndTime));
            processParameterMap.Add("Start Casting - Finish" + " - WORT COPPER", Enum.GetName(typeof(WortCopperProcessParameters), WortCopperProcessParameters.CastingStartTime));
            processParameterMap.Add("Casting to Whirpool - Finish" + " - WORT COPPER", Enum.GetName(typeof(WortCopperProcessParameters), WortCopperProcessParameters.CastingEndTime));

        }


        private void SetupHoldingVesselProcessParameterMap()
        {
            //throw new NotImplementedException();
        }

        private void SetupMashFilterProcessParameterMap()
        {
            processParameterMap.Add("Start Prefilling - Finish" + " - MASH FILTER", Enum.GetName(typeof(MashFilterProcessParameters), MashFilterProcessParameters.PrefillingStartTime));
            processParameterMap.Add("Prefilling Time - Finish" + " - MASH FILTER", Enum.GetName(typeof(MashFilterProcessParameters), MashFilterProcessParameters.PrefillingEndTime));
            processParameterMap.Add("WeakWort Transfer to WWT - Finish" + " - MASH FILTER", Enum.GetName(typeof(MashFilterProcessParameters), MashFilterProcessParameters.WeakWortTransferEndTime));
            processParameterMap.Add("Main Mash Filtration Time - Finish" + " - MASH FILTER", Enum.GetName(typeof(MashFilterProcessParameters), MashFilterProcessParameters.MainMashFiltrationEndTime));
            processParameterMap.Add("Sparging Time - Finish" + " - MASH FILTER", Enum.GetName(typeof(MashFilterProcessParameters), MashFilterProcessParameters.SpargingEndTime));
            processParameterMap.Add("Sparging to WWT Time - Finish" + " - MASH FILTER", Enum.GetName(typeof(MashFilterProcessParameters), MashFilterProcessParameters.SpargingToWWTEndTime));
            processParameterMap.Add("Extra Sparging Time - Finish" + " - MASH FILTER", Enum.GetName(typeof(MashFilterProcessParameters), MashFilterProcessParameters.ExtraSpargingEndTime));
            processParameterMap.Add("Dripping Time - Finish" + " - MASH FILTER", Enum.GetName(typeof(MashFilterProcessParameters), MashFilterProcessParameters.DrippingEndTime));
            processParameterMap.Add("Spent Grain Discharge - Finish" + " - MASH FILTER", Enum.GetName(typeof(MashFilterProcessParameters), MashFilterProcessParameters.SpentGrainDischargeEndTime));
            processParameterMap.Add("Mash Filter Ready at - Finish" + " - MASH FILTER", Enum.GetName(typeof(MashFilterProcessParameters), MashFilterProcessParameters.ReadyAtTime));
        }

        private void SetupMashTunProcessParameterMap()
        {
            processParameterMap.Add("Tranpsort Time - Finish" + " - MASH TUN", Enum.GetName(typeof(MashTunProcessParameters), MashTunProcessParameters.MashingInStartTime));
            processParameterMap.Add("Mash in Time - Finish" + " - MASH TUN", Enum.GetName(typeof(MashTunProcessParameters), MashTunProcessParameters.MashingInEndTime));
            processParameterMap.Add("Protein Rest Time - Finish" + " - MASH TUN", Enum.GetName(typeof(MashTunProcessParameters), MashTunProcessParameters.ProteinRestEndTime));
            processParameterMap.Add("Sacha. Rest time - Finish" + " - MASH TUN", Enum.GetName(typeof(MashTunProcessParameters), MashTunProcessParameters.SacharificationRestEndTime));
            processParameterMap.Add("Heating Time - Finish" + " - MASH TUN", Enum.GetName(typeof(MashTunProcessParameters), MashTunProcessParameters.HeatingUpEndTime));
            processParameterMap.Add("Mash Tun ready at - Finish" + " - MASH TUN", Enum.GetName(typeof(MashTunProcessParameters), MashTunProcessParameters.MashTunReadyAt));
        }

        private void SetupMashCopperProcessParameterMap()
        {
            processParameterMap.Add("Transport Time RAW Sorguum to WB MC - Finish" + " - MASH COPPER", Enum.GetName(typeof(MashCopperProcessParameters), MashCopperProcessParameters.MashingInStartTime));
            processParameterMap.Add("Mash in Time - Finish" + " - MASH COPPER", Enum.GetName(typeof(MashCopperProcessParameters), MashCopperProcessParameters.MashingInEndTime));
            processParameterMap.Add("Protein Rest Time - Finish" + " - MASH COPPER", Enum.GetName(typeof(MashCopperProcessParameters), MashCopperProcessParameters.ProteinRestEndTime));
            processParameterMap.Add("Heating Time - Finish" + " - MASH COPPER", Enum.GetName(typeof(MashCopperProcessParameters), MashCopperProcessParameters.HeatingUp1EndTime));
            processParameterMap.Add("Rest time - Finish" + " - MASH COPPER", Enum.GetName(typeof(MashCopperProcessParameters), MashCopperProcessParameters.Rest1EndTime));
            processParameterMap.Add("Heating Time - Finish 2" + " - MASH COPPER", Enum.GetName(typeof(MashCopperProcessParameters), MashCopperProcessParameters.HeatingUp2EndTime));
            processParameterMap.Add("Rest Time - Finish 2" + " - MASH COPPER", Enum.GetName(typeof(MashCopperProcessParameters), MashCopperProcessParameters.Rest2EndTime));
            processParameterMap.Add("Mash Transfer to MT - Finish" + " - MASH COPPER", Enum.GetName(typeof(MashCopperProcessParameters), MashCopperProcessParameters.TransferToMtEndTime));
        }

        public string GetProcessParameterName(string fieldName)
        {
            if (!processParameterMap.ContainsKey(fieldName))
            {
                return "";
            }
            return processParameterMap[fieldName];
        }
    }
}
