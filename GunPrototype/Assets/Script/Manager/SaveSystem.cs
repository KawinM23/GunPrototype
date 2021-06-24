using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


public class SaveSystem : MonoBehaviour {

    public List<LevelData> dataList;
    private string s;
    private string[] seperator = new string[] { "___" };
    private string[] loadedStrings;

    private void Awake() {

        //LevelData ld1 = new LevelData("Level1", true, new bool[] { true, true, true }, 5f);
        //LevelData ld2 = new LevelData("Level2", true, new bool[] { true, true, true }, 5f);

        //dataList.Add(ld1);
        //dataList.Add(ld2);
    }

    private void Start() {
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
        Debug.Log("Save");
    }

    public void ResetSave() {
        dataList = new List<LevelData>();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            string name = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            if (name.Contains("Level")) {
                LevelData newLevelData = new LevelData(name, false, new bool[] { false, false, false }, 0);
                dataList.Add(newLevelData);
            }
        }
        Save();

    }


    public void Load() {
        dataList = new List<LevelData>();

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

    public void SetStars(bool inTime,bool allEnemiesDefeated) {
        stars[0] = true;
         if(inTime && allEnemiesDefeated) {
            stars = new bool[] { true, true, true };
            return;
        }
        if(stars[1] || stars[2]) {
            return;
        } else {
            stars[1] = inTime;
            stars[2] = allEnemiesDefeated;
            return;
        }
        
    }
}
