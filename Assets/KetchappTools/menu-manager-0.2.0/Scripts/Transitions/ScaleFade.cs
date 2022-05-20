using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Menus.Transitions
{
    [CreateAssetMenu(fileName = "ScaleFade", menuName = "KetchappTools/Menus/Transitions/ScaleFade")]
    public class ScaleFade : Transition
    {
        #region Properties

        [SerializeField]
        private float duration = 0.25f;

        [SerializeField]
        private float scaleFrom = 0f;

        [SerializeField]
        private float scaleTo = 1f;

        [SerializeField]
        private Ease scaleEase = Ease.OutBack;

        [SerializeField]
        private float fadeFrom = 0f;

        [SerializeField]
        private float fadeTo = 1f;

        [SerializeField]
        private Ease fadeEase = Ease.Linear;

        [SerializeField, Range(0f, 1f)]
        private float fadeDuration = 0.5f;

        [SerializeField, Range(0f, 1f)]
        private float fadeDelay = 0f;

        private Sequence playSequence = null;

        #endregion

        #region Methods

        public override void Play(Menu menu, Action callback = null)
        {
            playSequence = DOTween.Sequence()
                .Append(menu.Body.RectTransform.DOScale(scaleTo, duration).From(scaleFrom).SetEase(scaleEase))
                .Insert(duration * fadeDelay, menu.Body.CanvasGroup.DOFade(fadeTo, duration * fadeDuration).From(fadeFrom).SetEase(fadeEase))
                .OnComplete(() =>
                {
                    callback?.Invoke();
                });
        }

        public override void SetToFrom(Menu menu)
        {
            menu.Body.RectTransform.localScale = Vector3.one * scaleFrom;
            menu.Body.CanvasGroup.alpha = fadeFrom;
        }

        public override void SetToTo(Menu menu)
        {
            menu.Body.RectTransform.localScale = Vector3.one * scaleTo;
            menu.Body.CanvasGroup.alpha = fadeTo;
        }

        #endregion
    }
}