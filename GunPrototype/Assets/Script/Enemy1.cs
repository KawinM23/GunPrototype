using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour {

    GameObject player;

    int hp = 10;
    float shootCooldown = 4;
    float nextShootTime;

    int seeDis = 10;

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        nextShootTime = Time.time + shootCooldown;
    }

    // Update is called once per frame
    void Update() {
        if (hp <= 0) {
            Destroy(gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            if (hp > 0) { hp -= 3; }
            //Destroy(gameObject);
        }
    }

    public void CheckSeePlayer() {
        Debug.Log(this.name + " check");

        Vector2 endPoint = (player.transform.position - transform.position).normalized * 1000;
        RaycastHit2D hit = Physics2D.Linecast(transform.position, endPoint, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);

        if (hit.collider != null) {
            if (hit.collider.gameObject.CompareTag("Player")) {
                ShootPlayer();
            }
        }
    }
    public void ShootPlayer() {
        Debug.Log(this.name + " shoot player.");
        if (Time.time >= nextShootTime && bulletPrefab != null) {
            Debug.Log("INST");
            nextShootTime = Time.time + shootCooldown;
            GameObject bulletClone = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(Vector3.up));
            bulletClone.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * 20;
        }

    }
}
