using UnityEngine;

[System.Serializable]
public class BaseState : MonoBehaviour
{
    public string stateName;

    public StateMachine stateMachine { get; set; }

    public virtual void Init(GameObject gm)
    {
        stateMachine = gm.GetComponent<StateMachine>();
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void StateUpdate() { }
}
