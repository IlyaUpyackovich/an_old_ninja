using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer))]
public class Player : Entity
{
    public HealthBar healthBar;
    public AudioSource landing;

    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        currentHealth = health;
        healthBar.SetMaxHealth(health);

        onDamage.AddListener(OnDamageTaken);
    }

    private void OnDamageTaken()
    {
        healthBar.SetHealth(currentHealth);
        StartCoroutine(Flash());
    }

    public override void Die()
    {
        FindObjectOfType<StateMachine>().Use("Death");
        onDeath.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Damage(1.5f);
        }
    }

    private IEnumerator Flash()
    {
        int max = 24;
        Color color = sprite.color;

        for (int i = 0; i < max; i++)
        {
            color.a = i % 2 == 0 ? 0.5f : 1;
            sprite.color = color;
            yield return new WaitForSeconds(invunerableTime / max);
        }

        color.a = 1;
        sprite.color = color;
    }

    public void OnLandingListener()
    {
        if (rb2D.velocity.y < -15f)
        {
            landing.Play();
        }
    }

    public void fullHeal()
    {
        currentHealth = health;
        healthBar.SetHealth(currentHealth);
    }
}
