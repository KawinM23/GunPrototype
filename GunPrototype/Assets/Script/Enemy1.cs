﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy {



    private new void Start() {
        base.Start();

        maxHp = 100;
        hp = maxHp;

        shield = new int[4] {25,0,0,0};
        shieldPosition = new bool[4] { true,false,false,false};
        shieldPointer = 0;

        seeDis = 25;
        shootCooldown = 1.5f;
    }

    private void Update() {
        if (!TimeManager.isPause && player != null) {
            if (hackable && Input.GetKeyDown(KeyCode.Mouse1)) {
                player.GetComponent<HackController>().StartHack(this, 4, 0.15f);
            }
            CheckSeePlayer();
        }
    }



}
