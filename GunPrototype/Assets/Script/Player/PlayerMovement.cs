using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour {
    public GameObject CM;
    public HackController hc;

    public float movementSpeed;
    private Rigidbody2D rb;

    public float jumpForce = 10f;
    public bool isJumping;
    public byte jumpCount = 0;
    private float jumpTimeCounter;
    public float jumpTime;
    public Transform feet;
    public LayerMask groundLayers;
    int groundLayerMask ;

    [HideInInspector] public bool isFacingRight = true;


    float mx;

    public void Start() {
        rb = GetComponent<Rigidbody2D>();
        hc = GetComponent<HackController>();
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Enemy") | 1 << LayerMask.NameToLayer("Platform");
        
    }

    public void Update() {
        mx = Input.GetAxisRaw("Horizontal");

        if (mx > 0f) {
            isFacingRight = true;
        } else if (mx < 0f) {
            isFacingRight = false;
        }

        Jump();


        if (Input.GetKeyDown(KeyCode.F)) {
            //CM.GetComponent<CinemachineVirtualCamera>().m_Follow = GameObject.Find("Enemy1").transform;
            hc.StartHack(4);
        }



    }

    public void FixedUpdate() {
        if (!hc.isHacking) {
            Vector2 movement = new Vector2(mx * movementSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
        if (!isJumping) {
            CheckGrounded();
        }
    }

    void Jump() {

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1) {
            jumpCount++;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping) {
            if (jumpTimeCounter>=0) {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * 0.9f);
                jumpTimeCounter -= Time.deltaTime;

            } else {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && isJumping) {
            isJumping = false;
        }

    }

    public bool CheckGrounded() {
        //if (feet.GetComponent<GroundCheck>().isGrounded) {
        //    jumpCount = 0;
        //    return true;
        //} else {
        //    return false;
        //}
        Collider2D groundCheck;
        groundCheck = Physics2D.OverlapBox(feet.position, new Vector2(0.8f, 0.05f), 0f, groundLayerMask);

        if (groundCheck != null) {
            Debug.Log("OnGround "+groundCheck);
            jumpCount = 0;
            return true;
        } else {
            return false;
        }
    }

    public void ResetJumping() {
        jumpCount = 0;
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
