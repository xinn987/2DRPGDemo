using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastAttackTime;
    
    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : 
        base(player, stateMachine, animBoolName) {}

    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || lastAttackTime + player.comboResetCooldown <= Time.time)
        {
            comboCounter = 0;
        }
        player.animator.SetInteger("ComboCounter", comboCounter);

        // 每次进入攻击时判断当前输入方向
        float attackDir = player.facingDir;
        if (xInput != 0)
        {
            attackDir = xInput;
        }
        
        player.SetVelocity(player.comboMoveSpeed[comboCounter].x * attackDir, player.comboMoveSpeed[comboCounter].y);
        stateTimer = 0.1f;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            player.SetVelocityZero();
        }
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastAttackTime = Time.time;

        // player.BusyLockFor(0.15f);
    }
}
