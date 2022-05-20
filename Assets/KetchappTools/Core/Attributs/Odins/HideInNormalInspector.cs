#if ODIN_INSPECTOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector.Editor;
#endif

namespace Sirenix.OdinInspector
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class HideInNormalInspectorAttribute : Attribute
    {
    }

#if UNITY_EDITOR

    public sealed class HideInNormalInspectorAttributeDrawer : OdinAttributeDrawer<HideInNormalInspectorAttribute>
    {
        [Obsolete]
        protected override void DrawPropertyLayout(InspectorProperty property, HideInNormalInspectorAttribute attribute, GUIContent label) { }
    }

#endif
    //#if UNITY_EDITOR

    //    [OdinDrawer]
    //    [DrawerPriority(1000, 0, 0)]
    //    public class HideInNormalInspectorAttributeDrawer : OdinAttributeDrawer<HideInNormalInspectorAttribute>
    //    {
    //        protected override void DrawPropertyLayout(InspectorProperty property, HideInNormalInspectorAttribute attribute, GUIContent label) { }
    //    }

    //#endif
}
#endif