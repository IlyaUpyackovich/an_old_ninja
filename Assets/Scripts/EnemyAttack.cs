using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof (EntityMovement)), RequireComponent(typeof(Rigidbody2D))]
public class EnemyAttack : MonoBehaviour
{
    public float searchRadius = 5f;
    public float dashForce = 200;
    public float cooldown = 1f;

    public LayerMask targets;
    public UnityEvent onAttack;
    public AudioSource attack;

    private Rigidbody2D rb2D;
    private EntityMovement entityMovement;

    private bool canAttack = true;

    private void Awake()
    {
        if (onAttack == null)
        {
            onAttack = new UnityEvent();
        }
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        entityMovement = GetComponent<EntityMovement>();
    }

    void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(
            transform.position,
            searchRadius,
            targets
        );

        if (collider == null) return;

        float xAxis = Mathf.Sign(collider.transform.position.x - transform.position.x);

        if (canAttack)
            Attack(collider.transform, xAxis);
    }

    void Attack(Transform target, float xAxis)
    {
        if (entityMovement.isOnGround && entityMovement.canJump)
        {
            float xForce = dashForce * xAxis;
            float yForce = dashForce * Mathf.Sin(target.position.y - rb2D.position.y);
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(new Vector2(xForce, yForce), ForceMode2D.Force);

            StartCoroutine(AttackCooldown());
            onAttack.Invoke();
            attack.Play();
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
}
