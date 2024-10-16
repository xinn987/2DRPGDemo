using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : 
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
        
        // 右键反击
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            stateMachine.ChangeState(player.counterAttackState);
            return;
        }
        // 左键攻击
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.attackState);
            return;
        }
        // 中键瞄准
        if (Input.GetKeyDown(KeyCode.Mouse2) && player.skillManager.throwSkill.CanUseSkill())
        {
            stateMachine.ChangeState(player.throwAimState);
            return;
        }
        // 地面起跳
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }
        // 空中姿态
        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
