using System;
using BrewingModel;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashFilter
    {
        //fields
        private Brew _brew;
        private IMashFilterState _currentState;

        //state members
        private IMashFilterState _idleState;
        private IMashFilterState _prefillingState;
        private IMashFilterState _weakWortTransferState;
        private IMashFilterState _mainMashFiltrationState;
        private IMashFilterState _spargingState;
        private IMashFilterState _spargingToWwtState;
        private IMashFilterState _extraSpargingState;
        private IMashFilterState _drippingState;
        private IMashFilterState _spentGrainDischargeState;
        private IMashFilterState _cleaningState;
        //private IMashFilterState _spargingToWwtState;


        public MashFilter(Brew brew)
        {
            _brew = brew;
            _idleState = new MashFilterIdleState();
            _prefillingState = new MashFilterPrefillingState();
            _weakWortTransferState = new MashFilterWeakWortTransferState();
            _mainMashFiltrationState = new MashFilterMainMashFiltrationState();
            _spargingState = new MashFilterSpargingState();
            _spargingToWwtState = new MashFilterSpargingToWwtState();
            _extraSpargingState = new MashFilterExtraSpargingState();
            _drippingState = new MashFilterDrippingState();
            _spentGrainDischargeState = new MashFilterSpentGrainDischargeState();
            _cleaningState = new MashFilterCleaningState();

            _currentState = _idleState;
            _currentState.InitBrew(this, _brew);
        }

        public MashFilter()
        {
            _brew = new Brew();
            _idleState = new MashFilterIdleState();
            _prefillingState = new MashFilterPrefillingState();
            _weakWortTransferState = new MashFilterWeakWortTransferState();
            _mainMashFiltrationState = new MashFilterMainMashFiltrationState();
            _spargingState = new MashFilterSpargingState();
            _spargingToWwtState = new MashFilterSpargingToWwtState();
            _extraSpargingState = new MashFilterExtraSpargingState();
            _drippingState = new MashFilterDrippingState();
            _spentGrainDischargeState = new MashFilterSpentGrainDischargeState();
            _cleaningState = new MashFilterCleaningState();

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

        public IMashFilterState IdleState
        {
            get
            {
                return _idleState;
            }
        }

        public IMashFilterState PrefillingState
        {
            get
            {
                return _prefillingState;
            }
        }

        public IMashFilterState WeakWortTransferState
        {
            get
            {
                return _weakWortTransferState;
            }
        }

        public IMashFilterState MainMashFiltrationState
        {
            get
            {
                return _mainMashFiltrationState;
            }
        }

        public IMashFilterState SpargingState
        {
            get
            {
                return _spargingState;
            }
        }


        public IMashFilterState SpargingToWwtState
        {
            get
            {
                return _spargingToWwtState;
            }
        }


        public IMashFilterState ExtraSpargingState
        {
            get
            {
                return _extraSpargingState;
            }
        }

        public IMashFilterState DrippingState
        {
            get
            {
                return _drippingState;
            }
        }

        public IMashFilterState SpentGrainDischargeState
        {
            get
            {
                return _spentGrainDischargeState;
            }
        }

        public IMashFilterState CleaningState
        {
            get
            {
                return _cleaningState;
            }
        }


        //Methods
        public void InitBrew(Brew brew)
        {
            _brew = brew;
        }

        public void SetState(IMashFilterState newState)
        {
            _currentState = newState;
            _currentState.OnEntry(this, _brew);
            PrintCurrentState();
            //return _currentState.ToString();
        }

        //State events
        public void StartPrefilling(string paramText, string startTime)
        {
            _currentState.StartPrefilling(paramText, startTime, this, _brew);
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
