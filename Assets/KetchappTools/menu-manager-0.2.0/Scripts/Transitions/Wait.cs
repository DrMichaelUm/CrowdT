using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace KetchappTools.Menus.Transitions
{
    [CreateAssetMenu(fileName = "Wait", menuName = "KetchappTools/Menus/Transitions/Wait")]
    public class Wait : Transition
    {
        #region Properties

        [SerializeField]
        private float duration = 0.25f;

        private Sequence playSequence = null;

        #endregion

        #region Methods

        public override void Play(Menu menu, Action callback = null)
        {
            playSequence = DOTween.Sequence()
                .AppendInterval(duration)
                .OnComplete(() =>
                {
                    callback?.Invoke();
                });
        }

        public override void SetToFrom(Menu menu)
        {
            //menu.Body.CanvasGroup.alpha = fadeFrom;
        }

        public override void SetToTo(Menu menu)
        {
            //menu.Body.CanvasGroup.alpha = fadeTo;
        }

        #endregion
    }
}