using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace KetchappTools.Core.DesignPatterns
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class SingletonScriptableObjectAttribute : Attribute
    {
        private string filename;
        public string Filename { get { return filename; } }

        public SingletonScriptableObjectAttribute(string _filename) { filename = _filename; }
    }

    public class SingletonScriptableObject<T> : SingletonScriptableObject where T : SingletonScriptableObject<T>
    {
        #region Properties

        protected static T instance = null;
        public static T Instance
        {
            get
            {
#if UNITY_EDITOR
                if (!instance)
                    EditorLoadResource();
#endif
                return instance;
            }
        }

        #endregion

        #region Methods

#if UNITY_EDITOR
        protected static void EditorLoadResource()
        {
            SingletonScriptableObjectAttribute singletonScriptableObjectAttribute = typeof(T).GetCustomAttribute(typeof(SingletonScriptableObjectAttribute), true) as SingletonScriptableObjectAttribute;
            if (singletonScriptableObjectAttribute == null)
                return;
            instance = Resources.Load<T>(singletonScriptableObjectAttribute.Filename);
            if (!instance)
            {
                instance = CreateInstance<T>();
                if (!System.IO.Directory.Exists(Application.dataPath + "/Resources"))
                    System.IO.Directory.CreateDirectory(Application.dataPath + "/Resources");
                UnityEditor.AssetDatabase.CreateAsset(instance, "Assets/Resources/" + singletonScriptableObjectAttribute.Filename + ".asset");
                UnityEditor.AssetDatabase.Refresh();
            }
        }
#endif

        protected override void Initialize()
        {
            instance = (T)this;
        }

        #endregion
    }

    public abstract class SingletonScriptableObject : ScriptableObject
    {
        protected abstract void Initialize();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoad()
        {
            foreach (Assembly iAssembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // HACK: IL2CPP crashes if you attempt to get the methods of some classes in these assemblies.
                if (iAssembly.FullName.StartsWith("System") || iAssembly.FullName.StartsWith("mscorlib"))
                    continue;
                foreach (Type iType in iAssembly.GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(SingletonScriptableObject))))
                {
                    SingletonScriptableObjectAttribute singletonScriptableObjectAttribute = iType.GetCustomAttribute(typeof(SingletonScriptableObjectAttribute), true) as SingletonScriptableObjectAttribute;
                    if (singletonScriptableObjectAttribute == null)
                        continue;
                    SingletonScriptableObject instance = Resources.Load<SingletonScriptableObject>(singletonScriptableObjectAttribute.Filename);
                    if (instance)
                        instance.Initialize();
                }
            }
        }

#if UNITY_EDITOR
        //[UnityEditor.Callbacks.DidReloadScripts(0)]
        //private static void DidReloadScripts()
        [UnityEditor.MenuItem("Ketchapp Tools/Core/Create Singleton Scriptable Objects")]
        private static void CreateSingletonScriptableObjects()
        {
            bool assetDatabaseRefresh = false;
            // Try get the instance, will auto create scriptable object if not exist
            foreach (Assembly iAssembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // HACK: IL2CPP crashes if you attempt to get the methods of some classes in these assemblies.
                if (iAssembly.FullName.StartsWith("System") || iAssembly.FullName.StartsWith("mscorlib"))
                    continue;
                foreach (Type iType in iAssembly.GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(SingletonScriptableObject))))
                {
                    SingletonScriptableObjectAttribute singletonScriptableObjectAttribute = iType.GetCustomAttribute(typeof(SingletonScriptableObjectAttribute), true) as SingletonScriptableObjectAttribute;
                    if (singletonScriptableObjectAttribute == null)
                        continue;
                    SingletonScriptableObject instance = Resources.Load<SingletonScriptableObject>(singletonScriptableObjectAttribute.Filename);
                    if (!instance)
                    {
                        instance = (SingletonScriptableObject)CreateInstance(iType);
                        if (!System.IO.Directory.Exists(Application.dataPath + "/Resources"))
                            System.IO.Directory.CreateDirectory(Application.dataPath + "/Resources");
                        UnityEditor.AssetDatabase.CreateAsset(instance, "Assets/Resources/" + singletonScriptableObjectAttribute.Filename + ".asset");
                        assetDatabaseRefresh = true;
                    }
                }
            }
            if (assetDatabaseRefresh && !UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                UnityEditor.AssetDatabase.Refresh();
        }
#endif
    }
}