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

    public Color star1Color;
    public Color star2Color;
    public Color star3Color;

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
                border1.SetActive(true);
                Transform border1Transform = border1.GetComponent<Transform>();
                Vector3 localScaleChange1 = new Vector3(0.14f,0.14f,0);
                border1Transform.localScale = new Vector3(0.3f, 0.3f, 0);
                Vector3 deltaScale1 = border1Transform.localScale - localScaleChange1;

                Vector3 deltaRotation1 = new Vector3(0, 0, 180);
                SpriteRenderer sr1 = border1.GetComponent<SpriteRenderer>();
                sr1.color = new Color(sr1.color.r, sr1.color.g,sr1.color.b,0.1f);
                while (border1Transform.transform.localScale.x >= localScaleChange1.x) {
                    border1Transform.localScale -= deltaScale1 * Time.deltaTime;
                    border1Transform.Rotate(-deltaRotation1 * Time.deltaTime);
                    sr1.color += new Color(0, 0, 0, 0.9f) * Time.deltaTime;

                    yield return null;
                }
                border1Transform.rotation = new Quaternion(1, 0, 0, 0);
                border1Transform.localScale = localScaleChange1;
                sr1.color = new Color(sr1.color.r, sr1.color.g, sr1.color.b, 1f);
                break;
            case 2:
                if (allEnemiesDefeated) {
                    border2.SetActive(true);
                    Transform border2Transform = border2.GetComponent<Transform>();
                    Vector3 localScaleChange2 = border2Transform.localScale;
                    border2Transform.localScale = new Vector3(0.3f, 0.3f, 0);
                    Vector3 deltaScale2 = border2Transform.localScale - localScaleChange2;

                    Vector3 deltaRotation2 = new Vector3(0, 0, 180);
                    SpriteRenderer sr2 = border2.GetComponent<SpriteRenderer>();
                    sr2.color = new Color(sr2.color.r, sr2.color.g, sr2.color.b, 0.1f);
                    while (border2Transform.transform.localScale.x >= localScaleChange2.x) {
                        border2Transform.localScale -= deltaScale2 * Time.deltaTime;
                        border2Transform.Rotate(-deltaRotation2 * Time.deltaTime);
                        sr2.color += new Color(0, 0, 0, 0.9f) * Time.deltaTime;

                        yield return null;
                    }
                    border2Transform.rotation = new Quaternion(1, 0, 0, 0);
                    border2Transform.localScale = localScaleChange2;
                    sr2.color = new Color(sr2.color.r, sr2.color.g, sr2.color.b, 1f);

                } else {

                }
                break;
            case 3:
                if (inTime) {
                    border3.SetActive(true);
                    Transform border3Transform = border3.GetComponent<Transform>();
                    Vector3 localScaleChange3 = border3Transform.localScale;
                    border3Transform.localScale = new Vector3(0.3f, 0.3f, 0);
                    Vector3 deltaScale3 = border3Transform.localScale - localScaleChange3;

                    Vector3 deltaRotation3 = new Vector3(0, 0, 180);
                    SpriteRenderer sr3 = border3.GetComponent<SpriteRenderer>();
                    sr3.color = new Color(sr3.color.r, sr3.color.g, sr3.color.b, 0.1f);
                    while (border3Transform.transform.localScale.x >= localScaleChange3.x) {
                        border3Transform.localScale -= deltaScale3 * Time.deltaTime;
                        border3Transform.Rotate(-deltaRotation3 * Time.deltaTime);
                        sr3.color += new Color(0, 0, 0, 0.9f) * Time.deltaTime;

                        yield return null;
                    }
                    border3Transform.rotation = new Quaternion(1, 0, 0, 0);
                    border3Transform.localScale = localScaleChange3;
                    sr3.color = new Color(sr3.color.r, sr3.color.g, sr3.color.b, 1f);

                } else {

                }
                break;
        }


    }
}
