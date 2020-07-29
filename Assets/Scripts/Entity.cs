using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public float health = 10;
    public float invunerableTime = 1.5f;

    public UnityEvent onDamage;
    public UnityEvent onDeath;

    public float currentHealth { get; set; }

    private bool isInvulnerable = false;

    private void Awake()
    {
        if (onDamage == null)
            onDamage = new UnityEvent();

        if (onDeath == null)
            onDeath = new UnityEvent();
    }

    private void Start() => currentHealth = health;

    public void Damage(float damage)
    {
        if (isInvulnerable)
            return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, health);
        onDamage.Invoke();

        if (currentHealth <= 0)
            Die();
        else
            StartCoroutine(Invulnerable(invunerableTime));
    }

    public virtual void Die()
    {
        onDeath.Invoke();
        Destroy(gameObject);
    }

    public IEnumerator Invulnerable(float time)
    {
        if (isInvulnerable)
            yield break;

        isInvulnerable = true;
        yield return new WaitForSeconds(time);
        isInvulnerable = false;
    }
}
