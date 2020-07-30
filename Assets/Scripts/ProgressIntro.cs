using UnityEngine;

public class ProgressIntro : MonoBehaviour
{
    public Survive survive;
    public GameObject player;
    public Checkpoint[] checkpoints;
    public RandomMobSpawner mobSpawner;

    public void OnPlayerDeath()
    {
        for (int i = checkpoints.Length - 1; i >= 0; i--)
        {
            if (checkpoints[i].active)
            {
                player.transform.position = checkpoints[i].transform.position;
                break;
            }
        }

        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Player>().fullHeal();

        if (mobSpawner.gameObject.activeSelf)
        {
            mobSpawner.Clean();
            mobSpawner.StopSpawn();
        }

        if (survive.enabled)
            survive.Clean();
    }

    public void OnDeathStateExit()
    {
        if (mobSpawner.gameObject.activeSelf)
        {
            mobSpawner.StartSpawn();
        }
    }
}
