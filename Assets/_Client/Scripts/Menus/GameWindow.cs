using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using CrowdT;
using Ketchapp.CrowdTerritory;
using TMPro;

namespace Menus
{
    public class GameWindow : KetchappTools.Menus.Menu
    {
        #region Properties

        public TMP_Text levelNumText;

        #endregion

        #region Menu Methods

        // Call at the start of the show animation
        protected override void OnShowStart()
        {
            base.OnShowStart();
        }

        // Call at the end of the show animation
        protected override void OnShowEnd()
        {
            base.OnShowEnd();
        }

        // Call at the start of the hide animation
        protected override void OnHideStart()
        {
            base.OnHideStart();
        }

        // Call at the end of the hide animation
        protected override void OnHideEnd()
        {
            base.OnHideEnd();
        }

        #endregion

        private void OnEnable()
        {
            levelNumText.text = $"Level {Save.data.level + 1}";
            GetComponent<Canvas>().worldCamera = FindObjectOfType<UICamera>().GetComponent<Camera>();
        }
    }
}

namespace KetchappTools.Menus
{
    public partial class MenuList
    {
        [SerializeField] private global::Menus.GameWindow gameWindow;
        public global::Menus.GameWindow GameWindow => gameWindow;
    }
}