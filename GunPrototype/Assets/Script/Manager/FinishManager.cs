using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishManager : MonoBehaviour
{
    [SerializeField] private SaveSystem ss;
    [SerializeField] private Text backText;
    [SerializeField] private Text timeUsedText;

    private bool backable;

    private static string levelName;

    private static float timePass;
    private static float timeLimited;
    private bool inTime;

    private static bool allEnemiesDefeated;

    public float waitTime;


    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    [SerializeField] private GameObject border1;
    [SerializeField] private GameObject border2;
    [SerializeField] private GameObject border3;
    private Animator border1Animator;
    private Animator border2Animator;
    private Animator border3Animator;

    public Color star1Color;
    public Color star2Color;
    public Color star3Color;


    // Start is called before the first frame update
    void Start()
    {
        backable = false;
        backText.enabled = false;

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
        if (ss.dataList != null && levelName != null) {
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
        border1Animator = border1.GetComponent<Animator>();
        border2Animator = border2.GetComponent<Animator>();
        border3Animator = border3.GetComponent<Animator>();
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(ShowTime());
        yield return StartCoroutine(PlayStar(1));
        yield return StartCoroutine(PlayStar(2));
        yield return StartCoroutine(PlayStar(3));
        yield return new WaitForSeconds(0.5f);
        Back();
    }

    IEnumerator PlayStar(int star) {
        switch (star) {
            case 1:
                border1Animator.Play("Border1Animation");
                border1Animator.SetBool("Play", true);
                yield return new WaitForSeconds(1f);
                star1.GetComponent<SpriteRenderer>().color = star1Color;
                yield return new WaitForSeconds(0.5f);
                break;
            case 2:
                border2Animator.Play("Border2Animation");
                yield return new WaitForSeconds(1f);
                border2Animator.SetBool("Play", false);
                if (inTime) {
                    border2Animator.SetBool("inTime", true);
                    star2.GetComponent<SpriteRenderer>().color = star2Color;
                    yield return new WaitForSeconds(0.75f);
                } else {
                    border2Animator.SetBool("inTime", false);
                    yield return new WaitForSeconds(0.75f);
                }
                break;
            case 3:
                border3Animator.Play("Border3Animation");
                yield return new WaitForSeconds(1f);
                border3Animator.SetBool("Play", false);
                if (allEnemiesDefeated) {
                    border3Animator.SetBool("allEnemyDefeated", true);
                    star3.GetComponent<SpriteRenderer>().color = star3Color;
                    yield return new WaitForSeconds(0.5f);
                } else {
                    border3Animator.SetBool("allEnemyDefeated", false);
                    yield return new WaitForSeconds(0.75f);
                }
                break;
        }
        yield return null;
    }

    IEnumerator ShowTime() {
        timeUsedText.text = timePass.ToString("N2")+" / "+timeLimited.ToString();
        yield return new WaitForSeconds(0.5f);
    }
}
