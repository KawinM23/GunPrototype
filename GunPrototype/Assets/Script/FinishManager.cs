using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishManager : MonoBehaviour
{
    [SerializeField] private SaveSystem ss;
    [SerializeField] private Text backText;

    private bool backable;

    private static string levelName;

    private static float timePass;
    private static float timeLimited;
    private bool inTime;

    private static bool allEnemiesDefeated;

    public float waitTime;

    [SerializeField] private GameObject border1;
    [SerializeField] private GameObject border2;
    [SerializeField] private GameObject border3;

    // Start is called before the first frame update
    void Start()
    {
        border1.SetActive(false);
        border2.SetActive(false);
        border3.SetActive(false);
        backable = false;
        backText.enabled = false;
        
        Debug.Log(timePass + "/" + timeLimited);
        Debug.Log(allEnemiesDefeated);
        inTime = timePass <= timeLimited;

        SaveFinish();
        StartCoroutine(PlayStars());
    }

    // Update is called once per frame
    void Update()
    {
        if(backable && Input.GetKeyDown(KeyCode.Space)) {
            LevelManager.StaticBackToMenu();
        }
        
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

    private void Back() {
        backable = true;
        backText.enabled = true;
    }

    IEnumerator PlayStars() {
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(PlayStar(1));
        yield return StartCoroutine(PlayStar(2));
        yield return StartCoroutine(PlayStar(3));
        yield return new WaitForSeconds(0.5f);
        Back();
    }

    IEnumerator PlayStar(int star) {
        switch (star) {
            case 1:
                break;
            case 2:
                

                break;
            case 3:
                
                break;
        }
        yield return null;


    }
}
