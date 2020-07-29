using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] SurfaceCalmMusic;
    public AudioSource[] BattleMusic;

    private AudioSource[] currentState;

    private void Start()
    {
        currentState = SurfaceCalmMusic;
        Play();
    }

    public void Battle()
    {
        Stop();
        currentState = BattleMusic;
        Play();
    }

    public void SurfaceCalm()
    {
        Stop();
        currentState = SurfaceCalmMusic;
        Play();
    }

    public void Pause()
    {
        for (int i = 0; i < currentState.Length; i++)
            currentState[i].Pause();
    }

    public void Play()
    {
        for (int i = 0; i < currentState.Length; i++)
            currentState[i].Play();
    }

    public void Stop()
    {
        for (int i = 0; i < currentState.Length; i++)
            currentState[i].Stop();
    }
}

