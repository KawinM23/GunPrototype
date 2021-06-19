using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackController : MonoBehaviour {
    private TimeManager tm;

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

        isHacking = false;
        hackPos = 0;
        hackTime = 0;
        hackDuration = 0;
    }

    // Update is called once per frame
    void Update() {
        if (!TimeManager.isPause && isHacking) {
            hackTimePass += Time.deltaTime;
            Debug.Log("P" + hackTime + hackPos + " " + hackList[hackPos]);
            if (hackPos < hackList.Length && Input.GetKeyDown(hackList[hackPos])) {
                
                hackPos++;
                if (hackPos == hackList.Length) {
                    HackSuccess();
                }
            }
            if (hackTimePass > hackDuration) {
                Debug.Log("EndTime");
                EndHack();
            }
        }

    }

    public void StartHack(Enemy enemy,int size,float duration) {
        if (!isHacking) {
            isHacking = true;
            targetEnemy = enemy;

            hackList = RandomList(size);
            tm.DoSlowmotion();
            Debug.Log(hackList[hackPos]);

            hackDuration = duration;
            hackTimePass = 0f;

            Debug.Log("StartHack");
        }
    }

    public void HackSuccess() {
        if (isHacking) {
            Debug.Log("Success");
            tm.hacking = false;
            isHacking = false;
            hackPos = 0;
            hackTime++;
            targetEnemy.BreakShield();
            targetEnemy.EndHack();
        }
    }

    public void EndHack() {
        if (isHacking) {
            Debug.Log("End");
            tm.hacking = false;
            isHacking = false;
            hackPos = 0;
            hackTime++;
            targetEnemy.EndHack();
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
}
