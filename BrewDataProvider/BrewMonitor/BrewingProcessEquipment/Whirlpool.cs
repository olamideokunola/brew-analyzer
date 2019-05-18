using System;
using BrewingModel;

namespace BrewingModel.BrewingProcessEquipment
{
    public class Whirlpool
    {
        //fields
        private Brew _brew;
        private IWhirlpoolState _currentState;

        //state members
        private IWhirlpoolState _idleState;
        private IWhirlpoolState _castingState;
        private IWhirlpoolState _restingState;
        private IWhirlpoolState _coolingState;
        private IWhirlpoolState _trubRemovalState;

        public Whirlpool(Brew brew)
        {
            _brew = brew;
            _idleState = new WhirlpoolIdleState();
            _castingState = new WhirlpoolCastingState();
            _restingState = new WhirlpoolRestingState();
            _coolingState = new WhirlpoolCoolingState();
            _trubRemovalState = new WhirlpoolTrubRemovalState();

            _currentState = _idleState;
            _currentState.InitBrew(this, _brew);
        }

        public Whirlpool()
        {
            _brew = new Brew();
            _idleState = new WhirlpoolIdleState();
            _castingState = new WhirlpoolCastingState();
            _restingState = new WhirlpoolRestingState();
            _coolingState = new WhirlpoolCoolingState();
            _trubRemovalState = new WhirlpoolTrubRemovalState();

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

        public IWhirlpoolState IdleState
        {
            get
            {
                return _idleState;
            }
        }

        public IWhirlpoolState CastingState
        {
            get
            {
                return _castingState;
            }
        }

        public IWhirlpoolState RestingState
        {
            get
            {
                return _restingState;
            }
        }

        public IWhirlpoolState CoolingState
        {
            get
            {
                return _coolingState;
            }
        }

        public IWhirlpoolState TrubRemovalState
        {
            get
            {
                return _trubRemovalState;
            }
        }

     

        //Methods
        public void InitBrew(Brew brew)
        {
            _brew = brew;
        }

        public void SetState(IWhirlpoolState newState)
        {
            _currentState = newState;
            _currentState.OnEntry(this, _brew);
            PrintCurrentState();
            //return _currentState.ToString();
        }

        //State events
        public void StartCasting(string paramText, string startTime)
        {
            _currentState.StartCasting(paramText, startTime, this, _brew);
        }

        public void SetEndTime(string paramText, string endTime)
        {
            _currentState.SetEndTime(paramText, endTime, this, _brew);
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
    }
}
