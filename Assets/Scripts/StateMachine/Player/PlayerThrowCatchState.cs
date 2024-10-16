using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowCatchState : PlayerState
{
    private Transform throwTransform;
    
    public PlayerThrowCatchState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        throwTransform = player.skillManager.throwSkill.throwInstance.transform;
        // 玩家面向瞄准方向
        if ((throwTransform.position.x - player.transform.position.x) * player.facingDir < 0)
        {
            player.Flip();
        }
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
