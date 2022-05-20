using System;
using Sirenix.Utilities;
using UnityEngine;

namespace CrowdT
{
    // [ScriptExecutionOrder(-1000)]
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        #region Variables

        private static T _instance = null;
        protected static T instance
        {
            get
            {
                if (!_instance)
                    InstantiateInternal();
                return _instance;
            }
        }
        public static T Instance { get { return instance; } }

        #endregion

        #region Instanciate Methods

        protected virtual void OnInstantiate() { }
        protected virtual void OnUninstantiate() { }

        public static void Instantiate(T instance = null, GameObject gameObject = null)
        {
            InstantiateInternal(instance, gameObject, true);
        }

        private static void InstantiateInternal(T instance = null, GameObject gameObject = null, bool forceCreate = false)
        {
            // Get attribut or create the default one
            SingletonAttribute singletonAttribute = typeof(T).GetCustomAttribute<SingletonAttribute>();
            if (singletonAttribute == null)
                singletonAttribute = new SingletonAttribute();

            // Manage multi instance
            if (_instance != null)
            {
                if (singletonAttribute.MultipleInstance == SingletonAttribute.MultipleInstanceMode.Ignore)
                {
                    // Destroy instance past in parameter
                    if (instance != null)
                    {
                        if (singletonAttribute.Destroy == SingletonAttribute.DestroyMode.GameObject)
                            Destroy(instance.gameObject);
                        else
                            Destroy(instance);
                    }
                    return;
                }

                // Uninstantiate previous instance
                Uninstantiate(singletonAttribute.Destroy == SingletonAttribute.DestroyMode.GameObject);
            }

            // Search for existing instance
            _instance = FindObjectOfType<T>();

            // Manage creation
            if (!_instance && (forceCreate || singletonAttribute.AutoCreate == SingletonAttribute.AutoCreateMode.Enable))
            {
                if (gameObject == null)
                    gameObject = new GameObject(typeof(T).ToString());
                _instance = gameObject.AddComponent<T>();
            }

            // Set DontDestroyOnLoad if necessary
            if (singletonAttribute.DontDestroyOnLoad == SingletonAttribute.DontDestroyOnLoadMode.Enable)
                DontDestroyOnLoad(_instance.gameObject);
        }

        public static void Uninstantiate(bool destroyGameObject = false)
        {
            if (destroyGameObject)
                Destroy(_instance.gameObject);
            else
                Destroy(_instance);
        }

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            if (_instance != this)
                InstantiateInternal((T)this);
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance.OnUninstantiate();
                _instance = null;
            }
        }

        #endregion
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class SingletonAttribute : Attribute
    {
        public enum DontDestroyOnLoadMode
        {
            Enable,
            Disable
        }

        public enum MultipleInstanceMode
        {
            Replace,
            Ignore
        }

        public enum AutoCreateMode
        {
            Enable,
            Disable
        }

        public enum DestroyMode
        {
            Component,
            GameObject
        }

        private DontDestroyOnLoadMode dontDestroyOnLoad;
        public DontDestroyOnLoadMode DontDestroyOnLoad { get { return dontDestroyOnLoad; } }

        private MultipleInstanceMode multipleInstance;
        public MultipleInstanceMode MultipleInstance { get { return multipleInstance; } }

        private AutoCreateMode autoCreate;
        public AutoCreateMode AutoCreate { get { return autoCreate; } }

        private DestroyMode destroy;
        public DestroyMode Destroy { get { return destroy; } }

        public SingletonAttribute(
            DontDestroyOnLoadMode dontDestroyOnLoad = DontDestroyOnLoadMode.Enable,
            MultipleInstanceMode multipleInstance = MultipleInstanceMode.Ignore,
            AutoCreateMode autoCreate = AutoCreateMode.Enable,
            DestroyMode destroy = DestroyMode.Component
            )
        {
            this.dontDestroyOnLoad = dontDestroyOnLoad;
            this.multipleInstance = multipleInstance;
            this.autoCreate = autoCreate;
            this.destroy = destroy;
        }
    }
}