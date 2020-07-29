using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RandomMobSpawner : MonoBehaviour
{
    public string spawnerName;
    public UnityEvent onActive;
    public Transform target;
    public GameObject[] mobs;
    public GameObject spawnEffect;
    public AudioClip spawnClip;

    public float interval = 5f;
    public float firstSpawnIn = 0f;

    private IEnumerator coroutine;
    private GameObject mobsObject;
    private AudioSource audioSource;
    private StateMachine stateMachine;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stateMachine = FindObjectOfType<StateMachine>();
        mobsObject = transform.Find("Mobs").gameObject;

        coroutine = Spawn();
        StartCoroutine(coroutine);

        onActive.Invoke();
    }

    public void Clean() {
        if (!gameObject.activeSelf) return;

        for (int i = 0; i < mobsObject.transform.childCount; i++)
        {
            Destroy(mobsObject.transform.GetChild(i).gameObject);
        }
    }

    public void StartSpawn()
    {
        StopCoroutine(coroutine);
        coroutine = Spawn();
        StartCoroutine(coroutine);
    }

    private GameObject InstantiateMob()
    {
        int index = Random.Range(0, mobs.Length - 1);
        GameObject mob = Instantiate(mobs[index], mobsObject.transform);
        mob.GetComponent<EntityAI>().target = target;
        mob.transform.position = transform.position;
        return mob;
    }

    private void InsantiateSpawnEffect()
    {
        GameObject particle = Instantiate(spawnEffect);
        particle.transform.position = transform.position;
        particle.SetActive(true);
        particle.GetComponent<Rigidbody>().AddForce(Vector2.up * 10, ForceMode.Impulse);

        StartCoroutine(DestroyEffect(particle));
    }

    private IEnumerator DestroyEffect(GameObject effect)
    {
        yield return new WaitForSeconds(8);
        Destroy(effect);
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(firstSpawnIn);

        while (true)
        {
            yield return new WaitUntil(() => stateMachine.currentState.stateName == "Play");

            InstantiateMob();
            InsantiateSpawnEffect();
            audioSource.PlayOneShot(spawnClip, 0.35f);
            yield return new WaitForSeconds(interval);
        }
    }
}
