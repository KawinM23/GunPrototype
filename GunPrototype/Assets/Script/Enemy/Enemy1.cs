using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy {



    private new void Start() {
        base.Start();

        shield = new int[4] {25,0,0,0};
        shieldPosition = new bool[4] { true,false,false,false};
        shieldPointer = 0;

        seeDis = 25;
    }

    private void Update() {
        if (!TimeManager.isPause && player != null) {
            if (hackable && Input.GetKeyDown(KeyCode.Mouse1)) {
                player.GetComponent<HackController>().StartEnemyHack(this, 4, 0.15f);
            }
            CheckSeePlayer();
        }
    }



}
