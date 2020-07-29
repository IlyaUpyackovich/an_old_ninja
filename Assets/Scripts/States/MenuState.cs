using UnityEngine;
using Cinemachine;

public class MenuState : BaseState
{
    public Dialogue introDialogue;

    public GameObject menu;
    public DialogueManager dialogueManager;
    public CinemachineVirtualCamera menuCamera;

    public override void StateUpdate()
    {
        if (Input.GetButtonDown("Submit"))
        {
            dialogueManager.StartDialogue(introDialogue);
            stateMachine.Use("Dialogue");
        }
    }

    public override void Enter()
    {
        menuCamera.Priority++;
        menu.SetActive(true);
    }

    public override void Exit()
    {
        menuCamera.Priority--;
        menu.SetActive(false);
    }
}
