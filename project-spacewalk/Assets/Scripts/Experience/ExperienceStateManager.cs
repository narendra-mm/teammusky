using System;
using System.Collections.Generic;
using GoogleRCS.Experience.ExperienceState;
using UnityEngine;

namespace GoogleRCS.Experience
{
    public class ExperienceStateManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public Action<IExperienceState, IExperienceState> OnStateChanged;

        private readonly Dictionary<Type, IExperienceState> _stateTable = new Dictionary<Type, IExperienceState>();
        private readonly List<Type> _stateOrderList = new List<Type>();
        public IExperienceState CurrentState { get; private set; }
        public IExperienceState PreviousState { get; private set; }
        public IExperienceState NextState { get; private set; }

        private bool _updateCurrentState = true;
        private string _name = nameof(ExperienceStateManager);

        public bool ShowLog { get; set; }

        protected void Update()
        {
            if (_updateCurrentState)
            {
                CurrentState?.UpdateState();
            }
        }

        public void Setup(IEnumerable<IExperienceState> experienceStates, string name = null)
        {
            if (name != null) _name = name;

            foreach (IExperienceState gamePhase in experienceStates)
            {
                _stateTable.Add(gamePhase.GetType(), gamePhase);
                _stateOrderList.Add(gamePhase.GetType());
                gamePhase.InitializeState(this);

                if (ShowLog) Debug.Log($"[{_name}] Added: {gamePhase.GetType().Name}");
            }
        }

        public T GetState<T>() where T : IExperienceState
        {
            var stateType = typeof(T);

            if (!_stateTable.TryGetValue(stateType, out IExperienceState state))
            {
                throw new Exception($"[{_name}] Not defined in state table: {stateType}");
            }

            return (T)state;
        }

        public T TransitionTo<T>() where T : IExperienceState
        {
            var nextState = GetState<T>();

            if (ShowLog) Debug.Log($"[{_name}] {(CurrentState == null ? "<NONE>" : CurrentState.GetType().Name)} -> {nextState.GetType().Name}");

            CurrentState?.ExitState();
            PreviousState = CurrentState;
            CurrentState = nextState;
            nextState.EnterState();
			OnStateChanged?.Invoke(PreviousState, nextState);
            return (T)CurrentState;
        }

        public void DisposeStates()
        {
            // CurrentState?.ExitState(this);

            foreach (IExperienceState gameState in _stateTable.Values)
            {
                gameState?.DisposeState();
            }

            _stateTable.Clear();
        }

        public void TransitionToNext()
        {
            var curIndex = _stateOrderList.IndexOf(CurrentState.GetType());
            var newIndex = _stateOrderList.WrapIndex(curIndex + 1);
            TransitionTo(newIndex);
        }

        public void TransitionToPrevious()
        {
            var curIndex = _stateOrderList.IndexOf(CurrentState.GetType());
            var newIndex = _stateOrderList.WrapIndex(curIndex - 1);
            TransitionTo(newIndex);
        }

        private void TransitionTo(int index)
        {
            CurrentState.ExitState();
            var stateType = _stateOrderList[index];
            var nextState = _stateTable[stateType];
            CurrentState = nextState;
            CurrentState.EnterState();
        }
    }
}