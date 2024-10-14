using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonBattleState : EnemySkeletonState
{
    public EnemySkeletonBattleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        stateTimer = enemySkeleton.battleTime;
    }

    public override void Update()
    {
        base.Update();
        float battleDir = Mathf.Sign(player.position.x - enemySkeleton.transform.position.x);
        enemySkeleton.SetVelocity(battleDir * enemySkeleton.moveSpeed, enemySkeleton.rb.velocity.y);
        
        if (enemySkeleton.IsPlayerDetected())
        {
            // 看到玩家则重置battle状态计时
            stateTimer = enemySkeleton.battleTime;
            
            // 攻击处于冷却且处于攻击距离内
            if (CanAttack() && enemySkeleton.IsPlayerDetected().distance <= enemySkeleton.attackDistance)
                stateMachine.ChangeState(enemySkeleton.attackState);
        }
        else
        {
            // 丢失玩家视野后，退出battle状态
            if (stateTimer < 0)
            {
                stateMachine.ChangeState(enemySkeleton.idleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        return enemySkeleton.lastAttackTime + enemySkeleton.attackCooldown <= Time.time;
    }
}
