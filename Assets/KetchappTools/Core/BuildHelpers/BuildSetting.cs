using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace KetchappTools.Core.BuildHelpers
{
    [DesignPatterns.SingletonScriptableObject("BuildHelpersSetting")]
    public class BuildSetting : DesignPatterns.SingletonScriptableObject<BuildSetting>
    {
        #region Properties

        protected const string SymbolPrefix = "KETCHAPPBUILD_"; 

        [SerializeField]
#if UNITY_EDITOR && ODIN_INSPECTOR
        [Sirenix.OdinInspector.OnValueChanged(nameof(OnValueChangedBuildType))]
#endif
        protected BuildTypes buildType = BuildTypes.Development;
        public BuildTypes BuildType => buildType;

        #endregion

        #region Editor Methods

#if UNITY_EDITOR
        protected void OnValueChangedBuildType()
        {
            SetupBuildSetting();
        }

        [UnityEditor.MenuItem("Ketchapp Tools/Core/Build Helper/Setup Build Setting")]
        public static void SetupBuildSetting()
        {
            // Setup manual symbol
            foreach (BuildTypes buildType in (BuildTypes[])System.Enum.GetValues(typeof(BuildTypes)))
                ScriptingSymbolManaged.Instance.RemoveManualSymbol(SymbolPrefix + buildType.ToString().ToUpper());
            ScriptingSymbolManaged.Instance.AddManualSymbol(SymbolPrefix + Instance.buildType.ToString().ToUpper());

            // Define scripting symbol
            ScriptingSymbolHelper.DefineScriptingSymbol();
        }

        [UnityEditor.MenuItem("Ketchapp Tools/Core/Build Helper/Build Settings")]
        private static void SelectBuildSetting()
        {
            UnityEditor.Selection.activeObject = Instance;
        }

        public void SetBuildSettingFromJSON()
        {
            string ketchappBuildSettingsPath = Application.dataPath + "/../ketchappbuildsettings.json";
            if (File.Exists(ketchappBuildSettingsPath))
            {
                BuildSettingData ketchappBuildSettings = JsonUtility.FromJson<BuildSettingData>(File.ReadAllText(ketchappBuildSettingsPath));
                switch (ketchappBuildSettings.BuildType)
                {
                    case "Development":
                        buildType = BuildTypes.Development;
                        break;
                    case "UA":
                        buildType = BuildTypes.UA;
                        break;
                    case "SoftLaunch":
                        buildType = BuildTypes.SoftLaunch;
                        break;
                    case "Launch":
                        buildType = BuildTypes.Launch;
                        break;
                }
            }

            // Setup the build setting
            SetupBuildSetting();
        }

        [System.Serializable]
        protected class BuildSettingData
        {
            public string BuildType = "Development";
        }
#endif

        #endregion
    }
}