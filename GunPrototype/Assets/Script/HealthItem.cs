using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] PolygonCollider2D itemCollider;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision != null && collision.CompareTag("Player")) {
            collision.GetComponent<PlayerController>().SetFullHealth();
            Destroy(gameObject);
        }
    }
}
