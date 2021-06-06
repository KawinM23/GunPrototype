using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 200f;
    public float bulletDamage = 10f;
    public float lifeTime = 5f;

    Vector3 dis;

    public Rigidbody2D rb;
    GameObject player;

    private void Start() {

        player = GameObject.FindGameObjectWithTag("Player");
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
        Destroy(gameObject);
    }



}
