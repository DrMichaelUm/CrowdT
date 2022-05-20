#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Core.DesignPatterns
{
    public abstract class StateMachineMonoBehaviour<M> : MonoBehaviour where M : StateMachineMonoBehaviour<M>
    {
        #region Properties

        private List<State<M>> states = new List<State<M>>();
        public List<State<M>> States => states;

        private State<M> state = null;
        public State<M> State => state;

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            // Get all states
            State<M>[] bStates = gameObject.GetComponents<State<M>>();
            foreach (State<M> iState in bStates)
                AddState(iState);
        }

        protected virtual void Update()
        {
            state?.UpdateState();
        }

        protected virtual void FixedUpdate()
        {
            state?.FixedUpdateState();
        }

        protected virtual void LateUpdate()
        {
            state?.LateUpdateState();
        }

        #endregion

        #region State Methods

        public void AddState(State<M> _state)
        {
            states.Add(_state);
            _state.InitializeState((M)this);
        }

        public void RemoveState(State<M> _state)
        {
            states.Remove(_state);
        }

        public State<M> GetState(Type _type)
        {
            foreach (State<M> iState in states)
            {
                if (iState.GetType() == _type)
                    return iState;
            }
            return null;
        }

        public void SwitchToState(Type _type)
        {
            State<M> bState = GetState(_type);
            if (bState)
                SwitchToState(bState);
        }

        public void SwitchToState(State<M> _state = null)
        {
            // Check if we don't switch to the same state
            if (state == _state)
                return;

            // Finish previous state
            if (state != null)
                state.EndState();

            // Switch state
            state = _state;

            // Start next state
            if (state != null)
                state.StartState();
        }

        #endregion
    }

    public abstract class State<M> : MonoBehaviour where M : StateMachineMonoBehaviour<M>
    {
        #region Properties

        protected M context = null;
        public M Context => context;

        protected bool activate = false;
        public bool Activate => activate;

        #endregion

        #region State Methods

#if ODIN_INSPECTOR
        [Button, ButtonGroup("DebugButton")]
#endif
        public void EnableState()
        {
            context.SwitchToState(this);
        }

#if ODIN_INSPECTOR
        [Button, ButtonGroup("DebugButton")]
#endif
        public void DisableState()
        {
            context.SwitchToState();
        }

        #endregion

        #region Loop State Methods

        public virtual void InitializeState(M _stateMachine)
        {
            context = _stateMachine;
        }

        public virtual void StartState()
        {
            activate = true;
        }

        public virtual void UpdateState() { }

        public virtual void FixedUpdateState() { }

        public virtual void LateUpdateState() { }

        public virtual void EndState()
        {
            activate = false;
        }

        #endregion
    }
}