using UnityEngine;

public class Bullet : MonoBehaviour {
    public Camera cm;

    public float bulletSpeed = 200f;
    public float bulletDamage = 10f;
    public float lifeTime = 3f;

    Vector3 dis;
    
    public Rigidbody2D rb;
    GameObject player;

    private void Start() {
        Cam();

        player = GameObject.FindGameObjectWithTag("Player");
        rb.velocity = transform.right * bulletSpeed;
        Invoke("DestroyBullet", lifeTime);
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(),player.transform.GetComponent<Collider2D>());
    }

    private void Update() {
        if((transform.position - cm.transform.position).magnitude > dis.magnitude/2) {
            Debug.Log((transform.position - cm.transform.position).magnitude);
            Debug.Log(dis.magnitude);
            Destroy(gameObject);
        }
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

    public void Cam() {
        cm = Camera.main;
        float height = 2f * cm.orthographicSize;
        float width = height * cm.aspect;
        dis = new Vector3(width, height, 0);
    }


}
