/*
 * An abstraction for a Brew
 */

using System;
using System.Collections.Generic;
using ProcessEquipmentParameters;
using ProcessFields.ProcessDurations;

namespace BrewingModel
{
    public interface IBrew
    {

        IDictionary<ProcessEquipment, IDictionary<string, string>> ProcessEquipmentParameters { get; }

        //getters
        string StartDate { get; }
        string StartTime { get; }
        string BrandName { get; }
        string BrewNumber{ get; }

        string Year { get; }
        string Month { get; }
        string Day { get; }

        //Methods
        void SetState(IBrewState newState);

        //Getters for states
        IBrewState IdleState { get; }
        IBrewState InProcessState { get; }
        IBrewState CompletedState { get; }

        //Getters & Setters for Process Equipment Fields
        string GetProcessParameterValue(ProcessEquipment processEquipment, string parameterName);
        void SetProcessParameterValue(ProcessEquipment processEquipment, string parameterName, string parameterValue);

        // Process Equipment Process Duration Calculations
        IDictionary<string, TimeSpan> GetMashCopperProcessDurations();
        IDictionary<string, TimeSpan> GetMashTunProcessDurations();
        IDictionary<string, TimeSpan> GetMashFilterProcessDurations();
        IDictionary<string, TimeSpan> GetWortCopperProcessDurations();
        IDictionary<string, TimeSpan> GetWhirlpoolProcessDurations();

        //Event methods
        void StartBrew(string startTime);

        //Action Methods in state classes
        void PrintCurrentState();

        IBrewState CurrentState { get; set; }

        // Datasource methods
        // void Save();
    }
}