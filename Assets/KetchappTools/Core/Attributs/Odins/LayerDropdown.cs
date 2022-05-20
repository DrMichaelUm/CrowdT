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
    public class LayerDropdownAttribute : Attribute
    {
    }

#if UNITY_EDITOR

    public sealed class LayerDropdownAttributeDrawer : OdinAttributeDrawer<LayerDropdownAttribute, int>
    {
        [Obsolete]
        protected override void DrawPropertyLayout(IPropertyValueEntry<int> entry, LayerDropdownAttribute attribute, GUIContent label)
        {
            entry.SmartValue = EditorGUILayout.LayerField((label != null) ? label : new GUIContent(""), entry.SmartValue);
        }
    }

#endif
}
#endif