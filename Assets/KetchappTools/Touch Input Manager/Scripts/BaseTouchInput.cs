using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.TouchInput
{
    /// <summary>
    /// Base abstract class to use touch input, inherit from this if you want a quick start, or create your own class
    /// </summary>
    public abstract class BaseTouchInput : MonoBehaviour
    {

        protected virtual void OnEnable()
        {
            TouchInputHandler.TOUCH_MANAGER.OnTouchDown += OnDown;
            TouchInputHandler.TOUCH_MANAGER.OnTouchUp += OnUp;
            TouchInputHandler.TOUCH_MANAGER.OnTap += OnTap;
            TouchInputHandler.TOUCH_MANAGER.OnTouchHeld += OnHeld;
        }
        protected virtual void OnDisable()
        {
            TouchInputHandler.TOUCH_MANAGER.OnTouchDown -= OnDown;
            TouchInputHandler.TOUCH_MANAGER.OnTouchHeld -= OnHeld;
            TouchInputHandler.TOUCH_MANAGER.OnTap -= OnTap;
            TouchInputHandler.TOUCH_MANAGER.OnTouchUp -= OnUp;
        }

        protected abstract void OnDown(FingerTouch finger);
        protected abstract void OnHeld(FingerTouch finger);
        protected abstract void OnUp(FingerTouch finger);
        protected abstract void OnTap(FingerTouch finger);
    }
}

