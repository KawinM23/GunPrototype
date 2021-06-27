using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    protected GameObject player;
    protected HackController hc;
    [SerializeField] private EnemyHealthbar enemyHealthbar;
    [SerializeField] Transform[] pathPoints;
    private int pathPointer;
    public GameObject bulletPrefab;

    protected int hp;
    public int maxHp;
    protected int[] shield;
    protected bool[] shieldPosition;
    [HideInInspector]public int shieldPointer;

    protected bool hackable;
    public int hackSize;
    public float hackTime;

    public float speed;
    public float waitDuration;
    public int seeDis;
    public float shootCooldown;
    protected float nextShootTime;

    public int bulletDamage;
    public int bulletSpeed;

    public LayerMask groundLayer;

    // Start is called before the first frame update
    protected void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        hc = player.GetComponent<HackController>();
        pathPointer = 0;

        hp = maxHp;
        hackable = false;
        nextShootTime = Time.time + 2;
    }


    public float HealthPercentage() {
        return ((float)hp / (float)maxHp);
    }

    public void getHit(int damage) {

        if (shieldPointer != -1 && hp - damage <= shield[shieldPointer] && !hackable) {
            hp = shield[shieldPointer];
            hackable = true;
            hc.StartCoroutine(hc.AddToHackableList(this.gameObject));
        } else if(!hackable){
            hp -= damage;
        }

        if (hp <= 0) {
            hc.RemoveFromHackableList(this.gameObject);
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
        EndHack();
        shieldPointer--;

        if (shieldPointer > 0) {
            while (shieldPointer >= 0 && !shieldPosition[shieldPointer]) {
                shieldPointer--;
            }
        } else {
            shieldPointer = -1;
        }
        enemyHealthbar.OnGetHit();
    }

    public void StartHack() {
        if (!TimeManager.isPause && player != null) {
                player.GetComponent<HackController>().StartEnemyHack(this, hackSize, hackTime);

        }
    }

    public void EndHack() {
        hackable = false;
        hc.RemoveFromHackableList(this.gameObject);
    }

    public IEnumerator FollowPath() {
        if(pathPoints.Length != 0) {
            foreach(Transform transform in pathPoints) {
                transform.SetParent(GameObject.Find("Path").transform);
            }
            while (pathPoints.Length != 0) {
                yield return StartCoroutine(MoveToNextPoint(pathPointer));
            }
        } else {
            while (true) {
                CheckSeePlayer();
                
                yield return null;
            }
        }
        
        
    }

    IEnumerator MoveToNextPoint(int pointer) {
        Vector2 destination = new Vector2(pathPoints[pointer].position.x, transform.position.y);
        while(transform.position.x != destination.x) {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
        pathPointer++;
        if(pathPointer== pathPoints.Length) {
            pathPointer = 0;
        }
        yield return new WaitForSeconds(waitDuration);
    }

    public bool SeePlayer() {

        Vector2 endPoint = transform.position + (player.transform.position - transform.position).normalized * seeDis;

        RaycastHit2D hit = Physics2D.Linecast(transform.position, endPoint, groundLayer);

        Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * seeDis, Color.red);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player")) {
            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
            return true;
        }
        return false;
    }
    public void ShootPlayer() {
        if (Time.time >= nextShootTime && bulletPrefab != null) {
            nextShootTime = Time.time + shootCooldown;
            GameObject bulletClone = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(Vector3.up));
            bulletClone.GetComponent<EnemyBullet>().SetBulletDamage(bulletDamage);
            bulletClone.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * bulletSpeed;
            
            //Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), bulletClone.transform.GetComponent<Collider2D>());
        }
    }

    public void CheckSeePlayer() {
        if (SeePlayer()) {
            ShootPlayer();
        }
    }
}
