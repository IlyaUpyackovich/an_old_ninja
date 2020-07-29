using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private string direction = "right";
    private float horizontalInput = 0f;

    private EntityMovement entityMovement;
    private StateMachine stateMachine;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        entityMovement = GetComponent<EntityMovement>();
        stateMachine = FindObjectOfType<StateMachine>();
    }

    // Update is called once per frame
    private void Update()
    {
        animator.SetFloat("HorizontalInput", Mathf.Abs(horizontalInput));

        if (stateMachine.currentState.stateName != "Play")
        {
            horizontalInput = 0f;
            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
            entityMovement.Jump();

    }

    private void FixedUpdate()
    {
        entityMovement.Move(horizontalInput);

        if (stateMachine.currentState.stateName != "Play") return;

        if (horizontalInput > 0f && direction == "left")
            Flip("right");

        if (horizontalInput < 0f && direction == "right")
            Flip("left");
    }

    private void Flip(string newDirection)
    {
        direction = newDirection;

        Vector3 playerScale = transform.localScale;
        playerScale.x = -playerScale.x;
        transform.localScale = playerScale;
    }

    public void OnFallEvent()
    {
        animator.SetBool("isJumping", true);
    }

    public void OnLandEvent()
    {
        animator.SetBool("isJumping", false);
    }
}
