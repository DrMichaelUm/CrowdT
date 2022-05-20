using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.TouchInput
{
#if UNITY_EDITOR
    [ScriptOrder(-10)]
#endif
    
    public class TouchInputHandler : MonoBehaviour
    {
        public static TouchInputHandler INSTANCE;

        public bool IsInputActive { get; private set; }
        public bool PlayerControllerReady { get { return true; } }

        private static TouchManager touchManager;
        public static TouchManager TOUCH_MANAGER
        {
            get
            {
                if (touchManager == null)
                {
                    touchManager = new TouchManager(INSTANCE.Settings);
                }
                    
                return touchManager;
            }
        }

        public TouchManagerSettings Settings;
        public bool autoEnabledOnStart;

        private void Awake()
        {
            if (INSTANCE != null)
                Destroy(INSTANCE.gameObject);
            INSTANCE = this;

            if (autoEnabledOnStart)
                SetActive(true);

        }

        public virtual void Initialise()
        {
            SetActive(true);
        }

        public virtual void ResetControls()
        {

        }

        protected virtual void Update()
        {
            if (!IsInputActive)
            {
                //Debug.Log("INPUT Not Active");
                return;
            }

            if (TOUCH_MANAGER != null)
                TOUCH_MANAGER.UpdateTouch();

        }
        

        #region CURSOR

        public static bool USE_CURSOR_MODE = false;
        public enum CursorMode { NAV3D, MENU }

        public static void SetCursorMode(CursorMode mode)
        {
            if (!USE_CURSOR_MODE) return;

            CursorLockMode m = CursorLockMode.None;

            switch (mode)
            {
                case CursorMode.NAV3D:
                    m = CursorLockMode.Locked;
                    break;
                case CursorMode.MENU:
                    m = CursorLockMode.None;
                    break;
            }

            // cursor mode = confined
            Cursor.lockState = m;
        }

        #endregion

        #region UTILITY
        private Coroutine delayActivationCoroutine;

        public void SetActive(bool value)
        {
            if (delayActivationCoroutine != null)
                StopCoroutine(delayActivationCoroutine);
            //Debug.Log("active input " + value);
            IsInputActive = value;

        }
        public void SetActiveWithDelay(bool value, float delay)
        {
            if (delayActivationCoroutine != null)
                StopCoroutine(delayActivationCoroutine);
            delayActivationCoroutine = StartCoroutine(DelayInputActivation(value, delay));
        }

        private IEnumerator DelayInputActivation(bool value, float delay)
        {
            yield return new WaitForSeconds(delay);
            SetActive(value);
        }

        #endregion
    }
}



