using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackController : MonoBehaviour {
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
        hackList = new KeyCode[] { KeyCode.W, KeyCode.W, KeyCode.W, KeyCode.W };

        isHacking = true;
        Debug.Log(hackList[hackPos]);
    }

    public void endHack() {
        Debug.Log("End");
        isHacking = false;
    }

    public bool PressOtherKeys(KeyCode kc) {
        return false;
    }
}
