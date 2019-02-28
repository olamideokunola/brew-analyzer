/*
 * An abstraction for a Brew
 */

using System;
using System.Collections.Generic;
//using BrewingModel.Datasources;
using ProcessEquipmentParameters;
using ProcessFields.ProcessDurations;

namespace BrewingModel
{
    public class BrewProxy : IBrew
    {
        private Brew _brew;

        //Process Equipment Parameters Collections
        public IDictionary<ProcessEquipment, IDictionary<string, string>> ProcessEquipmentParameters
        {
            get
            {
                return _brew.ProcessEquipmentParameters;
            }
        }

        public BrewProxy(string startDate, string brandName, string brewNumber)
        {
            _brew = new Brew(startDate, brandName, brewNumber);
        }

        public BrewProxy()
        {

        }

        //getters
        public string StartDate
        {
            get
            {
                return _brew.StartDate;
            }
        }

        public string StartTime
        {
            get
            {
                return _brew.StartTime;
            }
        }

        public string BrandName
        {
            get
            {
                return _brew.BrandName;
            }
        }

        public string BrewNumber
        {
            get
            {
                return _brew.BrewNumber;
            }
        }

        public string Year
        {
            get
            {
                return _brew.Year;
            }
        }

        public string Month
        {
            get
            {
                return _brew.Month;
            }
        }

        public string Day
        {
            get
            {
                return _brew.Day;
            }
        }

        //Methods
        public void SetState(IBrewState newState)
        {
            _brew.CurrentState = newState;
            PrintCurrentState();
        }


        //Getters for states
        public IBrewState IdleState
        {
            get
            {
                return _brew.IdleState;
            }
        }

        public IBrewState InProcessState
        {
            get
            {
                return _brew.InProcessState;
            }
        }

        public IBrewState CompletedState
        {
            get
            {
                return _brew.CompletedState;
            }
        }

        public IBrewState CurrentState { get => _brew.CurrentState; set => _brew.CurrentState = value; }

        //Getters & Setters for Process Equipment Fields
        public string GetProcessParameterValue(ProcessEquipment processEquipment, string parameterName)
        {
            IDictionary<string, string> processParameters = new Dictionary<string, string>();
            processParameters = _brew.ProcessEquipmentParameters[processEquipment];
            if (processParameters.Count == 0)
            {
                _brew.LoadEquipmentProcessParameters(processEquipment);
            }
            return _brew.GetProcessParameterValue(processEquipment, parameterName);
        }

        public void SetProcessParameterValue(ProcessEquipment processEquipment, string parameterName, string parameterValue)
        {
            _brew.SetProcessParameterValue(processEquipment, parameterName, parameterValue);
        }


        // Process Equipment Process Duration Calculations
        public IDictionary<string, TimeSpan> GetMashCopperProcessDurations()
        {
            return _brew.GetMashCopperProcessDurations();
        }

        public IDictionary<string, TimeSpan> GetMashTunProcessDurations()
        {
            return _brew.GetMashTunProcessDurations();
        }

        public IDictionary<string, TimeSpan> GetMashFilterProcessDurations()
        {
            return _brew.GetMashFilterProcessDurations();
        }

        public IDictionary<string, TimeSpan> GetWortCopperProcessDurations()
        {
            return _brew.GetWortCopperProcessDurations();
        }

        public IDictionary<string, TimeSpan> GetWhirlpoolProcessDurations()
        {
            return _brew.GetWhirlpoolProcessDurations();
        }


        //Event methods
        public void StartBrew(string startTime)
        {
            _brew.CurrentState.StartMashCopperMashingIn(startTime, _brew);
        }

        //Action Methods in state classes
        public void PrintCurrentState()
        {
            Console.WriteLine("-----------");
            Console.WriteLine("Current Brew State: " + _brew.CurrentState.GetType());
            Console.WriteLine("-----------");
        }

        //public void Save()
        //{
        //    DatasourceHandler datasourceHandler = DatasourceHandler.GetInstance();
        //    datasourceHandler.SaveBrew(_brew);
        //}
    }
}
