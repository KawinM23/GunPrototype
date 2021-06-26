using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject player;
    private HackController hc;

    protected bool hackable;
    public int hackSize;
    public float hackTime;

    private void Start() {
        player = GameObject.Find("Player");
        hc = player.GetComponent<HackController>();

        hc.AddToHackableList(this.gameObject);
    }

    private void Update() {
        
    }

    public void StartHack() {
        if (!TimeManager.isPause && player != null) {
            hc.StartDoorHack(this, hackSize, hackTime);
        }
    }

    public void EndHack() {

    }

    public void OpenDoor() {
        hackable = false;
        hc.RemoveFromHackableList(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
