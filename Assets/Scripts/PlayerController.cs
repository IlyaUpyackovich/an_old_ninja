using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float speed = 40;
    public UnityEvent onLandEvent;
    public UnityEvent onFallEvent;

    public LayerMask whatIsGround;
    public Transform groundCheck;

    [SerializeField]
    private string direction = "right";

    [Range(0, .3f)] [SerializeField]
    private float movementSmoothing = .05f;

    [SerializeField]
    private float jumpForce = 400;
    private bool canJump = true;
    private bool isOnGround = true;

    private float horizontalInput = 0f;
    private float jumpInput = 0f;

    private float groundCheckRadius = 0.05f;
    private float limitFallSpeed = 25f;
    private float addedForce = 10f;
    private Vector3 velocity;

    private Animator animator;
    private Rigidbody2D rb2D;


    private void Awake()
    {
        if (onLandEvent == null)
            onLandEvent = new UnityEvent();

        if (onFallEvent == null)
            onFallEvent = new UnityEvent();

        onLandEvent.AddListener(onLandEventListener);
        onFallEvent.AddListener(onFallEventListener);
    }


    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxis("Jump");

        animator.SetFloat("HorizontalInput", Mathf.Abs(horizontalInput));
    }

    private void FixedUpdate()
    {
        bool wasOnGround = isOnGround;
        isOnGround = IsGrounded();

        if (!isOnGround)
            onFallEvent.Invoke();

        if (isOnGround && !wasOnGround)
            onLandEvent.Invoke();

        LimitVelocity();
        Move();

        if (isOnGround && canJump && jumpInput > 0f)
            Jump();

        if (horizontalInput > 0f && direction == "left")
            Flip("right");

        if (horizontalInput < 0f && direction == "right")
            Flip("left");
    }

    private void Move()
    {
        Vector3 targetVelocity = new Vector2(
            speed * addedForce * horizontalInput * Time.fixedDeltaTime,
            rb2D.velocity.y
        );

        rb2D.velocity = Vector3.SmoothDamp(
            rb2D.velocity,
            targetVelocity,
            ref velocity,
            movementSmoothing
        );
    }

    private void LimitVelocity()
    {
        float playerYVelocity = rb2D.velocity.y;

        if (playerYVelocity < -limitFallSpeed)
            playerYVelocity = -limitFallSpeed;

        rb2D.velocity = new Vector2(rb2D.velocity.x, playerYVelocity);
    }

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            groundCheck.position,
            groundCheckRadius,
            whatIsGround
        );


        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                return true;
        }

        return false;
    }

    private void Jump()
    {
        rb2D.velocity = Vector2.right * rb2D.velocity;
        rb2D.AddForce(Vector2.up * jumpForce);
    }

    private void onFallEventListener()
    {
        canJump = false;
        animator.SetBool("isJumping", true);
    }

    private void onLandEventListener()
    {
        canJump = true;
        animator.SetBool("isJumping", false);
    }

    private void Flip(string newDirection)
    {
        direction = newDirection;

        Vector3 playerScale = transform.localScale;
        playerScale.x = -playerScale.x;
        transform.localScale = playerScale;
    }
}
