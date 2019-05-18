using System;
using BrewingModel;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashTun
    {
        //fields
        private Brew _brew;
        private IMashTunState _currentState;

        //state members
        private IMashTunState _idleState;
        private IMashTunState _mashingInState;
        private IMashTunState _proteinRestState;
        private IMashTunState _sacRestState;
        private IMashTunState _heatingUpState;
        private IMashTunState _mashTransferToMashFilterState;


        public MashTun(Brew brew)
        {
            _brew = brew;
            _idleState = new MashTunIdleState();
            _mashingInState = new MashTunMashingInState();
            _proteinRestState = new MashTunProteinRestState();
            _sacRestState = new MashTunSacRestState();
            _heatingUpState = new MashTunHeatingUpState();
            _mashTransferToMashFilterState = new MashTunMashTransferToMashFilterState();

            _currentState = _idleState;
            _currentState.InitBrew(this, _brew);
        }

        public MashTun()
        {
            _brew = new Brew();
            _idleState = new MashTunIdleState();
            _mashingInState = new MashTunMashingInState();
            _proteinRestState = new MashTunProteinRestState();
            _sacRestState = new MashTunSacRestState();
            _heatingUpState = new MashTunHeatingUpState();
            _mashTransferToMashFilterState = new MashTunMashTransferToMashFilterState();

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

        public IMashTunState IdleState
        {
            get
            {
                return _idleState;
            }
        }

        public IMashTunState MashingInState
        {
            get
            {
                return _mashingInState;
            }
        }

        public IMashTunState ProteinRestState
        {
            get
            {
                return _proteinRestState;
            }
        }

        public IMashTunState SacRestState
        {
            get
            {
                return _sacRestState;
            }
        }

        public IMashTunState HeatingUpState
        {
            get
            {
                return _heatingUpState;
            }
        }


        public IMashTunState MashTransferToMashFilterState
        {
            get
            {
                return _mashTransferToMashFilterState;
            }
        }

        //public IMashTunState EmptyState
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

        public void SetState(IMashTunState newState)
        {
            _currentState = newState;
            _currentState.OnEntry(this, _brew);
            PrintCurrentState();
            //return _currentState.ToString();
        }

        //State events
        public void StartMashingIn(string paramText, string startTime)
        {
            _currentState.StartMashingIn(paramText, startTime, this, _brew);
            _brew.SetState(new BrewInProcessState());
        }

        public void SetEndTime(string paramText, string endTime)
        {
            _currentState.SetEndTime(paramText, endTime, this, _brew);
        }

        public void SetProteinRestTemperature(string temperature)
        {
            _currentState.SetProteinRestTemperature(temperature, this, _brew);
        }

        public void SetSacharificationRestTemperature(string temperature)
        {
            _currentState.SetSacharificationRestTemperature(temperature, this, _brew);
        }

        public void SetHeatingUpTemperature(string temperature)
        {
            _currentState.SetHeatingUpTemperature(temperature, this, _brew);
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
