using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    
    [Header("Battle Info")] 
    public float battleTime;
    public float battleDetectRadius;
    public Transform playerCheck;
    public float playerCheckDistance;
    public LayerMask playerLayerMask;
    
    [Header("Attack Info")] 
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastAttackTime;
    
    [Header("Stunned Info")]
    public float stunDuration;
    public Vector2 stunDirection;
    [HideInInspector] public bool canStuned;
    public GameObject counterIndicator;
    
    #region Animations
    public EnemyStateMachine stateMachine { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public RaycastHit2D IsPlayerDetected() =>
        Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, playerLayerMask);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + playerCheckDistance * facingDir, playerCheck.position.y));
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
    
    public virtual void AnimatorTrigger() => stateMachine.currentState.AnimationTrigger();

    public virtual void OpenCounterStunWindow()
    {
        canStuned = true;
        counterIndicator.SetActive(true);
    }
    
    public virtual void CloseCounterStunWindow()
    {
        canStuned = false;
        counterIndicator.SetActive(false);
    }

    public virtual void CounterStunned()
    {
        CloseCounterStunWindow();
    }
    
}
