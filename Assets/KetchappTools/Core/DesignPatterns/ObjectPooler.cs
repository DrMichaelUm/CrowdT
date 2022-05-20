using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Core.DesignPatterns
{
    public class ObjectPooler : MonoBehaviour
    {
        static Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();
        static Dictionary<GameObject, List<GameObject>> inUse = new Dictionary<GameObject, List<GameObject>>();
        static Dictionary<GameObject, Transform> poolsParent = new Dictionary<GameObject, Transform>();
        static Dictionary<GameObject, Coroutine> destroyCoroutineByObject = new Dictionary<GameObject, Coroutine>();

        static ObjectPooler instance;
        public static ObjectPooler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<ObjectPooler>();

                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<ObjectPooler>();
                        singletonObject.name = "PoolParty (Singleton)";
                    }

                    DontDestroyOnLoad(instance);
                }

                return instance;
            }
        }

        static void CreatePool(GameObject prefab)
        {
            pools.Add(prefab, new Queue<GameObject>());
            inUse.Add(prefab, new List<GameObject>());

            Transform parent = new GameObject(prefab.name).transform;
            parent.SetParent(Instance.transform);
            poolsParent.Add(prefab, parent);
        }

        public static void SetPoolSize(Component prefab, int size)
        {
            SetPoolSize(prefab.gameObject, size);
        }

        public static void SetPoolSize(GameObject prefab, int size)
        {
            if (!pools.ContainsKey(prefab))
            {
                CreatePool(prefab);

                for (int i = 0; i < size; i++)
                    pools[prefab].Enqueue(InstantiateGameObject(prefab).gameObject);
            }
        }

        static GameObject InstantiateGameObject(GameObject prefab)
        {
            GameObject instance = Object.Instantiate(prefab, poolsParent[prefab]);
            poolsParent[prefab].name = "[" + (pools[prefab].Count + inUse[prefab].Count + 1) + "] " + prefab.name;
            instance.gameObject.SetActive(false);
            return instance;
        }

        #region Getting
        public new static T Instantiate<T>(T original) where T : Component
        {
            return Instantiate(original, original.transform.position, original.transform.rotation, null);
        }

        public new static T Instantiate<T>(T original, Transform parent) where T : Component
        {
            return Instantiate(original, original.transform.position, original.transform.rotation, parent);
        }

        public new static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Component
        {
            return Instantiate(original, position, rotation, null);
        }

        public new static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Component
        {
            GameObject originalGO = original.gameObject;
            T obj;

            if (!pools.ContainsKey(originalGO))
                CreatePool(originalGO);

            Queue<GameObject> pool = pools[originalGO];

            if (pool.Count != 0)
                obj = pool.Dequeue().GetComponent<T>();
            else
                obj = InstantiateGameObject(original.gameObject).GetComponent<T>();

            inUse[originalGO].Add(obj.gameObject);

            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.transform.SetParent(parent);

            obj.gameObject.SetActive(true);

            return obj;
        }
        #endregion

        #region Releasing
        public static void Destroy(Component comp, float t)
        {
            Destroy(comp.gameObject, t);
        }

        public static void Destroy(GameObject obj, float t)
        {
            destroyCoroutineByObject.Add(obj, Instance.StartCoroutine(DestroyDelayed(obj, t)));
        }

        static IEnumerator DestroyDelayed(GameObject obj, float t)
        {
            yield return new WaitForSeconds(t);

            Destroy(obj);
        }

        public static void Destroy(Component comp)
        {
            if (comp == null)
            {
                Debug.LogError("The object you want to destroy is null");
                return;
            }

            Destroy(comp.gameObject);
        }

        public static void Destroy(GameObject obj)
        {
            if (obj == null)
            {
                Debug.LogError("The object you want to destroy is null");
                return;
            }

            if (destroyCoroutineByObject.ContainsKey(obj))
            {
                Coroutine destroyCoroutine = destroyCoroutineByObject[obj];

                if (destroyCoroutine != null)
                    Instance.StopCoroutine(destroyCoroutine);

                destroyCoroutineByObject.Remove(obj);
            }

            GameObject prefab = null;

            foreach (GameObject key in inUse.Keys)
            {
                if (inUse[key].Contains(obj))
                {
                    prefab = key;
                    break;
                }
            }

            if (prefab == null)
            {
                Debug.LogError(obj + " prefab not found, destroying");
                Object.Destroy(obj);
                return;
            }

            inUse[prefab].Remove(obj);
            pools[prefab].Enqueue(obj);

            obj.SetActive(false);
            obj.transform.SetParent(poolsParent[prefab]);
        }

        public static void ReleasePoolObjects(Component comp) => ReleasePoolObjects(comp.gameObject);

        public static void ReleasePoolObjects(GameObject prefab)
        {
            for (int i = inUse[prefab].Count - 1; i >= 0; i--)
                Destroy(inUse[prefab][i]);
        }

        public static void ReleaseAllPoolsObjects()
        {
            foreach (GameObject prefab in pools.Keys)
                ReleasePoolObjects(prefab);

            //Instance.StopAllCoroutines();
        }
        #endregion
    }
}