﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision != null & collision.CompareTag("Player")) {
            LevelManager.FinishLevel();
        }
    }
}
