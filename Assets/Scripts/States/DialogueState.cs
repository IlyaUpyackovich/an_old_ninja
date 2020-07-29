using UnityEngine;
using Cinemachine;

public class DialogueState : BaseState
{
    public DialogueManager dialogueManager;
    public CinemachineVirtualCamera playCamera;

    public override void StateUpdate()
    {
        if (Input.GetButtonDown("Submit"))
        {
            dialogueManager.DisplayNextSentence();
        }
    }

    public override void Enter()
    {
        playCamera.Priority++;
    }

    public override void Exit()
    {
        playCamera.Priority--;
    }
}
