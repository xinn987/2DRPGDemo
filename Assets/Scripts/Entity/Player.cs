using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [HideInInspector] public SkillManager skillManager;
    public bool busyLock = false;

    [Header("Move Info")]
    public float moveSpeed;
    public float jumpForce;
    public float airMoveSpeedFactor;
    public float wallSlideSpeedFactor;

    [Header("Dash Info")] 
    public float dashSpeed;
    public float dashDuration;
    [HideInInspector] public float dashDir;
    
    [Header("Attack Info")]
    public float comboResetCooldown;
    public Vector2[] comboMoveSpeed;
    public float counterDuration;
    
    #region Animations
    public PlayerStateMachine stateMachine { get; private set; }
    
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState attackState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerThrowAimState throwAimState { get; private set; }
    public PlayerThrowCatchState throwCatchState { get; private set; }
    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        attackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        throwAimState = new PlayerThrowAimState(this, stateMachine, "ThrowAim");
        throwCatchState = new PlayerThrowCatchState(this, stateMachine, "ThrowCatch");
    }

    protected override void Start()
    {
        base.Start();
        
        skillManager = SkillManager.instance;
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        
        CheckDash();
        stateMachine.currentState.Update();
    }

    /**
     * 冲刺是高优先级操作，直接在每帧检测
     */
    public void CheckDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && skillManager.dashSkill.CanUseSkill())
        {
            stateMachine.ChangeState(dashState);
        }
    }
    
    public void AnimatorTrigger() => stateMachine.currentState.AnimationTrigger();
    
    public IEnumerator BusyLockFor(float seconds)
    {
        busyLock = true;
        yield return new WaitForSeconds(seconds);
        busyLock = false;
    }

}
