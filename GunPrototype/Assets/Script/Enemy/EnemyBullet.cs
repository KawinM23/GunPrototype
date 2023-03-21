using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    [SerializeField] private Rigidbody2D rb;
    private HackController hc;

    public int bulletDamage;

    // Start is called before the first frame update
    void Start() {
        Invoke("TimeOut", 5);
        hc = GameObject.Find("Player").GetComponent<HackController>();
    }

    // Update is called once per frame
    void Update() {

    }

    void TimeOut() {
        Destroy(gameObject);
    }

    //private void OnCollisionEnter2D(Collision2D collision) {
    //    if (collision != null) {
    //        if (collision.gameObject.tag == "Bullet") {
    //            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
    //            return;
    //        }
    //        if (collision.gameObject.tag == "Player") {
    //            PlayerController.getHit(bulletDamage);
    //        }
    //        Destroy(gameObject);

    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision != null) {
            if (collision.gameObject.CompareTag("Enemy")) {
                return;
            }
            if (collision.gameObject.CompareTag("Player")) {
                if (!hc.isHacking) {
                    PlayerController.Instance.getHit(bulletDamage);
                    Destroy(gameObject);
                    return;
                } else {
                    return;
                }
            }
            Destroy(gameObject);

        }
    }

    public void SetBulletDamage(int bulletDamage) {
        this.bulletDamage = bulletDamage;
    }


}
