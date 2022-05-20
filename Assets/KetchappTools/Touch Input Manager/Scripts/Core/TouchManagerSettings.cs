using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.TouchInput
{

    [CreateAssetMenu(fileName = "touchManagerSettings", menuName = "InputManager/TouchManagerSettings")]
    public class TouchManagerSettings : ScriptableObject
    {
        [Header("Touch screen area")]
        public bool useRectZone = true;
        [Tooltip("If true the touch in the touch zone are not taken, but the ones outside are")]
        public bool touchZoneInverse;
        [Tooltip("The rect zone where touch are taken,(in screen percentage)")]
        public Rect touchRectArea = new Rect(0, 0, 1, 1);

        [Header("Multiple touch")]
        public int MaxSimultaneousTouch = 1;
        public bool overrideDoubleTouchMode = true;

        [Header("Interaction mode options")]
        public bool useTwoFingerTouch;
        public bool useMoveType;
        [Range(0, 1)]
        public float moveTypeSensitivity = 0.5f;

        [Tooltip("if true a drag will disable the touch up callback")]
        public bool useDragRestriction;
        [Tooltip("The distance from which a touch move is considered as a drag and not as simple touch,(in screen percentage)")]
        [Range(0, 1)]
        public float dragTreshold = 0.1f;

        public float TapTreshold = 0.1f;
    }
}