using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    private new void Start() {
        base.Start();

        maxHp = 200;
        hp = maxHp;

        shield = new int[8] { 25, 0, 0, 0, 125, 0, 0, 0 };
        shieldPosition = new bool[8] { true, false, false, false, true, false, false, false };
        shieldPointer = 4;

        seeDis = 25;
        shootCooldown = 1.5f;
    }

    private void Update() {
        if (!TimeManager.isPause && player != null) {
            if (hackable && Input.GetKeyDown(KeyCode.Mouse1)) {
                player.GetComponent<HackController>().StartHack(this, 3, 0.12f);
            }
            CheckSeePlayer();
        }
    }
}
