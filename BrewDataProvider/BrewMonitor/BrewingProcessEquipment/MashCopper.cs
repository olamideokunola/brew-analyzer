using System;
using System.Collections.Generic;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashCopper 
    {
        //fields
        private Brew _brew;
        private IMashCopperState _currentState;

        public IMashCopperState CurrentState
        {
            get
            {
                return _currentState;
            }
        }

        //state members
        private IMashCopperState _idleState;
        private IMashCopperState _mashingInState;
        private IMashCopperState _proteinRestState;
        private IMashCopperState _heatingUp1State;
        private IMashCopperState _rest1State;
        private IMashCopperState _heatingUp2State;
        private IMashCopperState _rest2State;
        private IMashCopperState _mashTransferToMashTunState;
        //private IMashCopperState _emptyState;

        public MashCopper(Brew brew)
        {
            _brew = brew;
            _idleState = new MashCopperIdleState();
            _mashingInState = new MashCopperMashingInState();
            _proteinRestState = new MashCopperProteinRestState();
            _heatingUp1State = new MashCopperHeatingUp1State();
            _rest1State = new MashCopperRest1State();
            _heatingUp2State = new MashCopperHeatingUp2State();
            _rest2State = new MashCopperRest2State();
            _mashTransferToMashTunState = new MashCopperMashTransferToMashTunState();
            //_emptyState = new MashCopperEmptyState();

            _currentState = _idleState;
            _currentState.InitBrew(this, _brew);
        }

        public MashCopper()
        {
            _brew = new Brew();
            _idleState = new MashCopperIdleState();
            _mashingInState = new MashCopperMashingInState();
            _proteinRestState = new MashCopperProteinRestState();
            _heatingUp1State = new MashCopperHeatingUp1State();
            _rest1State = new MashCopperRest1State();
            _heatingUp2State = new MashCopperHeatingUp2State();
            _rest2State = new MashCopperRest2State();
            _mashTransferToMashTunState = new MashCopperMashTransferToMashTunState();
            //_emptyState = new MashCopperEmptyState();

            _currentState = _idleState;
            //_currentState.InitBrew(this, _brew);

        }


        //Getters
        public Brew Brew
        {
            get
            {
                return _brew;
            }
        }

        public IMashCopperState IdleState
        {
            get
            {
                return _idleState;
            }
        }

        public IMashCopperState MashingInState
        {
            get
            {
                return _mashingInState;
            }
        }

        public IMashCopperState ProteinRestState
        {
            get
            {
                return _proteinRestState;
            }
        }

        public IMashCopperState HeatingUp1State
        {
            get
            {
                return _heatingUp1State;
            }
        }

        public IMashCopperState Rest1State
        {
            get
            {
                return _rest1State;
            }
        }

        public IMashCopperState HeatingUp2State
        {
            get
            {
                return _heatingUp2State;
            }
        }

        public IMashCopperState Rest2State
        {
            get
            {
                return _rest2State;
            }
        }

        public IMashCopperState MashTransferToMashTunState
        {
            get
            {
                return _mashTransferToMashTunState;
            }
        }

        //public IMashCopperState EmptyState
        //{
        //    get
        //    {
        //        return _emptyState;
        //    }
        //}

        //Methods
        public void InitBrew(Brew brew)
        {
            _brew = brew;
        }

        public void SetState(IMashCopperState newState)
        {
            _currentState = newState;
            _currentState.OnEntry(this, _brew);
            PrintCurrentState();
            //return _currentState.ToString();
        }

        //State events
        public void StartMashingIn(Brew brew, string paramText, string startTime)
        {
            _currentState.StartMashingIn(paramText, startTime, this, brew);
           //_brew.Save();
        }

        public void SetEndTime(string paramText, string endTime)
        {
            _currentState.SetEndTime(paramText, endTime, this, _brew);
           // _brew.Save();
        }

        public void SetProteinRestTemperature(string temperature)
        {
            _currentState.SetProteinRestTemperature(temperature, this, _brew);
          //  _brew.Save();
        }

        //State actions in state classs


        //Other methods
        public void PrintCurrentState()
        {
            IStateDescription currentStateDescription = (BrewingModel.BrewingProcessEquipment.IStateDescription)_currentState;
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(currentStateDescription.GetStateDescription());
            Console.WriteLine("------------------------------------------");
        }

        public string CurrentStateString()
        {
            IStateDescription currentStateDescription = (BrewingModel.BrewingProcessEquipment.IStateDescription)_currentState;
            return currentStateDescription.GetShortState();
        }

    }
}
