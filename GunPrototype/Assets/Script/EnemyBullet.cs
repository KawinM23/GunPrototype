using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public Rigidbody2D rb;
    int bulletSpeed = 20;

    // Start is called before the first frame update
    void Start() {
        rb.velocity = Vector2.right * bulletSpeed;
    }

    // Update is called once per frame
    void Update() {

    }
}
