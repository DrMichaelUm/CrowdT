using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace KetchappTools.Core.BuildHelpers
{
    public class BuildHelpersPreprocessBuild : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }
        public void OnPreprocessBuild(BuildReport report)
        {
            // TODO Popup to select the build type ?
        }
    }
}
