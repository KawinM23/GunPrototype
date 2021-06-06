using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    [SerializeField] private Rigidbody2D rb;



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
            if (collision.gameObject.tag == "Bullet") {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(),true);
                return;
            }
            if (collision.gameObject.tag == "Player") {
                PlayerController.getHit(20);
            }
            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision != null) {
            if (collision.gameObject.tag == "Bullet") {
                return;
            }
            if (collision.gameObject.tag == "Player") {
                PlayerController.getHit(20);
            }
            Destroy(gameObject);

        }
    }


}
