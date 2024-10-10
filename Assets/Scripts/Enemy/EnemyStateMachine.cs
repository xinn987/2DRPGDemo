using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }

    public void Initialize(EnemyState initialState)
    {
        currentState = initialState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState newState)
    {
        Debug.Log($"Enemy ChangeState: {currentState.animBoolName} -> {newState.animBoolName}");
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
