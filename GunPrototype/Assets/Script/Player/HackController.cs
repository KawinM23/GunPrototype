﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackController : MonoBehaviour {
    private TimeManager tm;
    private HackInterface hi;
    private HackTimer ht;

    public bool isHacking = false;


    static KeyCode[] randomList = new KeyCode[4] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    KeyCode[] hackList;
    int hackPos;
    int hackTime;
    float hackDuration;
    float hackTimePass;

    Enemy targetEnemy; 


    // Start is called before the first frame update
    void Start() {
        tm = GameObject.Find("Manager").GetComponent<TimeManager>();
        hi = GameObject.Find("HackInterface").GetComponent<HackInterface>();
        ht = GameObject.Find("HackTimer").GetComponent<HackTimer>();

        hi.HideInterface();
        ht.HideTimer();

        isHacking = false;
        hackPos = 0;
        hackTime = 0;
        hackDuration = 0;
    }

    // Update is called once per frame
    void Update() {
        if (!TimeManager.isPause && isHacking) {
            hackTimePass += Time.deltaTime;
            if (hackPos < hackList.Length && Input.GetKeyDown(hackList[hackPos])) {
                hi.Press(hackPos);
                hackPos++;
                if (hackPos == hackList.Length) {
                    HackSuccess();
                }
            }
            if (hackTimePass > hackDuration) {
                Debug.Log("EndTime");
                HackFailed();
            }
        }

    }

    public void StartHack(Enemy enemy,int size,float duration) {
        if (!isHacking) {
            isHacking = true;
            targetEnemy = enemy;

            hackList = RandomList(size);
            
            hackDuration = duration;
            hackTimePass = 0f;

            hi.ShowHackList(hackList);
            ht.ShowTimer();
            tm.DoSlowmotion();
        }
    }

    public void HackSuccess() {
        if (isHacking) {
            Debug.Log("Success");
            EndHack();
            targetEnemy.BreakShield();
        }
    }

    public void HackFailed() {
        if (isHacking) {
            Debug.Log("Fail");
            EndHack();
        }
    }

    public void EndHack() {
        if (isHacking) {
            tm.hacking = false;
            isHacking = false;
            hackPos = 0;
            hackTime++;
            targetEnemy.EndHack();
            hi.HideInterface();
            ht.HideTimer();
        }
    }

    public KeyCode[] RandomList(int size) {
        KeyCode[] rl = new KeyCode[size];
        for (int i = 0; i < size; i++) {
            rl[i] = randomList[Random.Range(0, randomList.Length)];
        }
        return rl;
    }

    public bool PressOtherKeys(KeyCode kc) {
        return false;
    }
    
    public int GetKeyNumber(int index) {
        KeyCode kc = hackList[index];
        if (kc == KeyCode.W) {
            return 1;
        } else if (kc == KeyCode.A) {
            return 2;
        } else if (kc == KeyCode.S) {
            return 3;
        } else if (kc == KeyCode.D) {
            return 4;
        } else {
            return 0;
        }
    }

    public float GetTimePercentage() {
        return (hackDuration - hackTimePass) / hackDuration;
    }
}
