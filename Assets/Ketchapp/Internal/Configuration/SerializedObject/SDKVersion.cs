﻿using System;

namespace Ketchapp.Internal.Configuration
{
    [Serializable]
    public class SDKVersion
    {
#pragma warning disable SA1401 // Fields should be private
        public string Version;
        public string Name;
        public string Extension;
        public short ToolVersionId;
        public string Changelog;
        public SDKType Type;
        public string[] PluginPaths;
        public string[] FilesToDelete;
#pragma warning restore SA1401 // Fields should be private
    }

    [Serializable]
    public class SDKZipVersion : SDKVersion
    {
#pragma warning disable SA1401 // Fields should be private
        public string FolderInsideZip;
        public string FileName;
#pragma warning restore SA1401 // Fields should be private
    }

    public enum SDKType
    {
        Main = 1,
        Mediation = 2,
        Analytics = 3,
        Misc = 4
    }
}