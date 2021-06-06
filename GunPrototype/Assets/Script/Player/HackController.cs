using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackController : MonoBehaviour {
    [SerializeField] private TimeManager tm;
    public bool isHacking = false;


    KeyCode[] randomList = new KeyCode[4] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    KeyCode[] hackList;
    int hackPos;
    int hackTime;


    // Start is called before the first frame update
    void Start() {
        isHacking = false;
        hackPos = 0;
        hackTime = 0;
    }

    // Update is called once per frame
    void Update() {
        if (isHacking) {
            Debug.Log("P" + hackTime + hackPos + " " + hackList[hackPos]);
            if (hackPos < hackList.Length && Input.GetKeyDown(hackList[hackPos])) {
                
                hackPos++;
                if (hackPos == hackList.Length) {
                    endHack();
                }
            } else if (hackPos < hackList.Length) {

            }
        }

    }

    public void StartHack(int size) {
        if (!isHacking) {
            hackList = new KeyCode[size];
            for(int i = 0; i < size; i++) {
                hackList[i] = randomList[Random.Range(0, randomList.Length)];
            }


            isHacking = true;
            tm.DoSlowmotion();
            Debug.Log(hackList[hackPos]);
        }
    }

    public void endHack() {
        Debug.Log("End");
        tm.hacking = false;
        isHacking = false;
        hackPos = 0;
        hackTime ++;
    }

    public bool PressOtherKeys(KeyCode kc) {
        return false;
    }
}
