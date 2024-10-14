using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : 
        base(player, stateMachine, animBoolName) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        float slideSpeedFactor = yInput < 0 ? 1.0f : player.wallSlideSpeedFactor;
        player.SetVelocity(player.rb.velocity.x, player.rb.velocity.y * slideSpeedFactor, false);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }
        
        // if (xInput != 0 && xInput * player.facingDir < 0)
        // {
        //     // 输入方向与爬墙方向相反时，跳下墙面回到idle
        //     stateMachine.ChangeState(player.idleState);
        //     return;
        // }
        
        if (player.IsGroundDetected() || !player.IsWallDetected()) 
        {
            // 滑行到地面或者不贴墙后，回到idle
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
