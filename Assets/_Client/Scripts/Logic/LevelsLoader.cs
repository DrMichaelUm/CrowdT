using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrowdT.Level
{
    public class LevelsLoader : MonoBehaviour
    {
        [SerializeField] public GameObject[] levels;

        private void Awake()
        {
            Save.Load();

            var realLevelId = Save.data.level % levels.Length;
            Load(realLevelId);
        }
        
        private void Load(int levelID)
        {
            Instantiate(levels[levelID]);
        }

        [Button("Load Level")]
        public void LoadLevel(int levelID)
        {
            Save.data.level = levelID;
            Save.Store();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        [UsedImplicitly]
        public static void MoveToNextLevel()
        {
            IncreaseLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private static void IncreaseLevel()
        {
            var currentLevel = Save.data.level;
            currentLevel++;
            Save.data.level = currentLevel;
            Save.Store();
        }

        [UsedImplicitly]
        public static void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}