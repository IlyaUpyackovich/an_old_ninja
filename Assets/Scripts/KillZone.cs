using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Die();
        }
        else if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Entity>().Die();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
