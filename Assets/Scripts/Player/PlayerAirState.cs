using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : 
        base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
        if (xInput != 0)
        {
            player.SetVelocity(xInput * player.moveSpeed * player.airMoveSpeedFactor, player.rb.velocity.y);
        }
        
        if (player.IsWallDetected() && !player.IsGroundDetected())
        {
            player.stateMachine.ChangeState(player.wallSlideState);
        }
        if (player.IsGroundDetected())
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
