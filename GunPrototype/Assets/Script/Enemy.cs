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
    protected bool[] shieldPosition;
    public int shieldPointer;

    protected bool hackable;

    protected float shootCooldown;
    protected float nextShootTime;

    protected int seeDis = 20;

    public LayerMask groundLayer;

    // Start is called before the first frame update
    protected void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        hp = maxHp;
        hackable = false;
        nextShootTime = Time.time + 2;
    }

    // Update is called once per frame
    void Update() {
        if (player != null) {
            if (hackable && Input.GetKeyDown(KeyCode.Mouse1)) {
                player.GetComponent<HackController>().StartHack(this, 4, 5f);
            }
            CheckSeePlayer();
        }
    }

    public float HealthPercentage() {
        return ((float)hp / (float)maxHp);
    }

    public void getHit(int damage) {
        if (shieldPointer != -1 && hp - damage <= shield[shieldPointer]) {

            hp = shield[shieldPointer];
            hackable = true;
        } else {
            hp -= damage;
            hackable = false;
        }

        if (hp < 0) {
            Destroy(gameObject);
        }
        enemyHealthbar.OnGetHit();

    }

    public bool isShield(float f) {
        if (shieldPointer <shieldPosition.Length) {
            return shieldPosition[(int)f];
        } else {
            return false;
        }
        
    }

    public void BreakShield() {
        shieldPointer--;
        if (shieldPointer > 0) {
            while (!shieldPosition[shieldPointer]) {
                shieldPointer++;
            }
            EndHack();
            Debug.Log(shieldPointer);
        } else {
            shieldPointer = -1;
        }
        enemyHealthbar.OnGetHit();
    }

    public void EndHack() {
        hackable = false;
        getHit(0);
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
