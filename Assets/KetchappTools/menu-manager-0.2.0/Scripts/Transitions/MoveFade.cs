using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Menus.Transitions
{
    [CreateAssetMenu(fileName = "MoveFade", menuName = "KetchappTools/Menus/Transitions/MoveFade")]
    public class MoveFade : Transition
    {
        #region Properties

        [SerializeField]
        private float duration = 0.25f;

        [SerializeField]
        private Vector2 moveFrom = Vector2.right;

        [SerializeField]
        private Vector2 moveTo = Vector2.zero;

        [SerializeField]
        private Ease moveEase = Ease.Linear;

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
            Vector2 moveToSized = new Vector2(moveTo.x * menu.Body.RectTransform.rect.width, moveTo.y * menu.Body.RectTransform.rect.height);
            Vector2 moveFromSized = new Vector2(moveFrom.x * menu.Body.RectTransform.rect.width, moveFrom.y * menu.Body.RectTransform.rect.height);
            playSequence = DOTween.Sequence()
                .Append(menu.Body.RectTransform.DOAnchorPos(moveToSized, duration).From(moveFromSized).SetEase(moveEase))
                .Insert(duration * fadeDelay, menu.Body.CanvasGroup.DOFade(fadeTo, duration * fadeDuration).From(fadeFrom).SetEase(fadeEase))
                .OnComplete(() =>
                {
                    callback?.Invoke();
                });
        }

        public override void SetToFrom(Menu menu)
        {
            menu.Body.RectTransform.anchoredPosition = new Vector2(moveFrom.x * menu.Body.RectTransform.rect.width, moveFrom.y * menu.Body.RectTransform.rect.height);
            menu.Body.CanvasGroup.alpha = fadeFrom;
        }

        public override void SetToTo(Menu menu)
        {
            menu.Body.RectTransform.anchoredPosition = new Vector2(moveTo.x * menu.Body.RectTransform.rect.width, moveTo.y * menu.Body.RectTransform.rect.height);
            menu.Body.CanvasGroup.alpha = fadeTo;
        }

        #endregion
    }
}