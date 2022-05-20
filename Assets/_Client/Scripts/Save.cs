using Cysharp.Threading.Tasks;

namespace CrowdT
{
    using UnityEngine;

    [System.Serializable]
    public class Data
    {
        public bool firstAppLaunch = false;
        public int level = 0;
        public int coins = 0;
        public float volume = 0.5f;
    }

    public static class Save
    {
        public static Data data;
        static string keyName = "data";

        // Convert the data into PlayPrefs as XML
        public static void Store()
        {
            PlayerPrefs.SetString(keyName, Serializer.Serialize<Data>(data));
        }

        // Load the data from the PlayerPrefs and parse it into the data object
        public static void Load(System.Action whenLoad = null)
        {
            if (PlayerPrefs.HasKey(keyName))
            {
                data = Serializer.Deserialize<Data>(PlayerPrefs.GetString(keyName));
            }
            else
            {
                data = new Data();
                Store();
            }

            if (whenLoad != null) whenLoad.Invoke();
        }

        // Delete the save
        public static void Delete()
        {
            DeleteKey(keyName);
        }

        // Delete the given key inside the PlayerPrefs : DEVELOPER ONLY
        public static void DeleteKey(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
            }
        }
    }
}