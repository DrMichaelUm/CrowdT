using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

namespace KetchappTools.Menus.Utils
{
    public class SafeArea : UIBehaviour
    {
        #region Variables

        private static List<SafeArea> safeAreas = new List<SafeArea>();
        private static float adsHeight = 0f;

        private bool destroyed = false;

        private bool updateRectThisFrame = false;

        private RectTransform rectTransform;
        private RectTransform rectTransformParent;

        [SerializeField]
        private bool usingAdsHeight = true;

        public static event Action OnSafeAreaChanged;

        [SerializeField, OnValueChanged("UpdateSafeArea")]
        private bool debugSafeArea;

        [SerializeField, ShowIf("debugSafeArea"), OnValueChanged("UpdateSafeArea")]
        private Rect debugPixelSafeArea = new Rect(0, 102, 1125, 2202);

#if UNITY_EDITOR
        private void UpdateSafeArea()
        {
            if(UnityEditor.EditorApplication.isPlaying)
                updateRectThisFrame = true;
        }
#endif

        #endregion

        #region Unity Methods

        protected override void OnEnable()
        {
            safeAreas.Add(this);
        }

        protected override void OnDisable()
        {
            safeAreas.Remove(this);
        }

        public static void AdsHeightChanged(float _adsHeight)
        {
            adsHeight = _adsHeight;
            foreach (SafeArea iSafeArea in safeAreas)
            {
                // Force update next frame
                iSafeArea.updateRectThisFrame = true;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            updateRectThisFrame = true; // Not sure it's needed
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            destroyed = true;
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            updateRectThisFrame = true;
        }

        private void LateUpdate()
        {
            if (updateRectThisFrame)
            {
                updateRectThisFrame = false;
                UpdateRect();
                OnSafeAreaChanged?.Invoke();
            }
        }

        #endregion

        #region Methods

        private Rect GetScreenSafeAreaRelative(Vector2 _size)
        {
            Rect pixelSafeArea = Screen.safeArea;

            // Remove ads size if activated
            if (usingAdsHeight)
            {
                pixelSafeArea.height -= adsHeight;
                pixelSafeArea.y -= (adsHeight / 2f);
            }

            return new Rect(
                pixelSafeArea.x * _size.x / Screen.width,
                pixelSafeArea.y * _size.y / Screen.height,
                pixelSafeArea.width * _size.x / Screen.width,
                pixelSafeArea.height * _size.y / Screen.height
            );
        }

        private void UpdateRect()
        {
            if (destroyed)
                return;

            if (!rectTransform || !rectTransformParent)
            {
                rectTransform = (RectTransform)transform;
                rectTransformParent = (RectTransform)transform.parent;
            }

            Rect rect = GetScreenSafeAreaRelative(rectTransformParent.rect.size);
            rectTransform.offsetMin = new Vector2(rect.x, rect.y);
            rectTransform.sizeDelta = rect.size - rectTransformParent.rect.size;
        }

        #endregion
    }
}