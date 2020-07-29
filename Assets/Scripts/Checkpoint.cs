using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool active = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            active = true;
        }
    }
}
