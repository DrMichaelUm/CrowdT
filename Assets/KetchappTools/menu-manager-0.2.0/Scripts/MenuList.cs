using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace KetchappTools.Menus
{
    [Serializable]
    public partial class MenuList
    {
#if UNITY_EDITOR
        public void EditorSetMenu(Menu menu, string menuName)
        {
            string menuNamePrivate = char.ToLower(menuName[0]) + menuName.Substring(1);

            // Set the menu by reflexion
            Type menuListType = typeof(MenuList);
            FieldInfo fieldInfo = menuListType.GetField(menuNamePrivate,
                BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(this, menu);
        }
#endif
    }
}