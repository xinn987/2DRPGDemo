using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonAttackState : EnemySkeletonState
{
    public EnemySkeletonAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        enemySkeleton.SetVelocityZero();
        
        if (triggerCalled)
        {
            stateMachine.ChangeState(enemySkeleton.battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemySkeleton.lastAttackTime = Time.time;
    }
}
