using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Menus.Transitions
{
    [CreateAssetMenu(fileName = "Fade", menuName = "KetchappTools/Menus/Transitions/Fade")]
    public class Fade : Transition
    {
        #region Properties

        [SerializeField]
        private float duration = 0.25f;

        [SerializeField]
        private float fadeFrom = 0f;

        [SerializeField]
        private float fadeTo = 1f;

        [SerializeField]
        private Ease fadeEase = Ease.Linear;

        private Sequence playSequence = null;

        #endregion

        #region Methods

        public override void Play(Menu menu, Action callback = null)
        {
            playSequence = DOTween.Sequence()
                .Append(menu.Body.CanvasGroup.DOFade(fadeTo, duration).From(fadeFrom).SetEase(fadeEase))
                .OnComplete(()=>
                {
                    callback?.Invoke();
                });
        }

        public override void SetToFrom(Menu menu)
        {
            menu.Body.CanvasGroup.alpha = fadeFrom;
        }

        public override void SetToTo(Menu menu)
        {
            menu.Body.CanvasGroup.alpha = fadeTo;
        }

        #endregion
    }
}