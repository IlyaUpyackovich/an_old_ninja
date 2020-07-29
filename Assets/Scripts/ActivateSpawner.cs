using UnityEngine;

public class ActivateSpawner : MonoBehaviour
{
    public GameObject spawner;

    public void AcitvateSpanwer()
    {
        spawner.SetActive(true);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AcitvateSpanwer();
        }
    }
}
