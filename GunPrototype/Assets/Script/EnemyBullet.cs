using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public Rigidbody2D rb;
    static int bulletSpeed = 20;

    // Start is called before the first frame update
    void Start() {
        Invoke("TimeOut", 5);
    }

    // Update is called once per frame
    void Update() {

    }

    void TimeOut() {
        GameObject.Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision != null) {

        }
    }


}
