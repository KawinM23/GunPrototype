using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private HackController hc;
    private BoxCollider2D bc;

    public float movementSpeed;
    

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
        bc = GetComponent<BoxCollider2D>();
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform") | 1 << LayerMask.NameToLayer("Enemy");
        
    }

    public void Update() {
        mx = Input.GetAxisRaw("Horizontal");

        if (mx > 0f) {
            isFacingRight = true;
        } else if (mx < 0f) {
            isFacingRight = false;
        }

        Jump();
        JumpDown();


        if (Input.GetKeyDown(KeyCode.F)) {
            //CM.GetComponent<CinemachineVirtualCamera>().m_Follow = GameObject.Find("Enemy1").transform;

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
    void JumpDown() {

        Collider2D platformCheck;
        Collider2D otherCheck;
        platformCheck = Physics2D.OverlapBox(feet.position, new Vector2(0.8f, 0.01f), 0f, 1 << LayerMask.NameToLayer("Platform"));
        otherCheck = Physics2D.OverlapBox(feet.position, new Vector2(0.8f, 0.01f), 0f, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Enemy"));

        if (Input.GetKey(KeyCode.S) && platformCheck != null && otherCheck == null && !bc.isTrigger) {
            bc.isTrigger = true;
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
        groundCheck = Physics2D.OverlapBox(feet.position, new Vector2(0.8f, 0.01f), 0f, groundLayerMask);

        if (groundCheck != null) {
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

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision != null && collision.gameObject.CompareTag("Platform")) {
            bc.isTrigger = false;
        }
    }

}
