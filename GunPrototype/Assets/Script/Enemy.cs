using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private GameObject player;
    [SerializeField] private EnemyHealthbar enemyHealthbar;
    public GameObject bulletPrefab;

    protected int hp;
    protected int maxHp = 50;
    protected int[] shield;
    protected int shieldPointer;

    protected bool hackable;

    float shootCooldown = 4;
    float nextShootTime;

    int seeDis = 10;

    public LayerMask groundLayer;

    // Start is called before the first frame update
    protected void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        hp = maxHp;
        hackable = false;
        nextShootTime = Time.time + shootCooldown;
    }

    // Update is called once per frame
    void Update() {
        if (hp <= 0) {
            Destroy(gameObject);
        }
        CheckSeePlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            Debug.Log("Hit");
            if (hp > 0) { hp -= 3; }
            //Destroy(gameObject);
        }
    }

    public float HealthPercentage() {
        return ((float)hp / (float)maxHp);
    }

    public void getHit(int damage) {
        if (hp - damage < shield[shieldPointer]) {
            hp = shield[shieldPointer];
            hackable = true;
        } else {
            hp -= damage;
        }
        Debug.Log("HP " + hp);

    }

    public void CheckSeePlayer() {

        Vector2 endPoint = transform.position + (player.transform.position - transform.position).normalized * seeDis;

        RaycastHit2D hit = Physics2D.Linecast(transform.position, endPoint, groundLayer);

        Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * seeDis, Color.red);

        if (hit.collider != null) {
            if (hit.collider.gameObject.CompareTag("Player")) {
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
                ShootPlayer();
            }
        }
    }
    public void ShootPlayer() {
        if (Time.time >= nextShootTime && bulletPrefab != null) {

            Debug.Log(this.name + " shoot player.");
            nextShootTime = Time.time + shootCooldown;
            GameObject bulletClone = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(Vector3.up));
            bulletClone.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * 20;
            Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), bulletClone.transform.GetComponent<Collider2D>());
        }
    }
}
