using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace KetchappTools.Core.Save
{
    /// <summary>
    /// This class contains the file information needed to save (essentially file path info)
    /// </summary>
    [System.Serializable]
    public class FileConfiguration
    {
        public enum DirectoryMode { RelativeToPersistentDataPath, FullPath}

        public DirectoryMode directoryMode;
        public string directory = "";

        public string fileDirectory
        {
            get
            {
                switch (directoryMode)
                {
                    case DirectoryMode.RelativeToPersistentDataPath:
                        return Path.Combine(Application.persistentDataPath, directory);
                    case DirectoryMode.FullPath:
                        return directory;
                    default:
                        return directory;
                }
            }
        }
        public string fileName = "mySave";
        public string fileExtension = "save";

        public FileConfiguration() { }

        public FileConfiguration(DirectoryMode directoryMode, string directory, string fileName, string fileExtension)
        {
            this.directoryMode = directoryMode;
            this.directory = directory;
            this.fileName = fileName;
            this.fileExtension = fileExtension;
        }
        
        public FileConfiguration(string fullpath)
        {
            directoryMode = DirectoryMode.FullPath;
            directory = Path.GetDirectoryName(fullpath);
            fileName = Path.GetFileNameWithoutExtension(fullpath);
            fileExtension = Path.GetExtension(fullpath);
        }

        public string GetFilePath()
        {
            return Path.Combine(fileDirectory, fileName + "." + fileExtension);
        }

        /// <summary>
        /// return the file name with iteration suffix
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="iteration"></param>
        /// <returns></returns>
        public static string GetFileIterationName(string fileName, int iteration)
        {
            string iterationSuffix = "";
            if(iteration > 1)
            {
                iterationSuffix = iteration.ToString("_0000");
            }
            return fileName + iterationSuffix;
        }

        /// <summary>
        /// Clone the config with the iteration specified
        /// </summary>
        /// <param name="fileConfig">the file config you want to clone</param>
        /// <param name="iterationIndex">the iteration if you know it, else leave it empty</param>
        /// <returns></returns>
        public static FileConfiguration CreateIterationFrom(FileConfiguration fileConfig, int iterationIndex = -1)
        {
            // if iterationindex param = -1 search for next iteration available
            if(iterationIndex == -1)
            {
                iterationIndex = 0;
                FileConfiguration fg = new FileConfiguration(DirectoryMode.FullPath, fileConfig.fileDirectory, GetFileIterationName(fileConfig.fileName, iterationIndex), fileConfig.fileExtension);
                while (File.Exists(fg.GetFilePath()))
                {
                    iterationIndex++;
                    fg = new FileConfiguration(DirectoryMode.FullPath, fileConfig.fileDirectory, GetFileIterationName(fileConfig.fileName, iterationIndex), fileConfig.fileExtension);
                }
            }

            FileConfiguration newfileConfig = new FileConfiguration(DirectoryMode.FullPath, fileConfig.fileDirectory, GetFileIterationName(fileConfig.fileName, iterationIndex), fileConfig.fileExtension);
            return newfileConfig;

        }
    }

    public class SaveFileUtils
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Ketchapp Tools/Core/Save/Open persistent data folder")]
        static void OpenPersistentDataFolder()
        {
            Application.OpenURL(Application.persistentDataPath);
        }
#endif
    }
}
