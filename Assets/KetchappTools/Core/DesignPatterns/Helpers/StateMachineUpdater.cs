using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Core.DesignPatterns.Helpers
{
    public class StateMachineUpdater : SingletonAutoInitialize<StateMachineUpdater>
    {
        #region Properties

        public event Action OnUpdate = null;
        public event Action OnFixedUpdate = null;
        public event Action OnLateUpdate = null;

        #endregion

        #region Methods

        protected void Update()
        {
            OnUpdate?.Invoke();
        }

        protected void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        protected void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }

        #endregion
    }
}