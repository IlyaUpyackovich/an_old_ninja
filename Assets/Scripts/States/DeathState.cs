using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class DeathState : BaseState
{
    public CinemachineVirtualCamera playCamera;
    public GameObject tryAgainMenu;
    public AudioClip deathChore;
    public UnityEvent onExit;

    private MusicManager musicManager;
    private AudioSource audioSource;

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = deathChore;

        if (onExit == null)
        {
            onExit = new UnityEvent();
        }
    }

    public override void Enter()
    {
        playCamera.Priority++;
        tryAgainMenu.SetActive(true);
        audioSource.Play();
        musicManager.Stop();
    }

    public override void Exit()
    {
        playCamera.Priority--;
        tryAgainMenu.SetActive(false);
        audioSource.Stop();
        musicManager.Play();
        onExit.Invoke();
    }

    public override void StateUpdate()
    {
        if (Input.GetButtonDown("Submit"))
        {
            stateMachine.Use("Play");
        }
    }
}
