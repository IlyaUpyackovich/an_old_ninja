using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(EntityMovement))]
public class EntityAI : MonoBehaviour
{
    public Transform target;

    public string direction = "left";
    public float nextWaypointDistance = 5f;

    private int currentWaypoint = 0;
    private bool reachedEndOfPath;

    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb2D;
    private StateMachine stateMachine;
    private EntityMovement entityMovement;

    // Start is called before the first frame update
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        entityMovement = GetComponent<EntityMovement>();
        stateMachine = FindObjectOfType<StateMachine>();

        seeker = GetComponent<Seeker>();
        seeker.pathCallback += OnPathComplete;
        InvokeRepeating("UpdatePath", 0f, 0.1f);
    }

    private void OnDisable()
    {
        seeker.pathCallback -= OnPathComplete;
    }

    private void UpdatePath()
    {
        if (target && seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (p.error)
        {
            return;
        }

        path = p;
        currentWaypoint = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (path == null)
            return;

        if (stateMachine.currentState.stateName != "Play")
        {
            entityMovement.Move(0);
            return;
        }

        bool isTarget = false;
        float distanceToWaypoint =  UpdateWaypoint();
        float distanceToTarget = Vector3.Distance(rb2D.position, target.position);

        Vector2 offset;
        if (!reachedEndOfPath && distanceToWaypoint < distanceToTarget)
            offset = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        else
        {
            isTarget = true;
            offset = (target.position - transform.position).normalized;
        }

        float xAxis = Mathf.Sign(offset.x);

        entityMovement.Move(xAxis);

        if (!isTarget && offset.y > 0.4f) entityMovement.Jump();

        if (xAxis > 0f && direction == "left")
            Flip("right");

        if (xAxis < 0f && direction == "right")
            Flip("left");
    }

    private float UpdateWaypoint()
    {
        reachedEndOfPath = false;
        float distanceToWaypoint;

        while (true)
        {
            distanceToWaypoint = Vector3.Distance(rb2D.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                if (currentWaypoint < path.vectorPath.Count - 1)
                {
                    currentWaypoint++;
                }
                else
                {
                    reachedEndOfPath = true;
                    path = null;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        return distanceToWaypoint;
    }

    private void Flip(string newDirection)
    {
        direction = newDirection;

        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Slower")
        {
            entityMovement.isSlowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Slower")
        {
            entityMovement.isSlowed = false;
        }
    }
}
