using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KetchappTools.TouchInput
{
    public class FingerTouch
    {
        public int id;
        public Vector2 current;
        public Vector2 previous;
        public Vector2 delta { get { return current - previous; } }
        public Vector2 start;
        public Vector2 diff { get { return current - start; } }

        /// <summary>
        /// The duration on screen
        /// </summary>
        public float holdDuration;

        /// <summary>
        /// Is the finger over an UI object
        /// </summary>
        public bool isOverUI;
    }

    public class TouchManager
    {
        #region Properties

        // SETTINGS
        public TouchManagerSettings settings;

        // TOUCH RECT
        public Rect InputRect { get; private set; }
        public bool IsTouchInRectZone(Vector2 pos)
        {
            if (!settings.useRectZone)
                return true;
            else if (settings.touchZoneInverse)
                return !InputRect.Contains(pos);
            else return InputRect.Contains(pos);
        }

        // TOUCH COUNT STATE
        public enum TouchState { NONE, SINGLE, DOUBLE }
        public TouchState CurrentTouchState { get; private set; }

        // SWIPE
        private bool oneMoveTypePerTouch;
        public enum TouchMoveType { None, Up, Down, Right, Left }
        public TouchMoveType LastMoveType { get; private set; }
        public delegate void TouchMoveEvent(TouchMoveType moveType);
        public TouchMoveEvent OnMove;
        public Vector2 MoveTypeTresholds { get; set; }

        // SIMPLE TOUCH EVENTS
        public delegate void TouchMoveAction(FingerTouch finger);
        public TouchMoveAction OnTouchDown;
        public TouchMoveAction OnTouchHeld;
        public TouchMoveAction OnTouchUp;
        public TouchMoveAction OnTouchUpNoDrag;
        public TouchMoveAction OnTouchUpAfterDrag;
        public TouchMoveAction OnTap;

        // TWO FINGERS EVENTS
        public delegate void TwoFingerAction(float delta);
        public TwoFingerAction OnPinchHeld;
        public TwoFingerAction OnTurnHeld;

        // Private var
        private Vector2[] previousPos;
        private Vector2[] deltaPos;
        private Vector2[] startPos;

        private FingerTouch[] currentFinger;

        #endregion

        #region Methods

        /// <summary>
        /// Create a touchcontroller able to detect all single touch interaction
        /// </summary>
        public TouchManager(TouchManagerSettings settings)
        {
            this.settings = settings;
            SetRectZone(settings.touchRectArea);

            previousPos = new Vector2[settings.MaxSimultaneousTouch];
            deltaPos = new Vector2[settings.MaxSimultaneousTouch];
            startPos = new Vector2[settings.MaxSimultaneousTouch];

            currentFinger = new FingerTouch[settings.MaxSimultaneousTouch];

        }
        

        /// <summary>
        /// Activate the move direction (swipe) type event
        /// </summary>
        /// <param name="movetypeTresh"> the tresholds to trigger direction, in screen percentage</param>
        public void ActivateMoveTypes()
        {
            settings.useMoveType = true;
            SetSensitivity(settings.moveTypeSensitivity);
        }

        /// <summary>
        /// Deactivate swipe events
        /// </summary>
        public void DeactivateMoveTypes()
        {
            settings.useMoveType = false;
        }

        /// <summary>
        /// Set the swipe sensitivity
        /// </summary>
        /// <param name="s">1 = high, 0 = low sensitivity</param>
        public void SetSensitivity(float s)
        {
            s = Mathf.Clamp01(s);
            MoveTypeTresholds = new Vector2(Screen.width * (1 - s), Screen.height * (1 - s));
        }

        /// <summary>
        /// Define the touch zone where touches are possible
        /// </summary>
        /// <param name="rectZone"></param>
        public void SetRectZone(Rect rectZone)
        {
            InputRect = new Rect(rectZone.x * Screen.width, rectZone.y * Screen.height, rectZone.width * Screen.width, rectZone.height * Screen.height);
        }

        /// <summary>
        /// Detect swipe
        /// </summary>
        /// <param name="delta"></param>
        /// <param name="moveType"></param>
        /// <returns></returns>
        private bool DetectMoveTypes(Vector2 delta, out TouchMoveType moveType)
        {
            delta /= Time.deltaTime;// to get the delta swipe in 1 second

            if (Mathf.Abs(delta.x) > MoveTypeTresholds.x)
            {
                if (delta.x < 0)
                    moveType = TouchMoveType.Left;
                else
                    moveType = TouchMoveType.Right;

                return true;
            }
            if (Mathf.Abs(delta.y) > MoveTypeTresholds.y)
            {
                if (delta.y < 0)
                    moveType = TouchMoveType.Down;
                else
                    moveType = TouchMoveType.Up;

                return true;
            }

            moveType = TouchMoveType.Right;
            return false;
        }

        private bool IsDragRestrictionApply(Vector2 delta)
        {
            if (Mathf.Abs(delta.x) > settings.dragTreshold * Screen.width)
                return true;
            if (Mathf.Abs(delta.y) > settings.dragTreshold * Screen.height)
                return true;
            return false;
        }

        #region ON INPUT

        private void OnDown(FingerTouch finger)
        {
            //Debug.Log("down");
            if (OnTouchDown != null)
                OnTouchDown(finger);

            oneMoveTypePerTouch = true;

        }
        private void OnHeld(FingerTouch finger)
        {
            //Debug.Log("held");
            if (OnTouchHeld != null)
                OnTouchHeld(finger);

            if (settings.useMoveType && oneMoveTypePerTouch)
            {
                TouchMoveType mtype = TouchMoveType.None;
                if (DetectMoveTypes((Vector2)finger.current - finger.previous, out mtype))
                {
                    if (OnMove != null)
                        OnMove(mtype);

                    LastMoveType = mtype;

                    oneMoveTypePerTouch = false;
                }
            }
        }
        private void OnUp(FingerTouch finger)
        {

            if (settings.useDragRestriction && IsDragRestrictionApply(finger.diff)) // check is restriction on up call is raised
            {
                //Debug.Log("apply drag restriction");
                if (OnTouchUpAfterDrag != null)
                    OnTouchUpAfterDrag(finger);
            }
            else
            {
                if (OnTouchUpNoDrag != null)
                    OnTouchUpNoDrag(finger);
            }

            if (currentFinger[finger.id].holdDuration <= settings.TapTreshold)
                if (OnTap != null)
                    OnTap(finger);


                //Debug.Log("up");
                if (OnTouchUp != null)
                OnTouchUp(finger);
        }


        private void OnPinch(float distance)
        {
            if (OnPinchHeld != null)
                OnPinchHeld(distance);
        }

        private void OnTurn(float angle)
        {
            if (OnTurnHeld != null)
                OnTurnHeld(angle);
        }

        #endregion

        /// <summary>
        /// Detect if its a single or two fingers interaction
        /// </summary>
        private void DetectTouchState()
        {
#if UNITY_EDITOR || UNITY_STANDALONE

            bool twoFingers = (UnityEngine.Input.GetMouseButton(0) && UnityEngine.Input.GetMouseButtonDown(1))
                || (UnityEngine.Input.GetMouseButton(1) && UnityEngine.Input.GetMouseButtonDown(0))
                || (UnityEngine.Input.GetMouseButtonDown(1) && UnityEngine.Input.GetMouseButtonDown(0))
                || (UnityEngine.Input.GetMouseButton(1) && UnityEngine.Input.GetMouseButton(0));

            bool oneFinger = UnityEngine.Input.GetMouseButton(0) || UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetMouseButtonUp(0);

            if (twoFingers)
            {
                if(settings.useTwoFingerTouch)
                    CurrentTouchState = TouchState.DOUBLE;
                else CurrentTouchState = TouchState.SINGLE;
            }
            else if (oneFinger) CurrentTouchState = TouchState.SINGLE;
            else CurrentTouchState = TouchState.NONE;
#else
            if(UnityEngine.Input.touchCount >= 2)
                CurrentTouchState = TouchState.DOUBLE;
            else if(UnityEngine.Input.touchCount == 1)
                CurrentTouchState = TouchState.SINGLE;
            else CurrentTouchState = TouchState.NONE;

#endif
        }

        /// <summary>
        /// Update the touch controller, use it in a monobehaviour update method
        /// </summary>
        public void UpdateTouch()
        {
            if (CurrentTouchState != TouchState.DOUBLE)
                DetectTouchState(); // detect a new touch state

#if UNITY_EDITOR || UNITY_STANDALONE
            

            if (CurrentTouchState == TouchState.SINGLE || settings.overrideDoubleTouchMode)
            {
                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    if (!IsTouchInRectZone(UnityEngine.Input.mousePosition)) return;

                    currentFinger[0] = new FingerTouch();
                    currentFinger[0].id = 0;
                    currentFinger[0].current = UnityEngine.Input.mousePosition;
                    currentFinger[0].start = currentFinger[0].current;
                    currentFinger[0].previous = previousPos[0];
                    currentFinger[0].isOverUI = EventSystem.current.IsPointerOverGameObject();
                    currentFinger[0].previous = currentFinger[0].current;
                    currentFinger[0].holdDuration = 0;
                    OnDown(currentFinger[0]);
                }
                else if (UnityEngine.Input.GetMouseButton(0))
                {
                    if (!IsTouchInRectZone(UnityEngine.Input.mousePosition)) return;

                    currentFinger[0].current = UnityEngine.Input.mousePosition;
                    currentFinger[0].previous = previousPos[0];
                    currentFinger[0].isOverUI = EventSystem.current.IsPointerOverGameObject();
                    currentFinger[0].holdDuration += Time.deltaTime;
                    OnHeld(currentFinger[0]);
                }
                else if (UnityEngine.Input.GetMouseButtonUp(0))
                {
                    if(currentFinger[0] != null)
                        OnUp(currentFinger[0]);

                    CurrentTouchState = TouchState.NONE;

                    currentFinger[0] = null;
                }
            }
            else if (CurrentTouchState == TouchState.DOUBLE)
            {
                if (UnityEngine.Input.GetMouseButton(0) && UnityEngine.Input.GetMouseButton(1))
                {
                    Vector2 delta = (Vector2)UnityEngine.Input.mousePosition - previousPos[0];

                    OnPinch(delta.y);
                    OnTurn(delta.x);
                }
                else if (UnityEngine.Input.GetMouseButtonUp(0) || UnityEngine.Input.GetMouseButtonUp(1))
                {
                    CurrentTouchState = TouchState.NONE;
                }
            }

            previousPos[0] = UnityEngine.Input.mousePosition;

#else
            // touch controls


            if (CurrentTouchState == TouchState.SINGLE || settings.overrideDoubleTouchMode)
            {
                Touch touch;

                if (UnityEngine.Input.touchCount > 0)
                {
                    int touchCount = Mathf.Min(settings.MaxSimultaneousTouch, UnityEngine.Input.touchCount);
                    // loop through touch
                    for (int i = 0; i < touchCount; i++)
                    {
                        touch = UnityEngine.Input.GetTouch(i);
                        

                        if (touch.phase == TouchPhase.Began)
                        {
                            if (!IsTouchInRectZone(touch.position)) return;

                            currentFinger[touch.fingerId] = new FingerTouch();
                            currentFinger[touch.fingerId].id = touch.fingerId;
                            currentFinger[touch.fingerId].current = touch.position;
                            currentFinger[touch.fingerId].start = touch.position;
                            currentFinger[touch.fingerId].previous = previousPos[touch.fingerId];
                            currentFinger[touch.fingerId].isOverUI = EventSystem.current.IsPointerOverGameObject(touch.fingerId);
                            currentFinger[touch.fingerId].previous = currentFinger[touch.fingerId].current;
                            currentFinger[touch.fingerId].holdDuration = 0;

                            OnDown(currentFinger[touch.fingerId]);
                        }
                        else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                        {
                            if (!IsTouchInRectZone(touch.position)) return;

                            currentFinger[touch.fingerId].current = touch.position;
                            currentFinger[touch.fingerId].previous = previousPos[touch.fingerId];
                            currentFinger[touch.fingerId].isOverUI = EventSystem.current.IsPointerOverGameObject(touch.fingerId);
                            currentFinger[touch.fingerId].holdDuration += Time.deltaTime;
                            OnHeld(currentFinger[touch.fingerId]);
                        }
                        else if (touch.phase == TouchPhase.Ended)
                        {
                            if(currentFinger[touch.fingerId] != null)
                                OnUp(currentFinger[touch.fingerId]);

                            CurrentTouchState = TouchState.NONE;
                            currentFinger[touch.fingerId] = null;
                        }

                        previousPos[touch.fingerId] = touch.position;
                    }

                }
            }
            else if (CurrentTouchState == TouchState.DOUBLE)
            {
                if (settings.useTwoFingerTouch)
                {
                    PinchDetector.CalculatePinch();

                    if (PinchDetector.pinchDistanceDelta != 0)
                        OnPinch(PinchDetector.pinchDistanceDelta);
                    if (PinchDetector.turnAngleDelta != 0)
                        OnTurn(PinchDetector.turnAngleDelta);
                }

                if(UnityEngine.Input.touchCount <= 1)// reset the DOUBLE state only if both fingers are up
                {
                    CurrentTouchState = TouchState.NONE;
                }
            }

            
#endif
        }


        #endregion

        public static bool IsPointerOverGameObject()
        {
            return EventSystem.current.IsPointerOverGameObject() || EventSystem.current.IsPointerOverGameObject(0);
        }
    }

    /// <summary>
    /// The class which detects two fingers interaction
    /// </summary>
    public class PinchDetector
    {
        const float pinchTurnRatio = Mathf.PI / 2;
        const float minTurnAngle = 0.1f;

        const float pinchRatio = 1;
        const float minPinchDistance = 0.1f;

        const float panRatio = 1;
        const float minPanDistance = 0.1f;

        const float minSwipeDistance = 0.1f;

        /// <summary>
        ///   The delta of the angle between two touch points
        /// </summary>
        static public float turnAngleDelta;
        /// <summary>
        ///   The angle between two touch points
        /// </summary>
        static public float turnAngle;

        /// <summary>
        ///   The delta of the distance between two touch points that were distancing from each other
        /// </summary>
        static public float pinchDistanceDelta;
        /// <summary>
        ///   The distance between two touch points that were distancing from each other
        /// </summary>
        static public float pinchDistance;

        /// <summary>
        ///   Calculates Pinch and Turn - This should be used inside LateUpdate
        /// </summary>
        static public void CalculatePinch()
        {
            pinchDistance = pinchDistanceDelta = 0;
            turnAngle = turnAngleDelta = 0;

            // if two fingers are touching the screen at the same time ...
            if (UnityEngine.Input.touchCount == 2)
            {
                Touch touch1 = UnityEngine.Input.touches[0];
                Touch touch2 = UnityEngine.Input.touches[1];

                // ... if at least one of them moved ...
                if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
                {
                    // ... check the delta distance between them ...
                    pinchDistance = Vector2.Distance(touch1.position, touch2.position);
                    float prevDistance = Vector2.Distance(touch1.position - touch1.deltaPosition,
                                                          touch2.position - touch2.deltaPosition);
                    pinchDistanceDelta = pinchDistance - prevDistance;

                    // ... if it's greater than a minimum threshold, it's a pinch!
                    if (Mathf.Abs(pinchDistanceDelta) > minPinchDistance)
                    {
                        pinchDistanceDelta *= pinchRatio;
                    }
                    else
                    {
                        pinchDistance = pinchDistanceDelta = 0;
                    }

                    // ... or check the delta angle between them ...
                    turnAngle = Angle(touch1.position, touch2.position);
                    float prevTurn = Angle(touch1.position - touch1.deltaPosition,
                                           touch2.position - touch2.deltaPosition);
                    turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);

                    // ... if it's greater than a minimum threshold, it's a turn!
                    if (Mathf.Abs(turnAngleDelta) > minTurnAngle)
                    {
                        turnAngleDelta *= pinchTurnRatio;
                    }
                    else
                    {
                        turnAngle = turnAngleDelta = 0;
                    }
                }
            }
        }

        static private float Angle(Vector2 pos1, Vector2 pos2)
        {
            Vector2 from = pos2 - pos1;
            Vector2 to = new Vector2(1, 0);

            float result = Vector2.Angle(from, to);
            Vector3 cross = Vector3.Cross(from, to);

            if (cross.z > 0)
            {
                result = 360f - result;
            }

            return result;
        }
    }

}



