using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    GameObject player;
    public float bulletSpeed = 20f;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), player.transform.GetComponent<Collider2D>());
        rb.velocity = transform.right * bulletSpeed;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Debug.Log("Hit");
            //Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(),collision.transform.GetComponent<Collider2D>());
            //Physics2D.IgnoreCollision(collider,transform.GetComponent<Collider2D>());
            //Physics2D.IgnoreLayerCollision(11, 10);
            player.GetComponent<PlayerMovement>().Swap(collision.gameObject);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Player")) {
            
        } else {
            Destroy(gameObject);
        }
    }

}
