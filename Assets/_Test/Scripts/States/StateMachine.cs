using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Test.States
{
    public abstract class StateMachine : MonoBehaviour, IStationStateSwither
    {
        protected BaseState _currentState = null;
        protected List<BaseState> _allStates;

        public void SwitchState<T>() where T : BaseState
        {
            var state = _allStates.FirstOrDefault(s => s is T);
            if (_currentState != null)
                _currentState.Stop();
            state.Start();
            _currentState = state;
        }
    }

}
