using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }

    public void Initialize(PlayerState initialState)
    {
        currentState = initialState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        Debug.Log($"ChangeState: {currentState.GetType().Name} -> {newState.GetType().Name}");
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
