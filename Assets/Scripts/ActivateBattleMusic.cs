using UnityEngine;

public class ActivateBattleMusic : MonoBehaviour
{
    private bool wasTouched = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !wasTouched)
        {
            FindObjectOfType<MusicManager>().Battle();
            Destroy(gameObject);
        }
    }
}
