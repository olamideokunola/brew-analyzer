using System;
using BrewingModel;

namespace BrewingModel.BrewingProcessEquipment
{
    public class WortCopper
    {
        //fields
        private Brew _brew;
        private IWortCopperState _currentState;

        //state members
        private IWortCopperState _idleState;
        private IWortCopperState _heatingState;
        private IWortCopperState _boilingState;
        private IWortCopperState _extraBoilingState;
        private IWortCopperState _castingState;

        public WortCopper(Brew brew)
        {
            _brew = brew;
            _idleState = new WortCopperIdleState();
            _heatingState = new WortCopperHeatingState();
            _boilingState = new WortCopperBoilingState();
            _extraBoilingState = new WortCopperExtraBoilingState();
            _castingState = new WortCopperCastingState();

            _currentState = _idleState;
            _currentState.InitBrew(this, _brew);
        }

        public WortCopper()
        {
            _brew = new Brew();
            _idleState = new WortCopperIdleState();
            _heatingState = new WortCopperHeatingState();
            _boilingState = new WortCopperBoilingState();
            _extraBoilingState = new WortCopperExtraBoilingState();
            _castingState = new WortCopperCastingState();

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

        public IWortCopperState IdleState
        {
            get
            {
                return _idleState;
            }
        }

        public IWortCopperState HeatingState
        {
            get
            {
                return _heatingState;
            }
        }

        public IWortCopperState BoilingState
        {
            get
            {
                return _boilingState;
            }
        }

        public IWortCopperState ExtraBoilingState
        {
            get
            {
                return _extraBoilingState;
            }
        }

        public IWortCopperState CastingState
        {
            get
            {
                return _castingState;
            }
        }

     

        //Methods
        public void InitBrew(Brew brew)
        {
            _brew = brew;
        }

        public void SetState(IWortCopperState newState)
        {
            _currentState = newState;
            _currentState.OnEntry(this, _brew);
            PrintCurrentState();
            //return _currentState.ToString();
        }

        //State events
        public void StartHeating(string paramText, string startTime)
        {
            _currentState.StartHeating(paramText, startTime, this, _brew);
            _brew.SetState(new BrewInProcessState());
        }

        public void SetEndTime(string paramText, string endTime)
        {
            _currentState.SetEndTime(paramText, endTime, this, _brew);
        }

        public void StartCasting(string paramText, string startTime)
        {
            _currentState.StartCasting(paramText, startTime, this, _brew);
        }

        public void SetVolumeBeforeBoiling(string volume)
        {
           // _currentState.SetVolumeBeforeBoiling(volume, this, _brew);
        }

        public void SetVolumeAfterBoiling(string volume)
        {
            //_currentState.SetVolumeAfterBoiling(volume, this, _brew);
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
