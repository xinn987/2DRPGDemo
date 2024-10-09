using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("Move Info")]
    public float moveSpeed;
    public float jumpForce;
    public float airMoveSpeedFactor;
    public float wallSlideSpeedFactor;

    [Header("Dash Info")] 
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }
    public float dashCooldown;
    private float dashCooldownTimer;
    
    [Header("Attack Info")]
    public float comboResetCooldown;
    public Vector2[] comboMoveSpeed;
    
    public bool busyLock = false;
    
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
    }

    protected override void Start()
    {
        base.Start();
        
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        
        DashController();
        stateMachine.currentState.Update();
    }

    /**
     * 冲刺是高优先级操作，直接在每帧检测
     */
    public void DashController()
    {
        dashCooldownTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer < 0)
        {
            if (IsWallDetected() && !IsGroundDetected())
            {
                // 滑墙过程中冲刺，强制反向
                dashDir = -facingDir;
            }
            else
            {
                // 其他情况下冲刺，按照输入方向
                dashDir = Input.GetAxisRaw("Horizontal");
                if (dashDir == 0)
                    dashDir = facingDir;
            }
            dashCooldownTimer = dashCooldown;
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
