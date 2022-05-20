using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Core.DesignPatterns
{
    public class StateMachineLite<TEnum> where TEnum : struct, IConvertible, IComparable, IFormattable
    {
        #region Properties

        private System.Object objectTarget = null;

        private class StateMethodCache
        {
            public Action startState;
            public Action updateState;
            public Action fixedUpdateState;
            public Action lateUpdateState;
            public Action endState;
        }

        StateMethodCache _stateMethods;
        protected float elapsedTimeInState = 0f;
        public float ElapsedTimeInState => elapsedTimeInState;
        protected TEnum previousState;
        Dictionary<TEnum, StateMethodCache> _stateCache = new Dictionary<TEnum, StateMethodCache>();

        TEnum _state;
        public TEnum State { get { return state; } set { state = value; } }
        protected TEnum state
        {
            get
            {
                return _state;
            }
            set
            {
                if (_state.Equals(value))
                    return;

                // swap previous/current
                previousState = _state;
                _state = value;

                // exit the state, fetch the next cached state methods then enter that state
                if (_stateMethods.endState != null)
                    _stateMethods.endState();

                elapsedTimeInState = 0f;
                _stateMethods = _stateCache[_state];

                if (_stateMethods.startState != null)
                    _stateMethods.startState();
            }
        }

        #endregion

        #region Constructor

        public StateMachineLite(System.Object _objectTarget, TEnum _initalState)
        {
            // Set object target
            objectTarget = _objectTarget;

            // cache all of our state methods
            var enumValues = (TEnum[])Enum.GetValues(typeof(TEnum));
            foreach (var e in enumValues)
                ConfigureAndCacheState(e);

            // Initial state
            _state = _initalState;
            _stateMethods = _stateCache[_state];

            if (_stateMethods.startState != null)
                _stateMethods.startState();

            // Subscribe to updates
            Helpers.StateMachineUpdater.Instance.OnUpdate += Update;
            Helpers.StateMachineUpdater.Instance.OnFixedUpdate += FixedUpdate;
            Helpers.StateMachineUpdater.Instance.OnLateUpdate += LateUpdate;
        }

        #endregion

        #region Methods

        public void Destroy()
        {
            // Unubscribe from updates
            Helpers.StateMachineUpdater.Instance.OnUpdate -= Update;
            Helpers.StateMachineUpdater.Instance.OnFixedUpdate -= FixedUpdate;
            Helpers.StateMachineUpdater.Instance.OnLateUpdate -= LateUpdate;
        }

        protected void Update()
        {
            elapsedTimeInState += Time.deltaTime;

            if (_stateMethods.updateState != null)
                _stateMethods.updateState();
        }

        protected void FixedUpdate()
        {
            if (_stateMethods.fixedUpdateState != null)
                _stateMethods.fixedUpdateState();
        }

        protected void LateUpdate()
        {
            if (_stateMethods.lateUpdateState != null)
                _stateMethods.lateUpdateState();
        }

        private void ConfigureAndCacheState(TEnum stateEnum)
        {
            var stateName = stateEnum.ToString();

            var state = new StateMethodCache();
            state.startState = GetDelegateForMethod(stateName + "_Start");
            state.updateState = GetDelegateForMethod(stateName + "_Update");
            state.fixedUpdateState = GetDelegateForMethod(stateName + "_FixedUpdate");
            state.lateUpdateState = GetDelegateForMethod(stateName + "_LateUpdate");
            state.endState = GetDelegateForMethod(stateName + "_End");

            _stateCache[stateEnum] = state;
        }


        private Action GetDelegateForMethod(string methodName)
        {
            var methodInfo = objectTarget.GetType().GetMethod(methodName,
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

            if (methodInfo != null)
                return Delegate.CreateDelegate(typeof(Action), objectTarget, methodInfo) as Action;

            return null;
        }

        #endregion
    }
}