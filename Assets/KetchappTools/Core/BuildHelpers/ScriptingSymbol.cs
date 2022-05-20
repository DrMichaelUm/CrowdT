using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace KetchappTools.Core.BuildHelpers
{
    [ScriptingSymbol("KETCHAPPCORE")]
    [AttributeUsage(AttributeTargets.All)]
    public class ScriptingSymbolAttribute : Attribute
    {
        private string value;
        public string Value { get { return value; } }

        public ScriptingSymbolAttribute(string _value) { value = _value; }
    }

#if UNITY_EDITOR
    public class ScriptingSymbolHelper
    {
        //[UnityEditor.Callbacks.DidReloadScripts(10)]
        //private static void DidReloadScripts()
        [UnityEditor.MenuItem("Ketchapp Tools/Core/Build Helper/Define Scripting Symbol")]
        public static void DefineScriptingSymbol()
        {
            List<string> symbols = new List<string>();
            foreach (Assembly iAssembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // HACK: IL2CPP crashes if you attempt to get the methods of some classes in these assemblies.
                if (iAssembly.FullName.StartsWith("System") || iAssembly.FullName.StartsWith("mscorlib"))
                    continue;
                foreach (Type iType in iAssembly.GetTypes())
                {
                    ScriptingSymbolAttribute scriptingSymbolAttribute = iType.GetCustomAttribute(typeof(ScriptingSymbolAttribute), true) as ScriptingSymbolAttribute;
                    if (scriptingSymbolAttribute == null)
                        continue;
                    symbols.Add(scriptingSymbolAttribute.Value);
                }
            }

            // Adding the manual symbols
            symbols.AddRange(ScriptingSymbolManaged.Instance.ManualSymbols);

            // TODO Sanitaze symbols ?

            // Get targets
            BuildTargetGroup[] targets = new BuildTargetGroup[] { BuildTargetGroup.iOS, BuildTargetGroup.Android };
            foreach (BuildTargetGroup target in targets)
            {
                // Get current defined symbols
                string targetSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);
                bool targetSymbolsEdited = false;

                // Add symbols
                foreach(string symbol in symbols)
                {
                    if (!targetSymbols.Contains(symbol))
                    {
                        targetSymbols += $";{symbol}";
                        targetSymbolsEdited = true;
                    }
                }

                // Remove symbols
                List<string> symbolToRemoves = ScriptingSymbolManaged.Instance.List.Where(symbolSaved => !symbols.Any(symbolNew => symbolNew == symbolSaved)).ToList();
                foreach (string symbol in symbolToRemoves)
                {
                    targetSymbols = targetSymbols.Replace($";{symbol}", string.Empty);
                    targetSymbols = targetSymbols.Replace($"{symbol}", string.Empty); // TODO Find cleaner way to do that
                    targetSymbolsEdited = true;
                }

                // Save defined symbols
                if (targetSymbolsEdited)
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(target, targetSymbols);
            }

            // Save managed symbols
            ScriptingSymbolManaged.Instance.UpdateList(symbols);
        }
    }
#endif
}