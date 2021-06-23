using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    private bool onPortal;

    public int level;

    private void Update() {
        if (onPortal && Input.GetKeyDown(KeyCode.Mouse1)) {
            LevelManager.LoadLevel(level);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision != null && collision.gameObject.CompareTag("Player")) {
            onPortal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision != null && collision.gameObject.CompareTag("Player")) {
            onPortal = false;
        }
    }
}
