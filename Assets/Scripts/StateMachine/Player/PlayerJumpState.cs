using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) :
        base(player, stateMachine, animBoolName) {}

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(player.rb.velocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0)
        {
            player.SetVelocity(xInput * player.moveSpeed * player.airMoveSpeedFactor, player.rb.velocity.y);
        }
        if (player.rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}