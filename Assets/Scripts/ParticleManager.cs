using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject particle;

    public void Enable()
    {
        particle.SetActive(true);
    }
}
