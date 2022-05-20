using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Menus.Transitions
{
	//[CreateAssetMenu(fileName = "Transition", menuName = "KetchappTools/Menus/Transitions/")]
	public abstract class Transition : ScriptableObject
	{
        #region Properties

        #endregion

        #region Methods

        public abstract void Play(Menu menu, Action callback = null);

        public abstract void SetToFrom(Menu menu);

        public abstract void SetToTo(Menu menu);

        #endregion
    }
}