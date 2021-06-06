using UnityEngine;

public class Bullet : MonoBehaviour {

    private GameObject player;

    public float bulletSpeed = 200f;
    public float bulletDamage = 10f;
    public float lifeTime = 5f;

    Vector3 dis;

    public Rigidbody2D rb;


    private void Start() {

        player = GameObject.Find("Player");
        rb.velocity = transform.right * bulletSpeed;
        Invoke("DestroyBullet", lifeTime);
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), player.transform.GetComponent<Collider2D>());
    }

    private void Update() {

    }

    private void FixedUpdate() {
        //transform.Translate(transform.up * bulletSpeed * Time.fixedDeltaTime);

    }

    void DestroyBullet() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision != null) {
            if (collision.gameObject.tag == "Bullet") {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(),true);
                return;
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision != null) {
            if (collision.gameObject.tag == "Bullet") {
                return;
            }
            if(collision.gameObject.CompareTag("Enemy")) {
                collision.gameObject.GetComponent<Enemy>().getHit(10);
            }
            Destroy(gameObject);
        }
    }



}
