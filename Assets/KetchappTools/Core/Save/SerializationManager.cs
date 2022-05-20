using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

namespace KetchappTools.Core.Save
{
    public enum SaveFormat { Json, Binary }

    public static class SerializationManager
    {
        public enum ExistingFileResolveMode { Replace, Increment, Ignore }
        private static bool ResolvePathConflict(string filePath, out string newFilePath, ExistingFileResolveMode fileResolveMode = ExistingFileResolveMode.Replace)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return ResolveExistingFile(fileResolveMode, filePath, out newFilePath);
        }

        /// <summary>
        /// Solve file creation conflict with existing file
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="fullPath"></param>
        /// <param name="newFullPath"></param>
        /// <returns>Return true is the file creation can proceed else, need to abort</returns>
        private static bool ResolveExistingFile(ExistingFileResolveMode mode, string fullPath, out string newFullPath)
        {
            //Debug.Log("Resolve conflict with existing file [" + mode.ToString() + "] " + fullPath);
            string extension = Path.GetExtension(fullPath);
            switch (mode)
            {
                case ExistingFileResolveMode.Replace:
                    newFullPath = fullPath;
                    //Debug.Log("Resulted path is " + newFullPath);
                    return true;
                case ExistingFileResolveMode.Increment:

                    string path = newFullPath = fullPath;
                    int countOut = 0;
                    while (File.Exists(path) && countOut < 1000)
                    {
                        int i = path.LastIndexOf("_");
                        if (i != -1 && Int32.TryParse(path.Substring(i + 1, 4), out int inc))
                        {
                            inc++;
                            newFullPath = path.Substring(0, i + 1) + inc.ToString("0000") + extension;
                        }
                        else
                        {
                            newFullPath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + "_0001" + extension);
                        }
                        path = newFullPath;
                        countOut++;
                    }
                    if (countOut >= 1000)
                        Debug.Log("count OUT, more than 1000 file with same user name");

                    //Debug.Log("Resulted path is " + path);
                    return true;

                case ExistingFileResolveMode.Ignore:
                    newFullPath = fullPath;
                    //Debug.Log("Resulted path is " + newFullPath);
                    return false;
                default:
                    newFullPath = fullPath;
                    return false;
            }
        }

        // The save core does not use binary serialization, but I let this if you want to serialize any serializable C# object (other than scriptable objects)
        #region Binary

        public static bool SaveToBinary(object dataToSave, string filePath, ExistingFileResolveMode fileResolveMode = ExistingFileResolveMode.Replace)
        {
            string newFilePath = filePath;
            if (!ResolvePathConflict(filePath, out newFilePath, fileResolveMode))
            {
                Debug.LogError(string.Format("[Save] Error, can not solve conflict with file path name ", filePath));
                return false;
            }

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(newFilePath, FileMode.Create);
            formatter.Serialize(stream, dataToSave);
            stream.Close();

            return true;
        }

        public static object LoadFromBinary(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError(string.Format("[Save] Error, can not load from path {0}", filePath));
                return null;
            }

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            try
            {
                object data = formatter.Deserialize(stream);
                stream.Close();
                return data;
            }
            catch
            {
                Debug.LogError(string.Format("[Save] Error, fail to load file at {0}", filePath));
                stream.Close();
                return null;
            }
        }

        #endregion

        #region Json

        public static bool SaveToJson(object dataToSave, string filePath, ExistingFileResolveMode fileResolveMode = ExistingFileResolveMode.Replace)
        {
            string newFilePath = filePath;
            if (!ResolvePathConflict(filePath, out newFilePath, fileResolveMode))
            {
                Debug.LogError(string.Format("[Save] Error, can not solve conflict with file path name ", filePath));
                return false;
            }

            string json = JsonUtility.ToJson(dataToSave);

            StreamWriter writer = new StreamWriter(filePath, false);
            writer.Write(json);
            writer.Close();
            return true;
        }

        public static object LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogWarning(string.Format("[Save] Error, can not load from path {0}", filePath));
                return null;
            }

            StreamReader reader = new StreamReader(filePath);
            string json = reader.ReadToEnd();

            try
            {
                object data = JsonUtility.FromJson<object>(json);
                reader.Close();
                return data;
            }
            catch
            {
                Debug.LogError(string.Format("[Save] Error, fail to load file at {0}" + filePath));
                reader.Close();
                return null;
            }
        }

        public static bool OverwriteFromJson(object dataToOverwrite, string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogWarning(string.Format("[Save] Error, can not load from path {0}, file does not exist", filePath));
                return false;
            }

            StreamReader reader = new StreamReader(filePath);
            string json = reader.ReadToEnd();

            try
            {
                JsonUtility.FromJsonOverwrite(json, dataToOverwrite);
                reader.Close();
                return true;
            }
            catch
            {
                Debug.LogError(string.Format("[Save] Error, fail to load file at {0}" + filePath));
                reader.Close();
                return false;
            }
        }

        #endregion
    }

}

