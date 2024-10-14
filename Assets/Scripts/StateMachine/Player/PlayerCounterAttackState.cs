using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.counterDuration;
        player.animator.SetBool("CounterSuccess", false);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocityZero();

        if (stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
        
        // 检测是否存在可被反击的敌人
        Collider2D[] hits = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();  
            if (enemy != null)
            {
                if (enemy.canStuned)
                {
                    enemy.CounterStunned();
                    player.animator.SetBool("CounterSuccess", true);
                    // 设置一个较长的时间，等success动画完成，用帧事件triggerCalled退出状态
                    stateTimer = 10;
                }
            }
        }
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
