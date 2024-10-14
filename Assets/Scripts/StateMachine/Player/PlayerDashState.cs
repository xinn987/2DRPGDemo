using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : 
        base(player, stateMachine, animBoolName) {}

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;

        player.dashDir = calcDashDirection();
        player.skillManager.dashSkill.UseSkill();
        
        player.skillManager.cloneSkill.Create(player.transform);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashDir * player.dashSpeed, 0);
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (player.IsWallDetected() && !player.IsGroundDetected())
        {
            player.stateMachine.ChangeState(player.wallSlideState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private float calcDashDirection()
    {
        float dashDir;
        if (player.IsWallDetected() && !player.IsGroundDetected())
        {
            // 滑墙过程中冲刺，强制反向
            dashDir = -player.facingDir;
        }
        else
        {
            // 其他情况下冲刺，按照输入方向
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = player.facingDir;
        }
        return dashDir;
    }
}
