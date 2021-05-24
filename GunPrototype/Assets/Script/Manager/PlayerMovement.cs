using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed;
    public Rigidbody2D rb;

    float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayers;

    [HideInInspector] public bool isFacingRight = true;

    float mx;

    public void Update() {
        mx = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            Jump();
        }

        if (mx > 0f) {
            isFacingRight = true;
        } else if (mx < 0f){
            isFacingRight = false;
        }
    }

    public void FixedUpdate() {
        Vector2 movement = new Vector2(mx * movementSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    void Jump() {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        rb.velocity = movement;
    }

    public bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position,0.3f,groundLayers);
        if (groundCheck != null) {
            return true; }
        return false;
    }

    public void Respawn() {
        transform.position = Vector3.zero;
    }

    public void Swap(GameObject Target) {
        Vector3 temp = transform.position;
        transform.position = Target.transform.position;
        Target.transform.position = temp;
    }

}
