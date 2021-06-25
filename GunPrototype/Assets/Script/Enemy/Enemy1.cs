using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy {

    private new void Start() {
        base.Start();

        shield = new int[4] {maxHp/4,0,0,0};
        shieldPosition = new bool[4] { true,false,false,false};
        shieldPointer = 0;
    }

    private void Update() {
        
    }



}
