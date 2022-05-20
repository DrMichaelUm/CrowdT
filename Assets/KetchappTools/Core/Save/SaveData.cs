using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace KetchappTools.Core.Save
{
#if ODIN_INSPECTOR
    [InlineEditor]
#endif
    [System.Serializable]
    public abstract class SaveData<T> : ScriptableObject where T : SaveData<T>
    {
        const string defaultSaveExtension = "save";
        const string defaultSaveSubFolder = "";

        public FileConfiguration saveFileConfig { get; set; }


        public static T Create()
        {
            T data = CreateInstance<T>();
            return data;
        }
        public static T Create(FileConfiguration saveFileConfig)
        {
            T data = CreateInstance<T>();
            data.saveFileConfig = saveFileConfig;
            return data;
        }
        public static T Create(string saveName)
        {
            T data = CreateInstance<T>();
            FileConfiguration sConfig = new FileConfiguration()
            {
                fileExtension = defaultSaveExtension,
                fileName = saveName,
                directory = defaultSaveSubFolder
            };
            data.saveFileConfig = sConfig;
            return data;
        }

        public static T Duplicate(T original)
        {
            return original;
        }


        public void Save()
        {
            OnBeforeSave();
            SaveManager<T>.Save(this, saveFileConfig.GetFilePath());
            OnAfterSave();
        }
        public bool Load()
        {
            OnBeforeLoad();
            bool success = SaveManager<T>.LoadTo(this, saveFileConfig.GetFilePath());
            if (!success) // reset scriptable object
                ResetData();
            OnAfterLoad();
            return success;
        }
        public void DeleteFile()
        {
            SaveManager<T>.DeleteSaveFile(saveFileConfig.GetFilePath());
        }

        protected virtual void ResetData()
        {

        }


        protected virtual void OnBeforeSave() { }
        protected virtual void OnAfterSave() { }
        protected virtual void OnBeforeLoad() { }
        protected virtual void OnAfterLoad() { }


    }
}
