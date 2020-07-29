using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState[] states;
    public BaseState initialState;

    public BaseState currentState { get; set; }

    private void Start()
    {
        for (int i = 0; i < states.Length; i++)
        {
            states[i].Init(gameObject);
        }

        currentState = initialState;
        currentState.Enter();
    }

    public void Use(string stateName)
    {
        BaseState newState = null;

        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].stateName == stateName)
            {
                newState = states[i];
                break;
            }
        }

        if (newState == null) return;

        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    private void Update()
    {
        currentState.StateUpdate();
    }
}
