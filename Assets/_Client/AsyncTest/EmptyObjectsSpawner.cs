using UnityEngine;

namespace Ketchapp.CrowdTerritory
{
    public class EmptyObjectsSpawner : MonoBehaviour
    {
        public GameObject prefab;
        public int amount;

        private void Start()
        {
            for (var i = 0; i != amount; ++i)
            {
                Instantiate(prefab, Vector3.zero, Quaternion.identity);
            }
        }
    }
}
