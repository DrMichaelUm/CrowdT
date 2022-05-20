using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Jenkins
{
    /// 

    /// Single command to trigger the full build for iOS
    /// 

    static void CommandLineBuildiOS()
    {
        // Setup the ketchapp build settings
        KetchappTools.Core.BuildHelpers.BuildSetting.Instance.SetBuildSettingFromJSON();

        // Start the build
        Build(BuildTarget.iOS, "Build/iOS");
    }
    /// 

    /// Single command to trigger the full build for Android
    /// 

    static void CommandLineBuildAndroid()
    {
        Build(BuildTarget.Android, "Build/Android");
    }
	/// 

	/// Single command to trigger the full build for Mac Standalone
	/// 

	static void CommandLineBuildMac()
	{
		Build(BuildTarget.StandaloneOSX, "Build/MacOS");
	}
	/// 

	/// Single command to trigger the full build for Mac Standalone
	/// 

	static void CommandLineBuildWindows()
	{
		Build(BuildTarget.StandaloneWindows, "Build/Windows");
	}
	/// 

	/// Single command to trigger the full build for Mac Standalone
	/// 

	static void CommandLineBuildWebGL()
	{
		Build(BuildTarget.WebGL, "Build/WebGL");
	}
	/// 

	/// Find all enabled scenes and start a build for the given platform
	/// 

	/// Unity target platform
	/// Path in which to do the build
	static void Build(UnityEditor.BuildTarget targetPlatform, string path)
   {
        // Path must be specified
        if (path == null)
        {
            Debug.Log("ERROR: No build path specified");
            return;
        }
        // Find all enabled scenes in the project and put them into a string array
        List <string> sceneList = new List <string> ();
        foreach (EditorBuildSettingsScene ebs in EditorBuildSettings.scenes)
        {
            if (ebs.enabled)
                sceneList.Add(ebs.path);
        }
        // If we have nothing to build then exit
        string[] scenes = sceneList.ToArray();
        if (scenes == null || scenes.Length == 0)
        {
            Debug.Log("ERROR: No scenes enabled so there's nothing to build");
            return;
        }
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		buildPlayerOptions.scenes = scenes;
		buildPlayerOptions.target = targetPlatform;
		buildPlayerOptions.locationPathName = path;
		// Start the build
		BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}