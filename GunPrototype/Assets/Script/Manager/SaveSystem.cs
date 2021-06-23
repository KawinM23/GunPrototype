using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class SaveSystem : MonoBehaviour{

    public List<LevelData> dataList;
    private string s;
    private string[] seperator;
    private string[] loadedStrings;

    private void Awake() {
        dataList = new List<LevelData>();
        seperator = new string[] { "___" };

        //LevelData ld1 = new LevelData("Level1", true, new bool[] { true, true, true }, 5f);
        //LevelData ld2 = new LevelData("Level2", true, new bool[] { true, true, true }, 5f);

        //dataList.Add(ld1);
        //dataList.Add(ld2);

        Load();
        
    }

    public void Save() {

        s = "";
        
        for (int i = 0; i < dataList.Count; i++) {
            s += JsonUtility.ToJson(dataList[i]);
            if (i + 1 != dataList.Count) {
                s += seperator[0];
            }
        }

        File.WriteAllText(Application.dataPath + "/save.txt", s);

    }

    public void Load() {
        if (File.Exists(Application.dataPath + "/save.txt")) {
            string loadString = File.ReadAllText(Application.dataPath + "/save.txt");
            loadedStrings = loadString.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            foreach (string es in loadedStrings) {
                dataList.Add(JsonUtility.FromJson<LevelData>(es));
            }
        }

    }

}

[Serializable]
public class LevelData {
    [SerializeField]
    public string levelName;
    public bool completed;
    public bool[] stars;
    public float timeUsed;

    public LevelData(string levelName, bool completed, bool[] stars, float timeUsed) {
        this.levelName = levelName;
        this.completed = completed;
        this.stars = stars;
        this.timeUsed = timeUsed;
    }
}
