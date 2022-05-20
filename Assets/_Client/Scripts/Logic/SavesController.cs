using Sirenix.OdinInspector;
using UnityEngine;

namespace CrowdT
{
    public class SavesController : MonoBehaviour
    {
        public void Start()
        {
            Save.Load();
        }

        [Button]
        public void DeleteSave()
        {
            Save.Delete();
            Save.Load();
            Start();
        }
    }
}