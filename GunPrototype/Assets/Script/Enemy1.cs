using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy {



    private new void Start() {
        base.Start();
        maxHp = 100;
        hp = maxHp;
        shield = new int[1] { 25 };
        shieldPointer = 0;
    }



}
