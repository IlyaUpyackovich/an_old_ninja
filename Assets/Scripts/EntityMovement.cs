using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    public float speed = 40;
    public float jumpForce = 400f;

    [Range(0, 1)]
    public float slowFactor = 0.5f;

    [Range(0, 0.5f)]
    public float groundCheckRadius = 0.05f;

    public bool hasAirControl = false;

    public bool isOnGround { get; set; } = false;
    public bool isSlowed { get; set; } = false;
    public bool canJump { get; set; } = false;

    public LayerMask whatIsGround;
    public Transform groundCheck;

    public UnityEvent onLandEvent;
    public UnityEvent onFallEvent;
    public UnityEvent onJumpEvent;

    private float movementSmoothing = 0.05f;
    private float limitFallSpeed = 25f;
    private float addedForce = 10f;

    private Vector3 velocity;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        if (onLandEvent == null)
            onLandEvent = new UnityEvent();

        if (onFallEvent == null)
            onFallEvent = new UnityEvent();

        if (onJumpEvent == null)
            onJumpEvent = new UnityEvent();

        onLandEvent.AddListener(onLandEventListener);
        onFallEvent.AddListener(onFallEventListener);
    }

    private void onFallEventListener()
    {
        canJump = false;
    }

    private void onLandEventListener()
    {
        canJump = true;
    }

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
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

    private void LimitVelocity()
    {
        float entityYVelocity = rb2D.velocity.y;

        if (entityYVelocity < -limitFallSpeed)
            entityYVelocity = -limitFallSpeed;

        rb2D.velocity = new Vector2(rb2D.velocity.x, entityYVelocity);
    }

    private float getActualSpeed()
    {
        return isSlowed ? speed * slowFactor : speed;
    }

    public void Move(float xAxis)
    {
        if (!isOnGround && !hasAirControl)
            return;

        Vector3 targetVelocity = new Vector2(
            getActualSpeed() * addedForce * xAxis * Time.fixedDeltaTime,
            rb2D.velocity.y
        );

        rb2D.velocity = Vector3.SmoothDamp(
            rb2D.velocity,
            targetVelocity,
            ref velocity,
            movementSmoothing
        );
    }

    public void Jump()
    {
        if (isOnGround && canJump)
        {
            rb2D.velocity = Vector2.right * rb2D.velocity;
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            canJump = false;
            onJumpEvent.Invoke();
        }
    }
}
