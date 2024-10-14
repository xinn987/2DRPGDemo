using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonStunnedState : EnemySkeletonState
{
    public EnemySkeletonStunnedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.stunDuration;
        enemy.SetVelocity(enemy.stunDirection.x * -enemy.facingDir, enemy.stunDirection.y, false);
        
        enemySkeleton.entityFX.InvokeRepeating(nameof(EntityFX.StunBlinkFX), 0, .1f);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemySkeleton.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemySkeleton.entityFX.Invoke(nameof(EntityFX.ResetStunBlinkFX), 0);
    }
}
