using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour {
    public GameObject CM;

    public float movementSpeed;
    private Rigidbody2D rb;

    float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayers;
    int groundLayerMask ;

    [HideInInspector] public bool isFacingRight = true;
    public byte jumping = 0;

    float mx;

    public void Start() {
        rb = GetComponent<Rigidbody2D>();
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Enemy");
        
    }

    public void Update() {
        mx = Input.GetAxisRaw("Horizontal");

        if (mx > 0f) {
            isFacingRight = true;
        } else if (mx < 0f) {
            isFacingRight = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumping < 1) {
            Jump();
            Debug.Log(jumping);
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            CM.GetComponent<CinemachineVirtualCamera>().m_Follow = GameObject.Find("Enemy1").transform;
        }



    }

    public void FixedUpdate() {
        Vector2 movement = new Vector2(mx * movementSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    void Jump() {
        jumping++;
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        rb.velocity = movement;
    }

    public bool CheckGrounded() {
        if (feet.GetComponent<GroundCheck>().isGrounded) {
            jumping = 0;
            return true;
        } else {
            return false;
        }
        //Collider2D groundCheck;
        //groundCheck = Physics2D.OverlapBox(feet.position, new Vector2(0.8f, 0.05f), 0f, groundLayerMask);

        //if (groundCheck != null) {
        //    Debug.Log("OnGround "+groundCheck);
        //    jumping = 0;
        //    return true;
        //} else {
        //    return false;
        //}
    }

    public void ResetJumping() {
        jumping = 0;
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
