using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawner : MonoBehaviour
{
    GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision != null && collision.gameObject.CompareTag("Player")) {
            PlayerController.Instance.getHit(200);
        }
    }
}
