#if UNITY_EDITOR
using System.IO;
using System.Net.Http;
using UnityEditor;
using UnityEngine;
using Ketchapp.Editor.Purchasing;

namespace Ketchapp.Editor.Utils
{
    [InitializeOnLoad]
    internal class TokenRemover : EditorWindow
    {
        private static string TokenPath => Path.Combine(Application.dataPath, "Ketchapp", "Internal", "Editor", "Authentication", "Token");
        static TokenRemover()
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode && EditorApplication.timeSinceStartup <= 20)
            {
                FileUtil.DeleteFileOrDirectory(TokenPath);
                if (!File.Exists(IAPEditor.PurchasingEnumPath))
                {
                    IAPEditor.GenerateDefaultPurchasingFile();
                }
            }

            Application.logMessageReceived += HandleMayoEditorLog;
        }

        private static void HandleMayoEditorLog(string condition, string stackTrace, LogType logType)
        {
            if (logType == LogType.Exception)
            {
                if (Application.isEditor && !Application.isPlaying)
                {
                    if (condition.Contains(nameof(HttpRequestException)) && condition.Contains("Unauthorized"))
                    {
                        FileUtil.DeleteFileOrDirectory(TokenPath);
                    }
                }
            }
        }
    }
}
#endif