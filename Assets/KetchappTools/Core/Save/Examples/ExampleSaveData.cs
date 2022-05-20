using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Core.Save.Examples
{
    [System.Serializable]
    public class ExampleSaveData : SaveData<ExampleSaveData>
    {
        // data
        public int score;
        public string playerName;
        public List<Level> levels;

        [System.Serializable]
        public class Level
        {
            public bool isDone;
            public float duration;

            public Level()
            {
                isDone = false;
                duration = 0;
            }
        }

        public void CreateDefault()
        {
            score = 0;
            playerName = "John Doe";
            levels = new List<Level>();
            for (int i = 0; i < 5; i++)
                levels.Add(new Level());
        }

        public void ChangeData()
        {
            score = 10;
            levels[0].isDone = true;
            levels[0].duration = 20;
        }

        public string ToText()
        {
            string s = "";
            s += playerName + "\n";
            s += "score = " + score + "\n";
            for (int i = 0; i < levels.Count; i++)
            {
                s += string.Format("Achieved ({0}), Duration = {1}", levels[i].isDone, levels[i].duration);
                if (i < levels.Count - 1)
                    s += "\n";
            }
            return s;
        }

        

    }
}


