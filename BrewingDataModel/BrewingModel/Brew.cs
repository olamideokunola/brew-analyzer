/*
 * An abstraction for a Brew
 */

using System;
using System.Collections.Generic;
using ProcessEquipmentParameters;
using ProcessFields.ProcessDurations;
using Util;

//using BrewingModel.Datasources;

namespace BrewingModel
{
    public class Brew : IBrew
    {
        private string _startDate;

        private string _startTime;
        private string _brandName;
        private string _brewNumber;

        //Delegate state
        private IBrewState _currentState;

        //Brew states
        private IBrewState _idleState;
        private IBrewState _inProcessState;
        private IBrewState _completedState;

        //Process Equipment Parameters Collections
        private IDictionary<ProcessEquipment, IDictionary<string, string>> processEquipmentParameters =
            new Dictionary<ProcessEquipment, IDictionary<string, string>>();

        public IDictionary<ProcessEquipment, IDictionary<string, string>> ProcessEquipmentParameters
        {
            get
            {
                return processEquipmentParameters;
            }
        }

        public Brew(string startDate, string brandName, string brewNumber)
        {
            _startDate = startDate;
            _startTime = "";
            _brandName = brandName;
            _brewNumber = brewNumber;

            //Setup processEquipmentParameters
            processEquipmentParameters.Add(ProcessEquipment.MashCopper, new Dictionary<string, string>());
            processEquipmentParameters.Add(ProcessEquipment.MashTun, new Dictionary<string, string>());
            processEquipmentParameters.Add(ProcessEquipment.MashFilter, new Dictionary<string, string>());
            processEquipmentParameters.Add(ProcessEquipment.HoldingVessel, new Dictionary<string, string>());
            processEquipmentParameters.Add(ProcessEquipment.WortCopper, new Dictionary<string, string>());
            processEquipmentParameters.Add(ProcessEquipment.Whirlpool, new Dictionary<string, string>());

            //Setup states
            _idleState = new BrewIdleState();
            _inProcessState = new BrewInProcessState();
            _completedState = new BrewCompletedState();

            CurrentState = _idleState;
        }

        public Brew()
        {

        }

        //getters
        public string StartDate
        {
            get
            {
                return _startDate;
            }
        }

        public string StartTime
        {
            get
            {
                return _startTime;
            }
        }

        public string BrandName
        {
            get
            {
                return _brandName;
            }
        }

        public string BrewNumber
        {
            get
            {
                return _brewNumber;
            }
        }

        //Methods
        public void SetState(IBrewState newState)
        {
            CurrentState = newState;
            PrintCurrentState();
        }


        //Getters for states
        public IBrewState IdleState
        {
            get
            {
                return _idleState;
            }
        }

        public IBrewState InProcessState
        {
            get
            {
                return _inProcessState;
            }
        }

        public IBrewState CompletedState
        {
            get
            {
                return _completedState;
            }
        }

        public IBrewState CurrentState { get => _currentState; set => _currentState = value; }

        public string Year 
        {
            get
            {
                StringDateWorker stringDateWorker = StringDateWorker.GetInstance();
                return stringDateWorker.GetYear(_startDate);
            }
        }

        public string Month
        {
            get
            {
                StringDateWorker stringDateWorker = StringDateWorker.GetInstance();
                return stringDateWorker.GetMonth(_startDate);
            }
        }

        public string Day
        {
            get
            {
                StringDateWorker stringDateWorker = StringDateWorker.GetInstance();
                return stringDateWorker.GetDay(_startDate);
            }
        }

        //Getters & Setters for Process Equipment Fields
        public string GetProcessParameterValue(ProcessEquipment processEquipment, string parameterName)
        {
            string parameterValue = "";
            IDictionary<string, string> processParameter = new Dictionary<string, string>();
            processParameter = processEquipmentParameters[processEquipment];
            if (processParameter.ContainsKey(parameterName))
            {
                parameterValue = processParameter[parameterName];
            }
            return parameterValue;
        }

        public void SetProcessParameterValue(ProcessEquipment processEquipment, string parameterName, string parameterValue)
        {
            IDictionary<string, string> processParameter = new Dictionary<string, string>();
            processParameter = processEquipmentParameters[processEquipment];
            if (!processParameter.ContainsKey(parameterName))
            {
                processParameter.Add(parameterName, parameterValue);
            }
            else
            {
                processParameter[parameterName] = parameterValue;
            }
        }

        // Method foor getting process durations
        //private string GetProcessDuration1(ProcessEquipment processEquipment, string processStartTime, string processEndTime)
        //{
        //    string startTimeString = GetProcessParameterValue(processEquipment, processStartTime);
        //    string endTimeString = GetProcessParameterValue(processEquipment, processEndTime);

        //    if (startTimeString != "" && endTimeString != "")
        //    {
        //        DateTime startTime = DateHelper.GetProcessParameterDateTime(startTimeString);
        //        DateTime endTime = DateHelper.GetProcessParameterDateTime(GetProcessParameterValue(processEquipment, processEndTime));

        //        TimeSpan processDuration = endTime - startTime;

        //        string formattedDuration = processDuration.ToString("c");

        //        return formattedDuration;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        private TimeSpan GetProcessDuration(ProcessEquipment processEquipment, string processStartTime, string processEndTime)
        {
            string startTimeString = GetProcessParameterValue(processEquipment, processStartTime);
            string endTimeString = GetProcessParameterValue(processEquipment, processEndTime);

            if (startTimeString != "" && endTimeString != "")
            {
                DateTime startTime = DateHelper.GetProcessParameterDateTime(startTimeString);
                DateTime endTime = DateHelper.GetProcessParameterDateTime(GetProcessParameterValue(processEquipment, processEndTime));

                TimeSpan processDuration = endTime - startTime;

                //string formattedDuration = processDuration.ToString("c");

                return processDuration;
            }
            else
            {
                return TimeSpan.Zero;
            }   
        }

        // MashCopper Process Duration Calculations
        public IDictionary<string, TimeSpan> GetMashCopperProcessDurations()
        {
            // MashCopper
            IDictionary<string, TimeSpan> mashCopperProcessDurations = new Dictionary<string, TimeSpan>
            {
                { MashCopperProcessDurations.MashingInDuration.ToString(), GetMashCopperMashingInDuration() },
                { MashCopperProcessDurations.ProteinRestDuration.ToString(), GetMashCopperProteinRestDuration() },
                { MashCopperProcessDurations.HeatingUp1Duration.ToString(), GetMashCopperHeatingUp1Duration() },
                { MashCopperProcessDurations.Rest1Duration.ToString(), GetMashCopperRest1Duration() },
                { MashCopperProcessDurations.HeatingUp2Duration.ToString(), GetMashCopperHeatingUp2Duration() },
                { MashCopperProcessDurations.Rest2Duration.ToString(), GetMashCopperRest2Duration() }
            };

            return mashCopperProcessDurations;
        }

        private TimeSpan GetMashCopperRest2Duration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashCopper;
            string processStartTimeString = MashCopperProcessParameters.HeatingUp2EndTime.ToString();
            string processEndTimeString = MashCopperProcessParameters.Rest2EndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private TimeSpan GetMashCopperHeatingUp2Duration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashCopper;
            string processStartTimeString = MashCopperProcessParameters.Rest1EndTime.ToString();
            string processEndTimeString = MashCopperProcessParameters.HeatingUp2EndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private TimeSpan GetMashCopperRest1Duration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashCopper;
            string processStartTimeString = MashCopperProcessParameters.HeatingUp1EndTime.ToString();
            string processEndTimeString = MashCopperProcessParameters.Rest1EndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private TimeSpan GetMashCopperHeatingUp1Duration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashCopper;
            string processStartTimeString = MashCopperProcessParameters.ProteinRestEndTime.ToString();
            string processEndTimeString = MashCopperProcessParameters.HeatingUp1EndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private TimeSpan GetMashCopperProteinRestDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashCopper;
            string processStartTimeString = MashCopperProcessParameters.MashingInEndTime.ToString();
            string processEndTimeString = MashCopperProcessParameters.ProteinRestEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private TimeSpan GetMashCopperMashingInDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashCopper;
            string processStartTimeString = MashCopperProcessParameters.MashingInStartTime.ToString();
            string processEndTimeString = MashCopperProcessParameters.MashingInEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        // MashTun Process Duration Calculations
        public IDictionary<string, TimeSpan> GetMashTunProcessDurations()
        {
            // MashTun
            IDictionary<string, TimeSpan> mashTunProcessDurations = new Dictionary<string, TimeSpan>
            {
                { MashTunProcessDurations.MashingInDuration.ToString(), GetMashTunMashingInDuration() },
                { MashTunProcessDurations.ProteinRestDuration.ToString(), GetMashTunProteinRestDuration() },
                { MashTunProcessDurations.SaccharificationDuration.ToString(), GetMashTunSaccharificationDuration() },
                { MashTunProcessDurations.HeatingUpDuration.ToString(), GetMashTunHeatingUpDuration() },
            };

            return mashTunProcessDurations;
        }

        private TimeSpan GetMashTunHeatingUpDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashTun;
            string processStartTimeString = MashTunProcessParameters.SacharificationRestEndTime.ToString();
            string processEndTimeString = MashTunProcessParameters.HeatingUpEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private TimeSpan GetMashTunSaccharificationDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashTun;
            string processStartTimeString = MashTunProcessParameters.ProteinRestEndTime.ToString();
            string processEndTimeString = MashTunProcessParameters.SacharificationRestEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private TimeSpan GetMashTunProteinRestDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashTun;
            string processStartTimeString = MashTunProcessParameters.MashingInEndTime.ToString();
            string processEndTimeString = MashTunProcessParameters.ProteinRestEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private TimeSpan GetMashTunMashingInDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashTun;
            string processStartTimeString = MashTunProcessParameters.MashingInStartTime.ToString();
            string processEndTimeString = MashTunProcessParameters.MashingInEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        // MashFilter Process Duration Calculations
        public IDictionary<string, TimeSpan> GetMashFilterProcessDurations()
        {
            // MashFilter
            IDictionary<string, TimeSpan> mashFilterProcessDurations = new Dictionary<string, TimeSpan>
            {
                { MashFilterProcessDurations.MainWortFiltrationDuration.ToString(), GetMashFilterMainWortFiltrationDuration() },
                { MashFilterProcessDurations.SpargingRestDuration.ToString(), GetMashFilterSpargingRestDuration() },
                { MashFilterProcessDurations.TotalFiltrationDuration.ToString(), GetMashFilterTotalFiltrationDuration() },
            };

            return mashFilterProcessDurations;
        }

        private TimeSpan GetMashFilterTotalFiltrationDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashFilter;
            string processStartTimeString = MashFilterProcessParameters.PrefillingEndTime.ToString();
            string processEndTimeString = MashFilterProcessParameters.ExtraSpargingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private TimeSpan GetMashFilterSpargingRestDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashFilter;
            string processStartTimeString = MashFilterProcessParameters.MainMashFiltrationEndTime.ToString();
            string processEndTimeString = MashFilterProcessParameters.SpargingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private TimeSpan GetMashFilterMainWortFiltrationDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashFilter;
            string processStartTimeString = MashFilterProcessParameters.WeakWortTransferEndTime.ToString();
            string processEndTimeString = MashFilterProcessParameters.MainMashFiltrationEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }


        // WortCopper Process Duration Calculations
        public IDictionary<string, TimeSpan> GetWortCopperProcessDurations()
        {
            // WortCopper
            IDictionary<string, TimeSpan> mashFilterProcessDurations = new Dictionary<string, TimeSpan>
            {
                { WortCopperProcessDurations.HeatToBoilDuration.ToString(), GetWortCopperHeatToBoilDuration() },
                { WortCopperProcessDurations.BoilingDuration.ToString(), GetWortCopperBoilingDuration() }
            };

            return mashFilterProcessDurations;
        }

        private TimeSpan GetWortCopperBoilingDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.WortCopper;
            string processStartTimeString = WortCopperProcessParameters.HeatingEndTime.ToString();
            string processEndTimeString = WortCopperProcessParameters.BoilingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private TimeSpan GetWortCopperHeatToBoilDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.WortCopper;
            string processStartTimeString = WortCopperProcessParameters.HeatingStartTime.ToString();
            string processEndTimeString = WortCopperProcessParameters.HeatingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        // Whirlpool Process Duration Calculations
        public IDictionary<string, TimeSpan> GetWhirlpoolProcessDurations()
        {
            // Whirlpool
            IDictionary<string, TimeSpan> mashFilterProcessDurations = new Dictionary<string, TimeSpan>
            {
                { WhirlpoolProcessDurations.CastingDuration.ToString(), GetWhirlpoolCastingDuration() },
                { WhirlpoolProcessDurations.RestDuration.ToString(), GetWhirlpoolRestDuration() },
                { WhirlpoolProcessDurations.CoolingDuration.ToString(), GetWhirlpoolCoolingDuration() }
            };

            return mashFilterProcessDurations;
        }

        private TimeSpan GetWhirlpoolCoolingDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.Whirlpool;
            string processStartTimeString = WhirlpoolProcessParameters.RestingEndTime.ToString();
            string processEndTimeString = WhirlpoolProcessParameters.CoolingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private TimeSpan GetWhirlpoolRestDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.Whirlpool;
            string processStartTimeString = WhirlpoolProcessParameters.CastingEndTime.ToString();
            string processEndTimeString = WhirlpoolProcessParameters.RestingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private TimeSpan GetWhirlpoolCastingDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.Whirlpool;
            string processStartTimeString = WhirlpoolProcessParameters.CastingStartTime.ToString();
            string processEndTimeString = WhirlpoolProcessParameters.CastingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }



        //Event methods
        public void StartBrew(string startTime)
        {
            CurrentState.StartMashCopperMashingIn(startTime, this);
        }

        //Action Methods in state classes
        public void PrintCurrentState()
        {
            Console.WriteLine("-----------");
            Console.WriteLine("Current Brew State: " + CurrentState.GetType());
            Console.WriteLine("-----------");
        }

        // Load data from Datasource
        internal void LoadEquipmentProcessParameters(ProcessEquipment processEquipment)
        {
            IDictionary<string, string> loadedProcessEquipmentParameters = processEquipmentParameters[ProcessEquipment.MashCopper];

            //Datasource datasource = XlDatasource.GetInstance();
        }

        //public void Save()
        //{
        //    DatasourceHandler datasourceHandler = DatasourceHandler.GetInstance();
        //    datasourceHandler.SaveBrew(this);
        //}
    }
}
