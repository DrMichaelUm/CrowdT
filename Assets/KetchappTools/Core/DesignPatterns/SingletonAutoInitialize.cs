using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using KetchappTools.Core.Attributes;

namespace KetchappTools.Core.DesignPatterns
{
    [ScriptExecutionOrder(-1100)]
    public abstract class SingletonAutoInitialize<T> : SingletonAutoInitialize where T : SingletonAutoInitialize<T>
    {
        #region Variables

        private static T _instance = null;
        protected static T instance { get { return _instance; } }
        public static T Instance { get { return instance; } }

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            _instance = (T)this;
        }
        
        #endregion
    }

    public class SingletonAutoInitialize : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoad()
        {
            foreach (Assembly iAssembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // HACK: IL2CPP crashes if you attempt to get the methods of some classes in these assemblies.
                if (iAssembly.FullName.StartsWith("System") || iAssembly.FullName.StartsWith("mscorlib"))
                    continue;
                foreach (Type iType in iAssembly.GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(SingletonAutoInitialize))))
                {
                    GameObject b_gameObject = new GameObject(iType.ToString());
                    b_gameObject.AddComponent(iType);
                    DontDestroyOnLoad(b_gameObject);
                }
            }
        }
    }
}