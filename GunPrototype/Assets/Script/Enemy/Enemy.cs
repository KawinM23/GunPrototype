using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    protected GameObject player;
    [SerializeField] private EnemyHealthbar enemyHealthbar;
    [SerializeField] Transform[] pathPoints;
    private int pathPointer;
    public GameObject bulletPrefab;

    protected int hp;
    public int maxHp;
    protected int[] shield;
    protected bool[] shieldPosition;
    public int shieldPointer;

    protected bool hackable;

    public float speed;
    public float waitDuration;
    public int seeDis;
    public float shootCooldown;
    protected float nextShootTime;

    

    public LayerMask groundLayer;

    // Start is called before the first frame update
    protected void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        pathPointer = 0;

        hp = maxHp;
        hackable = false;
        nextShootTime = Time.time + 2;

        StartCoroutine(FollowPath());
    }

    // Update is called once per frame
    void Update() {
        if (!TimeManager.isPause && player != null) {
            if (hackable && Input.GetKeyDown(KeyCode.Mouse1)) {
                player.GetComponent<HackController>().StartHack(this, 4, 0.05f);
            }
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

        if (hp <= 0) {
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
                shieldPointer--;
            }
            EndHack();
        } else {
            shieldPointer = -1;
        }
        enemyHealthbar.OnGetHit();
    }

    public void EndHack() {
        hackable = false;
        getHit(0);
    }

    IEnumerator FollowPath() {
        if(pathPoints.Length != 0) {
            foreach(Transform transform in pathPoints) {
                transform.SetParent(GameObject.Find("Path").transform);
            }
        }
        
        while (pathPoints.Length != 0) {
            yield return StartCoroutine(MoveToNextPoint(pathPointer));
        }
    }

    IEnumerator MoveToNextPoint(int pointer) {
        Vector2 destination = new Vector2(pathPoints[pointer].position.x, transform.position.y);
        while(transform.position.x != destination.x) {
            if (!CheckSeePlayer()) {
                transform.position = Vector2.MoveTowards(transform.position, destination, speed*Time.deltaTime);
            }
            yield return null;
        }
        pathPointer++;
        if(pathPointer== pathPoints.Length) {
            pathPointer = 0;
        }
        yield return new WaitForSeconds(waitDuration);
    }

    public bool CheckSeePlayer() {

        Vector2 endPoint = transform.position + (player.transform.position - transform.position).normalized * seeDis;

        RaycastHit2D hit = Physics2D.Linecast(transform.position, endPoint, groundLayer);

        Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * seeDis, Color.red);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player")) {
            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
            ShootPlayer();
            return true;
        }
        return false;
    }
    public void ShootPlayer() {
        if (Time.time >= nextShootTime && bulletPrefab != null) {
            nextShootTime = Time.time + shootCooldown;
            GameObject bulletClone = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(Vector3.up));
            bulletClone.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * 20;
            Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), bulletClone.transform.GetComponent<Collider2D>());
        }
    }
}
