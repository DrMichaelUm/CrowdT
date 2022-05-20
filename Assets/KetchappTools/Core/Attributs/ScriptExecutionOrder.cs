using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif
using UnityEngine;

namespace KetchappTools.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class ScriptExecutionOrderAttribute : Attribute
    {

        public readonly int ExecutionOrder = 0;

        public ScriptExecutionOrderAttribute(int _executionOrder)
        {
            ExecutionOrder = _executionOrder;
        }

#if UNITY_EDITOR
        private const string PB_TITLE = "Updating Execution Order";
        private const string PB_MESSAGE = "Hold on to your butt, Cap'n!";
        private const string ERR_MESSAGE = "Unable to locate and set execution order for {0}";
        private const string DUP_MESSAGE = "Unable to locate and set execution order for {0}, the script name is already present";

        [InitializeOnLoadMethod]
        private static void Execute()
        {
            var type = typeof(ScriptExecutionOrderAttribute);
            var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            var scripts = new Dictionary<MonoScript, ScriptExecutionOrderAttribute>();

            var listTypes = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.StartsWith("Mono.Cecil")
                    || assembly.FullName.StartsWith("UnityScript")
                    || assembly.FullName.StartsWith("Boo.Lan")
                    || assembly.FullName.StartsWith("System")
                    || assembly.FullName.StartsWith("I18N")
                    || assembly.FullName.StartsWith("UnityEngine")
                    || assembly.FullName.StartsWith("UnityEditor")
                    || assembly.FullName.StartsWith("mscorlib")
                    )
                    continue;

                foreach (Type iType in assembly.GetTypes())
                {
                    if (!iType.IsClass
                        || iType.IsAbstract)
                        continue;

                    listTypes.Add(iType);
                }
            }
            var types = listTypes.ToArray();

            var progress = 0f;
            var step = 1f / types.Length;

            foreach (var item in types)
            {
                var attributes = item.GetCustomAttributes(type, true);
                if (attributes.Length != 1) continue;
                var attribute = attributes[0] as ScriptExecutionOrderAttribute;

                var guids = AssetDatabase.FindAssets(string.Format("{0} t:script", item.Name));
                foreach (var guid in guids)
                {
                    var script = AssetDatabase.LoadAssetAtPath<MonoScript>(AssetDatabase.GUIDToAssetPath(guid));
                    if(script.GetClass() == item)
                    {
                        scripts.Add(script, attribute);
                        break;
                    }
                }
            }

            var changed = false;
            foreach (var item in scripts)
            {
                if (MonoImporter.GetExecutionOrder(item.Key) != item.Value.ExecutionOrder)
                {
                    changed = true;
                    break;
                }
            }

            if (changed)
            {
                foreach (var item in scripts)
                {
                    var cancelled = EditorUtility.DisplayCancelableProgressBar(PB_TITLE, PB_MESSAGE, progress);
                    progress += step;

                    if (MonoImporter.GetExecutionOrder(item.Key) != item.Value.ExecutionOrder)
                    {
                        MonoImporter.SetExecutionOrder(item.Key, item.Value.ExecutionOrder);
                    }

                    if (cancelled) break;
                }
            }

            EditorUtility.ClearProgressBar();
        }
#endif
    }
}