using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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

    [Header("Collision Info")]
    public Transform groundCheck;
    public float groundCheckDistance;
    public Transform wallCheck;
    public float wallCheckDistance;
    public LayerMask groundLayerMask;
    
    [Header("Attack Info")]
    public float comboResetCooldown;
    public Vector2[] comboMoveSpeed;
    
    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;
    
    public bool busyLock = false;
    
    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    
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
    
    void Awake()
    {
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

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
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
    
    public void FlipController(float x)
    {
        if (x > 0 && !facingRight || x < 0 && facingRight)
            Flip();
    }

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    
    /**
     * 设置速度
     * 可以设置是否进行翻转判断，用于避免物理碰撞导致的少量速度回弹
     */
    public void SetVelocity(float xVelocity, float yVelocity, bool checkFlip = true)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        if (checkFlip)
            FlipController(xVelocity);
    }

    /**
     * 设置速度为0
     */
    public void SetVelocityZero() => rb.velocity = new Vector2(0, 0);
    
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayerMask);

    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, facingDir * Vector2.right, wallCheckDistance, groundLayerMask);
    
    public IEnumerator BusyLockFor(float seconds)
    {
        busyLock = true;
        yield return new WaitForSeconds(seconds);
        busyLock = false;
    }

    public void AnimatorTrigger() => stateMachine.currentState.AnimationTrigger();
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
    }
}
