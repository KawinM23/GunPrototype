using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeTrigger : MonoBehaviour {
    GameObject enemy;

    // Start is called before the first frame update
    void Start() {
        enemy = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            enemy.GetComponent<Enemy1>().CheckSeePlayer();
        }
    }

}
