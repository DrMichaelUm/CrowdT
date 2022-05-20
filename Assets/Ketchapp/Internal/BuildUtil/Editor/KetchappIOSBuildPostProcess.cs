#if UNITY_IPHONE
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ketchapp;
using Ketchapp.Editor.Utils;
using Ketchapp.Internal.Configuration;
using UnityEditor;
using UnityEditor.Android;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;
namespace Ketchapp.Internal.Build
{
    public class KetchappIOSBuildPostProcess
    {
#pragma warning disable SA1401 // Fields should be private
        public static string InfoPlistPath;
        public static string BuildPath;
        public static List<string> CrossPromoBundles;
        public static GameInfos Settings;
#pragma warning restore SA1401 // Fields should be private

        [PostProcessBuild(10000)]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuildProject)
        {
            Settings = Resources.Load<GameInfos>(KetchappEditorUtils.Configuration.ConfigurationObjectName);
            BuildPath = pathToBuildProject;
            if (buildTarget == BuildTarget.iOS)
            {
                string projectPath = BuildPath + "/Unity-iPhone.xcodeproj/project.pbxproj";

                PBXProject pbxProject = new PBXProject();
                pbxProject.ReadFromFile(projectPath);

                string target = string.Empty;
#if UNITY_2019_3_OR_NEWER
                target = pbxProject.GetUnityMainTargetGuid();
                var frameworkTarget = pbxProject.GetUnityFrameworkTargetGuid();
#else
            target = pbxProject.ProjectGuid();
#endif
                pbxProject.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
                pbxProject.SetBuildProperty(frameworkTarget, "ENABLE_BITCODE", "NO");
                pbxProject.WriteToFile(projectPath);

#if MEDIATION_FairBid
                pbxProject.SetBuildProperty(target, "VALIDATE_WORKSPACE", "YES");
                pbxProject.SetBuildProperty(target, "VALIDATE_WORKSPACE", "YES");
#endif

                ModifyInfoPlist();
#if CrossPromotion
                ModifyInterstitielBundle();
                AddCrossPromoBundle(pathToBuildProject, pbxProject, projectPath);
#endif
#if AppsFlyer
                CommentAppsflyerController();
#endif
            }
        }

        public static void ModifyInfoPlist()
        {
            InfoPlistPath = BuildPath + "/Info.plist";
            var plistParser = new PlistDocument();
            plistParser.ReadFromFile(InfoPlistPath);
            var rootDict = plistParser.root.AsDict();
            rootDict.SetString("NSCalendarsUsageDescription", "Some Ads may use this.");
            rootDict.SetString("GADApplicationIdentifier", Settings.IosConfiguration.AdMobAppId);

            foreach (var value in plistParser.root.values.ToList())
            {
                if (value.Key.Contains("UsageDescription"))
                {
                    plistParser.root.SetString(value.Key, "Some Ads may us this.");
                }
            }

            rootDict.SetString("NSUserTrackingUsageDescription", "Your data will be used to deliver personalized ads to you.");
            string bundleId = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS);
            PlistElement tempValue;

            if (!plistParser.root.values.TryGetValue("CFBundleURLTypes", out tempValue))
            {
                plistParser.root.CreateArray("CFBundleURLTypes");
            }

            var dict = plistParser.root["CFBundleURLTypes"].AsArray().AddDict();

            dict.SetString("CFBundleURLName", bundleId);
            var bundleArray = dict.CreateArray("CFBundleURLSchemes");
            bundleArray.AddString(bundleId);
            bundleArray.AddString(Settings.IosConfiguration.CrossPromotionBundle);

            if (!plistParser.root.values.TryGetValue("LSApplicationQueriesSchemes", out tempValue))
            {
                plistParser.root.CreateArray("LSApplicationQueriesSchemes");
            }

            var bundles = plistParser.root["LSApplicationQueriesSchemes"].AsArray();
            List<string> promoBundles = RetrievePromoBundles();

            if (!BundlesAlreadyContained(bundles))
            {
                foreach (string bundle in promoBundles)
                {
                    bundles.AddString(bundle);
                }
            }

            if (KetchappEditorUtils.Configuration.ConfigurationObject.SKAdNetworksId != null)
            {
                if (KetchappEditorUtils.Configuration.ConfigurationObject.SKAdNetworksId.Count > 0)
                {
                    var skAdNetworksIdValues = KetchappEditorUtils.Configuration.ConfigurationObject.SKAdNetworksId;
                    PlistElement existingSkAdNetworks;
                    if (rootDict.values.TryGetValue("SKAdNetworkItems", out existingSkAdNetworks))
                    {
                        foreach (var existing in existingSkAdNetworks.AsArray().values)
                        {
                            PlistElement skNetworkValue;
                            if (existing.AsDict().values.TryGetValue("SKAdNetworkIdentifier", out skNetworkValue))
                            {
                                skAdNetworksIdValues.Add(skNetworkValue.AsString());
                            }
                        }
                    }

                    rootDict.values.Remove("SKAdNetworkItems");
                    plistParser.root.CreateArray("SKAdNetworkItems");

                    var skAdNetworkdsIdArray = plistParser.root["SKAdNetworkItems"].AsArray();

                    foreach (string id in skAdNetworksIdValues)
                    {
                        var skDict = skAdNetworkdsIdArray.AddDict();
                        skDict.SetString("SKAdNetworkIdentifier", id);
                    }
                }
            }

            var atsRoot = plistParser.root.CreateDict("NSAppTransportSecurity");
            atsRoot.AsDict().SetBoolean("NSAllowsArbitraryLoads", true);
            File.WriteAllText(InfoPlistPath, plistParser.WriteToString());
        }

        public static bool BundlesAlreadyContained(PlistElementArray array)
        {
            foreach (PlistElement value in array.values.ToList())
            {
                if (value.ToString().Contains("com.ketchapp.game00"))
                {
                    return true;
                }
            }

            return false;
        }

        public static List<string> RetrievePromoBundles()
        {
            List<string> bundles = new List<string>();

            string[] lines = File.ReadAllLines(Application.dataPath + "/Ketchapp/Internal/BuildUtil/bundles.txt");

            return lines.ToList();
        }

        public static void CommentAppsflyerController()
        {
            var path = $"{BuildPath}/Libraries/Appsflyer/Plugins/iOS/AppsFlyerAppController.mm";
            var lines = File.ReadAllText(path);
            lines = lines.Replace("IMPL_APP_CONTROLLER_SUBCLASS(AppsFlyerAppController)", string.Empty);
            File.WriteAllText(path, lines);
        }

        public static void ModifyInterstitielBundle()
        {
            string controllerPath = BuildPath + "/Libraries/Ketchapp/Plugins/iOS/Ketchapp/KetchappController.mm";
            string controllerText = File.ReadAllText(controllerPath);
            controllerText = controllerText.Replace("com.ketchapp.yourgamename", Settings.IosConfiguration.CrossPromotionBundle == string.Empty ? "com.ketchapp.game00" : Settings.IosConfiguration.CrossPromotionBundle);
            File.WriteAllText(controllerPath, controllerText);
        }

        private static void AddCrossPromoBundle(string path, PBXProject pBXProject, string projectPath)
        {
            var targetGuid = pBXProject.GetUnityMainTargetGuid();
            var pathToBundle = Path.Combine(path, "Frameworks", "Ketchapp", "Plugins", "iOS", "Ketchapp", "Ketchapp.framework", "Resources.bundle");
            Debug.Log("GUID : " + targetGuid);
                var resourcesBuildPhase = pBXProject.GetResourcesBuildPhaseByTarget(targetGuid);
            Debug.Log("Resource build phase : " + resourcesBuildPhase);
                var resourcesFilesGuid = pBXProject.AddFile(pathToBundle, pathToBundle, PBXSourceTree.Source);
            Debug.Log("Resource build GUID : " + resourcesFilesGuid);
            pBXProject.AddFileToBuildSection(targetGuid, resourcesBuildPhase, resourcesFilesGuid);

            File.WriteAllText(projectPath, pBXProject.WriteToString());
        }
    }
}
#endif