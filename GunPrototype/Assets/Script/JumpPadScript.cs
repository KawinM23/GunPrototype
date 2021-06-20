using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour
{
    PlayerMovement pm;

    private void Start() {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision != null && collision.CompareTag("Player")) {
            pm.jumpCount = 0;
        }
    }
}
