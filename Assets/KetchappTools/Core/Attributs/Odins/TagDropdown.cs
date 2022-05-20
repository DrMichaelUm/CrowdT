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
    public class TagDropdownAttribute : Attribute
    {
    }

#if UNITY_EDITOR

    public sealed class TagDropdownAttributeDrawer : OdinAttributeDrawer<TagDropdownAttribute, string>
    {
        [Obsolete]
        protected override void DrawPropertyLayout(IPropertyValueEntry<string> entry, TagDropdownAttribute attribute, GUIContent label)
        {
            entry.SmartValue = EditorGUILayout.TagField((label != null) ? label : new GUIContent(""), entry.SmartValue);
        }
    }

#endif
}
#endif