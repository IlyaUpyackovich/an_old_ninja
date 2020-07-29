using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ActivateComponentCollider : MonoBehaviour
{
    public MonoBehaviour component;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!component.enabled && collision.CompareTag("Player"))
        {
            component.enabled = true;
        }
    }
}
