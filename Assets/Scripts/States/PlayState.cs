using Cinemachine;
using UnityEngine;

public class PlayState : BaseState
{
    public GameObject healthBar;
    public CinemachineVirtualCamera playCamera;

    public bool playerMove { get; set; } = false;

    public override void Enter()
    {
        playCamera.Priority++;
        healthBar.SetActive(true);
        playerMove = true;
    }

    public override void Exit()
    {
        playCamera.Priority--;
        healthBar.SetActive(false);
        playerMove = false;
    }
}
