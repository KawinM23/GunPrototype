using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour {
    int hp = 10;
    GameObject player;
    float seeDis = 10;

    public GameObject bulletPrefab;


    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
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
        Debug.Log(this.name + "shootplayer ");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position, seeDis);
        if (hit.collider != null) {
            if (hit.collider.gameObject.CompareTag("Player")) {
                ShootPlayer();
            }
        }

    }
    public void ShootPlayer() {
        GameObject bulletClone = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(Vector3.up));

    }
}
