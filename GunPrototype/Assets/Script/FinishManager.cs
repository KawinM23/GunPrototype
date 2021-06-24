using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishManager : MonoBehaviour
{
    [SerializeField] private SaveSystem ss;

    private static string levelName;

    private static float timePass;
    private static float timeLimited;
    private bool inTime;

    private static bool allEnemiesDefeated;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine();
        Debug.Log(timePass + "/" + timeLimited);
        Debug.Log(allEnemiesDefeated);
        inTime = timePass <= timeLimited;

        SaveFinish();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void FinishData(string lN,float tP, float tL,bool ed) {
        levelName = lN;
        timePass = tP;
        timeLimited = tL;
        allEnemiesDefeated = ed;
    }

    private void SaveFinish() {
        if (ss.dataList != null) {
            foreach (LevelData ld in ss.dataList) {
                if (levelName.Equals(ld.levelName)) {
                    ld.completed = true;
                    if (timePass < ld.timeUsed || ld.timeUsed == 0) {
                        ld.timeUsed = timePass;
                    }

                    ld.SetStars(inTime, allEnemiesDefeated);
                }
            }
            ss.Save();
        } else {
            Debug.LogWarning("No Data");
        }

        
    }
}
