using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Core.BuildHelpers
{
    [DesignPatterns.SingletonScriptableObject("ScriptingSymbolManaged")]
    public class ScriptingSymbolManaged : DesignPatterns.SingletonScriptableObject<ScriptingSymbolManaged>
    {
#if UNITY_EDITOR
        #region Properties

        [SerializeField]
        protected List<string> list = new List<string>();
        public List<string> List => list;

        public void UpdateList(List<string> _list)
        {
            list = _list;
            UnityEditor.EditorUtility.SetDirty(this);
        }

        [SerializeField]
        protected List<string> manualSymbols = new List<string>();
        public List<string> ManualSymbols => manualSymbols;

        public void AddManualSymbol(string _value)
        {
            if (!manualSymbols.Contains(_value))
            {
                manualSymbols.Add(_value);
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }

        public void RemoveManualSymbol(string _value)
        {
            if (manualSymbols.Contains(_value))
            {
                manualSymbols.Remove(_value);
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }

        #endregion
#endif
    }
}