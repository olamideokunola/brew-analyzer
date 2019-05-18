using System;
using BrewingModel;

namespace BrewingModel.BrewingProcessEquipment
{
    public class HoldingVessel
    {
        //fields
        private Brew _brew;
        private IHoldingVesselState _currentState;

        //state members
        private IHoldingVesselState _idleState;
        private IHoldingVesselState _fillingState;
        private IHoldingVesselState _transferToWcState;
        private IHoldingVesselState _rinsingState;

        public HoldingVessel(Brew brew)
        {
            _brew = brew;
            _idleState = new HoldingVesselIdleState();
            _fillingState = new HoldingVesselFillingState();
            //_transferToWcState = new HoldingVesselTransferToWcState();
            _rinsingState = new HoldingVesselRinsingState();

            _currentState = _idleState;
            _currentState.InitBrew(this, _brew);
        }

        public HoldingVessel()
        {
            _brew = new Brew();
            _idleState = new HoldingVesselIdleState();
            _fillingState = new HoldingVesselFillingState();
            //_transferToWcState = new HoldingVesselTransferToWcState();
            _rinsingState = new HoldingVesselRinsingState();

            _currentState = _idleState;
        }


        //Getters
        public Brew Brew
        {
            get
            {
                return _brew;
            }
        }

        public IHoldingVesselState IdleState
        {
            get
            {
                return _idleState;
            }
        }

        public IHoldingVesselState FillingState
        {
            get
            {
                return _fillingState;
            }
        }

        public IHoldingVesselState TransferToWcState
        {
            get
            {
                return _transferToWcState;
            }
        }

        public IHoldingVesselState RinsingState
        {
            get
            {
                return _rinsingState;
            }
        }


        //Methods
        public void InitBrew(Brew brew)
        {
            _brew = brew;
        }

        public void SetState(IHoldingVesselState newState)
        {
            _currentState = newState;
            _currentState.OnEntry(this, _brew);
            PrintCurrentState();
            //return _currentState.ToString();
        }

        //State events
        public void StartFilling(string paramText, string startTime)
        {
            _currentState.StartFilling(paramText, startTime, this, _brew);
            //_brew.SetState(new BrewInProcessState());
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
