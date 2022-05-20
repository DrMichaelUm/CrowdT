using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Menus
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Body : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        protected RectTransform rectTransform = null;
        public RectTransform RectTransform => rectTransform;

        [SerializeField]
        protected CanvasGroup canvasGroup = null;
        public CanvasGroup CanvasGroup => canvasGroup;

        #endregion

        #region Editor Methods

#if UNITY_EDITOR

        private void Reset()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

#endif

        #endregion
    }
}