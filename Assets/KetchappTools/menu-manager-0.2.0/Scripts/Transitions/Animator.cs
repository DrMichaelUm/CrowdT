using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace KetchappTools.Menus.Transitions
{
    [CreateAssetMenu(fileName = "Animator", menuName = "KetchappTools/Menus/Transitions/Animator")]
    public class Animator : Transition
    {
        #region Enums

        protected enum ParameterTypes
        {
            Float,
            Int,
            Bool,
            Trigger
        }

        #endregion

        #region Properties

        [SerializeField]
        private float duration = 0.25f;

        [SerializeField]
        private ParameterTypes parameterType = ParameterTypes.Trigger;

        [SerializeField, LabelText("Parameter Name")]
        private string parameterName = "";

        [SerializeField, LabelText("Parameter Value"), ShowIf("parameterType", ParameterTypes.Float)]
        private float parameterValueFloat = 0f;

        [SerializeField, LabelText("Parameter Value"), ShowIf("parameterType", ParameterTypes.Int)]
        private int parameterValueInt = 0;

        [SerializeField, LabelText("Parameter Value"), ShowIf("parameterType", ParameterTypes.Bool)]
        private bool parameterValueBool = true;

        private Sequence playSequence = null;

        #endregion

        #region Methods

        public override void Play(Menu menu, Action callback = null)
        {
            UnityEngine.Animator animator = menu.GetComponentInChildren<UnityEngine.Animator>();
            if(animator)
            {
                switch (parameterType)
                {
                    case ParameterTypes.Float:
                        animator.SetFloat(parameterName, parameterValueFloat);
                        break;
                    case ParameterTypes.Int:
                        animator.SetInteger(parameterName, parameterValueInt);
                        break;
                    case ParameterTypes.Bool:
                        animator.SetBool(parameterName, parameterValueBool);
                        break;
                    case ParameterTypes.Trigger:
                        animator.SetTrigger(parameterName);
                        break;
                }
            }

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