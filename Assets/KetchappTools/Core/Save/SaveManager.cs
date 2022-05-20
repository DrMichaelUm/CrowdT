using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace KetchappTools.Core.Save
{
    public static class SaveManager<T> where T : SaveData<T>
    {
        /// <summary>
        /// Save the data in the format you choose
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filePath">full path, should use persistent data path subfolder</param>
        /// <param name="format">json or binary</param>
        public static void Save(SaveData<T> data, string filePath, SerializationManager.ExistingFileResolveMode fileResolveMode = SerializationManager.ExistingFileResolveMode.Replace)
        {
            SerializationManager.SaveToJson(data, filePath, fileResolveMode);
        }
        /// <summary>
        /// Load data from file path
        /// </summary>
        /// <param name="filePath">the full path of the save file</param>
        /// <param name="format">json or binary</param>
        /// <returns></returns>
        public static SaveData<T> Load(string filePath)
        {
            return SerializationManager.LoadFromJson(filePath) as T;
        }
        /// <summary>
        /// Load directly into existing data
        /// </summary>
        /// <param name="data">existing data to overwrite</param>
        /// <param name="filePath">the full path of the save file</param>
        public static bool LoadTo(SaveData<T> data, string filePath)
        {
            return SerializationManager.OverwriteFromJson(data, filePath);
        }

        /// <summary>
        /// Delete save file if it exists
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteSaveFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch
                {
                    Debug.LogError("[Save] Error deleting the file at " + filePath);
                }
            }
        }
    }
}

