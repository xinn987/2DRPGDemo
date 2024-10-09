using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    
    #region Animations
    public EnemyStateMachine stateMachine { get; private set; }
    #endregion

    void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.currentState.Update();
    }
}
