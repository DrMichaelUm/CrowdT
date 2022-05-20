using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace KetchappTools.Menus
{
    [InitializeOnLoad]
    public class MenuEditor
    {
        #region Properties

        #endregion

        #region Constructor

        static MenuEditor()
        {
            if(!Application.isPlaying)
                Selection.selectionChanged += SelectionChanged;
        }

        private static void SelectionChanged()
        {
            // Return if not an object in the scene or in play mode
            if (!Selection.activeTransform || Application.isPlaying)
                return;

            // Return if not a menu
            Menu bMenu = Selection.activeTransform.GetComponent<Menu>();
            if (!bMenu)
                return;

            // Get Menu Manager
            StageHandle stageHandle = StageUtility.GetStageHandle(bMenu.gameObject);
            MenuManager menuManager = stageHandle.FindComponentOfType<MenuManager>();
            if (!menuManager)// || menuManager.DisplayType != MenuManager.DisplayTypes.Single)
                return;

            // Search all the menu in the scene and disable them
            Menu[] menus = menuManager.GetComponentsInChildren<Menu>(true);

            foreach (Menu iMenu in menus)
                iMenu.gameObject.SetActive(false);

            bMenu.gameObject.SetActive(true);
        }

        #endregion
    }
}