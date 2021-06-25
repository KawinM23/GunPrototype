using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Camera mainCemera;
    private GameObject player;

    Vector3 screenPoint;
    bool inScreen;
    bool inList;

    protected bool hackable;
    public int hackSize;
    public float hackTime;

    private void Start() {
        mainCemera = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player");

    }

    private void Update() {
        screenPoint = mainCemera.WorldToViewportPoint(transform.position);
        inScreen = screenPoint.z > -10 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if(!inList && inScreen) {
            player.GetComponent<HackController>().AddToHackableList(this.gameObject);
            inList = true;
        } else if(inList && !inScreen) {
            player.GetComponent<HackController>().RemoveFromHackableList(this.gameObject);
            inList = false;
        }
    }

    public void StartHack() {
        if (!TimeManager.isPause && player != null) {
            player.GetComponent<HackController>().StartDoorHack(this, hackSize, hackTime);
        }
    }

    public void EndHack() {

    }

    public void OpenDoor() {
        this.gameObject.SetActive(false);
    }
}
