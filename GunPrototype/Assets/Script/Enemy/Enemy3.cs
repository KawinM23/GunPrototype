using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{
    private new void Start() {
        base.Start();

        shield = new int[5] { 0, 0, 0, 0 ,maxHp};
        shieldPosition = new bool[5] { false, false, false, false, true };
        shieldPointer = 4;

    }

    // Update is called once per frame
    void Update()
    {
        if (SeePlayer()) {
            ShootPlayer();
        }
    }
}
