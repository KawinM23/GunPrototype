using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    private new void Start() {
        base.Start();

        shield = new int[8] { maxHp/8, 0, 0, 0, maxHp*5 / 8, 0, 0, 0 };
        shieldPosition = new bool[8] { true, false, false, false, true, false, false, false };
        shieldPointer = 4;

        shootCooldown = 1.5f;
        
    }

}
