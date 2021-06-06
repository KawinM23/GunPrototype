using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackController : MonoBehaviour {
    [SerializeField] private TimeManager tm;
    public bool isHacking = false;


    KeyCode[] hackList;
    int hackPos;


    // Start is called before the first frame update
    void Start() {
        isHacking = false;
        hackPos = 0;
    }

    // Update is called once per frame
    void Update() {
        if (isHacking) {
            if (hackPos < hackList.Length && Input.GetKeyDown(hackList[hackPos])) {
                Debug.Log("P "+hackList[hackPos]);
                hackPos++;
                if(hackPos == hackList.Length) {
                    endHack();
                } else {
                    Debug.Log(hackList[hackPos]);
                }
            } else if(hackPos < hackList.Length) {

            }
        }

    }

    public void StartHack(int size) {
        hackList = new KeyCode[] { KeyCode.W, KeyCode.D, KeyCode.W, KeyCode.S };
        
        isHacking = true;
        tm.DoSlowmotion();
        Debug.Log(hackList[hackPos]);
    }

    public void endHack() {
        Debug.Log("End");
        tm.hacking = false;
        isHacking = false;
        hackPos = 0;
    }

    public bool PressOtherKeys(KeyCode kc) {
        return false;
    }
}
