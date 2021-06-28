using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private HackController hc;
    private BoxCollider2D bc;
    private Transform manager;
    [SerializeField] private GameObject jp;

    public float movementSpeed;
    
    public float jumpForce = 10f;
    public bool isJumping;
    public byte jumpCount = 0;
    private float jumpTimeCounter;
    public float jumpTime;
    public Transform feet;
    public LayerMask groundLayers;
    private int groundLayerMask;

    [HideInInspector] public bool isFacingRight;
    [HideInInspector] public bool startingMoving;
    [HideInInspector] public bool die;

    private float mx;

    public void Start() {
        rb = GetComponent<Rigidbody2D>();
        hc = GetComponent<HackController>();
        bc = GetComponent<BoxCollider2D>();
        manager = GameObject.Find("Manager").transform;
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform") | 1 << LayerMask.NameToLayer("Enemy");

        isFacingRight = true;
        die = false;
    }

    public void Update() {
        if (!TimeManager.isPause && !die) {
            mx = Input.GetAxisRaw("Horizontal");

            if (mx > 0f) {
                isFacingRight = true;
            } else if (mx < 0f) {
                isFacingRight = false;
            }

            if (startingMoving) { 
                Jump();
                JumpDown();
            }
            CheckStartMoving();
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

    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2) {
            if (jumpCount == 1) {
                PlayJumpParticle();
            }
            jumpCount++;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping) {
            if (jumpTimeCounter >= 0) {
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

    private void JumpDown() {
        if (Input.GetKey(KeyCode.S)) {
            Collider2D platformCheck;
            Collider2D otherCheck;
            platformCheck = Physics2D.OverlapBox(feet.position, new Vector2(0.8f, 0.01f), 0f, 1 << LayerMask.NameToLayer("Platform"));
            otherCheck = Physics2D.OverlapBox(feet.position, new Vector2(0.8f, 0.01f), 0f, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Enemy"));

            if (platformCheck != null && otherCheck == null && !bc.isTrigger) {
                bc.isTrigger = true;
            }
        }
    }

    public void CheckStartMoving() {
        if (!startingMoving && Input.anyKeyDown && !Input.GetKey(KeyCode.Tab) && !Input.GetKey(KeyCode.Escape)) {
            startingMoving = true;
            GameObject.Find("Manager").GetComponent<TimeManager>().StartMoving();
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

    public void PlayJumpParticle() {
        GameObject newJp = Instantiate(jp, manager, true);
        newJp.GetComponent<ParticleSystem>().Play();
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

    public void Die() {
        die = true;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision != null && collision.gameObject.CompareTag("Platform")) {
            bc.isTrigger = false;
        }
    }
}