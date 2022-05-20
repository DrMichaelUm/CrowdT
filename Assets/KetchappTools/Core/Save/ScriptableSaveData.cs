using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace KetchappTools.Core.Save
{
    [CreateAssetMenu(fileName = "ScriptableSave", menuName = "KetchappTools/Core/Save/ScriptableSave")]
    [System.Serializable]
    public class ScriptableSaveData : SaveData<ScriptableSaveData>
    {
        #region Properties

        [Header("Save config")]
        [SerializeField]
        protected FileConfiguration saveConfig;

#if ODIN_INSPECTOR
        [PropertySpace(4)]
        [FoldoutGroup("Save options")]
        [Button("Save", ButtonSizes.Medium)]
        public void SaveInEditor() => Save();
        [FoldoutGroup("Save options")]
        [Button("Load", ButtonSizes.Medium)]
        public void LoadInEditor() => Load();
        [FoldoutGroup("Save options")]
        [Button("Delete save file", ButtonSizes.Medium)]
        public void DeleteFileInEditor() => DeleteFile();
#endif

        [Header("Data")]
        public System.DateTime lastSaveDate;

        #endregion

        public float score;

        protected override void OnBeforeLoad()
        {
            base.OnBeforeLoad();

            saveFileConfig = saveConfig;
        }
        protected override void OnBeforeSave()
        {
            base.OnBeforeSave();

            saveFileConfig = saveConfig;
        }

        protected override void ResetData()
        {
            base.ResetData();

            score = 0;
        }
    }

#if UNITY_EDITOR && !ODIN_INSPECTOR
    [CustomEditor(typeof(ScriptableSaveData))]
    public class ScriptableSaveDataEditor : Editor
    {
        private ScriptableSaveData _target;

        private void OnEnable()
        {
            _target = target as ScriptableSaveData;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Save"))
            {
                _target.Save();
            }
            if (GUILayout.Button("Load"))
            {
                _target.Load();
            }
            if (GUILayout.Button("Delete Save File"))
            {
                _target.DeleteFile();
            }
        }
    }
#endif
}

